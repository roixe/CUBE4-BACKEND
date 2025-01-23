using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec.Models
{
    public class ArticlesCommandes
    {
        public Article Article { get; set; }
        public int Quantite { get; set; }

        public ArticlesCommandes(Article article, int quantite)
        {
            Article = article;
            Quantite = quantite;
        }
    }
}
