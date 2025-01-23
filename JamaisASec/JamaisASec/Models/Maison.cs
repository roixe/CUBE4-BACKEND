using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec.Models
{
    public class Maison(string nom) : BaseModel
    {
        public int id { get; set; }
        public string nom { get; set; } = nom;
    }
}
