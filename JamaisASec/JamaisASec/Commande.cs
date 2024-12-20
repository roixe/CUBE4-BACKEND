using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec
{
    public class Commande
    {
        public int id { get; set; }
        public string reference { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public Client? client { get; set; }
        public Fournisseur? fournisseur { get; set; }
        public double Montant { get; private set; }
        public bool IsSelected { get; set; }
        public List<ArticlesCommandes> ArticlesCommandes { get; set; } = new List<ArticlesCommandes>();

    }
}
