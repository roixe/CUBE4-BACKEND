using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;
using System.Windows;

namespace JamaisASec.ViewModels.Modals
{
    public class ArticleModalViewModel : BaseViewModel
    {
        public Article Article { get; }
        private string? _nom;
        public string Nom
        {
            get => _nom ?? String.Empty;
            set => SetProperty(ref _nom, value, nameof(Nom));
        }

        private string? _description;
        public string Description
        {
            get => _description ?? String.Empty;
            set => SetProperty(ref _description, value, nameof(Description));
        }

        private int _quantite;
        public int Quantite
        {
            get => _quantite;
            set => SetProperty(ref _quantite, value, nameof(Quantite));
        }
        private int _quantiteMin;
        public int QuantiteMin
        {
            get => _quantiteMin;
            set => SetProperty(ref _quantiteMin, value, nameof(QuantiteMin));
        }
        private int _colisage;
        public int Colisage
        {
            get => _colisage;
            set => SetProperty(ref _colisage, value, nameof(Colisage));
        }
        private int _annee;
        public int Annee
        {
            get => _annee;
            set => SetProperty(ref _annee, value, nameof(Annee));
        }

        private int _prix;
        public int Prix
        {
            get => _prix;
            set => SetProperty(ref _prix, value, nameof(Prix));
        }
        public ICommand SaveCommand { get; }
        private readonly Window _window;

        public ArticleModalViewModel(Article article, Window window)
        {
            _window = window;
            Article = article ?? new Article();
            _nom = Article.nom;
            _description = Article.description;
            _quantite = Article.quantite;
            _quantiteMin = Article.quantite_Min;
            _annee = Article.annee;
            _prix = Article.prix_unitaire;
            SaveCommand = new RelayCommand<object>(_ => Save());
        }

        private void Save()
        {
            Article.nom = Nom;
            Article.description = Description;
            Article.quantite = Quantite;
            Article.quantite_Min = QuantiteMin;
            Article.annee = Annee;
            Article.prix_unitaire = Prix;
            MessageBox.Show(Article.nom);
            _window.DialogResult = true;
            _window.Close();
        }
    }
}
