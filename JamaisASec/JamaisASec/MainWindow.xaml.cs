﻿using System.Windows;
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
        List<Client> Clients { get; set; }
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

            Clients =
            [
                new("Les vins d'ici", "1 rue de la paix", "vinsdici@mail.com", "0123456789"),
                new("Le Chai des Amis", "12 avenue des Cèdres", "chaiamis@mail.com", "0987654321"),
                new("Saveurs de Bacchus", "34 boulevard du Château", "saveursbacchus@mail.com", "0147258369"),
                new("Cave et Terroir", "8 impasse des Vignerons", "caveetterroir@mail.com", "0178349265"),
                new("Les Ceps Dorés", "15 place des Vendanges", "cepsdores@mail.com", "0187654329"),
                new("Au Bon Cru", "23 allée des Sommeliers", "auboncru@mail.com", "0192837465"),
                new("Vigne et Passion", "45 route des Cépages", "vignepassion@mail.com", "0165483297"),
                new("Terres de Vins", "67 chemin des Grappes", "terresdevins@mail.com", "0156748392"),
                new("Le Raisin Bleu", "89 rue des Tonneaux", "raisinbleu@mail.com", "0143967285"),
                new("L'Art du Vin", "101 cours des Sommeliers", "artduvin@mail.com", "0192834765")
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
            MainFrame.Navigate(new PageClients(Clients));
            SetActiveButton(ClientsButton);
        }

        private void CommandesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageCommandes());
            SetActiveButton(CommandesButton);
        }
    }

    
}
