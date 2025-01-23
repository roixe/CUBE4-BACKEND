namespace JamaisASec.Models
{
    public class Client
    {
        public int id { get; set; }
        public string? nom { get; set; }
        public string? adresse { get; set; }
        public string? mail { get; set; }
        public string? telephone { get; set; }
        public bool IsSelected { get; set; }
    }
}
