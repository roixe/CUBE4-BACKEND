
namespace JamaisASec.Models
{
    public class ArticlesCommandesDTO
    {
        public int ID { get; set; } = 0; // 0 si nouvel article, sinon l'ID de l'article existant
        public int Commandes_ID { get; set; }
        public int Articles_ID { get; set; }
        public int Quantite { get; set; }
    }
}
