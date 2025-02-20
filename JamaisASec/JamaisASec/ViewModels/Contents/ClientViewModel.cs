using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    public class ClientViewModel : BaseViewModel
    {
        private Client _clientTemp;
        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (SetProperty(ref _isEditMode, value, nameof(IsEditMode)))
                {
                    if (_isEditMode)
                    {
                        // Copier les données du client dans ClientTemp pour l'édition
                        ClientTemp = new Client
                        {
                            id = Client.id,
                            nom = Client.nom,
                            adresse = Client.adresse,
                            mail = Client.mail,
                            telephone = Client.telephone
                        };
                    }
                }
            }
        }

        public Client Client { get; } // Client original, non modifié
        public Client ClientTemp
        {
            get => _clientTemp;
            set => SetProperty(ref _clientTemp, value, nameof(ClientTemp));
        }
        
        private ObservableCollection<Commande> _commandes = new();
        public ObservableCollection<Commande> Commandes
        {
            get => _commandes;
            set => SetProperty(ref _commandes, value, nameof(Commandes));
        }

        public ICommand NavigateCommand { get; }
        public ICommand SaveCommand { get; }

        public ClientViewModel(Client client, ICommand navigateCommand, bool isEditMode = false)
        {
            Client = client;
            ClientTemp = new Client(); // Valeur par défaut avant l'édition
            IsEditMode = isEditMode;
            NavigateCommand = navigateCommand;
            if (!IsEditMode)
            {
                LoadCommandesAsync();
            }
            SaveCommand = new RelayCommand<object>(_ => Save());
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
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des commandes : {ex.Message}");
            }
        }

        private async void Save()
        {
            if (!IsEditMode) return;

            try
            {
                // Mise à jour du client avec les nouvelles valeurs
                Client.nom = ClientTemp.nom;
                Client.adresse = ClientTemp.adresse;
                Client.mail = ClientTemp.mail;
                Client.telephone = ClientTemp.telephone;

                // Envoyer les modifications au serveur
                await _dataService.UpdateClientAsync(Client);

                NavigateCommand?.Execute((Client, false));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la sauvegarde : {ex.Message}");
            }
        }

    }
}
