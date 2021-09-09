using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Domain.DTOs;
using Infrastructure.Rabbitmq.Context;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace infrastructure.Rabbitmq.Context
{
    public class RabbitmqContext
    {
        private IModel _channel;
        private IConnection _connection;
        public event EventHandler<BasicDeliverEventArgs> WeatherConsume;

        public RabbitmqContext(IOptions<RabbitmqOption> options)
        {
            var factory = new ConnectionFactory
            {
                Uri = options.Value.HostName
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("weather_exchange", type: ExchangeType.Fanout, true, false,
                new Dictionary<string, object>
                {
                    { "x-message-ttl", 30000 }
                });

            _channel.QueueDeclare("weather_queue", true, false, false, null);
            _channel.QueueBind("weather_queue", "weather_exchange", "weather_queue");
            _channel.BasicQos(0, 10, false);
        }

        public void WeatherPublish(WeatherApiResultViewModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            _channel.BasicPublish(exchange: "weather_queue",
                routingKey: "weather_queue",
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(json));
        }

        public void StartConsume()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += WeatherConsume;

            _channel.BasicConsume(queue: "weather_queue",
                autoAck: true,
                consumer: consumer);
        }


    }
}