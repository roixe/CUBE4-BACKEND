using System.Windows;
using System.Windows.Controls;

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

            controlsFournisseur.AddItem += ControlsFournisseur_AjouterItem;
        }
        private void ControlsFournisseur_AjouterItem(object sender, RoutedEventArgs e)
        {
            AjouterFournisseurButton_Click(sender, e);
        }

        private void AjouterFournisseurButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterFournisseur
            var form = new Forms.FournisseurForm(Fournisseurs);

            form.ShowDialog();

            // Rafraîchir la grille après ajout d'un produit
            FournisseursGrid.Items.Refresh();
        }

        private void EditProduitButton_Click(object sender, RoutedEventArgs e)
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
    }
}
