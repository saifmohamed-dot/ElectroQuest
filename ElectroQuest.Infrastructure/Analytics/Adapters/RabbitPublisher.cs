using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using ElectroQuest.Infrastructure.Analytics.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.ComponentModel.Design;
using System.Text;
using System.Text.Json;

namespace ElectroQuest.Infrastructure.Analytics.Adapters
{
    // this a concrete class to publish on a real rabbitmq 
    // all of the configuration will be found in app.settings 
    // and passed as strongly typed options object ( rabbitmqOptions.cs )
    public class RabbitPublisher : IPublishMessage , IAsyncDisposable
    {
        // settings passed with IOption<RAbbitMQSettings.cs> 
        // configured in <ref="ElectroQuest.Infrastructure\InfrastructureDependencies.cs">

        readonly RabbitMQSettings _rabbitmqSettings;
        static IConnection _connection; // singletone : created once and re-used for multiple channels
        public RabbitPublisher(IOptions<RabbitMQSettings> settings)
        {
            _rabbitmqSettings = settings.Value;
        }
        public async Task PublishAsync<T>(T message)
        {
            using IChannel channel = await GetChannel();
            await ChannelSetup(channel);
            string body = JsonSerializer.Serialize(message); 
            await channel.BasicPublishAsync(_rabbitmqSettings.ExchangeName, _rabbitmqSettings.RoutingKey, Encoding.UTF8.GetBytes(body));
            //Console.WriteLine($"publish for Channel {x}");
        }
        // connectoin created only once 
        // and the following calls will reuse this it .
        // this is a singleton function 
        // so it had to be blocked till the connection created 
        // if you deal with it as async => most probably when you hit await 
        // we get another call for another publish task , and this task will ask if the same connection (in the initialization process still) is initialized or not
        // you will find your self initiated it multiple time .
        async Task<IChannel> GetChannel()
        {
            IConnection connection = GetConnection();
            IChannel channel = await connection.CreateChannelAsync();
            return channel;
        }
        IConnection GetConnection()
        {
            if(_connection == null )
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
            await channel.QueueBindAsync(_rabbitmqSettings.QueueName, _rabbitmqSettings.ExchangeName , _rabbitmqSettings.RoutingKey, null);
        }

        public async ValueTask DisposeAsync()
        {
            await _connection.CloseAsync();
        }

        public async Task CompletePublishNotify()
        {
            IChannel channel = await GetChannel();
            await ChannelSetup(channel);
            await channel.BasicPublishAsync(_rabbitmqSettings.ExchangeName, _rabbitmqSettings.RoutingKey, Encoding.UTF8.GetBytes("END"));
        }
    }
}
