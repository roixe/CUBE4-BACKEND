
namespace JamaisASec.Models
{
    public class Famille(string nom) : BaseModel
    {
        public int id { get; set; }
        private string _nom = nom;
        public string nom
        {
            get => _nom;
            set
            {
                if (_nom != value)
                {
                    _nom = value;
                    OnPropertyChanged(nameof(nom));
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Famille);
        }

        public bool Equals(Famille? other)
        {
            return other != null && id == other.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, nom);
        }
    }
}
