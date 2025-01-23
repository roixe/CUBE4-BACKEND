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

            AddCommand = new RelayCommand<object>(Add);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            DeleteCommand = new RelayCommand<Famille>(Delete);
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

        private void Add(object obj)
        {
            var famille = new Famille("Nouvelle famille");
            famille.id = _allFamilles.Count + 1;
            _allFamilles.Add(famille);
            Filter();
        }
        private void DeleteSelected(object obj)
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
