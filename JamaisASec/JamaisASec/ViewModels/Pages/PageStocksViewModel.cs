using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{
    class PageStocksViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Article> _allStocks = [];
        public ObservableCollection<Article> Stocks { get; } = [];
        
        public ICommand LoadDataCommand { get; }
        public ICommand IncrementStockCommand { get; }
        public ICommand DecrementStockCommand { get; }
        public ICommand IncrementStockMinCommand { get; }
        public ICommand DecrementStockMinCommand { get; }
        public ICommand IncrementColisageCommand { get; }
        public ICommand DecrementColisageCommand { get; }


        public PageStocksViewModel()
        {
            OnSearchTextChanged = _ => Filter();
            EventBus.Subscribe("ArticleUpdated", OnArticleUpdated);

            IncrementStockCommand = new RelayCommandAsync<Article>(async article =>
            {
                article.quantite++;
                await _dataService.UpdateArticleAsync(article); 
            });

            DecrementStockCommand = new RelayCommandAsync<Article>(async article =>
            {
                if (article.quantite > 0)
                {
                    article.quantite--;
                    await _dataService.UpdateArticleAsync(article);
                }
            });

            IncrementStockMinCommand = new RelayCommandAsync<Article>(async article =>
            {
                article.quantite_Min++;
                await _dataService.UpdateArticleAsync(article);
            });

            DecrementStockMinCommand = new RelayCommandAsync<Article>(async article =>
            {
                if (article.quantite_Min > 0)
                {
                    article.quantite_Min--;
                    await _dataService.UpdateArticleAsync(article);
                }
            });

            IncrementColisageCommand = new RelayCommandAsync<Article>(async article =>
            {
                article.colisage++;
                await _dataService.UpdateArticleAsync(article);
            });

            DecrementColisageCommand = new RelayCommandAsync<Article>(async article =>
            {
                if (article.colisage > 0)
                {
                    article.colisage--;
                    await _dataService.UpdateArticleAsync(article);
                }
            });
            //LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            _ = LoadData();
        }

        private void OnArticleUpdated()
        {
            // Mettre à jour les propriétés liées
            _ = LoadData();
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
