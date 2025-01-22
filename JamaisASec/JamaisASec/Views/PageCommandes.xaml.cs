using System.Windows;
using System.Windows.Controls;
using JamaisASec.Models;

namespace JamaisASec.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PageCommandes : Page
    {
        private List<Commande> Commandes;
        public PageCommandes(List<Commande> commandes)
        {
            InitializeComponent();
            Commandes = commandes;
            CommandesGrid.ItemsSource = Commandes;
        }
        private void HeaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var commande in Commandes)
                {
                    commande.IsSelected = true;
                }
                CommandesGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void HeaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var commande in Commandes)
                {
                    commande.IsSelected = false;
                }
                CommandesGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }
    }
}
