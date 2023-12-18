using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.View.ValueConverter
{
    public class BoolToRainConverter : IValueConverter
    {
        private const string IsRainingMessage = "Currently raining";
        private const string IsNotRainingMessage = "Currently not raining";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRaining = (bool)value;
            if (isRaining) 
            {
                return IsRainingMessage;
            }

            return IsNotRainingMessage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRaining = value as string;
            if (isRaining == IsRainingMessage)
            {
                return true;
            }

            return false;
        }
    }
}
