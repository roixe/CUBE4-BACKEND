
using System.ComponentModel;

namespace JamaisASec.Models
{
    public enum StatusCommande
    {
        [Description("En attente")]
        EnAttente, // en attente du fournisseur
        [Description("Réceptionnée")]
        Receptionnee, // receptionnée du fournisseur
        [Description("En cours")]
        EnCours, // en cours de préparation pour le client
        [Description("Prête")]
        Prete, // prête à être récupérer par le client
        [Description("Livrée")]
        Livree, // livrée au client
        [Description("Annulée")]
        Annulee,
        [Description("Inconnue")]
        Inconnue
    }
}
