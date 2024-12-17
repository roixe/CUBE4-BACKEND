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
            
            searchProduit.TextChanged += SearchProduit_TextChanged;

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
            var ajouterProduitForm = new Forms.ProduitForm(Produits, Familles);

            ajouterProduitForm.ShowDialog();

            // Rafraîchir la grille après ajout d'un produit
            ProduitGrid.Items.Refresh();
        }

        private void EditProduitButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le produit associé à la ligne
            if (sender is Button button && button.DataContext is Produit produitSelectionne)
            {
                // Ouvrir la fenêtre de modification
                var modifierProduitForm = new Forms.ProduitForm(Produits, Familles, produitSelectionne);

                // Afficher la fenêtre en mode dialogue
                modifierProduitForm.ShowDialog();

                // Rafraîchir la grille après modification
                ProduitGrid.Items.Refresh();
            }
        }

        private void RemoveProduitButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le produit associé à la ligne
            if (sender is Button button && button.DataContext is Produit produitSelectionne)
            {
                // Supprimer le produit de la liste
                Produits.Remove(produitSelectionne);
                // Rafraîchir la grille après modification
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
                ProduitGrid.Items.Refresh();
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

        private void SearchProduit_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchText = searchProduit.Text;
            FilterProduits(searchText);
        }

        private void FilterProduits(string searchText)
        {
            var filteredProduits = Produits.Where(p => p.Nom.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                       p.Description.Contains(searchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
            ProduitGrid.ItemsSource = filteredProduits;
        }
    }
}
