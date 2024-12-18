using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec
{
    public class Client
    {
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public bool IsSelected { get; set; }
        public Client(string nom, string adresse, string email, string telephone)
        {
            Nom = nom;
            Adresse = adresse;
            Mail = email;
            Telephone = telephone;
        }
    }
}
