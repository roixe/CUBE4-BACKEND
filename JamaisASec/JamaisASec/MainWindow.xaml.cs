using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Accueil());
        }

        private void AccueilButton_Click(object sender, RoutedEventArgs e)
        {
            // Charger une page d'accueil si elle existe
            MainFrame.Navigate(new Accueil());
        }

        private void ProduitsButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageProduits());
        }

        private void fournisseursButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageFournisseurs());
        }

        private void StocksButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Navigate(new PageStocks()); 
        }
    }
}
