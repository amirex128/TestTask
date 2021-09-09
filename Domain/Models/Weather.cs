namespace Domain.Models
{
    public class Weather
    {
        public int Id { get; set; }
        public string City { get; set; }
        public double Temperature { get; set; }

        public Weather(string city, double temperature)
        {
            City = city;
            Temperature = temperature;
        }
    }
}