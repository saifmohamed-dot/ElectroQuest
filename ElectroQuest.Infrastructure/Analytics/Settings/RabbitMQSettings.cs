namespace ElectroQuest.Infrastructure.Analytics.Settings
{
    public class RabbitMQSettings
    {
        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string RoutingKey { get; set; }
        public string ConnectionUri {  get; set; }
    }
}
