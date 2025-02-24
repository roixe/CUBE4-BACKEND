using System.Collections.ObjectModel;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Windows;
using JamaisASec.Models;
using Newtonsoft.Json;

namespace JamaisASec.Services
{
    /// <summary>
    /// Service de données, qui gère la récupération des données depuis l'API, et les mets en cache.
    /// </summary>
    public class DataService
    {
        private static DataService _instance;
        
        private readonly ApiService _apiService;

        // Cache des données
        private List<ArticleDTO>? _cachedArticles;
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

        /// <summary>
        /// Retourne les articles, avec mise en cache pour éviter les appels redondants.
        /// </summary>
        public async Task<List<ArticleDTO>> GetArticlesAsync()
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

        //public async Task AddArticleAsync(ArticleDTO Article)
        //{
        //    // Appel à l'API pour ajouter l'article
        //    //var addedArticle = await _apiService.AddArticleAsync(Article);
        //    // Vérification si l'article est bien ajouté dans la réponse
        //    if (Article != null)
        //    {
        //        //Mettre à jour le cache
        //        //_cachedArticles = await _apiService.GetArticlesAsync();
        //        if (_cachedArticles != null)
        //        {
        //            _cachedArticles.Add(Article);
        //        }
        //    }
        //}
        public async Task AddArticleAsync(ArticleDTO article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article), "L'article ne peut pas être null.");
            }

            try
            {
                // Récupérer l'URL de base depuis le fichier de configuration
                string apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"]; 

                // Créer l'URL complète pour appeler l'API
                string url = $"{apiBaseUrl}/Articles/create"; 

                
                var articleToSend = new ArticleDTO
                {
                    nom = article.nom,
                    description = article.description,
                    quantite = article.quantite,
                    quantite_Min = article.quantite_Min,
                    colisage = article.colisage,
                    prix_unitaire = article.prix_unitaire,
                    annee = article.annee,
                    famille = article.famille,
                    maison= article.maison,
                    fournisseur = article.fournisseur

                };

                // Sérialiser l'article en JSON
                string jsonContent = JsonConvert.SerializeObject(articleToSend);

                // Afficher le contenu de la requête dans un MessageBox
                MessageBox.Show($"Requête envoyée :\nURL: {url}\nDonnées JSON : {jsonContent}", "Vérification Requête", MessageBoxButton.OK, MessageBoxImage.Information);

                // Utiliser HttpClient pour effectuer la requête POST
                var httpClient = new HttpClient();
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Envoi de la requête POST à l'API
                var response = await httpClient.PostAsync(url, content);

                // Lire la réponse du serveur
                string responseContent = await response.Content.ReadAsStringAsync();

                // Afficher la réponse de l'API dans un MessageBox
                MessageBox.Show($"Réponse de l'API :\n{responseContent}", "Réponse API", MessageBoxButton.OK, MessageBoxImage.Information);

                // Vérification de la réponse de l'API
                if (response.IsSuccessStatusCode)
                {
                    // Si l'ajout est réussi, vous pouvez récupérer les données de l'article ajouté
                    var addedArticle = JsonConvert.DeserializeObject<ArticleDTO>(responseContent);

                    // Mettre à jour le cache si nécessaire
                    if (_cachedArticles != null)
                    {
                        _cachedArticles.Add(addedArticle);
                    }
                }
                else
                {
                    // Si la réponse n'est pas un succès, vous pouvez gérer l'erreur
                    // Afficher plus de détails dans le MessageBox
                    MessageBox.Show($"Erreur lors de l'ajout de l'article : {response.StatusCode} - {responseContent}", "Erreur lors de l'ajout", MessageBoxButton.OK, MessageBoxImage.Error);

                    // Vous pouvez également lever une exception si vous souhaitez gérer cela plus en profondeur
                    throw new Exception($"Erreur lors de l'ajout de l'article : {response.StatusCode} - {responseContent}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs (ex. réseau, format JSON incorrect, etc.)
                MessageBox.Show($"Une erreur s'est produite lors de l'ajout de l'article : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if(_cachedCommandes != null)
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

        internal async Task UpdateArticleAsync(ArticleDTO articleDTO)
        {
            throw new NotImplementedException();
        }
    }
}