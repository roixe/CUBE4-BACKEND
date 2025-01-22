using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace JamaisASec.Helpers.Converters
{
    class TabActiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var activeTab = value as string;
            var tabName = parameter as string;

            // Compare l'onglet actif avec le bouton correspondant
            return activeTab == tabName ? (Brush)Application.Current.FindResource("Burgundy") : Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
