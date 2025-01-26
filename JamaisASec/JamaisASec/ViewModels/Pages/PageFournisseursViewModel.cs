using System.Collections.ObjectModel;
using System.Windows;
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
                if (param is Fournisseur fournisseur)
                {
                    Navigate("FournisseurView", fournisseur);
                }
                else if (param is string tab)
                {
                    Navigate(tab);
                }
            });
            Navigate("FournisseursGrid");
        }

        private void Navigate(string tab, Fournisseur? fournisseur = null)
        {
            switch (tab)
            {
                case "FournisseursGrid":
                    CurrentContent = new FournisseursGrid();
                    break;
                case "FournisseurView":
                    if (fournisseur != null)
                    {
                        var fournisseurViewModel = new FournisseurViewModel(fournisseur);
                        var fournisseurView = new FournisseurView
                        {
                            DataContext = fournisseurViewModel
                        };
                        CurrentContent = fournisseurView;
                    }
                    break;
            }
        }
    }
}
