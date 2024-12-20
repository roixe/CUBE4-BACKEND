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
    /// Logique d'interaction pour ArticlesControl.xaml
    /// </summary>
    public partial class ArticlesControl : UserControl
    {
        public List<Article> Articles { get; private set; }

        public event EventHandler AddItem;

        public ArticlesControl(List<Article> articles)
        {
            InitializeComponent();
            Articles = articles;
            ArticleGrid.ItemsSource = Articles;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddItem?.Invoke(this, EventArgs.Empty);
        }

        private void EditArticleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Article articleSelectionne)
            {
                var modifierArticleForm = new Forms.ArticleForm(Articles, null, articleSelectionne);
                modifierArticleForm.ShowDialog();
                ArticleGrid.Items.Refresh();
            }
        }

        private void RemoveArticleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Article articleSelectionne)
            {
                Articles.Remove(articleSelectionne);
                ArticleGrid.Items.Refresh();
            }
        }

        private void HeaderArticleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var article in Articles)
            {
                article.IsSelected = true;
            }
            ArticleGrid.Items.Refresh();
        }

        private void HeaderArticleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var article in Articles)
            {
                article.IsSelected = false;
            }
            ArticleGrid.Items.Refresh();
        }

        private void SearchArticle_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchArticle.Text;
            FilterArticles(searchText);
        }

        private void FilterArticles(string searchText)
        {
            var filteredArticles = Articles
                .Where(p => p.nom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                            p.description.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
            ArticleGrid.ItemsSource = filteredArticles;
        }
    }
}