using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{
    class PageAchatsViewModel : BaseViewModel
    {
        public ObservableCollection<Commande> Achats { get; set; }
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
                    foreach (var achat in Achats)
                    {
                        achat.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }

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
