// Copyright Schneider-Electric 2024

using Confluent.Kafka;
using ETPX.FirmwareRepository.Settings;

namespace ETPX.FirmwareRepository.Consumer
{
    /// <summary>
    /// KafkaConsumerService Class
    /// </summary>
    public class KafkaConsumerService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IKafkaSettings _kafkaSettings;
        private readonly bool cancelled = false;


        /// <summary>
        /// KafkaConsumerService Constructor
        /// </summary>
        /// <param name="kafkaSettings"></param>
        /// <param name="logger"></param>
        public KafkaConsumerService(IKafkaSettings kafkaSettings, ILogger<KafkaConsumerService> logger)
        {
            _logger = logger;
            _kafkaSettings = kafkaSettings;
            var config = new ConsumerConfig
            {
                BootstrapServers = "pkc-z56v0.eu-west-1.aws.confluent.cloud:9092",
                GroupId = "uat.etpxfirmwareservice.se.com-etpxfirmwareservice",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SaslMechanism = SaslMechanism.OAuthBearer,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslOauthbearerMethod = SaslOauthbearerMethod.Oidc,
                SaslOauthbearerClientId = _kafkaSettings.KafkaClientId,
                SaslOauthbearerClientSecret = _kafkaSettings.KafkaClientSecret,
                SaslOauthbearerScope = "api://" + _kafkaSettings.KafkaClientId + "/.default",               
                SaslOauthbearerTokenEndpointUrl = "https://login.microsoftonline.com/" + _kafkaSettings.KafkaTenantId + "/oauth2/token",
                SaslOauthbearerExtensions = "logicalCluster=lkc-5m80oz,identityPoolId=pool-bPGQ",
                // Increase the SASL handshake timeout to 120 seconds
                SocketConnectionSetupTimeoutMs = 120000,
                //SocketTimeoutMs= 120000


            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _consumer.Subscribe("uat.push.pes.document");
            _logger.LogInformation("All values are initialized");

        }

        /// <summary>
        /// Consumer Method
        /// </summary>
        public void StartConsuming()
        {
            var cancellationToken = new CancellationToken();
            while (!cancelled)
            {
                _logger.LogInformation("Comsumer started reading messages");
                var consumeResult = _consumer.Consume(cancellationToken);
                var message = consumeResult?.Message?.Value;
                _consumer.Commit(consumeResult);
                _logger.LogInformation("Messages read {Message}", message);
                var topic = consumeResult?.Topic;
                _logger.LogInformation("Topic Name {Topic}", topic);
                if (topic == null || message == null) { continue; }
            }
            _logger.LogInformation("Consumer closed");
            _consumer.Close();

        }

    }

}
