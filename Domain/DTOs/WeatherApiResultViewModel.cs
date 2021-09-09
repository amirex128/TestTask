namespace Domain.DTOs
{
    public class WeatherApiResultViewModel
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }
    public class Location
    {
        public string name { get; set; }
        public string country { get; set; }
    }

    public class Current
    {
        public double temp_c { get; set; }
    }

    
}