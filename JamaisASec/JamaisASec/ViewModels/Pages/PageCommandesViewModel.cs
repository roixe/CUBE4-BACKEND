using System.Collections.ObjectModel;
using JamaisASec.Models;
using System.Windows.Input;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{

    class PageCommandesViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Commande> _allCommandes;
        public ObservableCollection<Commande> Commandes { get; set; }
        public ICommand LoadDataCommand { get; }
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
            _allCommandes = new ObservableCollection<Commande>();
            Commandes = new ObservableCollection<Commande>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var (commandes, _) = await _dataService.GetCommandesAndAchatsAsync();
            _allCommandes.Clear();
            foreach (var commande in commandes)
            {
                _allCommandes.Add(commande);
            }
            Filter();
        }

        private void Filter()
        {
            var filtered = _allCommandes
                .Where(m => m.reference.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ||
                       m.client.nom.Contains(SearchText?? string.Empty, StringComparison.OrdinalIgnoreCase))
                .ToList();
            Commandes.Clear();
            foreach (var commande in filtered)
            {
                Commandes.Add(commande);
            }
        }
    }
}
