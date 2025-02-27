using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    public class FournisseursGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Fournisseur> _allFournisseurs = [];
        public ObservableCollection<Fournisseur> Fournisseurs { get; } = [];

        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand DeleteCommand { get; }

        public FournisseursGridViewModel()
        {
            // Liaison du filtrage
            OnSearchTextChanged = _ => Filter();

            // Liaison de la sélection globale
            OnHeaderCheckBoxChanged = isChecked =>
            {
                foreach (var fournisseur in Fournisseurs)
                {
                    fournisseur.IsSelected = isChecked;
                }
            };

            EventBus.Subscribe("FournisseurUpdated", OnFournisseurUpdated);

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            AddCommand = new RelayCommand<object>(_ => Add());
            DeleteSelectedCommand = new RelayCommand<object>(_ => DeleteSelected());
            DeleteCommand = new RelayCommand<Fournisseur>(Delete);

            _ = LoadData();
        }
        
        private void OnFournisseurUpdated()
        {
            _ = LoadData();
        }
        private async Task LoadData()
        {
            var fournisseurs = await _dataService.GetFournisseursAsync();
            _allFournisseurs.Clear();
            foreach (var fournisseur in fournisseurs)
            {
                _allFournisseurs.Add(fournisseur);
            }
            Filter();
        }
        private void Filter()
        {
            var filtered = _allFournisseurs
                .Where(m => m.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                .ToList();
            Fournisseurs.Clear();
            foreach (var fournisseur in filtered)
            {
                Fournisseurs.Add(fournisseur);
            }
        }

        private void Add()
        {
            var fournisseur = new Fournisseur();
            fournisseur.id = _allFournisseurs.Count + 1;
            fournisseur.nom = "Nouveau fournisseur";
            _allFournisseurs.Add(fournisseur);
            Filter();
        }
        private void DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les fournisseurs sélectionnés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedFournisseurs = _allFournisseurs.Where(a => a.IsSelected).ToList();
                foreach (var fournisseur in selectedFournisseurs)
                {
                    _allFournisseurs.Remove(fournisseur);
                }
                Filter();
            }
            IsHeaderCheckBoxChecked = false;

        }

        private void Delete(Fournisseur fournisseur)
        {
            _allFournisseurs.Remove(fournisseur);
            Filter();
        }
    }
}
