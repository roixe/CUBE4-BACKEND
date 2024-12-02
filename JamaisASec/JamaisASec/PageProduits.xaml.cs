using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    public partial class ProduitsPage : Page
    {
        public ProduitsPage()
        {
            InitializeComponent();
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AjouterProduit_Click(object sender, RoutedEventArgs e)
        {

            AjouterProduitForm ajouterProduitForm = new AjouterProduitForm();
            ajouterProduitForm.ShowDialog();
        }
    }
}

