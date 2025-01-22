using System.Collections.ObjectModel;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels
{
    class PageStocksViewModel : BaseViewModel
    {
        public ObservableCollection<Article> Stocks { get; set; }
        public ICommand LoadDataCommand { get; }

        public PageStocksViewModel()
        {
            Stocks = new ObservableCollection<Article>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var stocks = await _dataService.GetArticlesAsync();
            Stocks.Clear();
            foreach (var stock in stocks)
            {
                Stocks.Add(stock);
            }
        }
    }
}
