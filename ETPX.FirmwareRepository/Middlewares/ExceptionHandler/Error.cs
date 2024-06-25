// Copyright Schneider-Electric 2024

﻿using System.Net;

namespace ETPX.FirmwareRepository.Middlewares.ExceptionHandler
{
    /// <summary>
    /// Error Definition
    /// </summary>
    public class Error
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
        /// Gets or Sets Message
        /// </summary>
        public List<string> ErrorMessages { get; set; }

        /// <summary>
        /// ErrorResponse Default
        /// </summary>
        public Error()
        {
            Success = false;
            StatusCode = (int)HttpStatusCode.InternalServerError;
            ErrorMessages = new List<string>() { "Internal Server error." };
        }
    }
}
