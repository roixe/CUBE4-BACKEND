using System.Collections.ObjectModel;
using JamaisASec.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using JamaisASec.Services;

namespace JamaisASec.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {
        public Article Article{ get; }
        private Article _articleTemp;
        public Article ArticleTemp
        {
            get => _articleTemp;
            set => SetProperty(ref _articleTemp, value, nameof(ArticleTemp));
        }

        private ObservableCollection<Fournisseur> _fournisseurs = new();
        public ObservableCollection<Fournisseur> Fournisseurs
        {
            get => _fournisseurs;
            set => SetProperty(ref _fournisseurs, value, nameof(Fournisseurs));
        }
        private ObservableCollection<Famille> _familles = new();
        public ObservableCollection<Famille> Familles
        {
            get => _familles;
            set => SetProperty(ref _familles, value, nameof(Familles));
        }
        private ObservableCollection<Maison> _maisons = new();
        public ObservableCollection<Maison> Maisons
        {
            get => _maisons;
            set => SetProperty(ref _maisons, value, nameof(Maisons));
        }

        private bool _isEditMode;  // Propriété pour savoir si on est en mode édition
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (SetProperty(ref _isEditMode, value, nameof(IsEditMode)))
                {
                    if (_isEditMode)
                    {
                        LoadData();  // Charger les données uniquement si on est en mode édition
                        ArticleTemp = new Article
                        {
                            id = Article.id,
                            nom = Article.nom,
                            description = Article.description,
                            prix_unitaire = Article.prix_unitaire,
                            quantite = Article.quantite,
                            quantite_Min = Article.quantite_Min,
                            colisage = Article.colisage,
                            fournisseur = Article.fournisseur,
                            famille = Article.famille,
                            maison = Article.maison
                        };
                    }
                }
            }
        }

        public ICommand NavigateCommand { get; }
        public ICommand SaveCommand { get; }

        public ArticleViewModel(Article article, ICommand navigateCommand, bool isEditMode = false)
        {
            Article = article;
            ArticleTemp = new Article(); // Valeur par défaut avant l'édition
            IsEditMode = isEditMode; // Définit le mode d'édition

            NavigateCommand = navigateCommand;
            SaveCommand = new RelayCommand<object>(_ => SaveArticle());
        }

        // Méthode pour charger les données en mode édition
        private async void LoadData()
        {
            var fournisseurs = await _dataService.GetFournisseursAsync();
            Fournisseurs = new ObservableCollection<Fournisseur>(fournisseurs);
            var familles = await _dataService.GetFamillesAsync();
            Familles = new ObservableCollection<Famille>(familles);
            var maisons = await _dataService.GetMaisonsAsync();
            Maisons = new ObservableCollection<Maison>(maisons);
        }

        private async void SaveArticle()
        {
            if (!IsEditMode) return;

            try
            {
                Article.nom = ArticleTemp.nom;
                Article.description = ArticleTemp.description;
                Article.prix_unitaire = ArticleTemp.prix_unitaire;
                Article.quantite = ArticleTemp.quantite;
                Article.quantite_Min = ArticleTemp.quantite_Min;
                Article.colisage = ArticleTemp.colisage;
                Article.fournisseur = ArticleTemp.fournisseur;
                Article.famille = ArticleTemp.famille;
                Article.maison = ArticleTemp.maison;
                
                await _dataService.UpdateArticleAsync(Article);

                NavigateCommand.Execute((Article, false));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la sauvegarde : {ex.Message}");
            }
        }
    }
}
