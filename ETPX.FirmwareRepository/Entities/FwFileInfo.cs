// Copyright Schneider-Electric 2024

﻿using System.ComponentModel.DataAnnotations;

namespace ETPX.FirmwareRepository.Entities
{
    /// <summary>
    /// Firmware list reponse
    /// </summary>
    public class GetFirmwareListResponse
    {
        /// <summary>
        /// The firmware detail list.
        /// </summary>
        public List<FirmwareDetail> FirmwareList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFirmwareListResponse"/> class.
        /// </summary>
        public GetFirmwareListResponse()
        {
            this.FirmwareList = new List<FirmwareDetail>();
        }
    }

    /// <summary>
    /// Firmware file details
    /// </summary>
    public class FirmwareDetail
    {
        /// <summary>
        /// Download URL of firmware file
        /// </summary>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// SHA256 Hash of firmware file to check file integrity
        /// </summary>
        public string CheckSumSHA256 { get; set; }

        /// <summary>
        /// Firmware version number
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Firmware version Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// File Extension
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Last modified date of firmware file
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FirmwareDetail()
        {
            this.Description = string.Empty;
            this.Version = string.Empty;
            this.FileExtension = string.Empty;
            this.LastModifiedDate = DateTime.MinValue;
            this.CheckSumSHA256 = string.Empty;
            this.DownloadUrl = string.Empty;
            this.FileName = string.Empty;
        }
    }
}
