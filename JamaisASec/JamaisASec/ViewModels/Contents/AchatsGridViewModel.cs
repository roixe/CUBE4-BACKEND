using System.Collections.ObjectModel;
using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;

namespace JamaisASec.ViewModels.Contents
{
    class AchatsGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Commande> _allAchats = [];
        public ObservableCollection<Commande> Achats { get; } = [];
        
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

            _dataService.CommandesUpdated += OnAchatsUpdated;

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
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

        private void OnAchatsUpdated(object? sender, EventArgs e)
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
    }
}
