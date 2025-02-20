using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    public class AchatViewModel : BaseViewModel
    {
        public Commande Achat { get; }
        private Commande _achatTemp;
        public Commande AchatTemp
        {
            get => _achatTemp;
            set => SetProperty(ref _achatTemp, value, nameof(AchatTemp));
        }

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
                        // Copier les données de l'achat dans AchatTemp pour l'édition
                        AchatTemp = new Commande
                        {
                            id = Achat.id,
                            date = Achat.date,
                            reference = Achat.reference,
                            status = Achat.status,
                            client = Achat.client
                        };
                    }
                }
            }
        }
        private ObservableCollection<ArticlesCommandes> _articles = new();
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
        public ICommand NavigateCommand { get; }
        public ICommand SaveCommand { get; }
        public AchatViewModel(Commande achat, ICommand navigateCommand, bool isEditMode=false)
        {
            Achat = achat;
            AchatTemp = new Commande(); // Valeur par défaut avant l'édition
            IsEditMode = isEditMode;

            Articles.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalAchat));

            NavigateCommand = navigateCommand;
            if(!IsEditMode)
            {
                LoadCommandAsync();
            }
            SaveCommand = new RelayCommand<object>(_=> Save());
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

        private async void Save()
        {
            if (!IsEditMode) return;

            try
            {
                // Mettre à jour des données de l'achat
                Achat.id = AchatTemp.id;
                Achat.date = AchatTemp.date;
                Achat.reference = AchatTemp.reference;
                Achat.status = AchatTemp.status;
                Achat.client = AchatTemp.client;

                // Envoyer les modification au serveur
                await _dataService.UpdateCommandeAsync(Achat);

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
