using System.Collections.ObjectModel;
using JamaisASec.Models;
using System.Windows.Input;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{

    class PageCommandesViewModel : BaseViewModel
    {
        public ObservableCollection<Commande> Commandes { get; set; }
        public ICommand LoadDataCommand { get; }

        public PageCommandesViewModel()
        {
            Commandes = new ObservableCollection<Commande>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var (commandes, _) = await _dataService.GetCommandesAndAchatsAsync();
            Commandes.Clear();
            foreach (var commande in commandes)
            {
                Commandes.Add(commande);
            }
        }
    }
}
