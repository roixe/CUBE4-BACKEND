using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    class FamillesGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Famille> _allFamilles = [];
        public ObservableCollection<Famille> Familles { get; } = [];
        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand DeleteCommand { get; }
        
        public FamillesGridViewModel()
        {
            // Liaison du filtrage
            OnSearchTextChanged = _ => Filter();

            // Liaison de la sélection globale
            OnHeaderCheckBoxChanged = isChecked =>
            {
                foreach (var famille in Familles)
                {
                    famille.IsSelected = isChecked;
                }
            };

            EventBus.Subscribe("FamilleUpdated", OnFamilleUpdated);

            //LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            AddCommand = new RelayCommandAsync<object>(_ => Add());
            EditCommand = new RelayCommandAsync<Famille>(Edit);
            DeleteSelectedCommand = new RelayCommandAsync<object>(_ => DeleteSelected());
            DeleteCommand = new RelayCommandAsync<Famille>(Delete);

            _ = LoadData();
        }

        private void OnFamilleUpdated()
        {
            // Mettre à jour les propriétés liées
            _ = LoadData();
        }

        private async Task LoadData()
        {
            var familles = await _dataService.GetFamillesAsync();
            _allFamilles.Clear();
            foreach (var famille in familles)
            {
                _allFamilles.Add(famille);
            }
            Filter();
        }

        private void Filter()
        {
            var filtered = _allFamilles
                .Where(m => m.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                .ToList();
            Familles.Clear();
            foreach (var famille in filtered)
            {
                Familles.Add(famille);
            }
        }

        private async Task Add()
        {
            var famille = new Famille("Nouvelle famille");
            famille.id = _allFamilles.Count + 1;
            await _dataService.CreateFamilleAsync(famille);
        }

        private async Task Edit(Famille famille)
        {
            if (famille == null) return;

            bool success = await _dataService.UpdateFamilleAsync(famille);
            if (!success)
            {
                MessageBox.Show("Erreur lors de la sauvegarde.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les familles sélectionnées et tous les articles associés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedFamilles = Familles.Where(a => a.IsSelected).ToList();
                foreach (var article in selectedFamilles)
                {
                    await _dataService.DeleteFamilleAsync(article.id);
                }
            }
        }

        private async Task Delete(Famille famille)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + famille.nom + " et tous les articles associés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _dataService.DeleteFamilleAsync(famille.id);
            }
        }
    }
}
