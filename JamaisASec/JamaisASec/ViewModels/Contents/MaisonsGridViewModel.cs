using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    class MaisonsGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Maison> _allMaisons;
        public ObservableCollection<Maison> Maisons { get; set; }
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
                    foreach (var maison in Maisons)
                    {
                        maison.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }

        public MaisonsGridViewModel()
        {
            _allMaisons = new ObservableCollection<Maison>();
            Maisons = new ObservableCollection<Maison>(_allMaisons);

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);

            AddCommand = new RelayCommand<object>(Add);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            DeleteCommand = new RelayCommand<Maison>(Delete);
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

        private void Add(object obj)
        {
            var maison = new Maison("Nouvelle maison");
            _allMaisons.Add(maison);
            Filter();
        }
        private void DeleteSelected(object obj)
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
