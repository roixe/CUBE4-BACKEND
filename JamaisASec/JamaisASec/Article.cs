using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec
{
    public class Article
    {
        public int id {  get; set; }
        public string nom { get; set; }
        public string description { get; set; } 
        public int quantite { get; set; }
        public int quantite_Min { get; set; }
        public int colisage { get; set; } 
        public int prix_unitaire { get; set; } 
        public int annee { get; set; } 
        public Famille famille { get; set; }
        public Maison maison { get; set; }
        public bool IsSelected { get; set; }
    }

}
