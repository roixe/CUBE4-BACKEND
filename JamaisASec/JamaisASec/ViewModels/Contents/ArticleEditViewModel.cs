using System.Collections.ObjectModel;
using JamaisASec.Models;

namespace JamaisASec.ViewModels
{
    public class ArticleEditViewModel : BaseViewModel
    {
        private Article _article;
        private ObservableCollection<Fournisseur> _fournisseurs;
        private ObservableCollection<Famille> _familles;
        private ObservableCollection<Maison> _maisons;
        public Article Article
        {
            get => _article;
            set => SetProperty(ref _article, value, nameof(Article));
        }
        public ObservableCollection<Fournisseur> Fournisseurs
        {
            get => _fournisseurs;
            set => SetProperty(ref _fournisseurs, value, nameof(Fournisseurs));
        }
        public ObservableCollection<Famille> Familles
        {
            get => _familles;
            set => SetProperty(ref _familles, value, nameof(Familles));
        }
        public ObservableCollection<Maison> Maisons
        {
            get => _maisons;
            set => SetProperty(ref _maisons, value, nameof(Maisons));
        }

        public ArticleEditViewModel(Article article)
        {
            _article = article;
            _fournisseurs = new ObservableCollection<Fournisseur>();
            _familles = new ObservableCollection<Famille>();
            _maisons = new ObservableCollection<Maison>();

            LoadData();
        }

        private async void LoadData()
        {
            var fournisseurs = await _dataService.GetFournisseursAsync();
            Fournisseurs = new ObservableCollection<Fournisseur>(fournisseurs);
            var familles = await _dataService.GetFamillesAsync();
            Familles = new ObservableCollection<Famille>(familles);
            var maisons = await _dataService.GetMaisonsAsync();
            Maisons = new ObservableCollection<Maison>(maisons);
        }
    }
}
