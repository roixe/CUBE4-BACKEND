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
        private bool _isHeaderCheckBoxChecked;
        public bool IsHeaderCheckBoxChecked
        {
            get => _isHeaderCheckBoxChecked;
            set
            {
                if (_isHeaderCheckBoxChecked != value)
                {
                    _isHeaderCheckBoxChecked = value;
                    OnPropertyChanged(nameof(IsHeaderCheckBoxChecked));
                    foreach (var commande in Commandes)
                    {
                        commande.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }


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
