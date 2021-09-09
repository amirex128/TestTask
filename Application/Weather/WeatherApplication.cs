using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.DTOs;
using Hangfire;
using infrastructure.Rabbitmq.Context;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Weather
{
    public class WeatherApplication
    {
        private IBackgroundJobClient _backgroundJobClient;
        private RabbitmqContext _rabbitmqContext;
        private IHttpClientFactory _httpClient;
        private IConfiguration _configuration;

        public WeatherApplication(IBackgroundJobClient backgroundJobClient, RabbitmqContext rabbitmqContext,
            IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _backgroundJobClient = backgroundJobClient;
            _rabbitmqContext = rabbitmqContext;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> ScheduleTask(WeatherViewModel model, HttpResponseMessage httpResponse)
        {
            var result = await httpResponse.Content.ReadAsStringAsync();
            var weatherApiResultViewModel = JsonConvert.DeserializeObject<WeatherApiResultViewModel>(result);
            var jobId = _backgroundJobClient.Schedule(() =>
                    _rabbitmqContext.WeatherPublish(weatherApiResultViewModel),
                DateTimeOffset.Parse(model.StartedAt.ToString()));
            return jobId;
        }

        public async Task<HttpResponseMessage> GetApiWeather(WeatherViewModel model)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"current.json?key={_configuration["WeatherApi"]}&q={model.Lat},{model.Lon}&aqi=yes");

            var httpClient = _httpClient.CreateClient("weather");

            var httpResponse = await httpClient.SendAsync(requestMessage);
            return httpResponse;
        }
    }
}