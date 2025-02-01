using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace JamaisASec.Models
{
    public class Article
    {
        internal bool IsSelected;

        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("Nom")]
        public string Nom { get; set; } = string.Empty;

        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("Quantite")]
        public int Quantite { get; set; }

        [JsonPropertyName("Quantite_Min")]
        public int Quantite_Min { get; set; }

        [JsonPropertyName("Colisage")]
        public int Colisage { get; set; }

        [JsonPropertyName("Prix_unitaire")]
        public int Prix_Unitaire { get; set; }

        [JsonPropertyName("Annee")]
        public int Annee { get; set; }

        [JsonPropertyName("Familles_ID")]
        public int Familles_ID { get; set; }

        [JsonPropertyName("Maisons_ID")]
        public int Maisons_ID { get; set; }

        [JsonPropertyName("Fournisseurs_ID")]
        public int Fournisseurs_ID { get; set; }
    }


}
