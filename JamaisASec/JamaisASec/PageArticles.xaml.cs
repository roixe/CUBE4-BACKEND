using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using JamaisASec.UserControls;
using System.Windows.Media;

namespace JamaisASec
{
    public partial class PageArticles : Page
    {
        private List<Article> Articles { get; set; }
        private List<Famille> Familles { get; set; }

        private ArticlesControl articlesControl;
        private FamillesControl famillesControl;
        private Button activeTab;

        public PageArticles(List<Article> articles, List<Famille> familles)
        {
            InitializeComponent();

            var articlesControl = new ArticlesControl(articles);
            var famillesControl = new FamillesControl(familles);

            activeTab = ShowArticlesButton;
            UpdateTabVisuals();

            ContentArea.Content = articlesControl;

            // Navigation entre les UserControls si nécessaire
            ShowArticlesButton.Click += (s, e) => { SwitchTab(ShowArticlesButton, new ArticlesControl(articles)); };
            ShowFamillesButton.Click += (s, e) => { SwitchTab(ShowFamillesButton, new FamillesControl(familles)); };
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

            activeTab.BorderBrush = (Brush)FindResource("Burgundy");
        }
    }
}
