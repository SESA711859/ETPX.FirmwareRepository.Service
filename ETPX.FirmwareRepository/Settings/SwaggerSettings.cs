// Copyright Schneider-Electric 2024

﻿namespace ETPX.FirmwareRepository.Settings
{
    /// <summary>
    /// The Swagger Settings
    /// </summary>
    public class SwaggerSettings
    {
        /// <summary>
        /// Gets or sets the Version of the service instance
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the Title of the service instance
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Description of the service instance
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the RoutePrefixWithSlash of the service instance
        /// </summary>
        public string RoutePrefixWithSlash { get; set; }

        /// <summary>
        /// SwaggerSettings Default Constructor
        /// </summary>
        public SwaggerSettings()
        {
            Version = string.Empty;
            Title = string.Empty;
            Description = string.Empty;
            RoutePrefixWithSlash = string.Empty;
        }
    }
}