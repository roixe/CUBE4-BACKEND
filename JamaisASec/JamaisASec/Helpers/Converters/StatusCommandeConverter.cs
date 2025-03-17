using System.Text.Json;
using System.Text.Json.Serialization;
using JamaisASec.Models;
public class StatusCommandeConverter : JsonConverter<StatusCommande>
{
    public override StatusCommande Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var statusString = reader.GetString();

        // Essayer de convertir le string en enum StatusCommande
        if (Enum.TryParse(statusString, true, out StatusCommande status))
        {
            return status;
        }

        // Valeur par défaut si la conversion échoue
        return StatusCommande.Inconnue;
    }

    public override void Write(Utf8JsonWriter writer, StatusCommande value, JsonSerializerOptions options)
    {
        // Serialiser l'énum en string
        writer.WriteStringValue(value.ToString());
    }
}

