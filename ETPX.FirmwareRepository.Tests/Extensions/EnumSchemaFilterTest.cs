// Copyright Schneider-Electric 2024

using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.Extensions;
using Microsoft.OpenApi.Any;
using Moq;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ETPX.FirmwareRepository.Tests.Extensions
{
    public class EnumSchemaFilterTest
    {
        private readonly EnumSchemaFilter schemaFilter;
        public EnumSchemaFilterTest()
        {
            schemaFilter = new EnumSchemaFilter();

        }
        [Fact]
        public void EnumSchemaFilter_Call_Throw_NoException()
        {
            Mock<EnumSchemaFilter> filter = new Mock<EnumSchemaFilter>();
            Mock<Microsoft.OpenApi.Models.OpenApiSchema> openApiSchemaMock = new Mock<Microsoft.OpenApi.Models.OpenApiSchema>();
            Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext schemaFilterContext = new SchemaFilterContext(typeof(EntityType), null, null);

            //Arrange
            filter
                .Setup(s => s.Apply(openApiSchemaMock.Object, schemaFilterContext));


            Microsoft.OpenApi.Models.OpenApiSchema openApiSchema = new Microsoft.OpenApi.Models.OpenApiSchema();
            openApiSchema.Enum = new List<IOpenApiAny>();
            string json = "\"scope\": {\r\n            \"brand\": \"Schneider Electric\",\r\n            \"country\": \"WW\"\r\n        }";
            openApiSchema.Enum.Add(OpenApiAnyFactory.CreateFromJson(json));
            schemaFilter.Apply(openApiSchema, schemaFilterContext);
            //Assert
            Assert.True(true);
        }
    }
}
