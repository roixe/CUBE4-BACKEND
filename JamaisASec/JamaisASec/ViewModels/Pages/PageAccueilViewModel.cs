using System.Collections.ObjectModel;
using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;

namespace JamaisASec.ViewModels.Pages
{
    public class PageAccueilViewModel : BaseViewModel
    {
        public ObservableCollection<Article> Articles { get; set; }
        public ObservableCollection<Commande> Commandes { get; set; }
        public ObservableCollection<Commande> Achats { get; set; }

        public ICommand LoadDataCommand { get; }

        public PageAccueilViewModel()
        {
            Articles = [];
            Commandes = [];
            Achats = [];

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);

        }

        private async Task LoadData()
        {
            //Charger les articles
            var articles = await _dataService.GetArticlesAsync();
            Articles.Clear();
            foreach (var article in articles)
            {
                Articles.Add(article);
            }

            // Charger les commandes et les achats
            var (commandes, achats) = await _commandeService.GetCommandesAndAchatsAsync();
            Commandes.Clear();
            foreach (var commande in commandes)
            {
                if (commande?.status?.Equals("en cours", StringComparison.CurrentCultureIgnoreCase) == true)
                {
                    Commandes.Add(commande);
                }
            }

            Achats.Clear();
            foreach (var achat in achats)
            {
                if (achat?.status?.Equals("en attente", StringComparison.CurrentCultureIgnoreCase) == true)
                {
                    Achats.Add(achat);
                }
            }
        }
    }
}
