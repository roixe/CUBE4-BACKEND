using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec
{
    public class Article
    {
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; } = 0;
        public int StockMin { get; set; } = 0;
        public int Colisage { get; set; } = 1;
        public int Prix { get; set; } = 0;
        public int Annee { get; set; } = 0;
        public string Famille { get; set; } = string.Empty;
        public bool IsSelected { get; set; }

        public Article(string nom, string description, string famille, int prix, int annee)
        {
            Nom = nom;
            Description = description;
            Famille = famille;
            Prix = prix;
            Annee = annee;
        }
    }

}
