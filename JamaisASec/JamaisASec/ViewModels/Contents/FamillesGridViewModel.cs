using System.Collections.ObjectModel;
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

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            AddCommand = new RelayCommand<object>(_ => Add());
            EditCommand = new RelayCommand<Famille>(async (famille) => await Edit(famille));
            DeleteSelectedCommand = new RelayCommand<object>(_ => DeleteSelected());
            DeleteCommand = new RelayCommand<Famille>(Delete);

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

        private void Add()
        {
            var famille = new Famille("Nouvelle famille");
            famille.id = _allFamilles.Count + 1;
            _allFamilles.Add(famille);
            Filter();
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
        private void DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les familles sélectionnées ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedFamilles = Familles.Where(a => a.IsSelected).ToList();
                foreach (var article in selectedFamilles)
                {
                    _allFamilles.Remove(article);
                }
                Filter();
            }
        }

        private void Delete(Famille famille)
        {
            _allFamilles.Remove(famille);
            Filter();
        }
    }
}
