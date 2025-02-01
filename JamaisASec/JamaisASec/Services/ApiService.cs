using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
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

        public async Task<List<ArticlesCommandes>> GetArticlesCommandesById(int commandeId)
        {
            var response = await _httpClient.GetAsync($"ArticlesCommandes/get/command/{commandeId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ArticlesCommandes>>(content) ?? new List<ArticlesCommandes>();
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

        public async Task<List<Commande>> GetClientsCommandesAsync(int clientId)
        {
            var response = await _httpClient.GetAsync($"Commandes/get/byClient/{clientId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Commande>>(content) ?? new List<Commande>();
        }

        public async Task<List<Commande>> GetFournisseursAchatsAsync(int fournisseurId)
        {
            var response = await _httpClient.GetAsync($"Commandes/get/byFournisseur/{fournisseurId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Commande>>(content) ?? new List<Commande>();
        }

        public async Task<Article> AddArticleAsync(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article), "L'article ne peut pas être null.");
            }

            if (article.Fournisseurs_ID == 0)
            {
                article.Fournisseurs_ID = 1; 
            }

           
            var json = JsonSerializer.Serialize(article);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Articles/create", content);
            var jsoncontent = JsonSerializer.Serialize(article);
            
   
            if (!response.IsSuccessStatusCode)
            {
                
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error in API response: {response.StatusCode} - {errorContent}");
            }

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Article>(responseContent);
        }


        public async Task<Article> UpdateArticleAsync(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            // Sérialisation en JSON
            var json = JsonSerializer.Serialize(article);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Envoi de la requête PUT
            var response = await _httpClient.PutAsync($"api/Article/{article.ID}", content);
            response.EnsureSuccessStatusCode();

            // Désérialisation de la réponse en Article
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Article>(responseContent);
        }

       
    }
}
