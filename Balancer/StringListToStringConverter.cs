using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Balancer
{
    //[ValueConversion(typeof(List<string>), typeof(string))]
    public class StringListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strs = (List<string>)value;

            if (strs != null)
            {
                string.Join(",", strs);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;

            if (str != null)
            {
                var strs = str.Split(',');

                return strs;
            }


            return DependencyProperty.UnsetValue;
        }
    }
}
