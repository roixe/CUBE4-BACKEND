using System.Windows;
using System.Windows.Controls;
using JamaisASec.Models;

namespace JamaisASec.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour MaisonsControl.xaml
    /// </summary>
    public partial class MaisonsTab : UserControl
    {
        public List<Maison> Maisons { get; private set; }

        public event EventHandler AddItem;

        public MaisonsTab(List<Maison> maisons)
        {
            InitializeComponent();
            Maisons = maisons;
            MaisonsGrid.ItemsSource = Maisons;
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
            foreach (var maison in Maisons)
            {
                maison.IsSelected = false;
            }
            MaisonsGrid.Items.Refresh();
        }

        private void SearchMaison_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchMaison.Text;
            FilterMaisons(searchText);
        }

        private void FilterMaisons(string searchText)
        {
            var filteredMaisons = Maisons
                .Where(f => f.nom.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
            MaisonsGrid.ItemsSource = filteredMaisons;
        }
    }
}
