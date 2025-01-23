using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec.Models
{
    public class Achat
    {
        public int ID { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public List<ArticlesCommandes> ArticlesCommandes { get; set; } = new List<ArticlesCommandes>();
        public DateTime Date { get; set; }
        public string Statut { get; set; }
        public double Montant { get; private set; }
        public bool IsSelected { get; set; }

        public Achat(int id, Fournisseur fournisseur, List<ArticlesCommandes> articles, DateTime date, string status)
        {
            ID = id;
            Fournisseur = fournisseur;
            ArticlesCommandes = articles;
            Date = date;
            Statut = status;
            // Calcul automatique du montant total
            Montant = ArticlesCommandes.Sum(aq => aq.Quantite * aq.Article.prix_unitaire);
        }
    }
}
