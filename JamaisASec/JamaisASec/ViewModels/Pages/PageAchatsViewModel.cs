using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{
    class PageAchatsViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Commande> _allAchats;
        public ObservableCollection<Commande> Achats { get; set; }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value, nameof(SearchText)))
                {
                    Filter();
                }
            }
        }
        private bool _isHeaderCheckBoxChecked;
        public bool IsHeaderCheckBoxChecked
        {
            get => _isHeaderCheckBoxChecked;
            set
            {
                if (SetProperty(ref _isHeaderCheckBoxChecked, value, nameof(IsHeaderCheckBoxChecked)))
                {
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
            _allAchats = new ObservableCollection<Commande>();
            Achats = new ObservableCollection<Commande>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var (_, achats) = await _dataService.GetCommandesAndAchatsAsync();
            _allAchats.Clear();
            foreach (var achat in achats)
            {
                _allAchats.Add(achat);
            }
            Filter();
        }

        private void Filter()
        {
            var filtered = _allAchats
                .Where(m => m.reference.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ||
                            m.fournisseur.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                .ToList();
            Achats.Clear();
            foreach (var achat in filtered)
            {
                Achats.Add(achat);
            }
        }
    }
}
