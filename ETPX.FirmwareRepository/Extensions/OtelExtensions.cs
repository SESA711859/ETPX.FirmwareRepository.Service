// Copyright Schneider-Electric 2024

using ETPX.FirmwareRepository.Settings;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace ETPX.FirmwareRepository.Extensions
{
    /// <summary>
    /// Class to define Otel Configuration
    /// </summary>
    public static class OtelExtensions
    {
        /// <summary>
        /// Method to AddOLTPConfigurations
        /// </summary>
        /// <param name="builder"></param>
        public static void AddOLTPConfigurations(this WebApplicationBuilder builder)
        {
            var otelSettings = builder.Configuration.GetSection(nameof(OtelSettings)).Get<OtelSettings>();

            Action<ResourceBuilder> configureResource = r =>
            r.AddService(
                serviceName: builder.Configuration.GetValue("ServiceName", defaultValue: typeof(Program).Assembly.GetName().Name)!,
                serviceVersion: builder.Configuration["APP_CFG_API_VERSION"]?.ToString() ?? "null",
                serviceInstanceId: Environment.MachineName);

            builder.Logging.AddOpenTelemetry(options =>
            {
                var resourceBuilder = ResourceBuilder.CreateDefault().AddAttributes(new[]
    {
        new KeyValuePair<string, object>("siem", false),
        new KeyValuePair<string, object>("appEnvironment",builder.Configuration["APP_ENVIRONMENT_NAME"]?.ToString() ?? "null"),
        new KeyValuePair<string, object>(Constants.ComponentName,Constants.ComponentValue?.ToString() ?? "null")
    });
                configureResource(resourceBuilder);
                options.SetResourceBuilder(resourceBuilder);
                options.IncludeFormattedMessage = true;
                options.AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = new Uri(otelSettings!.Endpoint);
                    otlpOptions.Protocol = OtlpExportProtocol.HttpProtobuf;
                });
            });
        }
    }
}
