using System.Windows.Controls;

namespace JamaisASec
{
    public partial class PageProduits : Page
    {
        public PageProduits(List<Produit> produits)
        {
            InitializeComponent();
            ProduitGrid.ItemsSource = produits; 
        }
    }
}

