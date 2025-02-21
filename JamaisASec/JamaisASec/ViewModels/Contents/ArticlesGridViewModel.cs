using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;
using JamaisASec.ViewModels.Modals;
using JamaisASec.Views.Modals;

namespace JamaisASec.ViewModels.Contents
{
    class ArticlesGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Article> _allArticles = [];
        public ObservableCollection<Article> Articles { get; } = [];
        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand DeleteCommand { get; }

        public ArticlesGridViewModel()
        {

            // Liaison du filtrage
            OnSearchTextChanged = _ => Filter();

            // Liaison de la sélection globale
            OnHeaderCheckBoxChanged = isChecked =>
            {
                foreach (var article in Articles)
                {
                    article.IsSelected = isChecked;
                }
            };

            _dataService.ArticlesUpdated += OnArticlesUpdated;

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            AddCommand = new RelayCommand<object>(_ => Add());
            EditCommand = new RelayCommand<Article>(Edit);
            DeleteSelectedCommand = new RelayCommand<object>(_ => DeleteSelected());
            DeleteCommand = new RelayCommand<Article>(Delete);

            _ = LoadData();
        }

        private void OnArticlesUpdated(object? sender, EventArgs e)
        {
            // Mettre à jour les propriétés liées
            _ = LoadData();
        }

        private async Task LoadData()
        {
            var articles = await _dataService.GetArticlesAsync();
            _allArticles.Clear();
            foreach (var article in articles)
            {
                _allArticles.Add(article);
            }
            Filter();
        }
        private void Filter()
        {
            var filtered = _allArticles
                .Where(m => m.nom != null && m.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase)).ToList();

            Articles.Clear();
            foreach (var article in filtered)
            {
                Articles.Add(article);
            }
        }

        private async void Add()
        {
            var modal = new ArticleModal();
            var article = new Article();
            var modalVM = new ArticleModalViewModel(article, modal);
            modal.DataContext = modalVM;
            var result = modal.ShowDialog();
            if (result == true)
            {
                await _dataService.CreateArticleAsync(modalVM.Article);
                LoadDataCommand.Execute(null);
            }
        }

        private async void Edit(Article article)
        {
            var modal = new ArticleModal();
            var modalVM = new ArticleModalViewModel(article, modal);
            modal.DataContext = modalVM;
            var result = modal.ShowDialog();
            if (result == true)
            {
                await _dataService.UpdateArticleAsync(modalVM.Article);
                LoadDataCommand.Execute(null);
            }
        }
        private void DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les articles sélectionnés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedArticles = _allArticles.Where(a => a.IsSelected).ToList();
                foreach (var article in selectedArticles)
                {
                    _allArticles.Remove(article);
                }
                Filter();
            }
        }

        private void Delete(Article article)
        {
            _allArticles.Remove(article);
            Filter();
        }
    }
}
