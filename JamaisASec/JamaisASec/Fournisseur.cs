using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec
{
    public class Fournisseur
    {
        public int id { get; set; }
        public string nom { get; set; } = string.Empty;
        public string adresse { get; set; } = string.Empty; 
        public string mail { get; set; } = string.Empty;
        public string telephone { get; set; } = string.Empty;
        public string siret { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
