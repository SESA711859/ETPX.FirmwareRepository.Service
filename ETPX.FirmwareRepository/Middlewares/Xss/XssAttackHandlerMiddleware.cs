// Copyright Schneider-Electric 2024

﻿using Ganss.Xss;
using System.Text;
using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ETPX.FirmwareRepository.Middlewares.Xss
{
    /// <summary>
    /// Middleware to prevent XSS attack
    /// </summary>
    public class XssAttackHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<XssAttackHandlerMiddleware> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public XssAttackHandlerMiddleware(RequestDelegate next, ILogger<XssAttackHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;   
        }

        /// <summary>
        /// Hanlder method to prevent XSS attack
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public async Task Invoke(HttpContext httpContext)
        {
            // enable buffering so that the request can be read by the model binders next
            httpContext.Request.EnableBuffering();
            var exception = new BadRequestException("XSS injection detected from middleware.");
            // leaveOpen: true to leave the stream open after disposing,
            // so it can be read by the model binders
            using (var streamReader = new StreamReader
                  (httpContext.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var raw = await streamReader.ReadToEndAsync();
                var sanitiser = new HtmlSanitizer();
                var sanitised = sanitiser.Sanitize(raw);

                if (raw != sanitised)
                {
                    _logger.LogError(exception, "StatusCode : {StatusCode}, Message : {Message}", HttpStatusCode.BadRequest, exception.Message);
                    throw exception;
                }
            }
            var queryCollection = httpContext.Request.Query;
            foreach (var query in queryCollection)
            {
                var raw = query.Value.ToString();
                var sanitiser = new HtmlSanitizer();
                var sanitised = sanitiser.Sanitize(raw);

                if (raw != sanitised)
                {
                    _logger.LogError(exception, "StatusCode : {StatusCode}, Message : {Message}", HttpStatusCode.BadRequest, exception.Message);
                    throw exception;
                }
            }


            // rewind the stream for the next middleware
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next.Invoke(httpContext);
        }
    }
}
