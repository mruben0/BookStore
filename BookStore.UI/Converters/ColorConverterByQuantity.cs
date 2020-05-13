using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BookStore.UI.Converters
{
    public class ColorConverterByQuantity : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte byteValue)
            {
                byte red = (byte)(255 - byteValue);
                return new SolidColorBrush(Color.FromRgb(red, byteValue, 0));
            }
            else
            {
                throw new ArgumentException("value should be byte");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}