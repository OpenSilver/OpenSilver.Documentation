
using System;

namespace OpenSilverAndAspire.Models
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
        public int TemperatureF { get; set; }
        public string Day
        {
            get { return Date.ToString("dddd"); }
        }
        public string ShortDate
        {
            get { return Date.ToString("MMMM d, yyyy"); }
        }
    }
}
