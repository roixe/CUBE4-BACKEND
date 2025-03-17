
namespace JamaisASec.Models
{
    public class ArticlesCommandes : BaseModel
    {
        private int _quantite;
        private Article _article;
        public int id { get; set; }

        public Article article
        {
            get => _article;
            set => SetProperty(ref _article, value);  // Utilise SetProperty pour notifier des changements
        }

        public int quantite
        {
            get => _quantite;
            set
            {
                if (SetProperty(ref _quantite, value))
                {
                    OnPropertyChanged(nameof(TotalPrice)); // Notification du changement du prix total
                }
            }
        }

        public decimal TotalPrice => quantite * article.prix_unitaire;
    }
}
