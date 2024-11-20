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
        public PageStocks()
        {
            InitializeComponent();
            var produits = new List<Produit>
            {
                new Produit("Produit A", "Description du produit A", "Famille 1"),
                new Produit("Produit B", "Description du produit B", "Famille 2"),
                new Produit("Produit C", "Description du produit C", "Famille 1" )
            };

            // Lier les données au DataGrid
            MonDataGrid.ItemsSource = produits;
        }



        private void IncrementStock_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Produit produit)
            {
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
                MonDataGrid.Items.Refresh(); // Met à jour le DataGrid
            }
        }

        private void DecrementStock_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Produit produit)
            {
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
                MonDataGrid.Items.Refresh(); // Met à jour le DataGrid
            }
        }

    }
}
