using System.Collections.ObjectModel;
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
    }
}
