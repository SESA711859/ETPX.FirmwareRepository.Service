// Copyright Schneider-Electric 2024

﻿namespace ETPX.FirmwareRepository.Settings
{
    /// <summary>
    /// Inteface to BSL settings.
    /// </summary>
    public interface IBslSettings
    {
        /// <summary>
        /// API token URL
        /// </summary>
        string TokenUrl { get; set; }

        /// <summary>
        /// Base URL to PIM document part 1
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Grant type 
        /// </summary>
        string GrantType { get; set; }

        /// <summary>
        /// Client ID to BSL
        /// </summary>
        string ClientId { get; set; }

        /// <summary>
        /// Client secret to BSL
        /// </summary>
        string ClientSecret { get; set; }

        /// <summary>
        /// Firmware file download URL
        /// </summary>
        string FileDownloadBaseUrl { get; set; }
    }
}
