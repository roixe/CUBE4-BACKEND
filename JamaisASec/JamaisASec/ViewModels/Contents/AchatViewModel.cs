using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JamaisASec.Models;

namespace JamaisASec.ViewModels.Contents
{
    public class AchatViewModel : BaseViewModel
    {
        public Commande Achat { get; }
        private ObservableCollection<ArticlesCommandes> _articles;
        public ObservableCollection<ArticlesCommandes> Articles
        {
            get => _articles;
            set
            {
                _articles = value;
                OnPropertyChanged(nameof(Articles));
                OnPropertyChanged(nameof(TotalAchat));
            }
        }
        public string TotalAchat
        {
            get
            {
                decimal total = Articles?.Sum(article => article.quantite * article.article.prix_unitaire) ?? 0;
                return total.ToString("C2", CultureInfo.GetCultureInfo("fr-FR"));
            }
        }

        public AchatViewModel(Commande achat)
        {
            _articles = new ObservableCollection<ArticlesCommandes>();
            Articles = new ObservableCollection<ArticlesCommandes>();

            Achat = achat;
            LoadCommandAsync();
        }

        private async void LoadCommandAsync()
        {
            try
            {
                var articlesAchats = await _apiService.GetArticlesCommandesById(Achat.id);
                Articles.Clear();
                foreach (var articleAchat in articlesAchats)
                {
                    Articles.Add(articleAchat);
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
