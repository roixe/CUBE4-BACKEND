using System.Collections.ObjectModel;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Tab
{
    class MaisonsTabViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Maison> _allMaisons;
        public ObservableCollection<Maison> Maisons { get; set; }
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
                    foreach (var maison in Maisons)
                    {
                        maison.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }


        public MaisonsTabViewModel()
        {
            _allMaisons = new ObservableCollection<Maison>();
            Maisons = new ObservableCollection<Maison>(_allMaisons);
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
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
    }
}
