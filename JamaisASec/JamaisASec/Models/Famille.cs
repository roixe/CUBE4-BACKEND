using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec.Models
{
    public class Famille(string nom)
    {
        public int id { get; set; }
        public string nom { get; set; } = nom;
        public bool IsSelected { get; set; }
    }
}
