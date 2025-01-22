using System.Collections.ObjectModel;
using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel.Design;

namespace JamaisASec.ViewModels
{
    public class PageAccueilViewModel : BaseViewModel
    {

        public ObservableCollection<Article> Articles { get; set; }
        public ObservableCollection<Commande> Commandes { get; set; }
        public ObservableCollection<Commande> Achats { get; set; }

        public ICommand LoadDataCommand { get; }

        public PageAccueilViewModel()
        {
            Articles = new ObservableCollection<Article>();
            Commandes = new ObservableCollection<Commande>();
            Achats = new ObservableCollection<Commande>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);

        }

        private async Task LoadData()
        {
            var articles = await _apiService.GetArticlesAsync();
            var commandes = await _apiService.GetCommandesAsync();
            

            Articles.Clear();
            foreach (var article in articles)
            {
                Articles.Add(article);
            }

            Commandes.Clear();
            Achats.Clear();
            foreach (var commande in commandes)
            {
                if (commande.fournisseur != null)
                    Achats.Add(commande);
                else if (commande.client != null)
                    Commandes.Add(commande);
            }
        }
    }
}
