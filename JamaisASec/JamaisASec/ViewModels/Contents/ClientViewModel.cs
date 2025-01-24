using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    public class ClientViewModel : BaseViewModel
    {
        public Client Client { get; }

        private ObservableCollection<Commande> _commandes = new();
        public ObservableCollection<Commande> Commandes
        {
            get => _commandes;
            set => SetProperty(ref _commandes, value, nameof(Commandes));
        }

        public ClientViewModel(Client client)
        {
            Client = client;
            LoadCommandesAsync();
        }

        private async void LoadCommandesAsync()
        {
            try
            {
                var commandes = await _apiService.GetClientsCommandesAsync(Client.id);
                Commandes = new ObservableCollection<Commande>(commandes);
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des commandes : {ex.Message}");
            }
        }
    }
}
