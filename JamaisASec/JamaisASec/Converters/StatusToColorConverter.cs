using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace JamaisASec.Converters
{
    class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string statut)
            {
                switch (statut.ToLower())
                {
                    case "en cours":
                        return Application.Current.Resources["OceanBlue"] as SolidColorBrush;
                    case "livree":
                        return Application.Current.Resources["ForestGreen"] as SolidColorBrush;
                    case "en attente":
                        return Application.Current.Resources["OceanBlue"] as SolidColorBrush;
                    case "receptionnee":
                        return Application.Current.Resources["ForestGreen"] as SolidColorBrush;
                    case "annulee":
                        return Application.Current.Resources["CalmRed"] as SolidColorBrush;
                    default:
                        return Brushes.Gray;
                }
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
