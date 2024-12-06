using System;

namespace JamaisASec
{
    public class Produit
    {
        public required string Nom { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public string? Familles { get; set; }
        public int Prix { get; set; }
    }
}

