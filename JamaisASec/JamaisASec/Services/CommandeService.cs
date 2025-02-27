using System.Collections.ObjectModel;
using JamaisASec.Models;

namespace JamaisASec.Services
{
    public class CommandeService
    {
        private readonly IApiService _apiService;

        public CommandeService(IApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        }

        public async Task<(ObservableCollection<Commande> Commandes, ObservableCollection<Commande> Achats)> GetCommandesAndAchatsAsync()
        {
            var commandes = await _apiService.GetCommandesAsync();

            var commandesClients = new ObservableCollection<Commande>(
                commandes.Where(c => c.client != null));

            var commandesFournisseurs = new ObservableCollection<Commande>(
                commandes.Where(c => c.fournisseur != null));

            return (commandesClients, commandesFournisseurs);
        }
    }
}
