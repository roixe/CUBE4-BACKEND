
using System.Text.Json.Serialization;

namespace JamaisASec.Models
{
    public class Commande : BaseModel
    {
        public int id { get; set; }
        public string? reference { get; set; }
        public DateTime? date { get; set; }
        [JsonConverter(typeof(StatusCommandeConverter))]
        public StatusCommande? status { get; set; }
        public Client? client { get; set; }
        public Fournisseur? fournisseur { get; set; }
    }
}
