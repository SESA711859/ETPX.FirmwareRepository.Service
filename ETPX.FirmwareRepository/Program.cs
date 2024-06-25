// Copyright Schneider-Electric 2024

using ETPX.FirmwareRepository.BSL;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.Extensions;
using ETPX.FirmwareRepository.Helpers;
using ETPX.FirmwareRepository.Consumer;
using ETPX.FirmwareRepository.Middlewares.ExceptionHandler;
using ETPX.FirmwareRepository.Middlewares.Xss;
using ETPX.FirmwareRepository.ServiceProvider;
using ETPX.FirmwareRepository.Settings;
using ETPX.FirmwareRepository.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Logging 
builder.Logging.ClearProviders();

#region OTLP Logging
builder.AddOLTPConfigurations();
#endregion

var scheme = builder.Configuration["APP_CFG_SWAGGER_SCHEME"];
var host = builder.Configuration["APP_CFG_SWAGGER_HOST"];
var docsPrefix = builder.Configuration["APP_CFG_DOCS_PREFIX"];

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add services to the container.

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResponse), (int)HttpStatusCode.BadRequest));
        options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResponse), (int)HttpStatusCode.NotFound));
    })
    .ConfigureApiBehaviorOptions(x => { x.SuppressMapClientErrors = true; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var settings = builder.Configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();

// BSL Settings
builder.Services.AddOptions<BslSettings>().BindConfiguration("BslSettings");
builder.Services.AddSingleton<IBslSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<BslSettings>>().Value);

builder.Services.AddSingleton(typeof(KafkaConsumerService));


// Kafka Settings
builder.Services.AddOptions<KafkaSettings>().BindConfiguration("KafkaSettings");
builder.Services.AddSingleton<IKafkaSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<KafkaSettings>>().Value);



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = settings?.Title,
        Version = settings?.Version,
        Description = settings?.Description,
        Contact = new OpenApiContact { Name = "ETPX Commissioning Service Team", Email= "etpxcommissioningservice@se.com" }
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, true);
    c.SchemaFilter<EnumSchemaFilter>();
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton(typeof(UtilityHelper)); 

builder.Services.AddSingleton(serviceProvider => new BslClient(serviceProvider.GetRequiredService<IOptions<BslSettings>>().Value));
builder.Services.AddSingleton<IFirmwareVersionProvider>(serviceProvider => new FirmwareVersionProvider(serviceProvider.GetRequiredService<BslClient>(), serviceProvider.GetRequiredService<UtilityHelper>(), serviceProvider.GetRequiredService<IBslSettings>()));

builder.Services.AddCors();
builder.Services.AddHealthChecks();
// Validators
builder.Services.AddScoped<IValidator<string>, CommericalReferenceValidator>();

// Add FV validators
builder.Services.AddValidatorsFromAssemblyContaining<CommericalReferenceValidator>();

var app = builder.Build();

var consumerMessage = app.Services.GetRequiredService<KafkaConsumerService>();
Task.Run(() => consumerMessage.StartConsuming());
Thread.Sleep(5000);

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"{settings?.RoutePrefixWithSlash}swagger/v1/swagger.json", $"{settings?.Title} {settings?.Version}");
        c.RoutePrefix = "docs/swagger";
    });
}
else
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "docs/swagger/{documentName}/swagger.json";
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        {
            swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = !string.IsNullOrEmpty(scheme) && !string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(docsPrefix) ? $"{scheme}://{host}{docsPrefix}" : $"https://{httpReq.Host.Value}" } };
        });
    });
    app.UseSwaggerUI(c => c.RoutePrefix = "docs/swagger");

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.MapHealthChecks("/health");
app.UseExceptionHandlerMiddleware();
app.UseMiddleware<XssAttackHandlerMiddleware>();
app.Run();
