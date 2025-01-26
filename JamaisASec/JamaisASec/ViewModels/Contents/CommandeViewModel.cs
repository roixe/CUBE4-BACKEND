using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JamaisASec.Models;

namespace JamaisASec.ViewModels.Contents
{
    public class CommandeViewModel : BaseViewModel
    {
        public Commande Commande { get; }
        private ObservableCollection<ArticlesCommandes> _articles;
        public ObservableCollection<ArticlesCommandes> Articles
        {
            get => _articles;
            set {
                _articles = value;
                OnPropertyChanged(nameof(Articles));
                OnPropertyChanged(nameof(TotalCommande));
            }
        }
        public string TotalCommande
        {
            get
            {
                decimal total = Articles?.Sum(article => article.quantite * article.article.prix_unitaire) ?? 0;
                return total.ToString("C2", CultureInfo.GetCultureInfo("fr-FR"));
            }
        }

        public CommandeViewModel(Commande commande)
        {
            _articles = new ObservableCollection<ArticlesCommandes>();
            Articles = new ObservableCollection<ArticlesCommandes>();

            Commande = commande;
            LoadCommandAsync();
        }

        private async void LoadCommandAsync()
        {
            try
            {
                var articlesCommandes = await _apiService.GetArticlesCommandesById(Commande.id);
                Articles.Clear();
                foreach (var articleCommande in articlesCommandes)
                {
                    Articles.Add(articleCommande);
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des articles : {ex.Message}");
            }
        }
    }
}
