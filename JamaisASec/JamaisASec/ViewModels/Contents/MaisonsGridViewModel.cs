using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    class MaisonsGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Maison> _allMaisons = [];
        public ObservableCollection<Maison> Maisons { get; } = [];
        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand DeleteCommand { get; }

        public MaisonsGridViewModel()
        {
            // Liaison du filtrage
            OnSearchTextChanged = _ => Filter();

            // Liaison de la sélection globale
            OnHeaderCheckBoxChanged = isChecked =>
            {
                foreach (var maison in Maisons)
                {
                    maison.IsSelected = isChecked;
                }
            };

            EventBus.Subscribe("MaisonUpdated", OnMaisonUpdated);

            //LoadDataCommand = new RelayCommandAsync(async () => await LoadData());<
            AddCommand = new RelayCommandAsync<object>(_ => Add());
            EditCommand = new RelayCommandAsync<Maison>(Edit);
            DeleteSelectedCommand = new RelayCommandAsync<object>(_ => DeleteSelected());
            DeleteCommand = new RelayCommandAsync<Maison>(Delete);

            _ = LoadData();
        }

        private void OnMaisonUpdated()
        {
            // Mettre à jour les propriétés liées
            _ = LoadData();
        }

        private async Task LoadData()
        {
            var maisons = await _dataService.GetMaisonsAsync();
            _allMaisons.Clear();
            foreach (var maison in maisons)
            {
                _allMaisons.Add(maison);
            }
            Filter();
        }

        private void Filter()
        {
            var filtered = _allMaisons
                .Where(m => m.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Maisons.Clear();
            foreach (var maison in filtered)
            {
                Maisons.Add(maison);
            }
        }

        private async Task Add()
        {
            var maison = new Maison("Nouvelle maison");
            maison.id = _allMaisons.Count + 1;
            await _dataService.CreateMaisonAsync(maison);
        }

        private async Task Edit(Maison maison)
        {
            if (maison == null) return;

            await _dataService.UpdateMaisonAsync(maison);
        }

        private async Task DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les maisons sélectionnés et tous les articles associés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedMaisons = Maisons.Where(a => a.IsSelected).ToList();
                foreach (var maison in selectedMaisons)
                {
                    await _dataService.DeleteMaisonAsync(maison.id);
                }
            }
        }

        private async Task Delete(Maison maison)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + maison.nom + " et tous les articles associés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _dataService.DeleteMaisonAsync(maison.id);
            }
        }
    }
}
