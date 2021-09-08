using System;
using Infrastructure.Rabbitmq.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.Rabbitmq.Context
{
    public class RabbitmqBootstrap
    {
        public static void AddRabbitmq(IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<RabbitmqContext>();
            services.Configure<RabbitmqOption>(option =>
            {
                option.HostName =
                    new Uri(configuration["rabbitmq"]);
            });
        }
    }
}