// Copyright Schneider-Electric 2024

﻿namespace ETPX.FirmwareRepository.Settings
{
    /// <summary>
    /// Settings to Connect BSL API gateway
    /// </summary>
    public class BslSettings : IBslSettings
    {
        /// <summary>
        /// Gets or sets API token URL
        /// </summary>
        public string TokenUrl { get; set; }

        /// <summary>
        /// Gets or sets Base URL to PIM document part 1
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets Grant type 
        /// </summary>
        public string GrantType { get; set; }

        /// <summary>
        /// Gets or sets Client ID to BSL
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets Client secret to BSL
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Firmware file download URL
        /// </summary>
        public string FileDownloadBaseUrl { get; set; }

        /// <summary>
        /// BslSettings Default Constructor
        /// </summary>
        public BslSettings()
        {
            TokenUrl = string.Empty;
            BaseUrl = string.Empty;
            GrantType = string.Empty;
            ClientId = string.Empty;
            ClientSecret = string.Empty;
            FileDownloadBaseUrl = string.Empty;
        }
    }
}
