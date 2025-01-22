using System.Windows;
using System.Windows.Controls;
using JamaisASec.Models;

namespace JamaisASec.Views
{
    /// <summary>
    /// Logique d'interaction pour PageAchats.xaml
    /// </summary>
    public partial class PageAchats : Page
    {
        private List<Commande> Achats;
        public PageAchats(List<Commande> achats)
        {
            InitializeComponent();
            Achats = achats;
            AchatsGrid.ItemsSource = Achats;
        }
        private void HeaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var achat in Achats)
                {
                    achat.IsSelected = true;
                }
                AchatsGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void HeaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var achat in Achats)
                {
                    achat.IsSelected = false;
                }
                AchatsGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }
    }
}
