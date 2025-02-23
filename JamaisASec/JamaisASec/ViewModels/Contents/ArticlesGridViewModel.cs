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
        private readonly ObservableCollection<ArticleDTO> _allArticles;
        public ObservableCollection<ArticleDTO> Articles { get; }
        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand DeleteCommand { get; }
        private string? _searchText;
        public string SearchText
        {
            get => _searchText ?? string.Empty;
            set
            {
                if (SetProperty(ref _searchText, value, nameof(SearchText)))
                {
                    Filter();
                }
            }
        }
        private bool _isHeaderCheckBoxChecked;
        public bool IsHeaderCheckBoxChecked
        {
            get => _isHeaderCheckBoxChecked;
            set
            {
                if (SetProperty(ref _isHeaderCheckBoxChecked, value, nameof(IsHeaderCheckBoxChecked)))
                {
                    foreach (var article in Articles)
                    {
                        article.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }


        public ArticlesGridViewModel()
        {
            _allArticles = new ObservableCollection<ArticleDTO>();
            Articles = new ObservableCollection<ArticleDTO>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);

            _dataService.ArticlesUpdated += OnArticlesUpdated;

            AddCommand = new RelayCommand<object>(Add);
            EditCommand = new RelayCommand<ArticleDTO>(Edit);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            DeleteCommand = new RelayCommand<ArticleDTO>(Delete);
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

        private async void Add(object obj)
        {
            var modal = new ArticleModal();
            var article = new ArticleDTO();
            var modalVM = new ArticleModalViewModel(article, modal);
            modal.DataContext = modalVM;
            var result = modal.ShowDialog();
            if (result == true)
            {
                await _dataService.AddArticleAsync(modalVM.Article);
                LoadDataCommand.Execute(null);
            }
        }

        private async void Edit(ArticleDTO article)
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
        private void DeleteSelected(object obj)
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

        private void Delete(ArticleDTO article)
        {
            _allArticles.Remove(article);
            Filter();
        }
    }
}
