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

        public ICommand LoadDataCommand { get; }
        public ICommand NavigateCommand { get; }
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

            _dataService.CommandesUpdated += OnCommandesUpdated;

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            NavigateCommand = navigateCommand;

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

    }
}
