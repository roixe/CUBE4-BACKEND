﻿
namespace JamaisASec.Models
{
    public class Maison(string nom) : BaseModel
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
            return Equals(obj as Maison);
        }

        public bool Equals(Maison? other)
        {
            return other != null && id == other.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, nom);
        }
    }
}
