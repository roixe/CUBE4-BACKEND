
namespace JamaisASec.Models
{
    class ClientDTO
    {
        public int ID { get; set; } = 0; 
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Mail { get; set; }
        public string? mot_De_Passe { get; set; }
        public string Telephone { get; set; }
    }
}
