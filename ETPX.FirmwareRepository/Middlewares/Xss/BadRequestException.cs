// Copyright Schneider-Electric 2024

﻿using System.Runtime.Serialization;

namespace ETPX.FirmwareRepository.Middlewares.Xss
{
    /// <summary>
    /// Bad Request Exception
    /// </summary>
    [Serializable]
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public BadRequestException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BadRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BadRequestException(SerializationInfo info, StreamingContext context): base(info, context)
        {
        }
    }
}
