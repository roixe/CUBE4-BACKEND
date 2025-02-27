using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Contents
{
    public class CommandeViewModel : BaseViewModel
    {
        public Commande Commande { get; } = new();
        private Commande _commandeTemp = new();
        private Commande CommandeTemp
        {
            get => _commandeTemp;
            set => SetProperty(ref _commandeTemp, value, nameof(CommandeTemp));
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
                        // Copier les données de la commande dans CommandeTemp pour l'édition
                        CommandeTemp = new Commande
                        {
                            id = Commande.id,
                            date = Commande.date,
                            reference = Commande.reference,
                            status = Commande.status,
                            client = Commande.client
                        };
                    }
                }
            }
        }
        private ObservableCollection<ArticlesCommandes> _articles = new();
        public ObservableCollection<ArticlesCommandes> Articles
        {
            get => _articles;
            set {
                _articles = value;
                OnPropertyChanged(nameof(Articles));
                OnPropertyChanged(nameof(TotalCommande));
            }
        }
        public string TotalCommande
        {
            get
            {
                decimal total = Articles?.Sum(article => article.quantite * article.article.prix_unitaire) ?? 0;
                return total.ToString("C2", CultureInfo.GetCultureInfo("fr-FR"));
            }
        }

        public ICommand NavigateCommand { get; }
        public ICommand SaveCommand { get; }
        public CommandeViewModel(Commande commande, ICommand navigateCommand, bool isEditMode = false)
        {
            Commande = commande;
            CommandeTemp = new Commande();
            IsEditMode = isEditMode;

            Articles.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalCommande));

            NavigateCommand = navigateCommand;
            if (!IsEditMode)
            {
                LoadCommandAsync();
            }
            SaveCommand = new RelayCommand<object>(_ => Save());
        }

        private async void LoadCommandAsync()
        {
            try
            {
                var articlesCommandes = await _apiService.GetArticlesCommandesById(Commande.id);
                Articles.Clear();
                foreach (var articleCommande in articlesCommandes)
                {
                    Articles.Add(articleCommande);
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des articles : {ex.Message}");
            }
        }

        private void Save()
        {
            if (!IsEditMode) return;

            try
            {
                // Mettre à jour les données de la commande
                Commande.date = CommandeTemp.date;
                Commande.reference = CommandeTemp.reference;
                Commande.status = CommandeTemp.status;
                Commande.client = CommandeTemp.client;

                // Envoyer les modifications au serveur
                //await _dataService.UpdateCommandeAsync(Commande);

                NavigateCommand?.Execute((Commande, false));
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la sauvegarde : {ex.Message}");
            }
        }
    }
}
