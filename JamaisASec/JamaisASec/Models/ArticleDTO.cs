namespace JamaisASec.Models
{
    public class ArticleDTO
    {
        public int id { get; set; }
        public string? nom { get; set; }
        public string? description { get; set; }
        public int quantite { get; set; }
        public int quantite_Min { get; set; }
        public int colisage { get; set; }
        public int prix_unitaire { get; set; }
        public int annee { get; set; }
        public Famille? famille { get; set; }
        public Maison? maison { get; set; }
        public Fournisseur? fournisseur { get; set; }
        //public bool IsSelected { get; internal set; }
        public bool IsSelected { get; set; }
        //public int Familles_ID { get; set; }
        //public int Maisons_ID { get; set; }
        //public int Fournisseurs_ID { get; set; }


    }
}
