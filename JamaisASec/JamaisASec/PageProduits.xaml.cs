using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    public partial class PageProduits : Page
    {
        private List<Produit> Produits { get; set; }
        private List<Famille> Familles { get; set; }

        public PageProduits(List<Produit> produits, List<Famille> familles)
        {
            InitializeComponent();
            Produits = produits;
            ProduitGrid.ItemsSource = Produits;

            controlsProduit.AddItem += ControlsProduit_AjouterItem;
            Familles = familles;
            FamillesGrid.ItemsSource = Familles;
            controlsFamille.AddItem += ControlsFamille_AjouterItem;
        }
        private void ControlsProduit_AjouterItem(object sender, RoutedEventArgs e)
        {
            AjouterProduitButton_Click(sender, e);
        }
        private void AjouterProduitButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterProduitForm
            var ajouterProduitForm = new Forms.AjouterProduitForm(Produits);

            ajouterProduitForm.ShowDialog();

            // Rafraîchir la grille après ajout d'un produit
            ProduitGrid.Items.Refresh();
        }
    }

        private void ControlsFamille_AjouterItem(object sender, RoutedEventArgs e)
        {
            AjouterFamilleButton_Click(sender, e);
        }
        private void AjouterFamilleButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterProduitForm
            var ajouterProduitForm = new Forms.AjouterFamille(Familles);

            ajouterProduitForm.ShowDialog();

            // Rafraîchir la grille après ajout d'un produit
            FamillesGrid.Items.Refresh();
        }
    }
}
