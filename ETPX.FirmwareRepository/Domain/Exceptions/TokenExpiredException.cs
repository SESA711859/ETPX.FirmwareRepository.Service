// Copyright Schneider-Electric 2024

﻿using System.Runtime.Serialization;

namespace ETPX.FirmwareRepository.Domain.Exceptions
{

    /// <summary>
    /// API token expired Exception
    /// </summary>
    [Serializable]
    public class TokenExpiredException : Exception
    {
        /// <summary>
        /// TokenExpiredException Constructor
        /// </summary>
        /// <param name="message"></param>
        public TokenExpiredException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TokenExpiredException(SerializationInfo info, StreamingContext context) :base(info, context)
        {
        }
    }
}
