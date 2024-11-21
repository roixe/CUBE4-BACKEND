using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JamaisASec
{
    /// <summary>
    /// Interaction logic for PageStocks.xaml
    /// </summary>
    public partial class PageStocks : Page
    {
        public PageStocks(List<Produit> produits)
        {
            InitializeComponent();

            // Lier les données au DataGrid
            StockGrid.ItemsSource = produits;
        }

        private void IncrementStock_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si le bouton est un bouton de stock
            if (sender is Button button && button.DataContext is Produit produit)
            {
                // Incrémenter le stock en fonction du bouton cliqué
                if (button.Name == "AddStock")
                {
                    produit.Stock++;
                }
                else if (button.Name == "AddStockMin")
                {
                    produit.StockMin++;
                } else if (button.Name == "AddColisage")
                {
                    produit.Colisage++;
                }
                StockGrid.Items.Refresh(); // Met à jour le DataGrid
            }
        }

        private void DecrementStock_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si le bouton est un bouton de stock
            if (sender is Button button && button.DataContext is Produit produit)
            {
                // Décrémenter le stock en fonction du bouton cliqué
                if (button.Name == "RemoveStock")
                {
                    produit.Stock--;
                }
                else if (button.Name == "RemoveStockMin")
                {
                    produit.StockMin--;
                }
                else if (button.Name == "RemoveColisage")
                {
                    produit.Colisage--;
                }
                StockGrid.Items.Refresh(); // Met à jour le DataGrid
            }
        }

    }
}
