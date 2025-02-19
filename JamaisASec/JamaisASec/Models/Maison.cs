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
        public override bool Equals(object obj)
        {
            return Equals(obj as Maison);
        }

        public bool Equals(Maison other)
        {
            return other != null && id == other.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, nom);
        }
    }
}
