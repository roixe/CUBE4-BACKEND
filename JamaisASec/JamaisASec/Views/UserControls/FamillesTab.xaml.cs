using System.Windows;
using System.Windows.Controls;
using JamaisASec.Models;

namespace JamaisASec.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour FamillesControl.xaml
    /// </summary>
    public partial class FamillesTab : UserControl
    {
        public List<Famille> Familles { get; private set; }

        public event EventHandler AddItem;

        public FamillesTab(List<Famille> familles)
        {
            InitializeComponent();
            Familles = familles;
            FamillesGrid.ItemsSource = Familles;
        }

        private void HeaderFamilleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var famille in Familles)
            {
                famille.IsSelected = true;
            }
            FamillesGrid.Items.Refresh();
        }

        private void HeaderFamilleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var famille in Familles)
            {
                famille.IsSelected = false;
            }
            FamillesGrid.Items.Refresh();
        }

        private void SearchFamille_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchFamille.Text;
            FilterFamilles(searchText);
        }

        private void FilterFamilles(string searchText)
        {
            var filteredFamilles = Familles
                .Where(f => f.nom.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
            FamillesGrid.ItemsSource = filteredFamilles;
        }
    }
}
