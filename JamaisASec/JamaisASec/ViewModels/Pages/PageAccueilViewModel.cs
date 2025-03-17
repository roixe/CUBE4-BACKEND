using System.Collections.ObjectModel;
using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;

namespace JamaisASec.ViewModels.Pages
{
    public class PageAccueilViewModel : BaseViewModel
    {
        public ObservableCollection<Article> Articles { get; } = [];
        public ObservableCollection<Commande> Commandes { get; } = [];
        public ObservableCollection<Commande> Achats { get; } = [];

        public ICommand LoadDataCommand { get; }

        public PageAccueilViewModel()
        {

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            EventBus.Subscribe("CommandeUpdated", OnCommandeUpdated);
            EventBus.Subscribe("AchatUpdated", OnCommandeUpdated);

            _ = LoadData();
        }

        private void OnCommandeUpdated()
        {
            _ = LoadData();
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
                if (commande?.status == StatusCommande.EnCours)
                {
                    Commandes.Add(commande);
                }
            }

            Achats.Clear();
            foreach (var achat in achats)
            {
                if (achat?.status == StatusCommande.EnAttente)
                {
                    Achats.Add(achat);
                }
            }
        }
    }
}
