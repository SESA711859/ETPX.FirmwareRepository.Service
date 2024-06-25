// Copyright Schneider-Electric 2024

﻿using System.Text.Json.Serialization;

namespace ETPX.FirmwareRepository.Entities
{
    /// <summary>
    /// Type of an Entity
    /// </summary>
    public enum EntityType
    {

        /// <summary>
        /// Commercial Reference No
        /// </summary>
        CommercialReferenceNo,

        /// <summary>
        /// Firmware Version
        /// </summary>
        FirmwareVersion,
    }
}