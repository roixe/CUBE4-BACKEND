using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    public class AchatViewModel : BaseViewModel
    {
        public Commande Achat { get; }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (SetProperty(ref _isEditMode, value, nameof(IsEditMode)))
                {
                    if (_isEditMode)
                    {
                        _ = LoadArticlesFournisseur();
                    }
                }
            }
        }
        private ObservableCollection<ArticlesCommandes> _articles = [];
        
        private ObservableCollection<ArticlesCommandes> _articlesTemp = [];
        public ObservableCollection<ArticlesCommandes> ArticlesTemp
        {
            get => _articlesTemp;
            set => SetProperty(ref _articlesTemp, value, nameof(ArticlesTemp));
        }
        private ObservableCollection<Article> _articlesFournisseur = [];
        public ObservableCollection<Article> ArticlesFournisseur
        {
            get => _articlesFournisseur;
            set => SetProperty(ref _articlesFournisseur, value, nameof(ArticlesFournisseur));
        }
        private Article _selectedArticle = new();
        public Article SelectedArticle
        {
            get => _selectedArticle;
            set => SetProperty(ref _selectedArticle, value, nameof(SelectedArticle));
        }

        public string TotalAchat
        {
            get
            {
                decimal total = ArticlesTemp?.Sum(article => article.quantite * article.article.prix_unitaire) ?? 0;
                return total.ToString("C2", CultureInfo.GetCultureInfo("fr-FR"));
            }
        }
        
        public ICommand NavigateCommand { get; }
        public ICommand AddArticleCommand { get; }
        public ICommand IncrementQty { get; }
        public ICommand DecrementQty { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteSelectedCommand { get; }

        public AchatViewModel(Commande achat, ICommand navigateCommand, bool isEditMode=false)
        {
            Achat = achat;
            IsEditMode = isEditMode;

            ArticlesTemp.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalAchat));

            NavigateCommand = navigateCommand;

            LoadCommandAsync(); 

            AddArticleCommand = new RelayCommand<object>(_ => AddArticle());
            IncrementQty = new RelayCommand<ArticlesCommandes>(IncrementQuantity);
            DecrementQty = new RelayCommand<ArticlesCommandes>(DecrementQuantity);
            SaveCommand = new RelayCommand<object>(_=> Save());
            DeleteSelectedCommand = new RelayCommand<object>(_ => DeleteSelected());
        }

        private async void LoadCommandAsync()
        {
            try
            {
                var articlesAchats = await _apiService.GetArticlesCommandesById(Achat.id);
                _articles.Clear();
                ArticlesTemp.Clear();
                foreach (var articleAchat in articlesAchats)
                {
                    _articles.Add(articleAchat);
                    ArticlesTemp.Add(articleAchat);
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des articles : {ex.Message}");
            }
        }

        private async Task LoadArticlesFournisseur()
        {
            if (Achat.fournisseur != null)
            {
                try
                {
                    var articles = await _dataService.GetArticlesByFournisseurAsync(Achat.fournisseur.id); // Récupère les articles via l'API
                    ArticlesFournisseur.Clear();
                    foreach (var article in articles)
                    {
                        ArticlesFournisseur.Add(article);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des articles : {ex.Message}");
                }
            }
        }

        private void AddArticle()
        {
            if (SelectedArticle != null)
            {
                // Ajouter un nouvel article à la liste des articles dans la commande
                var articleCommande = new ArticlesCommandes
                {
                    id = 0,
                    article = SelectedArticle,
                    quantite = SelectedArticle.colisage // au minimum le colisage
                };

                ArticlesTemp.Add(articleCommande);
            }
        }

        private void IncrementQuantity(ArticlesCommandes articleCommande)
        {
            if (articleCommande != null)
            {
                articleCommande.quantite += articleCommande.article.colisage;
                OnPropertyChanged(nameof(TotalAchat)); // Mettre à jour le total après modification
            }
        }

        private void DecrementQuantity(ArticlesCommandes articleCommande)
        {
            if (articleCommande.quantite > articleCommande.article.colisage) // Eviter d'aller en dessous du colisage
            {
                articleCommande.quantite -= articleCommande.article.colisage; 
                OnPropertyChanged(nameof(TotalAchat)); // Mettre à jour le total après modification
            }
        }

        private void DeleteSelected()
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les articles sélectionnés ?",
                        "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedArticles = ArticlesTemp.Where(a => a.IsSelected).ToList();
                foreach (var article in selectedArticles)
                {
                    ArticlesTemp.Remove(article);
                }
            }
            OnPropertyChanged(nameof(TotalAchat)); // Mettre à jour le total après modification
        }

        private async void Save()
        {
            if (!IsEditMode) return;

            try
            {
                var commande_id = Achat.id;

                // Mise à jour ou création des articles
                foreach (var articleCommande in ArticlesTemp)
                {
                    var existingArticle = _articles.FirstOrDefault(a => a.article.id == articleCommande.article.id);
                    if (existingArticle != null)
                    {
                        // Si l'article existe dans articles, c'est une mise à jour
                        await _dataService.UpdateArticleCommandeAsync(articleCommande);
                    }
                    else
                    {
                        // Si l'article n'existe pas dans articles, c'est une création
                        await _dataService.CreateArticleCommandeAsync(articleCommande, commande_id);
                    }
                }

                // Suppression des articles qui ne sont plus dans `ArticlesTemp`
                var articlesToDelete = _articles
                    .Where(a => !ArticlesTemp.Any(temp => temp.article.id == a.article.id))
                    .ToList(); // ToList() pour éviter la modification de collection en cours d’itération

                foreach (var articleToDelete in articlesToDelete)
                {
                    await _dataService.DeleteArticleCommandeAsync(articleToDelete.id);
                }

                NavigateCommand?.Execute((Achat, false));
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la sauvegarde : {ex.Message}");
            }
        }
    }
}
