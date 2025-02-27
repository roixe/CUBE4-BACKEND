using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using JamaisASec.Models;

namespace JamaisASec.Services
{
    public class ApiService : IApiService
    {
        private static ApiService _instance;
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            var apiBaseUrl = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                throw new ArgumentNullException(nameof(apiBaseUrl), "ApiBaseUrl cannot be null or empty.");
            }
            _httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
        }
        public static ApiService Instance
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

        public static void Initialize()
        {
            if (_instance == null)
            {
                _instance = new ApiService();
            }
        }
        private async Task<T> RunWithLoadingCursor<T>(Func<Task<T>> apiCall)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                return await apiCall();
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        #region Getters
        public async Task<List<Article>> GetArticlesAsync()
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync("Articles/get/all");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ArticleDTO>>(content) ?? new List<ArticleDTO>();
            });
        }


        public async Task<List<Commande>> GetCommandesAsync()
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync("Commandes/get/all");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    var commandes = JsonSerializer.Deserialize<List<Commande>>(content, new JsonSerializerOptions
                    {
                        Converters = { new StatusCommandeConverter() } 
                    });
                    return commandes ?? new List<Commande>(); // Retourner une liste vide si désérialisation échoue
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs de désérialisation
                    System.Diagnostics.Debug.WriteLine($"Erreur de désérialisation : {ex.Message}");
                    return new List<Commande>();
                }
            });
        }

        public async Task<List<ArticlesCommandes>> GetArticlesCommandesById(int commandeId)
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync($"ArticlesCommandes/get/command/{commandeId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ArticlesCommandes>>(content) ?? new List<ArticlesCommandes>();
            });
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync("Clients/get/all");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Client>>(content) ?? new List<Client>();
            });
        }

        public async Task<List<Fournisseur>> GetFournisseursAsync()
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync("Fournisseurs/get/all");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Fournisseur>>(content) ?? new List<Fournisseur>();
            });
        }

        public async Task<List<Famille>> GetFamillesAsync()
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync("Familles/get/all");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Famille>>(content) ?? new List<Famille>();
            });
        }

        public async Task<List<Maison>> GetMaisonsAsync()
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync("Maisons/get/all");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Maison>>(content) ?? new List<Maison>();
            });
        }

        public async Task<List<Commande>> GetClientsCommandesAsync(int clientId)
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync($"Commandes/get/byClient/{clientId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Commande>>(content) ?? new List<Commande>();
            });
        }

        public async Task<List<Commande>> GetFournisseursAchatsAsync(int fournisseurId)
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.GetAsync($"Commandes/get/byFournisseur/{fournisseurId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Commande>>(content) ?? new List<Commande>();
            });
        }
        #endregion

        #region Creators
        public async Task<bool> CreateArticleCommandeAsync(ArticlesCommandes articleCommande, int commande_id)
        {
            var dto = new ArticlesCommandesDTO
            {
                ID = articleCommande.id,
                Commandes_ID = commande_id,
                Articles_ID = articleCommande.article.id,
                Quantite = articleCommande.quantite
            };
            return await RunWithLoadingCursor(async () =>
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("ArticlesCommandes/create", content);
                return response.IsSuccessStatusCode;
            });
        }
        #endregion

        #region Updaters
        public async Task<bool> UpdateMaisonAsync(Maison maison)
        {
            return await RunWithLoadingCursor(async () =>
            {
                var json = JsonSerializer.Serialize(maison);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"Maisons/update/{maison.id}", content);

                return response.IsSuccessStatusCode;
            });
        }

        public async Task<bool> UpdateFamilleAsync(Famille famille)
        {
            return await RunWithLoadingCursor(async () =>
            {
                var json = JsonSerializer.Serialize(famille);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"Familles/update/{famille.id}", content);

                return response.IsSuccessStatusCode;
            });
        }

        public async Task<bool> UpdateArticleCommandeAsync(ArticlesCommandes articleCommande)
        {
            var dto = new ArticlesCommandesDTO
            {
                ID = articleCommande.id,
                Quantite = articleCommande.quantite
            };
            return await RunWithLoadingCursor(async () =>
            {
                var json = JsonSerializer.Serialize(articleCommande);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"ArticlesCommandes/update/{articleCommande.id}", content);
                return response.IsSuccessStatusCode;
            });
        }

        public async Task<bool> UpdateStatusCommandeAsync(Commande commande)
        {
            return await RunWithLoadingCursor(async () =>
            {
                var json = JsonSerializer.Serialize(new { Status = commande.status.ToString() });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"Commandes/update/status/{commande.id}", content);
                return response.IsSuccessStatusCode;
            });
        }
        #endregion

        #region Deleters
        public async Task<bool> DeleteArticleCommandeAsync(int id)
        {
            return await RunWithLoadingCursor(async () =>
            {
                var response = await _httpClient.DeleteAsync($"ArticlesCommandes/delete/{id}");
                string error = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode;
            });
        }
        #endregion
    }
}
