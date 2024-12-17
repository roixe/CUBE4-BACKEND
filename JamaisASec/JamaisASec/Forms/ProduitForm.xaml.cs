using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace JamaisASec.Forms
{
    public partial class ProduitForm : Window
    {
        private List<Produit> Produits { get; set; }
        public List<Famille> Familles { get; set; }
        public bool IsEditMode { get; private set; }
        private Produit? ProduitEnCours { get; set; }

        public ProduitForm(List<Produit> produits, List<Famille> familles, Produit? produitAModifier = null)
        {
            InitializeComponent();
            Produits = produits ?? [];
            Familles = familles ?? [];
            produitFamille.ItemsSource = Familles;
            produitFamille.DisplayMemberPath = "Nom";
            
            IsEditMode = produitAModifier != null;
            ProduitEnCours = produitAModifier;
            this.Title = IsEditMode ? "Modifier un produit" : "Ajouter un produit";

            if (ProduitEnCours != null)
            {
                produitNom.Text = ProduitEnCours.Nom;
                produitDescription.Text = ProduitEnCours.Description;
                produitStock.Text = ProduitEnCours.Stock.ToString();
                produitStockMin.Text = ProduitEnCours.StockMin.ToString();
                produitColisage.Text = ProduitEnCours.Colisage.ToString();
                produitPrix.Text = ProduitEnCours.Prix.ToString();
                produitFamille.SelectedItem = Familles.FirstOrDefault(f => f.Nom == ProduitEnCours.Famille);
            }
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            string nom = produitNom.Text;
            string description = produitDescription.Text;
            int stock = int.TryParse(produitStock.Text, out int parsedStock) ? parsedStock : 0;
            int stockMin = int.TryParse(produitStockMin.Text, out int parsedStockMin) ? parsedStockMin : 0;
            int colisage = int.TryParse(produitColisage.Text, out int parsedColisage) ? parsedColisage : 1;
            int prix = int.TryParse(produitPrix.Text, out int parsedPrix) ? parsedPrix : 0;
            string famille = ((Famille)produitFamille.SelectedItem)?.Nom ?? string.Empty;

            if (ProduitEnCours != null)
            {
                ProduitEnCours.Nom = nom;
                ProduitEnCours.Description = description;
                ProduitEnCours.Stock = stock;
                ProduitEnCours.StockMin = stockMin;
                ProduitEnCours.Colisage = colisage;
                ProduitEnCours.Prix = prix;
                ProduitEnCours.Famille = famille;
            }
            else
            {
                Produit produitAjoute = new Produit(nom, description, famille, prix)
                {
                    Stock = stock,
                    StockMin = stockMin,
                    Colisage = colisage
                };

                Produits.Add(produitAjoute);
            }

            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            // Validation du nom
            if (string.IsNullOrWhiteSpace(produitNom.Text))
            {
                produitNom.ErrorMessage = "Veuillez entrer le nom du produit.";
                isValid = false;
            }
            else
            {
                produitNom.ErrorMessage = string.Empty;
            }

            // Validation de la description
            if (string.IsNullOrWhiteSpace(produitDescription.Text))
            {
                produitDescription.ErrorMessage = "Veuillez entrer la description du produit.";
                isValid = false;
            }
            else
            {
                produitDescription.ErrorMessage = string.Empty;
            }

            // Validation de la famille
            if (produitFamille.SelectedItem == null)
            {
                produitFamille.ErrorMessage = "Veuillez sélectionner une famille.";
                isValid = false;
            }
            else
            {
                produitFamille.ErrorMessage = string.Empty;
            }

            // Validation de produitStock
            if (!int.TryParse(produitStock.Text, out int stock) || stock < 0)
            {
                produitStock.ErrorMessage = "Veuillez entrer un nombre entier valide pour le stock.";
                isValid = false;
            }
            else
            {
                produitStock.ErrorMessage = string.Empty;
            }

            // Validation de produitStockMin
            if (!int.TryParse(produitStockMin.Text, out int stockMin) || stockMin < 0)
            {
                produitStockMin.ErrorMessage = "Veuillez entrer un nombre entier valide pour le stock minimum.";
                isValid = false;
            }
            else
            {
                produitStockMin.ErrorMessage = string.Empty;
            }

            // Validation de produitColisage
            if (!int.TryParse(produitColisage.Text, out int colisage) || colisage < 1)
            {
                produitColisage.ErrorMessage = "Veuillez entrer un nombre entier valide pour le colisage.";
                isValid = false;
            }
            else
            {
                produitColisage.ErrorMessage = string.Empty;
            }

            // Validation de produitPrix
            if (!int.TryParse(produitPrix.Text, out int prix) || prix <= 0)
            {
                produitPrix.ErrorMessage = "Veuillez renseigner le prix (nombre entier valide).";
                isValid = false;
            }
            else
            {
                produitPrix.ErrorMessage = string.Empty;
            }

            return isValid;
        }

    }
}
