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
            AddCommand = new RelayCommandAsync<object>(_ => Add());
            DeleteSelectedCommand = new RelayCommandAsync<object>(_ => DeleteSelected());
            DeleteCommand = new RelayCommandAsync<Client>(Delete);

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

        private async Task Add()
        {
            var client = new Client
            {
                nom = "Nouveau client",
                adresse = "Nouvelle adresse",
                mail = "nouveau@example.com",
                telephone = "0123456789"
            };

            await _dataService.CreateClientAsync(client);
        }

        private async Task DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les clients sélectionnés ?", "Confirmation",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedClients = _allClients.Where(a => a.IsSelected).ToList();
                foreach (var client in selectedClients)
                {
                    await _dataService.DeleteClientAsync(client.id);
                }
                Filter();
            }
        }

        private async Task Delete(Client client)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + client.nom + " ?", "Confirmation",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _dataService.DeleteClientAsync(client.id);
            }
        }
    }
}
