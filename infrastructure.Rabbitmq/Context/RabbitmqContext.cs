using Infrastructure.Rabbitmq.Context;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace infrastructure.Rabbitmq.Context
{
    public class RabbitmqContext
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public RabbitmqContext(IOptions<RabbitmqOption> options)
        {
            var factory = new ConnectionFactory
            {
                Uri = options.Value.HostName
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();   
        }

        public void StartConsume()
        {
            throw new System.NotImplementedException();
        }
    }
}