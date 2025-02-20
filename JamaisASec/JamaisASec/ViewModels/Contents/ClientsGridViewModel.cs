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

namespace JamaisASec.ViewModels.Contents
{
    public class ClientsGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Client> _allClients;
        public ObservableCollection<Client> Clients { get; set; }
        private string? _searchText;
        public string SearchText
        {
            get => _searchText ?? string.Empty;
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
                    foreach (var client in Clients)
                    {
                        client.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }
        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand DeleteCommand { get; }

        public ClientsGridViewModel()
        {
            _allClients = new ObservableCollection<Client>();
            Clients = new ObservableCollection<Client>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);

            _dataService.ClientsUpdated += OnClientsUpdated;

            AddCommand = new RelayCommand<object>(Add);
            EditCommand = new RelayCommand<Client>(Edit);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            DeleteCommand = new RelayCommand<Client>(Delete);
        }

        private void OnClientsUpdated(object? sender, EventArgs e)
        {
            // Mettre à jour les propriétés liées
            _ = LoadData();
        }

        private async Task LoadData()
        {
            var clients = await _dataService.GetClientsAsync();
            _allClients.Clear();
            foreach (var client in clients)
            {
                _allClients.Add(client);
            }
            Filter();
        }

        private void Filter()
        {
            var filtered = _allClients
                .Where(m => m.nom != null && m.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase)).ToList();
            Clients.Clear();
            foreach (var client in filtered)
            {
                Clients.Add(client);
            }
        }

        private void Add(object obj)
        {
            var client = new Client();
            client.id = _allClients.Count + 1;
            client.nom = "Nouveau client";
            _allClients.Add(client);
            Filter();
        }

        private void Edit(Client client)
        {
            MessageBox.Show("Édition du client " + client.nom);
        }

        private void DeleteSelected(object obj)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les clients sélectionnés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedClients = _allClients.Where(a => a.IsSelected).ToList();
                foreach (var client in selectedClients)
                {
                    _allClients.Remove(client);
                }
                Filter();
            }
        }

        private void Delete(Client client)
        {
            _allClients.Remove(client);
            Filter();
        }

    }
}
