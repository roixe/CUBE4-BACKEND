using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JamaisASec.Helpers.Converters
{
    public class ObjectEditModeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] != null && values[1] != null && bool.TryParse(values[1].ToString(), out bool editMode))
            {
                return (values[0], editMode);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
