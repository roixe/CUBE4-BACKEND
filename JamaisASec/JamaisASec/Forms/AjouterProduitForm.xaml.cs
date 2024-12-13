using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JamaisASec.Forms
{
    public partial class AjouterProduitForm : Window
    {
        public Produit? ProduitAjoute { get; private set; } // Allow ProduitAjoute to be nullable
        private List<Produit> Produits { get; set; }
        public List<string> Familles { get; set; }

        public AjouterProduitForm(List<Produit> produits)
        {
            InitializeComponent();
            Produits = produits ?? new List<Produit>();
            Familles = new List<string> { "Vin Rouge", "Vin Blanc", "Vin Rosé", "Crémant" };
            produitFamille.ItemsSource = Familles;
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) { return; }

            string nom = produitNom.Text;
            string description = produitDescription.Text;
            int.TryParse(produitStock.Text, out int stock);
            int.TryParse(produitStockMin.Text, out int stockMin);
            int.TryParse(produitColisage.Text, out int colisage);
            int.TryParse(produitPrix.Text, out int prix);
            string famille = (string)produitFamille.SelectedItem;

            // Créer un nouveau produit
            ProduitAjoute = new Produit(nom, description, famille, prix)
            {
                Stock = stock,
                StockMin = stockMin,
                Colisage = colisage
            };

            // Ajouter le produit à la liste des produits
            Produits.Add(ProduitAjoute);

            this.Close();
        
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(produitNom.Text))
            {
                produitNom.ErrorMessage = "Ce champ est obligatoire.";
                isValid = false;
            }
            else
            {
                produitNom.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(produitDescription.Text))
            {
                produitDescription.ErrorMessage = "Ce champ est obligatoire.";
                isValid = false;
            }
            else
            {
                produitDescription.ErrorMessage = string.Empty;
            }

            if (produitFamille.SelectedItem == null)
            {
                produitFamille.ErrorMessage = "Veuillez sélectionner une famille.";
                isValid = false;
            }
            else
            {
                produitFamille.ErrorMessage = string.Empty;
            }


            return isValid;
        }
    }
}
