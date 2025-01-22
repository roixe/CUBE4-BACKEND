using System.Collections.ObjectModel;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Tab
{
    class ArticlesTabViewModel : BaseViewModel
    {
        public ObservableCollection<Article> Articles { get; }
        public ICommand LoadDataCommand { get; }

        public ArticlesTabViewModel()
        {
            Articles = new ObservableCollection<Article>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var articles = await _dataService.GetArticlesAsync();
            Articles.Clear();
            foreach (var article in articles)
            {
                Articles.Add(article);
            }
        }
    }
}
