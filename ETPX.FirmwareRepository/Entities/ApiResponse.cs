// Copyright Schneider-Electric 2024

﻿using FluentValidation.Results;
using System.Net;
using System.Text.Json.Serialization;

namespace ETPX.FirmwareRepository.Entities
{
    /// <summary>
    /// ApiResponse Definition
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or Sets Success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or Sets StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets ErrorMessages
        /// </summary>
        public IEnumerable<string> ErrorMessages { get; set; }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Result { get; set; }

        /// <summary>
        /// ApiResponse Constructor With Message
        /// </summary>
        public ApiResponse(string message, HttpStatusCode httpStatusCode = HttpStatusCode.NotFound){
            Success = false;
            StatusCode = (int)httpStatusCode;
            ErrorMessages = new List<string>() { message };
            Result = null!;
        }

        /// <summary>
        /// ApiResponse Constructor With Valid Response
        /// </summary>
        public ApiResponse(object result)
        {
            Success = true;
            ErrorMessages = Enumerable.Empty<string>();
            Result = result;
        }
    }
}