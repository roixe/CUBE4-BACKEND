using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace JamaisASec.Forms
{
    public partial class ArticleForm : Window
    {
        private List<Article> Articles { get; set; }
        public List<Famille> Familles { get; set; }
        public bool IsEditMode { get; private set; }
        private Article? ArticleEnCours { get; set; }

        public ArticleForm(List<Article> articles, List<Famille> familles, Article? articleAModifier = null)
        {
            InitializeComponent();
            Articles = articles ?? [];
            Familles = familles ?? [];
            articleFamille.ItemsSource = Familles;
            articleFamille.DisplayMemberPath = "Nom";
            
            IsEditMode = articleAModifier != null;
            ArticleEnCours = articleAModifier;
            this.Title = IsEditMode ? "Modifier un article" : "Ajouter un article";

            if (ArticleEnCours != null)
            {
                articleNom.Text = ArticleEnCours.Nom;
                articleDescription.Text = ArticleEnCours.Description;
                articleStock.Text = ArticleEnCours.Stock.ToString();
                articleStockMin.Text = ArticleEnCours.StockMin.ToString();
                articleColisage.Text = ArticleEnCours.Colisage.ToString();
                articlePrix.Text = ArticleEnCours.Prix.ToString();
                articleFamille.SelectedItem = Familles.FirstOrDefault(f => f.Nom == ArticleEnCours.Famille);
            }
        }

        private void FormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            string nom = articleNom.Text;
            string description = articleDescription.Text;
            int stock = int.TryParse(articleStock.Text, out int parsedStock) ? parsedStock : 0;
            int stockMin = int.TryParse(articleStockMin.Text, out int parsedStockMin) ? parsedStockMin : 0;
            int colisage = int.TryParse(articleColisage.Text, out int parsedColisage) ? parsedColisage : 1;
            int prix = int.TryParse(articlePrix.Text, out int parsedPrix) ? parsedPrix : 0;
            string famille = ((Famille)articleFamille.SelectedItem)?.Nom ?? string.Empty;
            int annee = int.TryParse(articleAnnee.Text, out int parsedAnnee) ? parsedAnnee : 0;

            if (ArticleEnCours != null)
            {
                ArticleEnCours.Nom = nom;
                ArticleEnCours.Description = description;
                ArticleEnCours.Stock = stock;
                ArticleEnCours.StockMin = stockMin;
                ArticleEnCours.Colisage = colisage;
                ArticleEnCours.Prix = prix;
                ArticleEnCours.Famille = famille;
            }
            else
            {
                Article produitAjoute = new Article(nom, description, famille, prix, annee)
                {
                    Stock = stock,
                    StockMin = stockMin,
                    Colisage = colisage
                };

                Articles.Add(produitAjoute);
            }

            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            // Validation du nom
            if (string.IsNullOrWhiteSpace(articleNom.Text))
            {
                articleNom.ErrorMessage = "Veuillez entrer le nom du produit.";
                isValid = false;
            }
            else
            {
                articleNom.ErrorMessage = string.Empty;
            }

            // Validation de la description
            if (string.IsNullOrWhiteSpace(articleDescription.Text))
            {
                articleDescription.ErrorMessage = "Veuillez entrer la description du produit.";
                isValid = false;
            }
            else
            {
                articleDescription.ErrorMessage = string.Empty;
            }

            // Validation de la famille
            if (articleFamille.SelectedItem == null)
            {
                articleFamille.ErrorMessage = "Veuillez sélectionner une famille.";
                isValid = false;
            }
            else
            {
                articleFamille.ErrorMessage = string.Empty;
            }

            // Validation de produitStock
            if (!int.TryParse(articleStock.Text, out int stock) || stock < 0)
            {
                articleStock.ErrorMessage = "Veuillez entrer un nombre entier valide pour le stock.";
                isValid = false;
            }
            else
            {
                articleStock.ErrorMessage = string.Empty;
            }

            // Validation de produitStockMin
            if (!int.TryParse(articleStockMin.Text, out int stockMin) || stockMin < 0)
            {
                articleStockMin.ErrorMessage = "Veuillez entrer un nombre entier valide pour le stock minimum.";
                isValid = false;
            }
            else
            {
                articleStockMin.ErrorMessage = string.Empty;
            }

            // Validation de produitColisage
            if (!int.TryParse(articleColisage.Text, out int colisage) || colisage < 1)
            {
                articleColisage.ErrorMessage = "Veuillez entrer un nombre entier valide pour le colisage.";
                isValid = false;
            }
            else
            {
                articleColisage.ErrorMessage = string.Empty;
            }

            // Validation de produitPrix
            if (!int.TryParse(articlePrix.Text, out int prix) || prix <= 0)
            {
                articlePrix.ErrorMessage = "Veuillez renseigner le prix (nombre entier valide).";
                isValid = false;
            }
            else
            {
                articlePrix.ErrorMessage = string.Empty;
            }

            return isValid;
        }

    }
}
