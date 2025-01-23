using JamaisASec.Models;

namespace JamaisASec.Services
{
    interface IApiService
    {
        Task<List<Article>> GetArticlesAsync();
        Task<List<Commande>> GetCommandesAsync();
        Task<List<Client>> GetClientsAsync();
        Task<List<Fournisseur>> GetFournisseursAsync();
    }
}
