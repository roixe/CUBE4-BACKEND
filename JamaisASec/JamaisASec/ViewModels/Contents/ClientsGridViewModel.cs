using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ObservableCollection<Client> Clients { get; }

        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand DeleteCommand { get; }

        public ClientsGridViewModel()
        {
            _allClients = new ObservableCollection<Client>();
            Clients = new ObservableCollection<Client>();

            // Liaison du filtrage
            OnSearchTextChanged = _ => Filter();

            // Liaison de la sélection globale
            OnHeaderCheckBoxChanged = isChecked =>
            {
                foreach (var client in Clients)
                {
                    client.IsSelected = isChecked;
                }
            };

            EventBus.Subscribe("ClientUpdated", OnClientUpdated);

            // Initialisation des commandes
            //LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            AddCommand = new RelayCommand<object>(_ => Add());
            DeleteSelectedCommand = new RelayCommand<object>(_ => DeleteSelected());
            DeleteCommand = new RelayCommand<Client>(Delete);

            // Chargement des données initiales
            _ = LoadData();
        }

        private void OnClientUpdated()
        {
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
                .Where(m => !string.IsNullOrEmpty(m.nom) && m.nom.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Clients.Clear();
            foreach (var client in filtered)
            {
                Clients.Add(client);
            }
        }

        private void Add()
        {
            var client = new Client
            {
                id = _allClients.Count + 1,
                nom = "Nouveau client"
            };

            _allClients.Add(client);
            Filter();
        }

        private void DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les clients sélectionnés ?", "Confirmation",
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
