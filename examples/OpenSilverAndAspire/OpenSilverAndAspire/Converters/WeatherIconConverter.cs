using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenSilverAndAspire.Converters
{
    public class WeatherIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string summary)
            {
                switch (summary.ToLower())
                {
                    case "freezing": return "❄";
                    case "bracing": return "❄";
                    case "chilly": return "🌥";
                    case "cool": return "🌤";
                    case "mild": return "☀";
                    case "warm": return "☀";
                    case "balmy": return "🌞";
                    case "hot": return "🔥";
                    case "sweltering": return "🔥";
                    case "scorching": return "🔥";
                    default: return "☁";
                }
            }

            return "☁";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
