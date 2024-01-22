using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection? _connection;
        private readonly IModel? _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMqHost"],
                Port = int.Parse(_configuration["RabbitMqPort"]!)
            };

            try
            {
                // Console.WriteLine($"--> HostName: {_configuration["RabbitMqHost"]}");
                // Console.WriteLine($"--> Port: {_configuration["RabbitMqPort"]}");
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMqConnectionShutDown;

                Console.WriteLine("--> Connected to RabbitMQ MessageBus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connet to RabbitMQ: {ex}");

            }
        }
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);

            if (_connection is not null && _connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ connection open, sendding message . . . ");
                // To do send the message
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ connection is closed, not sending");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                routingKey: "",
                basicProperties: null,
                body: body);

            Console.WriteLine($"--> Message has send: {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("Message Bus Dispose");
            if (_channel is not null && _channel.IsOpen)
            {
                _channel.Close();
                _connection!.Close();
            }
        }

        private void RabbitMqConnectionShutDown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ connection has shutdown");
        }
    }
}