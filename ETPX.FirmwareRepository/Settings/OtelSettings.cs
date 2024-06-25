// Copyright Schneider-Electric 2024

﻿namespace ETPX.FirmwareRepository.Settings
{
    /// <summary>
    /// OtelSettings Definition
    /// </summary>
    public class OtelSettings
    {
        /// <summary>
        /// Gets or Sets Ip
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Initialize OtelSettings with endpoint
        /// </summary>
        /// <param name="endpoint"></param>
        public OtelSettings(string endpoint)
        {
            Endpoint = endpoint;
        }
    }
}
