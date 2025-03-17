
namespace JamaisASec.Models
{
    public class Fournisseur : BaseModel
    {
        public int id { get; set; }
        public string nom { get; set; } = string.Empty;
        public string adresse { get; set; } = string.Empty; 
        public string mail { get; set; } = string.Empty;
        public string telephone { get; set; } = string.Empty;
        public string siret { get; set; } = string.Empty;
        public override bool Equals(object? obj)
        {
            return Equals(obj as Fournisseur);
        }

        public bool Equals(Fournisseur? other)
        {
            return other != null && id == other.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, nom);
        }
    }
}
