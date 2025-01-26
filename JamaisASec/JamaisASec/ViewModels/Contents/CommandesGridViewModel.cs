using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using JamaisASec.Services;
using JamaisASec.Models;
using JamaisASec.Views.Contents;
using System.Collections.ObjectModel;
using System.Windows;

namespace JamaisASec.ViewModels.Contents
{
    class CommandesGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Commande> _allCommandes;
        public ObservableCollection<Commande> Commandes { get; set; }
        public ICommand LoadDataCommand { get; }
        private string? _searchText;
        public string SearchText
        {
            get => _searchText ?? String.Empty;
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
                if (_isHeaderCheckBoxChecked != value)
                {
                    _isHeaderCheckBoxChecked = value;
                    OnPropertyChanged(nameof(IsHeaderCheckBoxChecked));
                    foreach (var commande in Commandes)
                    {
                        commande.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }

        public CommandesGridViewModel()
        {
            _allCommandes = new ObservableCollection<Commande>();
            Commandes = new ObservableCollection<Commande>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
            
        }

        private async Task LoadData()
        {
            var (commandes, _) = await _dataService.GetCommandesAndAchatsAsync();
            _allCommandes.Clear();
            foreach (var commande in commandes)
            {
                _allCommandes.Add(commande);
            }
            Filter();
        }

        private void Filter()
        {
            var filtered = _allCommandes
                .Where(m => (m.reference?.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (m.client?.nom?.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();
            Commandes.Clear();
            foreach (var commande in filtered)
            {
                Commandes.Add(commande);
            }
        }

    }
}
