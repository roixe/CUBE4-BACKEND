using JamaisASec.Views.Pages;
using JamaisASec.Services;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using JamaisASec.Models;
using JamaisASec.ViewModels.Contents;
using JamaisASec.Views.Contents;
using JamaisASec.ViewModels.Pages;

namespace JamaisASec.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private readonly Dictionary<string, Page> _pagesCache = new();

        // Propriétés de navigation
        private object _currentPage = new();
        public object CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        // Commande de navigation
        public ICommand NavigateCommand { get; }
        public ICommand RefreshCommand { get; }

        // Propriétés de chaque page
        private bool _isDashboardActive;
        public bool IsDashboardActive
        {
            get => _isDashboardActive;
            set
            {
                if (_isDashboardActive != value)
                {
                    _isDashboardActive = value;
                    OnPropertyChanged(nameof(IsDashboardActive));
                }
            }
        }

        private bool _isArticlesActive;
        public bool IsArticlesActive
        {
            get => _isArticlesActive;
            set
            {
                if (_isArticlesActive != value)
                {
                    _isArticlesActive = value;
                    OnPropertyChanged(nameof(IsArticlesActive));
                }
            }
        }

        private bool _isClientsActive;
        public bool IsClientsActive
        {
            get => _isClientsActive;
            set
            {
                if (_isClientsActive != value)
                {
                    _isClientsActive = value;
                    OnPropertyChanged(nameof(IsClientsActive));
                }
            }
        }

        private bool _isCommandesActive;
        public bool IsCommandesActive
        {
            get => _isCommandesActive;
            set
            {
                if (_isCommandesActive != value)
                {
                    _isCommandesActive = value;
                    OnPropertyChanged(nameof(IsCommandesActive));
                }
            }
        }

        private bool _isFournisseursActive;
        public bool IsFournisseursActive
        {
            get => _isFournisseursActive;
            set
            {
                if (_isFournisseursActive != value)
                {
                    _isFournisseursActive = value;
                    OnPropertyChanged(nameof(IsFournisseursActive));
                }
            }
        }

        private bool _isAchatsActive;
        public bool IsAchatsActive
        {
            get => _isAchatsActive;
            set
            {
                if (_isAchatsActive != value)
                {
                    _isAchatsActive = value;
                    OnPropertyChanged(nameof(IsAchatsActive));
                }
            }
        }

        private bool _isStocksActive;
        public bool IsStocksActive
        {
            get => _isStocksActive;
            set
            {
                if (_isStocksActive != value)
                {
                    _isStocksActive = value;
                    OnPropertyChanged(nameof(IsStocksActive));
                }
            }
        }

        public MainViewModel()
        {
            NavigateCommand = new RelayCommand<string>(Navigate);
            RefreshCommand = new RelayCommand<object>(async _ => await RefreshAllData());

            Navigate("Dashboard");
        }

        private void Navigate(string pageTag)
        {
            IsDashboardActive = false;
            IsArticlesActive = false;
            IsClientsActive = false;
            IsCommandesActive = false;
            IsFournisseursActive = false;
            IsAchatsActive = false;
            IsStocksActive = false;
            // Vérifier si la page est déjà dans le cache
            if (!_pagesCache.ContainsKey(pageTag))
            {
                // Si la page n'est pas dans le cache, la créer et l'ajouter
                switch (pageTag)
                {
                    case "Dashboard":
                        _pagesCache[pageTag] = new PageAccueil();
                        IsDashboardActive = true;
                        break;
                    case "Articles":
                        _pagesCache[pageTag] = new PageArticles();
                        IsArticlesActive = true;
                        break;
                    case "Clients":
                        var pageClientViewModel = new PageClientsViewModel();
                        var pageClients = new BasePageNavigation
                        {
                            DataContext = pageClientViewModel
                        };
                        _pagesCache[pageTag] = pageClients;
                        IsClientsActive = true;
                        break;
                    case "Commandes":
                        var pageCommandesViewModel = new PageCommandesViewModel();
                        var pageCommandes = new BasePageNavigation
                        {
                            DataContext = pageCommandesViewModel
                        };
                        _pagesCache[pageTag] = pageCommandes;
                        IsCommandesActive = true;
                        break;
                    case "Fournisseurs":
                        var pageFournisseurViewModel = new PageFournisseursViewModel();
                        var pageFournisseurs = new BasePageNavigation
                        {
                            DataContext = pageFournisseurViewModel
                        };
                        _pagesCache[pageTag] = pageFournisseurs;
                        IsFournisseursActive = true;
                        break;
                    case "Achats":
                        var pageAchatsViewModel = new PageAchatsViewModel();
                        var pageAchats = new BasePageNavigation
                        {
                            DataContext = pageAchatsViewModel
                        };
                        _pagesCache[pageTag] = pageAchats;
                        IsAchatsActive = true;
                        break;
                    case "Stocks":
                        _pagesCache[pageTag] = new PageStocks();
                        IsStocksActive = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // La page existe déjà dans le cache, définir son état
                switch (pageTag)
                {
                    case "Dashboard": IsDashboardActive = true; break;
                    case "Articles": IsArticlesActive = true; break;
                    case "Clients": IsClientsActive = true; break;
                    case "Commandes": IsCommandesActive = true; break;
                    case "Fournisseurs": IsFournisseursActive = true; break;
                    case "Achats": IsAchatsActive = true; break;
                    case "Stocks": IsStocksActive = true; break;
                }
            }

            // Naviguer vers la page existante ou nouvellement créée
            CurrentPage = _pagesCache[pageTag];
        }

        private async Task RefreshAllData()
        {
            await DataService.Instance.RefreshAllCaches();
        }
    }
}
