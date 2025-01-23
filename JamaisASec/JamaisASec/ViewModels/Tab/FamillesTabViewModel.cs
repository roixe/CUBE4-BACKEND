using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Tab
{
    class FamillesTabViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Famille> _allFamilles;
        public ObservableCollection<Famille> Familles { get; }
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
                    foreach (var famille in Familles)
                    {
                        famille.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }

        public FamillesTabViewModel()
        {
            _allFamilles = new ObservableCollection<Famille>();
            Familles = new ObservableCollection<Famille>(_allFamilles);
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
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
    }
}
