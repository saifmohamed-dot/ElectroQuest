using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using ElectroQuest.Infrastructure.Analytics.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ElectroQuest.Infrastructure.Analytics.Adapters
{
    public class RabbitConsumer : IConsumeMessage
    {
        readonly RabbitMQSettings _rabbitmqSettings;
        static IConnection _connection;
        public RabbitConsumer(IOptions<RabbitMQSettings> settings)
        {
            _rabbitmqSettings = settings.Value;
        }
        public async Task ConsumeAsync(Func<string, bool> onRecieve)
        {

            IConnection connection = GetConnection();
            IChannel channel = await connection.CreateChannelAsync();
            await ChannelSetup(channel);
            await channel.BasicQosAsync(0, 1, false);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {
                Console.WriteLine("Enter Recived Task");
                string content = Encoding.UTF8.GetString(args.Body.ToArray());
                //if(content == "END")
                //{
                //    await channel.BasicAckAsync(args.DeliveryTag, false);
                //    await channel.CloseAsync();
                //    return;
                //}
                for (int i = 0; i<3; i++)
                {
                    if(onRecieve(content))
                    {
                        await channel.BasicAckAsync(args.DeliveryTag, false);
                        break;
                    }
                }
            };
            await channel.BasicConsumeAsync(_rabbitmqSettings.QueueName, false, consumer);
        }
        IConnection GetConnection()
        {
            if (_connection == null)
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = new Uri(_rabbitmqSettings.ConnectionUri);
                _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
                Console.WriteLine($"connection constructed");
            }

            return _connection;
        }
        async Task ChannelSetup(IChannel channel)
        {
            await channel.ExchangeDeclareAsync(
                _rabbitmqSettings.ExchangeName,
                _rabbitmqSettings.ExchangeType,
                durable: true,
                autoDelete: false,
                arguments: null
            );
            await channel.QueueDeclareAsync(
                _rabbitmqSettings.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            await channel.QueueBindAsync(_rabbitmqSettings.QueueName, _rabbitmqSettings.ExchangeName, _rabbitmqSettings.RoutingKey, null);
        }

        public async ValueTask DisposeAsync()
        {
            await _connection.CloseAsync();
        }
    }
}
