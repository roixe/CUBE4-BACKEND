using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JamaisASec.Models;

namespace JamaisASec.ViewModels.Contents
{
    public class FournisseurViewModel : BaseViewModel
    {
        public Fournisseur Fournisseur { get; }

        private ObservableCollection<Commande> _achats = new();
        public ObservableCollection<Commande> Achats
        {
            get => _achats;
            set => SetProperty(ref _achats, value, nameof(Achats));
        }

        public FournisseurViewModel(Fournisseur fournisseur)
        {
            Fournisseur = fournisseur;
            LoadCommandesAsync();
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
    }
}
