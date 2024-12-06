using System.Windows.Controls;

namespace JamaisASec
{
    /// <summary>
    /// Interaction logic for PageFournisseurs.xaml
    /// </summary>
    public partial class PageFournisseurs : Page
    {
        public PageFournisseurs(List<Fournisseur> fournisseurs)
        {
            InitializeComponent();
            FournisseursGrid.ItemsSource = fournisseurs;
        }
    }
}
