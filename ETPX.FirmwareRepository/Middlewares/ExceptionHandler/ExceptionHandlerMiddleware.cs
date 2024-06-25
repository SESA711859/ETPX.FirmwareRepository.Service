// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.Middlewares.Xss;
using System.Net;

namespace ETPX.FirmwareRepository.Middlewares.ExceptionHandler
{
    /// <summary>
    /// The Exception Handle Middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// The Exception Handle Middleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// The Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleSystemExceptionMessageAsync(context, ex, _logger).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Method to handle system exceptions
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        private static Task HandleSystemExceptionMessageAsync(HttpContext context, Exception exception, ILogger<ExceptionHandlerMiddleware> logger)
        {
            context.Response.ContentType = "application/json";
           
            var errorResponse = exception switch
            {
                KeyNotFoundException => new Error() { StatusCode = (int)HttpStatusCode.NotFound, ErrorMessages =new List<string>(){ "Data not found." } },
                ArgumentNullException => new Error() { StatusCode = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { "one or more paramater(s) are invalid." } },
                BadRequestException => new Error() { StatusCode = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string>() { "one or more paramater(s) are invalid." } },
                FormatException => new Error() { StatusCode = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string>() { "one or more paramater(s) are invalid." } },
                _ => new Error() { StatusCode = (int)HttpStatusCode.InternalServerError, ErrorMessages = new List<string>() { "Internal server error occurred. Please contact administrator." } }
            };

            context.Response.StatusCode =errorResponse.StatusCode;
            logger.LogError(exception, "StatusCode : {StatusCode}, Message : {Message}", errorResponse.StatusCode, exception.Message);

            
            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}