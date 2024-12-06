using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    public partial class AjouterProduitForm : Window
    {
        public Produit ProduitAjoute { get; private set; }
        private List<Produit> Produits { get; set; }

        public AjouterProduitForm(List<Produit> produits)
        {
            InitializeComponent();
            Produits = produits ?? new List<Produit>();
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Vérifier les erreurs de validation
                if (Validation.GetHasError(StockTextBox) || Validation.GetHasError(PrixTextBox))
                {
                    MessageBox.Show("Veuillez corriger les erreurs dans les champs Stock et Prix.");
                    return;
                }

                // Récupérer les valeurs des champs de texte
                string nom = NomTextBox.Text;
                string description = DescriptionTextBox.Text;
                string famille = FamillesTextBox.Text;

                if (string.IsNullOrWhiteSpace(nom))
                {
                    MessageBox.Show("Le nom du produit est obligatoire.");
                    return;
                }

                if (!int.TryParse(StockTextBox.Text, out int stock))
                {
                    MessageBox.Show("Le stock doit être un entier valide.");
                    return;
                }

                if (!int.TryParse(StockMinTextBox.Text, out int stockMin))
                {
                    MessageBox.Show("Le stock minimum doit être un entier valide.");
                    return;
                }

                if (!int.TryParse(ColisageTextBox.Text, out int colisage))
                {
                    MessageBox.Show("Le colisage doit être un entier valide.");
                    return;
                }

                if (!int.TryParse(PrixTextBox.Text, out int prix))
                {
                    MessageBox.Show("Le prix doit être un entier valide.");
                    return;
                }

                // Créer un nouveau produit
                ProduitAjoute = new Produit(nom, description, famille, prix)
                {
                    Stock = stock,
                    StockMin = stockMin,
                    Colisage = colisage
                };

                // Ajouter le produit à la liste des produits
                Produits.Add(ProduitAjoute);

                // Afficher un message de confirmation
                MessageBox.Show("Produit ajouté avec succès!");

               
                this.Close();
            }
            catch (Exception ex)
            {
                // Gérer les erreurs inattendues
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
            }
        }
    }
}
