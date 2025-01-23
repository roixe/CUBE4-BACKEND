using System.Collections.ObjectModel;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{
    class PageFournisseursViewModel : BaseViewModel
    {
        public ObservableCollection<Fournisseur> Fournisseurs { get; set; }
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
                    foreach (var fournisseur in Fournisseurs)
                    {
                        fournisseur.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }


        public PageFournisseursViewModel()
        {
            Fournisseurs = new ObservableCollection<Fournisseur>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var fournisseurs = await _dataService.GetFournisseursAsync();
            Fournisseurs.Clear();
            foreach (var fournisseur in fournisseurs)
            {
                Fournisseurs.Add(fournisseur);
            }
        }
    }
}
