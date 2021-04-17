using System;

namespace Application_API
{
    public class WeatherForecast
    {
        //this changes done in Windos vs
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}