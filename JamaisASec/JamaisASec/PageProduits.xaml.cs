using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    public partial class ProduitsPage : Page
    {
        public ObservableCollection<Produit> Produits { get; set; }

        public ProduitsPage()
        {
            InitializeComponent();
            Produits = new ObservableCollection<Produit>();
            DataContext = this;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Logique pour gérer la sélection de produit
        }

        private void AjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            AjouterProduitForm ajouterProduitForm = new AjouterProduitForm();
            ajouterProduitForm.ShowDialog();

            // Vérifiez si un produit a été ajouté
            if (ajouterProduitForm.ProduitAjoute != null)
            {
                Produits.Add(ajouterProduitForm.ProduitAjoute);
            }
        }
    }
}
