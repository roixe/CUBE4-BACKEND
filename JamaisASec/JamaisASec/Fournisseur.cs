using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec
{
    public class Fournisseur
    {
        public string Nom { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty; 
        public string Mail { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string SIRET { get; set; } = string.Empty;
        public bool IsSelected { get; set; }

        public Fournisseur(string nom, string adresse, string mail, string telephone, string siret) { 
            Nom = nom;
            Adresse = adresse;
            Mail = mail;
            Telephone = telephone;
            SIRET = siret;
        }
    }
}
