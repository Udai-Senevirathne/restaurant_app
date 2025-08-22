using System;
using System.Globalization;
using System.Windows.Data;

namespace POSSystem
{
    public class WidthRangeConverter : IValueConverter
    {
        public static readonly WidthRangeConverter Instance = new();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double)value;
            var limit = double.Parse(parameter?.ToString() ?? "600");
            return width >= limit;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}