using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace JamaisASec
{
    public partial class PageArticles : Page
    {
        private List<Article> Articles { get; set; }
        private List<Famille> Familles { get; set; }

        public PageArticles(List<Article> articles, List<Famille> familles)
        {
            InitializeComponent();

            Articles = articles;
            ArticleGrid.ItemsSource = Articles;
            controlsArticle.AddItem += ControlsArticle_AjouterItem;

            searchArticle.TextChanged += SearchArticle_TextChanged;

            Familles = familles;
            FamillesGrid.ItemsSource = Familles;
            controlsFamille.AddItem += ControlsFamille_AjouterItem;

            searchFamille.TextChanged += SearchFamille_TextChanged;
        }

        private void ControlsArticle_AjouterItem(object sender, RoutedEventArgs e)
        {
            AjouterArticleButton_Click(sender, e);
        }

        private void AjouterArticleButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterProduitForm
            var ajouterProduitForm = new Forms.ArticleForm(Articles, Familles);

            ajouterProduitForm.ShowDialog();

            // Rafraîchir la grille après ajout d'un produit
            ArticleGrid.Items.Refresh();
        }

        private void EditArticleButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le produit associé à la ligne
            if (sender is Button button && button.DataContext is Article articleSelectionne)
            {
                // Ouvrir la fenêtre de modification
                var modifierArticleForm = new Forms.ArticleForm(Articles, Familles, articleSelectionne);

                // Afficher la fenêtre en mode dialogue
                modifierArticleForm.ShowDialog();

                // Rafraîchir la grille après modification
                ArticleGrid.Items.Refresh();
            }
        }

        private void RemoveArticleButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le produit associé à la ligne
            if (sender is Button button && button.DataContext is Article articleSelectionne)
            {
                // Supprimer le produit de la liste
                Articles.Remove(articleSelectionne);
                // Rafraîchir la grille après modification
                ArticleGrid.Items.Refresh();
            }
        }

        private void HeaderArticleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var article in Articles)
                {
                    article.IsSelected = true;
                }
                ArticleGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void HeaderArticleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var article in Articles)
                {
                    article.IsSelected = false;
                }
                ArticleGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void ControlsFamille_AjouterItem(object sender, RoutedEventArgs e)
        {
            AjouterFamilleButton_Click(sender, e);
        }

        private void AjouterFamilleButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterProduitForm
            var ajouterProduitForm = new Forms.FamilleForm(Familles);

            ajouterProduitForm.ShowDialog();

            // Rafraîchir la grille après ajout d'un produit
            FamillesGrid.Items.Refresh();
        }

        private void EditFamilleButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le produit associé à la ligne
            if (sender is Button button && button.DataContext is Famille familleSelectionne)
            {
                // Ouvrir la fenêtre de modification
                var modifierProduitForm = new Forms.FamilleForm(Familles, familleSelectionne);

                // Afficher la fenêtre en mode dialogue
                modifierProduitForm.ShowDialog();

                // Rafraîchir la grille après modification
                FamillesGrid.Items.Refresh();
            }
        }

        private void RemoveFamilleButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le produit associé à la ligne
            if (sender is Button button && button.DataContext is Famille familleSelectionne)
            {
                // Supprimer le produit de la liste
                Familles.Remove(familleSelectionne);
                // Rafraîchir la grille après modification
                FamillesGrid.Items.Refresh();
            }
        }

        private void HeaderFamilleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var famille in Familles)
                {
                    famille.IsSelected = true;
                }
                FamillesGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void HeaderFamilleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var famille in Familles)
                {
                    famille.IsSelected = false;
                }
                FamillesGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void SearchArticle_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchText = searchArticle.Text;
            FilterArticles(searchText);
        }

        private void FilterArticles(string searchText)
        {
            var filteredArticles = Articles.Where(p => p.Nom.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                       p.Description.Contains(searchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
            ArticleGrid.ItemsSource = filteredArticles;
        }

        private void SearchFamille_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchText = searchFamille.Text;
            FilterFamilles(searchText);
        }

        private void FilterFamilles(string searchText)
        {
            var filteredFamilles = Familles.Where(f => f.Nom.Contains(searchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
            FamillesGrid.ItemsSource = filteredFamilles;
        }
    }
}
