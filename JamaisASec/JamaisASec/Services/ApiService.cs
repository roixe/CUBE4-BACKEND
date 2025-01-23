using System.Net.Http;
using System.Text.Json;
using JamaisASec.Models;

namespace JamaisASec.Services
{
    public class ApiService : IApiService
    {
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

        public async Task<List<Article>> GetArticlesAsync()
        {
            var response = await _httpClient.GetAsync("Articles/get/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Article>>(content) ?? new List<Article>();
        }

        public async Task<List<Commande>> GetCommandesAsync()
        {
            var response = await _httpClient.GetAsync("Commandes/get/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Commande>>(content) ?? new List<Commande>();
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            var response = await _httpClient.GetAsync("Clients/get/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Client>>(content) ?? new List<Client>();
        }

        public async Task<List<Fournisseur>> GetFournisseursAsync()
        {
            var response = await _httpClient.GetAsync("Fournisseurs/get/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Fournisseur>>(content) ?? new List<Fournisseur>();
        }

        public async Task<List<Famille>> GetFamillesAsync()
        {
            var response = await _httpClient.GetAsync("Familles/get/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Famille>>(content) ?? new List<Famille>();
        }

        public async Task<List<Maison>> GetMaisonsAsync()
        {
            var response = await _httpClient.GetAsync("Maisons/get/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Maison>>(content) ?? new List<Maison>();
        }
    }
}
