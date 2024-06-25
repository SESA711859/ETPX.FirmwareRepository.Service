// Copyright Schneider-Electric 2024

namespace ETPX.FirmwareRepository.Settings
{
    /// <summary>
    /// Inteface to IKafkaSettings.
    /// </summary>
    public interface IKafkaSettings
    {
        /// <summary>
        /// KafkaClientId
        /// </summary>
        string KafkaClientId { get; set; }

        /// <summary>
        /// KafkaClientSecret
        /// </summary>
        string KafkaClientSecret { get; set; }

        /// <summary>
        /// KafkaTenantId
        /// </summary>
        string KafkaTenantId { get; set; }
    }
}
