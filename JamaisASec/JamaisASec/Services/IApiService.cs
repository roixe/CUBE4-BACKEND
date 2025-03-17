using JamaisASec.Models;

namespace JamaisASec.Services
{
    public interface IApiService
    {
        Task<List<Article>> GetArticlesAsync();
        //Task<List<ArticleDTO>> GetArticlesAsync();
        Task<List<Commande>> GetCommandesAsync();
        Task<List<Client>> GetClientsAsync();
        Task<List<Fournisseur>> GetFournisseursAsync();
        Task<List<ArticlesCommandes>> GetArticlesCommandesById(int commandeId);
        Task<List<Famille>> GetFamillesAsync();
        Task<List<Maison>> GetMaisonsAsync();
        Task<List<Commande>> GetClientsCommandesAsync(int clientId);
        Task<List<Commande>> GetFournisseursAchatsAsync(int fournisseurId);

        Task<bool> CreateArticleAsync(Article article);
        Task<bool> CreateMaisonAsync(Maison maison);
        Task<bool> CreateFamilleAsync(Famille famille);
        Task<bool> CreateClientAsync(Client client);
        Task<bool> CreateFournisseurAsync(Fournisseur fournisseur);
        Task<bool> CreateArticleCommandeAsync(ArticlesCommandes articleCommande, int commande_id);
        Task<bool> UpdateArticleAsync(Article article);
        Task<bool> UpdateMaisonAsync(Maison maison);
        Task<bool> UpdateFamilleAsync(Famille famille);
        Task<bool> UpdateClientAsync(Client client);
        Task<bool> UpdateFournisseurAsync(Fournisseur fournisseur);
        Task<bool> UpdateArticleCommandeAsync(ArticlesCommandes articleCommande);
        Task<bool> UpdateCommandeAsync(Commande commande);
        Task<bool> UpdateStatusCommandeAsync(Commande commande);
        Task<bool> DeleteArticleAsync(int id);
        Task<bool> DeleteMaisonAsync(int id);
        Task<bool> DeleteFamilleAsync(int id);
        Task<bool> DeleteClientAsync(int id);
        Task<bool> DeleteFournisseurAsync(int id);
        Task<bool> DeleteArticleCommandeAsync(int id);
    }
}
