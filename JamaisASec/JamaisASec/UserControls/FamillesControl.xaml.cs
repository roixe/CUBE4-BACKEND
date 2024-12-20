using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JamaisASec.UserControls
{
    /// <summary>
    /// Logique d'interaction pour FamillesControl.xaml
    /// </summary>
    public partial class FamillesControl : UserControl
    {
        public List<Famille> Familles { get; private set; }

        public event EventHandler AddItem;

        public FamillesControl(List<Famille> familles)
        {
            InitializeComponent();
            Familles = familles;
            FamillesGrid.ItemsSource = Familles;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddItem?.Invoke(this, EventArgs.Empty);
        }

        private void EditFamilleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Famille familleSelectionne)
            {
                var modifierFamilleForm = new Forms.FamilleForm(Familles, familleSelectionne);
                modifierFamilleForm.ShowDialog();
                FamillesGrid.Items.Refresh();
            }
        }

        private void RemoveFamilleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Famille familleSelectionne)
            {
                Familles.Remove(familleSelectionne);
                FamillesGrid.Items.Refresh();
            }
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
                .Where(f => f.Nom.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
            FamillesGrid.ItemsSource = filteredFamilles;
        }
    }
}
