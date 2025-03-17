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

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            AddCommand = new RelayCommand<object>(_ => Add());
            EditCommand = new RelayCommand<Maison>(async (maison) => await Edit(maison));
            DeleteSelectedCommand = new RelayCommand<object>(_ => DeleteSelected());
            DeleteCommand = new RelayCommand<Maison>(Delete);

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

        private void Add()
        {
            var maison = new Maison("Nouvelle maison");
            _allMaisons.Add(maison);
            Filter();
        }

        private async Task Edit(Maison maison)
        {
            if (maison == null) return;

            await _dataService.UpdateMaisonAsync(maison);
        }

        private void DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les maisons sélectionnés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedMaisons = Maisons.Where(a => a.IsSelected).ToList();
                foreach (var maison in selectedMaisons)
                {
                    _allMaisons.Remove(maison);
                }
                Filter();
            }
        }

        private void Delete(Maison maison)
        {
            _allMaisons.Remove(maison);
            Filter();
        }
    }
}
