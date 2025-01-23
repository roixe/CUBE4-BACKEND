using System.Collections.ObjectModel;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Tab
{
    class ArticlesTabViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Article> _allArticles;
        public ObservableCollection<Article> Articles { get; }
        public ICommand LoadDataCommand { get; }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
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


        public ArticlesTabViewModel()
        {
            _allArticles = new ObservableCollection<Article>();
            Articles = new ObservableCollection<Article>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
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
                .Where(m => m.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase)).ToList();

            Articles.Clear();
            foreach (var article in filtered)
            {
                Articles.Add(article);
            }
        }
    }
}
