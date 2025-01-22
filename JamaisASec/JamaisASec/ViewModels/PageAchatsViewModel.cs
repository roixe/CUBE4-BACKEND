using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels
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
