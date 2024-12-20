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
    /// Logique d'interaction pour MaisonsControl.xaml
    /// </summary>
    public partial class MaisonsControl : UserControl
    {
        public List<Maison> Maisons { get; private set; }

        public event EventHandler AddItem;

        public MaisonsControl(List<Maison> maisons)
        {
            InitializeComponent();
            Maisons = maisons;
            MaisonsGrid.ItemsSource = Maisons;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddItem?.Invoke(this, EventArgs.Empty);
        }

        private void EditMaisonButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Maison maisonSelectionne)
            {
                var modifierFamilleForm = new Forms.MaisonForm(Maisons, maisonSelectionne);
                modifierFamilleForm.ShowDialog();
                MaisonsGrid.Items.Refresh();
            }
        }

        private void RemoveMaisonButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Maison maisonSelectionne)
            {
                Maisons.Remove(maisonSelectionne);
                MaisonsGrid.Items.Refresh();
            }
        }

        private void HeaderMaisonCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var maison in Maisons)
            {
                maison.IsSelected = true;
            }
            MaisonsGrid.Items.Refresh();
        }

        private void HeaderMaisonCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var famille in Maisons)
            {
                famille.IsSelected = false;
            }
            MaisonsGrid.Items.Refresh();
        }

        private void SearchFamille_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchMaison.Text;
            FilterFamilles(searchText);
        }

        private void FilterFamilles(string searchText)
        {
            var filteredMaisons = Maisons
                .Where(f => f.nom.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
            MaisonsGrid.ItemsSource = filteredMaisons;
        }
    }
}
