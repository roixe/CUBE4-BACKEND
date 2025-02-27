using System.Collections.ObjectModel;
using JamaisASec.Models;

namespace JamaisASec.Services
{
    public class DataService
    {
        private static DataService _instance;
        private readonly IApiService _apiService;

        // Cache pour chaque type de données
        private CacheService<Article> _articleCache;
        private CacheService<Client> _clientCache;
        private CacheService<Fournisseur> _fournisseurCache;
        private CacheService<Commande> _commandeCache;
        private CacheService<Maison> _maisonCache;
        private CacheService<Famille> _familleCache;

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

        public static void Initialize(IApiService apiService)
        {
            if (_instance == null)
            {
                _instance = new DataService(apiService);
            }
        }

        public DataService(IApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));

            // Initialisation des services de cache
            _articleCache = new CacheService<Article>(async () => await _apiService.GetArticlesAsync());
            _clientCache = new CacheService<Client>(async () => await _apiService.GetClientsAsync());
            _fournisseurCache = new CacheService<Fournisseur>(async () => await _apiService.GetFournisseursAsync());
            _commandeCache = new CacheService<Commande>(async () => await _apiService.GetCommandesAsync());
            _maisonCache = new CacheService<Maison>(async () => await _apiService.GetMaisonsAsync());
            _familleCache = new CacheService<Famille>(async () => await _apiService.GetFamillesAsync());

            // Abonnement aux événements de mise à jour de cache via EventBus
            _articleCache.RefreshOnEvent("ArticleUpdated");
            _clientCache.RefreshOnEvent("ClientUpdated");
            _fournisseurCache.RefreshOnEvent("FournisseurUpdated");
            _commandeCache.RefreshOnEvent("CommandeUpdated");
            _maisonCache.RefreshOnEvent("MaisonUpdated");
            _familleCache.RefreshOnEvent("FamilleUpdated");
        }

        #region Getters
        public Task<List<Article>> GetArticlesAsync() => _articleCache.GetAsync();

        public Task<List<Client>> GetClientsAsync() => _clientCache.GetAsync();

        public Task<List<Fournisseur>> GetFournisseursAsync() => _fournisseurCache.GetAsync();

        public Task<List<Maison>> GetMaisonsAsync() => _maisonCache.GetAsync();

        public Task<List<Famille>> GetFamillesAsync() => _familleCache.GetAsync();

        public async Task<List<Article>> GetArticlesByFournisseurAsync(int id)
        {
            var articles = await _articleCache.GetAsync();
            return articles.Where(a => a.fournisseur?.id == id).ToList();
        }

        /// <summary>
        /// Retourne une séparation des commandes en Commandes (clients) et Achats (fournisseurs).
        /// </summary>
        public async Task<(ObservableCollection<Commande> Commandes, ObservableCollection<Commande> Achats)> GetCommandesAndAchatsAsync()
        {
            // Récupérer les commandes depuis le cache
            var commandes = await _commandeCache.GetAsync();

            // Séparer les commandes liées aux clients et aux fournisseurs
            var commandesClients = new ObservableCollection<Commande>(
                commandes.Where(c => c.client != null));

            var commandesFournisseurs = new ObservableCollection<Commande>(
                commandes.Where(c => c.fournisseur != null));

            return (commandesClients, commandesFournisseurs);
        }
        #endregion

        #region Creaters
        public async Task CreateArticleAsync(Article article)
        {
            if (article == null) return;

            var success = await _apiService.CreateArticleAsync(article);
            if (success)
            {
                EventBus.Publish("ArticleUpdated"); // Publier un événement pour mettre à jour le cache
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
                EventBus.Publish("ArticleUpdated");
            }
        }

        public async Task UpdateMaisonAsync(Maison maison)
        {
            if (maison == null) return;

            var success = await _apiService.UpdateMaisonAsync(maison);
            if (success)
            {
                EventBus.Publish("MaisonUpdated"); // Publier un événement pour mettre à jour le cache
                EventBus.Publish("ArticleUpdated"); 
            }
        }

        public async Task<bool> UpdateFamilleAsync(Famille famille)
        {
            if (famille == null) return false;

            var success = await _apiService.UpdateFamilleAsync(famille);

            // Si l'appel est un succès, on met à jour le cache
            if (success)
            {
                EventBus.Publish("FamilleUpdated"); // Publier un événement pour mettre à jour le cache
                EventBus.Publish("ArticleUpdated");
            }

            return success;
        }

        public async Task UpdateClientAsync(Client client)
        {
            if (client == null) return;

            //var success = await _apiService.UpdateClientAsync(client);
            var success = true;
            if (success)
            {
                EventBus.Publish("ClientUpdated"); // Publier un événement pour mettre à jour le cache
            }
        }

        public async Task UpdateFournisseurAsync(Fournisseur fournisseur)
        {
            if (fournisseur == null) return;
            var success = true;

            //var success = await _apiService.UpdateFournisseurAsync(fournisseur);
            if (success)
            {
                EventBus.Publish("FournisseurUpdated"); // Publier un événement pour mettre à jour le cache
            }
        }

        public async Task UpdateCommandeAsync(Commande commande)
        {
            if (commande == null) return;
            var success = true;

            //var success = await _apiService.UpdateCommandeAsync(commande);
            if (success)
            {
                EventBus.Publish("CommandeUpdated"); // Publier un événement pour mettre à jour le cache
            }
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
                EventBus.Publish("CommandeUpdated");
            }
            return success;
        }
        #endregion

        #region Deleters
        public async Task<bool> DeleteArticleAsync(int id)
        {
            var success = await _apiService.DeleteArticleAsync(id);
            if (success)
            {
                EventBus.Publish("ArticleUpdated"); // Publier un événement pour mettre à jour le cache
            }
            return success;
        }

        public async Task<bool> DeleteArticleCommandeAsync(int id)
        {
            return await _apiService.DeleteArticleCommandeAsync(id);
        }
        #endregion
    }
}