using System.Windows.Controls;
using System.Windows.Media;
using JamaisASec.Models;
using JamaisASec.Views.UserControls;

namespace JamaisASec.Views
{
    public partial class PageArticles : Page
    {
        private List<Article> Articles { get; set; }
        private List<Famille> Familles { get; set; }
        private List<Maison> Maisons { get; set; }
        private ArticlesTab ArticlesTab { get; set; }
        private FamillesTab FamillesTab { get; set; }
        private MaisonsTab MaisonsTab { get; set; }

        private Button activeTab;

        public PageArticles(List<Article> articles, List<Famille> familles, List<Maison> maisons)
        {
            InitializeComponent();
            Articles = articles;
            Familles = familles;
            Maisons = maisons;
            ArticlesTab = new ArticlesTab(Articles);
            FamillesTab = new FamillesTab(Familles);
            MaisonsTab = new MaisonsTab(Maisons);

            var articlesControl = new ArticlesTab(Articles);

            activeTab = ShowArticlesButton;
            UpdateTabVisuals();

            ContentArea.Content = articlesControl;

            // Navigation entre les UserControls si nécessaire
            ShowArticlesButton.Click += (s, e) => { SwitchTab(ShowArticlesButton, ArticlesTab); };
            ShowFamillesButton.Click += (s, e) => { SwitchTab(ShowFamillesButton, FamillesTab); };
            ShowMaisonsButton.Click += (s, e) => { SwitchTab(ShowMaisonsButton, MaisonsTab); };

        }
       
        private void SwitchTab(Button newActiveTab, UserControl newContent)
        {
            // Mettre à jour l'état actif
            activeTab = newActiveTab;
            ContentArea.Content = newContent;

            // Rafraîchir l'apparence des boutons
            UpdateTabVisuals();
        }
        private void UpdateTabVisuals()
        {
            ShowArticlesButton.BorderBrush = Brushes.Transparent;
            ShowFamillesButton.BorderBrush = Brushes.Transparent;
            ShowMaisonsButton.BorderBrush = Brushes.Transparent;

            activeTab.BorderBrush = (Brush)FindResource("Burgundy");
        }
    }
}
