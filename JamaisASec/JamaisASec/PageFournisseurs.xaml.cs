using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace JamaisASec
{
    /// <summary>
    /// Interaction logic for PageFournisseurs.xaml
    /// </summary>
    public partial class PageFournisseurs : Page
    {
        private List<Fournisseur> Fournisseurs { get; set; }

        public PageFournisseurs(List<Fournisseur> fournisseurs)
        {
            InitializeComponent();
            Fournisseurs = fournisseurs;
            FournisseursGrid.ItemsSource = Fournisseurs;

            controlsFournisseur.AddItem += AjouterButton_Click;
            searchFournisseur.TextChanged += SearchFournisseur_TextChanged;
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterFournisseur
            var form = new Forms.FournisseurForm(Fournisseurs);

            form.ShowDialog();

            // Rafraîchir la grille après ajout d'un produit
            FournisseursGrid.Items.Refresh();
        }

        private void EditFournisseurButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le client associé à la ligne
            if (sender is Button button && button.DataContext is Fournisseur fournisseurSelectionne)
            {
                // Ouvrir la fenêtre de modification
                var modifierProduitForm = new Forms.FournisseurForm(Fournisseurs, fournisseurSelectionne);

                // Afficher la fenêtre en mode dialogue
                modifierProduitForm.ShowDialog();

                // Rafraîchir la grille après modification
                FournisseursGrid.Items.Refresh();
            }
        }

        private void RemoveFournisseurButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le client associé à la ligne
            if (sender is Button button && button.DataContext is Fournisseur fournisseurSelectionne)
            {
                // Supprimer le client de la liste
                Fournisseurs.Remove(fournisseurSelectionne);
                // Rafraîchir la grille après suppression
                FournisseursGrid.Items.Refresh();
            }
        }

        private void SearchFournisseur_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchText = searchFournisseur.Text;
            FilterFournisseurs(searchText);
        }

        private void FilterFournisseurs(string searchText)
        {
            var filteredFournisseurs = Fournisseurs.Where(f => f.nom.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                             f.adresse.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                             f.mail.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                             f.telephone.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                             f.siret.Contains(searchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
            FournisseursGrid.ItemsSource = filteredFournisseurs;
        }

        private void HeaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var fournisseur in Fournisseurs)
                {
                    fournisseur.IsSelected = true;
                }
                FournisseursGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void HeaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var fournisseur in Fournisseurs)
                {
                    fournisseur.IsSelected = false;
                }
                FournisseursGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }
    }
}
