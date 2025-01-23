using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{
    class PageFournisseursViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Fournisseur> _allFournisseurs;
        public ObservableCollection<Fournisseur> Fournisseurs { get; set; }
        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand DeleteCommand { get; }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
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
                if (SetProperty(ref _isHeaderCheckBoxChecked, value, nameof(IsHeaderCheckBoxChecked)))
                {
                    foreach (var fournisseur in Fournisseurs)
                    {
                        fournisseur.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }

        public PageFournisseursViewModel()
        {
            _allFournisseurs = new ObservableCollection<Fournisseur>();
            Fournisseurs = new ObservableCollection<Fournisseur>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);

            AddCommand = new RelayCommand<object>(Add);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            DeleteCommand = new RelayCommand<Fournisseur>(Delete);
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

        private void Add(object obj)
        {
            var fournisseur = new Fournisseur();
            fournisseur.id = _allFournisseurs.Count + 1;
            fournisseur.nom = "Nouveau fournisseur";
            _allFournisseurs.Add(fournisseur);
            Filter();
        }
        private void DeleteSelected(object obj)
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
        }

        private void Delete(Fournisseur fournisseur)
        {
            _allFournisseurs.Remove(fournisseur);
            Filter();
        }
    }
}
