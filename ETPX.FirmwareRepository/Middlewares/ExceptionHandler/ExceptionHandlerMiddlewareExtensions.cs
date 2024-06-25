// Copyright Schneider-Electric 2024

﻿namespace ETPX.FirmwareRepository.Middlewares.ExceptionHandler
{
    /// <summary>
    /// Exception HandlerMiddlewareExtensions
    /// </summary>
    public static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Extension Method to use ExceptionHandlerMiddleware
        /// </summary>
        /// <param name="app"></param>
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}