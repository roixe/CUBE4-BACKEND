using System.Collections.ObjectModel;
using JamaisASec.Models;

namespace JamaisASec.Services
{
    /// <summary>
    /// Service de données, qui gère la récupération des données depuis l'API, et les mets en cache.
    /// </summary>
    public class DataService
    {
        private static DataService _instance;
        
        private readonly IApiService _apiService;

        // Cache des données
        private List<Article>? _cachedArticles;
        private List<Client>? _cachedClients;
        private List<Fournisseur>? _cachedFournisseurs;
        private List<Commande>? _cachedCommandes;
        private List<Maison>? _cachedMaisons;
        private List<Famille>? _cachedFamilles;
        private object articleDTO;

        public event EventHandler<EventArgs> ArticlesUpdated;
        public event EventHandler<EventArgs> CommandesUpdated;
        public event EventHandler<EventArgs> ClientsUpdated;
        public event EventHandler<EventArgs> FournisseursUpdated;

        public DataService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public static DataService Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("DataService instance has not been initialized.");
                }
                return _instance;
            }
        }
        public static void Initialize(ApiService apiService)
        {
            if (_instance == null)
            {
                _instance = new DataService(apiService);
            }
        }

        #region Getters
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

        public async Task<List<Article>> GetArticlesByFournisseurAsync(int id)
        {
            if (_cachedArticles == null) await GetArticlesAsync();
 
            return _cachedArticles.Where(a => a.fournisseur?.id == id).ToList();
            
        }

        #endregion

        #region Creaters
        public async Task CreateArticleAsync(Article Article)
        {
            if (Article == null) return; 
            
            // Appel à l'API pour ajouter l'article
            var success = await _apiService.CreateArticleAsync(Article);
            // Vérification si l'article est bien ajouté dans la réponse
            if (success)
            {
                //Mettre à jour le cache
                if (_cachedArticles != null)
                {
                    _cachedArticles.Add(Article);
                }
                ArticlesUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task CreateArticleCommandeAsync(ArticlesCommandes articleCommande, int commande_id)
        {
            await _apiService.CreateArticleCommandeAsync(articleCommande, commande_id);
        }

        #endregion

        #region Updaters
        public async Task UpdateArticleAsync(Article article)
        {
            // Appel à l'API pour mettre à jour l'article
            //var updatedArticle = await _apiService.UpdateArticleAsync(article);

            // Vérification si l'article est bien mis à jour dans la réponse
            if (article != null)
            {
                //Mettre à jour le cache
                //_cachedArticles = await _apiService.GetArticlesAsync();
                if (_cachedArticles != null)
                {
                    var index = _cachedArticles.FindIndex(a => a.id == article.id);
                    if (index != -1)
                    {
                        _cachedArticles[index] = article;
                    }
                }
                ArticlesUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task<bool> UpdateMaisonAsync(Maison maison)
        {
            if (maison == null) return false;

            var success = await _apiService.UpdateMaisonAsync(maison);

            // Si l'appel est un succès, on met à jour le cache
            if (success)
            {
                if (_cachedMaisons != null)
                {
                    var index = _cachedMaisons.FindIndex(m => m.id == maison.id);
                    if (index != -1)
                    {
                        _cachedMaisons[index] = maison;
                    }
                }
                if (_cachedArticles != null)
                {
                    foreach (var article in _cachedArticles)
                    {
                        if (article.maison?.id == maison.id)
                        {
                            article.maison = maison;
                        }
                    }
                }
                ArticlesUpdated?.Invoke(this, EventArgs.Empty);
            }

            return success;
        }

        public async Task<bool> UpdateFamilleAsync(Famille famille)
        {
            if (famille == null) return false;

            var success = await _apiService.UpdateFamilleAsync(famille);

            // Si l'appel est un succès, on met à jour le cache
            if (success)
            {
                if (_cachedFamilles != null)
                {
                    var index = _cachedFamilles.FindIndex(m => m.id == famille.id);
                    if (index != -1)
                    {
                        _cachedFamilles[index] = famille;
                    }
                }
                if (_cachedArticles != null)
                {
                    foreach (var article in _cachedArticles)
                    {
                        if (article.famille?.id == famille.id)
                        {
                            article.famille = famille;
                        }

                    }
                }
                ArticlesUpdated?.Invoke(this, EventArgs.Empty);
            }

            return success;
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            if (client == null) return false;
            var success = true;
            //var success = await _apiService.UpdateClientAsync(client);
            // Si l'appel est un succès, on met à jour le cache
            if (success)
            {
                if (_cachedClients != null)
                {
                    var index = _cachedClients.FindIndex(c => c.id == client.id);
                    if (index != -1)
                    {
                        _cachedClients[index] = client;
                    }
                }
                if (_cachedCommandes != null)
                {
                    foreach (var commande in _cachedCommandes)
                    {
                        if (commande.client?.id == client.id)
                        {
                            commande.client = client;
                        }
                    }
                }
                ClientsUpdated?.Invoke(this, EventArgs.Empty);
                CommandesUpdated?.Invoke(this, EventArgs.Empty);
            }
            return success;
        }

        public async Task<bool> UpdateFournisseurAsync(Fournisseur fournisseur)
        {
            if (fournisseur == null) return false;
            var success = true;
            //var success = await _apiService.UpdateFournisseurAsync(fournisseur);
            // Si l'appel est un succès, on met à jour le cache
            if (success)
            {
                if (_cachedFournisseurs != null)
                {
                    var index = _cachedFournisseurs.FindIndex(f => f.id == fournisseur.id);
                    if (index != -1)
                    {
                        _cachedFournisseurs[index] = fournisseur;
                    }
                }
                if (_cachedCommandes != null)
                {
                    foreach (var commande in _cachedCommandes)
                    {
                        if (commande.fournisseur?.id == fournisseur.id)
                        {
                            commande.fournisseur = fournisseur;
                        }
                    }
                }
                FournisseursUpdated?.Invoke(this, EventArgs.Empty);
                CommandesUpdated?.Invoke(this, EventArgs.Empty);
            }
            return success;
        }

        public async Task<bool> UpdateCommandeAsync(Commande commande)
        {
            if (commande == null) return false;
            var success = true;
            //var success = await _apiService.UpdateCommandeAsync(commande);
            // Si l'appel est un succès, on met à jour le cache
            if (success)
            {
                if (_cachedCommandes != null)
                {
                    var index = _cachedCommandes.FindIndex(c => c.id == commande.id);
                    if (index != -1)
                    {
                        _cachedCommandes[index] = commande;
                    }
                }
                CommandesUpdated?.Invoke(this, EventArgs.Empty);
            }
            return success;
        }

        public async Task<bool> UpdateArticleCommandeAsync(ArticlesCommandes articleCommande)
        {
            if (articleCommande == null) return false;
            return await _apiService.UpdateArticleCommandeAsync(articleCommande);
        }

        public async Task<bool> UpdateStatusCommandeAsync(Commande commande)
        {
            if (commande == null) return false;
            var success = await _apiService.UpdateStatusCommandeAsync(commande);
            // Si l'appel est un succès, on met à jour le cache
            if (success)
            {
                if (_cachedCommandes != null)
                {
                    var index = _cachedCommandes.FindIndex(c => c.id == commande.id);
                    if (index != -1)
                    {
                        _cachedCommandes[index] = commande;
                    }
                }
                CommandesUpdated?.Invoke(this, EventArgs.Empty);
            }
            return success;
        }
        #endregion

        #region Deleters
        public async Task<bool> DeleteArticleCommandeAsync(int id)
        {
            return await _apiService.DeleteArticleCommandeAsync(id);
        }
        #endregion
    }
}