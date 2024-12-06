using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    public partial class PageProduits : Page
    {
        private List<Produit> Produits { get; set; }

        public PageProduits(List<Produit> produits)
        {
            InitializeComponent();
            Produits = produits ?? new List<Produit>();
            ProduitGrid.ItemsSource = Produits;
        }

        private void AjouterProduitButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterProduitForm
            var ajouterProduitForm = new AjouterProduitForm(Produits);

            ajouterProduitForm.ShowDialog();

            // Rafraîchir la grille après ajout d'un produit
            ProduitGrid.ItemsSource = null; 
            ProduitGrid.ItemsSource = Produits; 
        }
    }
}
