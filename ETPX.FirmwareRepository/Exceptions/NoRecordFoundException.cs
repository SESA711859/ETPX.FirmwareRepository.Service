// Copyright Schneider-Electric 2024

﻿using System.Runtime.Serialization;

namespace ETPX.FirmwareRepository.Exceptions
{
    /// <summary>
    /// No record Found Exception
    /// </summary>
    [Serializable]
    public class NoRecordFoundException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public NoRecordFoundException(string? message) : base(message)
        {
        }
     
        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected NoRecordFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
