using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Models;
using infrastructure.EntityFramework.Context;
using infrastructure.Rabbitmq.Context;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private RabbitmqContext _rabbitmqContext;
        private SqliteContext _sqliteContext;
        private ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, RabbitmqContext rabbitmqContext, SqliteContext sqliteContext)
        {
            _logger = logger;
            _rabbitmqContext = rabbitmqContext;
            _sqliteContext = sqliteContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Initialize Consume Messages");

            _rabbitmqContext.WeatherConsume += async (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var weatherApiResultViewModel = JsonConvert.DeserializeObject<WeatherApiResultViewModel>(message);
                if (weatherApiResultViewModel == null || !(weatherApiResultViewModel.current.temp_c > 14)) return;
                var weather = new Weather(weatherApiResultViewModel.location.name,
                    weatherApiResultViewModel.current.temp_c);
                _sqliteContext.Weathers.Add(weather);
                await _sqliteContext.SaveChangesAsync(stoppingToken);
            };

            _logger.LogInformation("Start Consume Messages");
            _rabbitmqContext.StartConsume();
            await Task.CompletedTask;
            Console.ReadKey();
        }
    }
}