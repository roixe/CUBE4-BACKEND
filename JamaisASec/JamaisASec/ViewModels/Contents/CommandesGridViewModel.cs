using System.Windows.Input;
using JamaisASec.Services;
using JamaisASec.Models;
using System.Collections.ObjectModel;

namespace JamaisASec.ViewModels.Contents
{
    class CommandesGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Commande> _allCommandes = [];
        public ObservableCollection<Commande> Commandes { get; } = [];
        private StatusCommande _selectedStatus;
        public StatusCommande SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value, nameof(SelectedStatus));
        }

        public ObservableCollection<StatusCommande> Status { get; set; }

        public ICommand LoadDataCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditStatusCommand { get; }
        public ICommand RowDoubleClickCommand { get; }
        public CommandesGridViewModel(ICommand navigateCommand)
        {
            // Liaison du filtrage
            OnSearchTextChanged = _ => Filter();

            // Liaison de la sélection globale
            OnHeaderCheckBoxChanged = isChecked =>
            {
                foreach (var commande in Commandes)
                {
                    commande.IsSelected = isChecked;
                }
            };

            Status = new ObservableCollection<StatusCommande>(new[]
            {
                StatusCommande.EnCours,
                StatusCommande.Prete,
                StatusCommande.Livree,
                StatusCommande.Annulee
            });
            SelectedStatus = Status.FirstOrDefault();

            _dataService.CommandesUpdated += OnCommandesUpdated;

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            NavigateCommand = navigateCommand;
            AddCommand = new RelayCommand<object>(_ => Add());
            EditStatusCommand = new RelayCommand<object>(_ => EditStatus());

            RowDoubleClickCommand = new RelayCommand<Commande>(commande =>
            {
                if (commande != null)
                {
                    NavigateCommand.Execute(commande);
                }
            });

            _ = LoadData();
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

        private void Add()
        {
            return;
        }

        private async void EditStatus()
        {
            var selectedCommandes = Commandes.Where(c => c.IsSelected).ToList();
            if (selectedCommandes != null)
            {
                foreach(var commande in selectedCommandes)
                {
                    commande.status = SelectedStatus;
                    await _dataService.UpdateCommandeAsync(commande);
                    commande.IsSelected = false;
                }
            }
        }
    }
}
