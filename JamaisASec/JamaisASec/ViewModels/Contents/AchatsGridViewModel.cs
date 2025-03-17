using System.Collections.ObjectModel;
using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;
using System.Data;
using System.Windows;

namespace JamaisASec.ViewModels.Contents
{
    class AchatsGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Commande> _allAchats = [];
        public ObservableCollection<Commande> Achats { get; } = [];
        private StatusCommande _selectedStatus;
        public StatusCommande SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value, nameof(SelectedStatus));
        }

        public ObservableCollection<StatusCommande> Status { get; set; }

        public ICommand EditStatusCommand { get; }

        public ICommand LoadDataCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand RowDoubleClickCommand { get; }

        public AchatsGridViewModel(ICommand navigateCommand)
        {
            // Liaison du filtrage
            OnSearchTextChanged = _ => Filter();

            // Liaison de la sélection globale
            OnHeaderCheckBoxChanged = isChecked =>
            {
                foreach (var achat in Achats)
                {
                    achat.IsSelected = isChecked;
                }
            };

            Status = new ObservableCollection<StatusCommande>(new[]
            {
                StatusCommande.EnAttente,
                StatusCommande.Receptionnee,
                StatusCommande.Annulee
            });
            SelectedStatus = Status.FirstOrDefault();

            EventBus.Subscribe("CommandeUpdated", OnAchatsUpdated);

            EditStatusCommand = new RelayCommand<object>(_ => EditStatus());
            //LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            NavigateCommand = navigateCommand;
            
            RowDoubleClickCommand = new RelayCommand<Commande>(achat =>
            {
                if (achat != null)
                {
                    NavigateCommand?.Execute(achat);
                }
            });

            _ = LoadData();
        }

        private void OnAchatsUpdated()
        {
            _ = LoadData();
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
                .Where(m => (m.reference?.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (m.fournisseur?.nom?.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();
            Achats.Clear();
            foreach (var achat in filtered)
            {
                Achats.Add(achat);
            }
        }

        private async void EditStatus()
        {
            var selectedAchats = _allAchats.Where(a => a.IsSelected).ToList();
            if (selectedAchats != null)
            {
                foreach (var achat in selectedAchats)
                {
                    achat.status = SelectedStatus;
                    await _dataService.UpdateStatusCommandeAsync(achat);
                }
            }

        }
    }
}
