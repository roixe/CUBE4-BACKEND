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
        private List<Article> Articles { get; set; }

        public PageStocks(List<Article> articles)
        {
            InitializeComponent();

            Articles = articles;
            StockGrid.ItemsSource = Articles;

            searchStock.TextChanged += SearchStock_TextChanged;
        }

        private void IncrementStock_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si le bouton est un bouton de stock
            if (sender is Button button && button.DataContext is Article article)
            {
                // Incrémenter le stock en fonction du bouton cliqué
                if (button.Name == "AddStock")
                {
                    article.quantite++;
                }
                else if (button.Name == "AddStockMin")
                {
                    article.quantite_Min++;
                }
                else if (button.Name == "AddColisage")
                {
                    article.colisage++;
                }
                StockGrid.Items.Refresh(); // Met à jour le DataGrid
            }
        }

        private void DecrementStock_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si le bouton est un bouton de stock
            if (sender is Button button && button.DataContext is Article article)
            {
                // Décrémenter le stock en fonction du bouton cliqué
                if (button.Name == "RemoveStock")
                {
                    article.quantite--;
                }
                else if (button.Name == "RemoveStockMin")
                {
                    article.quantite_Min--;
                }
                else if (button.Name == "RemoveColisage")
                {
                    article.colisage--;
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
            var filteredStocks = Articles.Where(p => p.nom.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                     p.description.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) );
                                                    // p.Famille.Contains(searchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
            StockGrid.ItemsSource = filteredStocks;
        }
    }
}
