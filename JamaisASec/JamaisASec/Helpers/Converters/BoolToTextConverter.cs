using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JamaisASec.Helpers.Converters
{
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEditMode)
            {
                // Vérifie si un paramètre est fourni pour personnaliser le résultat
                if (parameter is string param && param.Contains(";"))
                {
                    var options = param.Split(';');
                    return isEditMode ? options[0] : options[1];
                }

                return isEditMode ? "Modifier" : "Ajouter";
            }

            return DependencyProperty.UnsetValue; // Valeur par défaut si le type n'est pas valide
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack n'est pas supporté pour BoolToTextConverter.");
        }
    }
}
