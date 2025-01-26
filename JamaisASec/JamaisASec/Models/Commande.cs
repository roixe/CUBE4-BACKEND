
using System.ComponentModel;

namespace JamaisASec.Models
{
    public class Commande : BaseModel
    {
        public int id { get; set; }
        public string? reference { get; set; }
        public DateTime? date { get; set; }
        public string? status { get; set; }
        public Client? client { get; set; }
        public Fournisseur? fournisseur { get; set; }
    }
}
