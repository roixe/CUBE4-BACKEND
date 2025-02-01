using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using JamaisASec.Views.Modals;


namespace JamaisASec.ViewModels.Modals
{
    public class ArticleModalViewModel : BaseViewModel
    {
        private readonly DataService _dataService;
        private readonly Window _window;

        public Article Article { get; }
        public ObservableCollection<Famille> Familles { get; set; }
        public ObservableCollection<Maison> Maisons { get; set; }

        private string? _nom;
        public string Nom
        {
            get => _nom ?? string.Empty;
            set => SetProperty(ref _nom, value, nameof(Nom));
        }

        private string? _description;
        public string Description
        {
            get => _description ?? string.Empty;
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

        private int _prix;
        private ArticleModal modal;

        public int Prix
        {
            get => _prix;
            set => SetProperty(ref _prix, value, nameof(Prix));
        }

        public ICommand LoadDataCommand { get; }
        public ICommand SaveCommand { get; }

        public ArticleModalViewModel(Article article, Window window, DataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _window = window ?? throw new ArgumentNullException(nameof(window));

            Familles = new ObservableCollection<Famille>();
            Maisons = new ObservableCollection<Maison>();

            Article = article ?? new Article();
            InitializeFieldsFromArticle();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            SaveCommand = new RelayCommand<object>(_ => Save());

            // Charger les données au démarrage
            LoadDataCommand.Execute(null);
        }

        public ArticleModalViewModel(Article article, ArticleModal modal)
        {
            Article = article;
            this.modal = modal;
        }

        private void InitializeFieldsFromArticle()
        {
            _nom = Article.Nom;
            _description = Article.Description;
            _quantite = Article.Quantite;
            _quantiteMin = Article.Quantite_Min;
            _colisage = Article.Colisage;
            _annee = Article.Annee;
            _prix = Article.Prix_Unitaire;
            _selectedFamille = Familles.FirstOrDefault(f => f.id == Article.Familles_ID);
            _selectedMaison = Maisons.FirstOrDefault(m => m.id == Article.Maisons_ID);
        }

        private async Task LoadData()
        {
            try
            {
                // Charger les maisons
                var maisons = await _dataService.GetMaisonsAsync();
                Maisons.Clear();
                foreach (var maison in maisons)
                {
                    Maisons.Add(maison);
                    if (maison.id == Article.Maisons_ID)
                    {
                        SelectedMaison = maison;
                    }
                }

                // Charger les familles
                var familles = await _dataService.GetFamillesAsync();
                Familles.Clear();
                foreach (var famille in familles)
                {
                    Familles.Add(famille);
                    if (famille.id == Article.Familles_ID)
                    {
                        SelectedFamille = famille;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Save()
        {
            if (!Validate())
            {
                MessageBox.Show("Les données saisies ne sont pas valides. Veuillez vérifier tous les champs obligatoires.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                UpdateArticleFromFields();

                // Sauvegarder l'article via le service
                await _dataService.AddArticleAsync(Article);

                MessageBox.Show("Article ajouté avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Fermer la fenêtre
                _window.DialogResult = true;
                _window.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la sauvegarde : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateArticleFromFields()
        {
            Article.Nom = Nom;
            Article.Description = Description;
            Article.Quantite = Quantite;
            Article.Quantite_Min = QuantiteMin;
            Article.Colisage = Colisage;
            Article.Annee = Annee;
            Article.Prix_Unitaire = Prix;
            Article.Familles_ID = SelectedFamille?.id ?? 0;
            Article.Maisons_ID = SelectedMaison?.id ?? 0;
        }

        private bool Validate()
        {
            // Validation des champs obligatoires
            if (string.IsNullOrWhiteSpace(Nom) ||
                string.IsNullOrWhiteSpace(Description) ||
                SelectedFamille == null ||
                SelectedMaison == null ||
                Quantite < 0 ||
                QuantiteMin < 0 ||
                Colisage < 0 ||
                Prix <= 0 ||
                Annee <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
