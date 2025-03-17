using System.Windows.Controls;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;
using JamaisASec.ViewModels.Contents;
using JamaisASec.Views.Contents;

namespace JamaisASec.ViewModels.Pages
{
    class PageFournisseursViewModel : BaseViewModel
    {
        private FournisseursGrid? _gridCache;
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
        public ICommand NavigateCommand { get; }

        public PageFournisseursViewModel()
        {
            NavigateCommand = new RelayCommand<object>(param =>
            {
                switch(param)
                {
                    case (Fournisseur fournisseur, bool isEditMode):
                        Navigate(isEditMode ? "FournisseurEditView" : "FournisseurView", fournisseur);
                        break;
                    default:
                        Navigate("FournisseursGrid");
                        break;
                }
            });
            Navigate("FournisseursGrid");
        }

        private void Navigate(string tab, Fournisseur? fournisseur = null)
        {
            switch (tab)
            {
                case "FournisseursGrid":
                    if(_gridCache == null)
                    {
                        _gridCache = new FournisseursGrid();
                    }
                    CurrentContent = _gridCache;
                    break;
                case "FournisseurView":
                    if (fournisseur != null)
                    {
                        var fournisseurViewModel = new FournisseurViewModel(fournisseur, NavigateCommand);
                        var fournisseurView = new FournisseurView
                        {
                            DataContext = fournisseurViewModel
                        };
                        CurrentContent = fournisseurView;
                    }
                    break;
                case "FournisseurEditView":
                    if (fournisseur != null)
                    {
                        var fournisseurViewModel = new FournisseurViewModel(fournisseur, NavigateCommand, isEditMode: true);
                        var fournisseurEditView = new FournisseurEditView
                        {
                            DataContext = fournisseurViewModel
                        };
                        CurrentContent = fournisseurEditView;
                    }
                    break;
            }
        }
    }
}
