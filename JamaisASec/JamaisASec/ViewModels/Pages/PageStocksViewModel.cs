using System.Collections.ObjectModel;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{
    class PageStocksViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Article> _allStocks;
        public ObservableCollection<Article> Stocks { get; set; }
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
        public ICommand LoadDataCommand { get; }

        public PageStocksViewModel()
        {
            _allStocks = new ObservableCollection<Article>();
            Stocks = new ObservableCollection<Article>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var stocks = await _dataService.GetArticlesAsync();
            _allStocks.Clear();
            foreach (var stock in stocks)
            {
                _allStocks.Add(stock);
            }
            Filter();
        }

        private void Filter()
        {
            var filtered = _allStocks
                    .Where(m => m.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            Stocks.Clear();
            foreach (var stock in filtered)
            {
                Stocks.Add(stock);
            }
        }
    }
}
