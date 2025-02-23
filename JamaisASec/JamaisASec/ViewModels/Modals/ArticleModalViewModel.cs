using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace JamaisASec.ViewModels.Modals
{
    public class ArticleModalViewModel : BaseViewModel
    {
        public ArticleDTO Article { get; }
        public ObservableCollection<Famille> Familles { get; set; }
        public ObservableCollection<Maison> Maisons { get; set; }
        public ObservableCollection<Fournisseur> Fournisseurs { get; set; }

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
        private Famille? _selectedFamille;
        public Famille? SelectedFamille
        {
            get => _selectedFamille;
            set => SetProperty(ref _selectedFamille, value, nameof(SelectedFamille));
        }

        private Maison? _selectedMaison;
        public Maison? SelectedMaison
        {
            get => _selectedMaison;
            set => SetProperty(ref _selectedMaison, value, nameof(SelectedMaison));
        }

        private Fournisseur? _selectedFournisseur;
        public Fournisseur? SelectedFournisseur
        {
            get => _selectedFournisseur;
            set
            {
                SetProperty(ref _selectedFournisseur, value, nameof(SelectedFournisseur));
            }
        }
        private int _prix;
        public int Prix
        {
            get => _prix;
            set => SetProperty(ref _prix, value, nameof(Prix));
        }
        public ICommand LoadDataCommand { get; }
        public ICommand SaveCommand { get; }
        private readonly Window _window;

        public ArticleModalViewModel(ArticleDTO article, Window window)
        {
            _window = window;
            Maisons = new ObservableCollection<Maison>();
            Familles = new ObservableCollection<Famille>();
            Fournisseurs = new ObservableCollection<Fournisseur>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);

            Article = article ?? new ArticleDTO();
            _nom = Article.nom;
            _description = Article.description;
            _quantite = Article.quantite;
            _quantiteMin = Article.quantite_Min;
            _colisage = Article.colisage;
            _annee = Article.annee;
            _prix = Article.prix_unitaire;

            SaveCommand = new RelayCommand<object>(_ => Save());
        }

        private async Task LoadData()
        {
            var maisons = await _dataService.GetMaisonsAsync();
            Maisons.Clear();
            foreach (var maison in maisons)
            {
                Maisons.Add(maison);
                if (maison.nom == Article.maison?.nom)
                {
                    SelectedMaison = maison;
                }
            }
            var familles = await _dataService.GetFamillesAsync();
            Familles.Clear();
            foreach (var famille in familles)
            {
                if(famille.nom == Article.famille?.nom)
                {
                    SelectedFamille = famille;
                }
                Familles.Add(famille);
            }
        }

        private void Save()
        {
            if (!Validate())
            {
                return;
            }

            // Créer un ArticleDTO à partir des données saisies dans la vue
            var articleDTO = new ArticleDTO
            {
                nom = Nom,
                description = Description,
                quantite = Quantite,
                quantite_Min = QuantiteMin,
                colisage = Colisage,
                annee = Annee,
                prix_unitaire = Prix,
                famille = SelectedFamille,  
                maison = SelectedMaison,    
                fournisseur = SelectedFournisseur 
            };

            // Appeler le service pour ajouter l'article via l'API
            _dataService.AddArticleAsync(articleDTO);

            _window.DialogResult = true;
            _window.Close();
        }

        private bool Validate()
        {
            bool isValid = true;

            // Validation pour le nom
            if (string.IsNullOrEmpty(Nom))
            {
                isValid = false;
            }

            // Validation pour la description
            if (string.IsNullOrEmpty(Description))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
