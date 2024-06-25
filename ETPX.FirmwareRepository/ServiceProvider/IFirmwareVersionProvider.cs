// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.Entities;

namespace ETPX.FirmwareRepository.ServiceProvider
{
    /// <summary>
    /// Firmware version provider
    /// </summary>
    public interface IFirmwareVersionProvider
    {
        /// <summary>
        /// Gets list of Firmware files for given commercial reference.
        /// </summary>
        /// <param name="commercialReference">Commercial Reference Number</param>
        /// <returns>List of Firmware details</returns>
        Task<List<FirmwareDetail>> GetFirmwareListAsync(string commercialReference);

        /// <summary>
        /// Gets the latest Firmware details for given commercial reference and firmware Version.
        /// </summary>
        /// <param name="commercialReference">Commercial Reference Number</param>
        /// <param name="fwVersion">Optional Firmware version</param>
        /// <returns>List of Firmware Detail</returns>
        Task<List<FirmwareDetail>> GetLatestFirmwareAsync(string commercialReference, Version? fwVersion);
        
        /// <summary>
        /// Gets the specific Firmware details for given commercial reference and firmware Version.
        /// </summary>
        /// <param name="commercialReference">Commercial Reference Number</param>
        /// <param name="fwVersion">Firmware version</param>
        /// <returns>List of Firmware Detail</returns>
        Task<List<FirmwareDetail>> GetSpecificFwAsync(string commercialReference, Version fwVersion);
    }
}

