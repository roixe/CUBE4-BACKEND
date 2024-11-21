using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec
{
    public class Produit
    {
        public string Nom { get; set; } = string.Empty; // Fix for CS8618
        public string Description { get; set; } = string.Empty; // Fix for CS8618
        public int Stock { get; set; }
        public int StockMin { get; set; } = 0; // Fix for CS8618
        public int Colisage { get; set; } = 1; // Fix for CS8618
        public string Famille { get; set; } = string.Empty; // Fix for CS8618

        public int Prix { get; set; } = 0; // Fix for CS8618
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
