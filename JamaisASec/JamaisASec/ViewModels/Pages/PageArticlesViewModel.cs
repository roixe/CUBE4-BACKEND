using System.Windows.Controls;
using System.Windows.Input;
using JamaisASec.Services;
using JamaisASec.Views.UserControls;
using JamaisASec.Views.Contents;
using JamaisASec.Models;
using JamaisASec.ViewModels.Contents;
using System.ComponentModel.Design;
using System.Windows;

namespace JamaisASec.ViewModels.Pages
{
    class PageArticlesViewModel : BaseViewModel
    {
        private readonly Dictionary<string, UserControl> _tabCache = new();
        private UserControl _currentContent = new();
        public UserControl CurrentContent
        {
            get => _currentContent;
            set
            {
                if (_currentContent != value)
                {
                    _currentContent = value;
                    OnPropertyChanged(nameof(CurrentContent));
                }
            }
        }
        private string _activeTab = "";
        public string ActiveTab
        {
            get => _activeTab;
            set
            {
                if (_activeTab != value)
                {
                    _activeTab = value;
                    OnPropertyChanged(nameof(ActiveTab));
                }
            }
        }
        public ICommand NavigateCommand { get; }

        public PageArticlesViewModel()
        {
            NavigateCommand = new RelayCommand<object>(param =>
            {
                switch(param)
                {
                    //Si le commandParameter est un bouton (dans le dataGrid)
                    case Button button:
                        //Récupérer le datacontext du bouton (l'article)
                        Article? article = button.DataContext as Article;
                        string? tag = button.Tag.ToString();
                        switch (tag)
                        {
                            case "View":
                                Navigate("ArticleView", article);
                                break;
                            case "Edit":
                                Navigate("ArticleEditView", article);
                                break;
                        }
                        break;
                    //Si le commandParameter est un string (les onglets)
                    case string tab when tab == "ArticlesGrid" || tab == "MaisonsGrid" || tab == "FamillesGrid":
                        Navigate(tab);
                        break;
                }
            });
            Navigate("ArticlesGrid");
        }

        private void Navigate(string tab, Object? obj = null)
        {
            if (tab.EndsWith("Grid"))
            {
                if (!_tabCache.ContainsKey(tab))
                {
                    switch (tab)
                    {
                        case "ArticlesGrid":
                            _tabCache[tab] = new ArticlesGrid();
                            break;
                        case "MaisonsGrid":
                            _tabCache[tab] = new MaisonsGrid();
                            break;
                        case "FamillesGrid":
                            _tabCache[tab] = new FamillesGrid();
                            break;
                    }
                }
                CurrentContent = _tabCache[tab];
            }
            else
            {
                switch (tab)
                {
                    case "ArticleView":
                        if (obj is Article article)
                        {
                            var articleViewModel = new ArticleViewModel(article);
                            var articleView = new ArticleView
                            {
                                DataContext = articleViewModel
                            };
                            CurrentContent = articleView;
                        }
                        break;

                    case "ArticleEditView":
                        if (obj is Article articleEdit)
                        {
                            var articleEditViewModel = new ArticleEditViewModel(articleEdit);
                            var articleEditView = new ArticleEditView
                            {
                                DataContext = articleEditViewModel
                            };
                            CurrentContent = articleEditView;
                        }
                        break;
                }
            }

            ActiveTab = tab;
        }
    }
}