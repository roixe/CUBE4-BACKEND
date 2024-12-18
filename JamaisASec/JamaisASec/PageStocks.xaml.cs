using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace JamaisASec
{
    /// <summary>
    /// Interaction logic for PageStocks.xaml
    /// </summary>
    public partial class PageStocks : Page
    {
        private List<Produit> Produits { get; set; }

        public PageStocks(List<Produit> produits)
        {
            InitializeComponent();

            Produits = produits;
            StockGrid.ItemsSource = Produits;

            searchStock.TextChanged += SearchStock_TextChanged;
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
                }
                else if (button.Name == "AddColisage")
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

        private void SearchStock_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchText = searchStock.Text;
            FilterStocks(searchText);
        }

        private void FilterStocks(string searchText)
        {
            var filteredStocks = Produits.Where(p => p.Nom.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                     p.Description.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                     p.Famille.Contains(searchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
            StockGrid.ItemsSource = filteredStocks;
        }
    }
}
