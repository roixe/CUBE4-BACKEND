using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JamaisASec.Models;
using System.Windows.Data;

namespace JamaisASec.Helpers.Converters
{
    public class TotalPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ArticlesCommandes articleCommande)
            {
                return articleCommande.quantite * articleCommande.article.prix_unitaire;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
