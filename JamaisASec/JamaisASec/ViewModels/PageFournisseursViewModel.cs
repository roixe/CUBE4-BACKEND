using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels
{
    class PageFournisseursViewModel : BaseViewModel
    {
        public ObservableCollection<Fournisseur> Fournisseurs { get; set; }
        public ICommand LoadDataCommand { get; }

        public PageFournisseursViewModel()
        {
            Fournisseurs = new ObservableCollection<Fournisseur>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var fournisseurs = await _apiService.GetFournisseursAsync();
            Fournisseurs.Clear();
            foreach (var fournisseur in fournisseurs)
            {
                Fournisseurs.Add(fournisseur);
            }
        }
    }
}
