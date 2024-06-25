// Copyright Schneider-Electric 2024

﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ETPX.FirmwareRepository.Extensions
{
    /// <summary>
    /// EnumSchemaFilter to hide Unknown value from Swagger UI
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applying rule to hide default value of Enum in Swagger
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public virtual void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum){
                schema.Enum.RemoveAt(0);
            }
        }
    }
}