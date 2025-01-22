using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JamaisASec.Models;
using System.Windows.Input;
using JamaisASec.Services;

namespace JamaisASec.ViewModels
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
            var (commandes, _) = await _commandeService.GetCommandesAndAchatsAsync();
            Commandes.Clear();
            foreach (var commande in commandes)
            {
                Commandes.Add(commande);
            }
        }
    }
}
