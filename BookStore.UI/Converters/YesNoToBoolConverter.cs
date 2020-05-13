using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookStore.UI.Converters
{
    internal class YesNoToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (value is string stringValue)
            {
                return stringValue.ToLower() == "yes";
            }
            else
            {
                throw new ArgumentException("value should be string");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (value is bool boolValue)
            {
                return boolValue ? "yes" : "no";
            }
            else
            {
                throw new ArgumentException("value should be bool");
            }
        }
    }
}
