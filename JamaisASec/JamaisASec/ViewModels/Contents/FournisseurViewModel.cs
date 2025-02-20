using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    public class FournisseurViewModel : BaseViewModel
    {
        
        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if(SetProperty(ref _isEditMode, value, nameof(IsEditMode)))
                {
                    if (_isEditMode)
                    {
                        // Copier les données du fournisseur dans FournisseurTemp pour l'édition
                        FournisseurTemp = new Fournisseur
                        {
                            id = Fournisseur.id,
                            nom = Fournisseur.nom,
                            adresse = Fournisseur.adresse,
                            mail = Fournisseur.mail,
                            telephone = Fournisseur.telephone,
                            siret = Fournisseur.siret
                        };
                    }
                }
            }
        }

        public Fournisseur Fournisseur { get; }
        private Fournisseur _fournisseurTemp;
        public Fournisseur FournisseurTemp
        {
            get => _fournisseurTemp;
            set => SetProperty(ref _fournisseurTemp, value, nameof(FournisseurTemp));
        }

        private ObservableCollection<Commande> _achats = new();
        public ObservableCollection<Commande> Achats
        {
            get => _achats;
            set => SetProperty(ref _achats, value, nameof(Achats));
        }

        public ICommand NavigateCommand { get; }
        public ICommand SaveCommand { get; }
        public FournisseurViewModel(Fournisseur fournisseur, ICommand navigateCommand, bool isEditMode = false)
        {
            Fournisseur = fournisseur;
            FournisseurTemp = new Fournisseur();
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
                var achats = await _apiService.GetFournisseursAchatsAsync(Fournisseur.id);
                Achats = new ObservableCollection<Commande>(achats);
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des achats : {ex.Message}");
            }
        }

        private async void Save()
        {
            if(!IsEditMode) return;
            try
            {
                // Mettre à jour du fournisseru avec les nouvelles valeurs
                Fournisseur.nom = FournisseurTemp.nom;
                Fournisseur.adresse = FournisseurTemp.adresse;
                Fournisseur.mail = FournisseurTemp.mail;
                Fournisseur.telephone = FournisseurTemp.telephone;
                Fournisseur.siret = FournisseurTemp.siret;

                // Envoyer les modifications au serveur
                await _dataService.UpdateFournisseurAsync(Fournisseur);

                NavigateCommand?.Execute((Fournisseur, false));

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la sauvegarde : {ex.Message}");
            }
        }
    }
}
