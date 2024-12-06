using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec
{
    public class Produit
    {
        public string Nom { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public int Stock { get; set; }
        public int StockMin { get; set; } = 0; 
        public int Colisage { get; set; } = 1; 
        public string Famille { get; set; } = string.Empty; 

        public int Prix { get; set; } = 0; 
        public Produit(string nom, string description, string famille, int prix)
        {
            Nom = nom;
            Description = description;
            Famille = famille;
            Prix = prix;
            Stock = 0;
            StockMin = 0;
            Colisage = 1;
        }
    }

}
