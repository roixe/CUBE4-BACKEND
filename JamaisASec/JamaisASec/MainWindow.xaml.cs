using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    public partial class MainWindow : Window
    {
        List<Produit> Produits { get; set; }
        List<Fournisseur> Fournisseurs { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            // Initialiser les produits
            Produits =
            [
                new("Produit A", "Description du produit A", "Famille 1", 20),
                new("Produit B", "Description du produit B", "Famille 2", 25),
                new("Produit C", "Description du produit C", "Famille 1", 30)
            ];

            Fournisseurs =
            [
                new("Domaine Tariquet", "adresse 1", "tariquet@mail.com", "0123456789", "123456789"),
                new("Pelleheaut", "adresse 2", "pelleheaut@mail.com", "0123456789", "123456789"),
                new("Domaine Uby", "adresse 3", "uby@mail.com", "012345678", "123456789")
            ];
            MainFrame.Navigate(new Accueil(Produits));
        }

        private void AccueilButton_Click(object sender, RoutedEventArgs e)
        {
            // Charger une page d'accueil si elle existe
            MainFrame.Navigate(new Accueil(Produits));
        }

        private void ProduitsButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageProduits(Produits));
        }

        private void FournisseursButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageFournisseurs(Fournisseurs));
        }

        private void StocksButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Navigate(new PageStocks(Produits)); 
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageClients());
        }
    }
}
