using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec.Models
{
    public class ArticlesCommandes
    {
        public int id { get; set; }
        public Article article { get; set; }
        public int quantite { get; set; }
    }
}
