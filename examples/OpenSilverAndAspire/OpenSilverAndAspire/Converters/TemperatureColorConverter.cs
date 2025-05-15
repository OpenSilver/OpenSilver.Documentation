using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace OpenSilverAndAspire.Converters
{
    public class TemperatureColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int temp)
            {
                if (temp < -10)
                    return new SolidColorBrush(Color.FromArgb(255, 33, 150, 243)); // Very cold - blue
                else if (temp < 0)
                    return new SolidColorBrush(Color.FromArgb(255, 3, 169, 244)); // Cold - light blue
                else if (temp < 10)
                    return new SolidColorBrush(Color.FromArgb(255, 76, 175, 80)); // Cool - green
                else if (temp < 20)
                    return new SolidColorBrush(Color.FromArgb(255, 255, 193, 7)); // Warm - yellow
                else if (temp < 30)
                    return new SolidColorBrush(Color.FromArgb(255, 255, 152, 0)); // Hot - orange
                else
                    return new SolidColorBrush(Color.FromArgb(255, 244, 67, 54)); // Very hot - red
            }

            return new SolidColorBrush(Color.FromArgb(255, 158, 158, 158)); // Default gray
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
