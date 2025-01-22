using System.Collections.ObjectModel;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{
    class PageAchatsViewModel : BaseViewModel
    {
        public ObservableCollection<Commande> Achats { get; set; }
        public ICommand LoadDataCommand { get; }

        public PageAchatsViewModel()
        {
            Achats = new ObservableCollection<Commande>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var (_, achats) = await _dataService.GetCommandesAndAchatsAsync();
            Achats.Clear();
            foreach (var achat in achats)
            {
                Achats.Add(achat);
            }
        }
    }
}
