// Copyright Schneider-Electric 2024

namespace ETPX.FirmwareRepository.Settings
{
    /// <summary>
    /// Settings to Connect KAfka Confluent
    /// </summary>
    public class KafkaSettings: IKafkaSettings
    {
        /// <summary>
        /// Gets or sets KafkaClientId
        /// </summary>
        public string KafkaClientId { get; set; }

        /// <summary>
        /// Gets or sets KafkaClientSecret
        /// </summary>
        public string KafkaClientSecret { get; set; }

        /// <summary>
        /// Gets or sets KafkaTenantId
        /// </summary>
        public string KafkaTenantId { get; set; }

        /// <summary>
        /// KafkaSettings Default Constructor
        /// </summary>
        public KafkaSettings()
        {
            KafkaClientId = string.Empty;
            KafkaClientSecret = string.Empty;
            KafkaTenantId = string.Empty;
        }

    }
}
