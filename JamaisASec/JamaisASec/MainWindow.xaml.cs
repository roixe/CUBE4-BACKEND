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
            MainFrame.Navigate(new Accueil()); // Clear the frame to show default view
        }

        private void ProduitsButton_Click(object sender, RoutedEventArgs e)
        {
            // Naviguer vers la page des produits
            MainFrame.Navigate(new ProduitsPage());
        }
    }
}
