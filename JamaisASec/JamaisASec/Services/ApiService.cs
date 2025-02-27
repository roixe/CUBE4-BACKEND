using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;

namespace JamaisASec.Services
{
    public class ApiService : IApiService
    {
        private static readonly Lazy<ApiService> _instance = new(() => new ApiService());
        public static ApiService Instance => _instance.Value;
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

        private async Task<T> RunWithLoadingCursor<T>(Func<Task<T>> apiCall)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                return await apiCall();
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur HTTP : {ex.Message}");
                return default!;
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur JSON : {ex.Message}");
                return default!;
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
                return JsonSerializer.Deserialize<List<Article>>(content) ?? new List<Article>();
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

        public async Task<bool> CreateArticleAsync(Article article)
        {
            var dto = new ArticleDTO
            {
                ID = article.id,
                Nom = article.nom,
                Description = article.description,
                Quantite = article.quantite,
                Quantite_Min = article.quantite_Min,
                Colisage = article.colisage,
                Prix_unitaire = article.prix_unitaire,
                Annee = article.annee,
                Familles_ID = article.famille.id,
                Maisons_ID = article.maison.id,
                Fournisseurs_ID = article.fournisseur.id

            };

            return await RunWithLoadingCursor(async () =>
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Articles/create", content);
                return response.IsSuccessStatusCode;
            });
        }

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
