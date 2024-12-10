using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace JamaisASec
{
    public partial class MainWindow : Window
    {
        List<ToggleButton> MenuButtons { get; set; }
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
            MenuButtons =
            [
                DashboardButton,
                ProduitsButton,
                FournisseursButton,
                StocksButton,
                ClientsButton,
                CommandesButton
            ];
            MainFrame.Navigate(new DashBoard(Produits));
        }

        private void SetActiveButton(ToggleButton button)
        {
            foreach (var btn in MenuButtons)
            {
                if (btn != button)
                {
                    btn.IsChecked = false;
                }
            }
            button.IsChecked = true;
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashBoard(Produits));
            SetActiveButton(DashboardButton);

        }

        private void ProduitsButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageProduits(Produits));
            SetActiveButton(ProduitsButton);
        }

        private void FournisseursButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageFournisseurs(Fournisseurs));
            SetActiveButton(FournisseursButton);
        }

        private void StocksButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageStocks(Produits));
            SetActiveButton(StocksButton);
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageClients());
            SetActiveButton(ClientsButton);
        }

        private void CommandesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageCommandes());
            SetActiveButton(CommandesButton);
        }
    }
}
