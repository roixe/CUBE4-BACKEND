using System.Collections.ObjectModel;
using JamaisASec.Models;

namespace JamaisASec.Services
{
    /// <summary>
    /// Service de données, qui gère la récupération des données depuis l'API, et les mets en cache.
    /// </summary>
    public class DataService
    {
        private readonly ApiService _apiService;

        // Cache des données
        private List<Article> _cachedArticles;
        private List<Client> _cachedClients;
        private List<Fournisseur> _cachedFournisseurs;
        private List<Commande> _cachedCommandes;
        private List<Maison> _cachedMaisons;
        private List<Famille> _cachedFamilles;

        public DataService(ApiService apiService)
        {
            _apiService = apiService;
        }

        /// <summary>
        /// Retourne les articles, avec mise en cache pour éviter les appels redondants.
        /// </summary>
        public async Task<List<Article>> GetArticlesAsync()
        {
            if (_cachedArticles == null)
            {
                _cachedArticles = await _apiService.GetArticlesAsync();
            }
            return _cachedArticles;
        }

        /// <summary>
        /// Retourne les clients, avec mise en cache.
        /// </summary>
        public async Task<List<Client>> GetClientsAsync()
        {
            if (_cachedClients == null)
            {
                _cachedClients = await _apiService.GetClientsAsync();
            }
            return _cachedClients;
        }

        /// <summary>
        /// Retourne les fournisseurs, avec mise en cache.
        /// </summary>
        public async Task<List<Fournisseur>> GetFournisseursAsync()
        {
            if (_cachedFournisseurs == null)
            {
                _cachedFournisseurs = await _apiService.GetFournisseursAsync();
            }
            return _cachedFournisseurs;
        }

        /// <summary>
        /// Retourne toutes les commandes, avec mise en cache.
        /// </summary>
        public async Task<List<Commande>> GetCommandesAsync()
        {
            if (_cachedCommandes == null)
            {
                _cachedCommandes = await _apiService.GetCommandesAsync();
            }
            return _cachedCommandes;
        }

        /// <summary>
        /// Retourne une séparation des commandes en Commandes (clients) et Achats (fournisseurs).
        /// </summary>
        public async Task<(ObservableCollection<Commande> Commandes, ObservableCollection<Commande> Achats)> GetCommandesAndAchatsAsync()
        {
            var commandes = await GetCommandesAsync();

            var commandesClients = new ObservableCollection<Commande>(
                commandes.Where(c => c.client != null));

            var commandesFournisseurs = new ObservableCollection<Commande>(
                commandes.Where(c => c.fournisseur != null));

            return (commandesClients, commandesFournisseurs);
        }

        /// <summary>
        /// Force un rechargement des données (utile pour actualiser depuis l'API).
        /// </summary>
        public async Task RefreshDataAsync()
        {
            _cachedArticles = await _apiService.GetArticlesAsync();
            _cachedClients = await _apiService.GetClientsAsync();
            _cachedFournisseurs = await _apiService.GetFournisseursAsync();
            _cachedCommandes = await _apiService.GetCommandesAsync();
        }

        public async Task<List<Maison>> GetMaisonsAsync()
        {
            if (_cachedMaisons == null)
            {
                _cachedMaisons = await _apiService.GetMaisonsAsync();
            }
            return _cachedMaisons;
        }

        public async Task<List<Famille>> GetFamillesAsync()
        {
            if (_cachedFamilles == null)
            {
                _cachedFamilles = await _apiService.GetFamillesAsync();
            }
            return _cachedFamilles;
        }
    }
}