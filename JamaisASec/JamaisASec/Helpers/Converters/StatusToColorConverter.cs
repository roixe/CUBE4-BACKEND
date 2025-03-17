using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using JamaisASec.Models;

namespace JamaisASec.Helpers.Converters
{
    class StatusToColorConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StatusCommande status)
            {
                switch (status)
                {
                    case StatusCommande.EnCours:
                        return Application.Current.Resources["Lemon"] as SolidColorBrush;
                    case StatusCommande.Prete:
                        return Application.Current.Resources["Mandarine"] as SolidColorBrush;
                    case StatusCommande.Livree:
                        return Application.Current.Resources["ForestGreen"] as SolidColorBrush;
                    case StatusCommande.EnAttente:
                        return Application.Current.Resources["Mandarine"] as SolidColorBrush;
                    case StatusCommande.Receptionnee:
                        return Application.Current.Resources["ForestGreen"] as SolidColorBrush;
                    case StatusCommande.Annulee:
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
