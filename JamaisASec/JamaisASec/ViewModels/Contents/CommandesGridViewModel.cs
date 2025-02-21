using System.Windows.Input;
using JamaisASec.Services;
using JamaisASec.Models;
using System.Collections.ObjectModel;

namespace JamaisASec.ViewModels.Contents
{
    class CommandesGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Commande> _allCommandes;
        public ObservableCollection<Commande> Commandes { get; set; }
        
        private string? _searchText;
        public string SearchText
        {
            get => _searchText ?? String.Empty;
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

        public ICommand LoadDataCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand RowDoubleClickCommand { get; }
        public CommandesGridViewModel(ICommand navigateCommand)
        {
            _allCommandes = new ObservableCollection<Commande>();
            Commandes = new ObservableCollection<Commande>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);

            _dataService.CommandesUpdated += OnCommandesUpdated;

            NavigateCommand = navigateCommand;

            RowDoubleClickCommand = new RelayCommand<Commande>(commande =>
            {
                if (commande != null)
                {
                    NavigateCommand.Execute(commande);
                }
            });
        }

        private void OnCommandesUpdated(object? sender, EventArgs e)
        {
            _ = LoadData();
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
                .Where(m => (m.reference?.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (m.client?.nom?.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();
            Commandes.Clear();
            foreach (var commande in filtered)
            {
                Commandes.Add(commande);
            }
        }

    }
}
