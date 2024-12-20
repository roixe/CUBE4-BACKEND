using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace JamaisASec
{
    public partial class MainWindow : Window
    {
        ApiClient Client { get; set; }
        List<ToggleButton> MenuButtons { get; set; }
        List<Article> Articles { get; set; }
        List<Famille> Familles { get; set; }
        List<Commande> Achats { get; set; }
        List<Commande> Commandes { get; set; }
        public MainWindow()
        {
            InitializeComponent();
         
            Client = new ApiClient();

            var task = Task.Run(() => Client.GetAsync<List<Article>>("Articles/get/all"));
            task.Wait();
            Articles = task.Result;

            var commandesTask = Task.Run(() => Client.GetAsync<List<Commande>>("Commandes/get/all"));
            commandesTask.Wait();
            var allCommandes = commandesTask.Result;
            Achats = new List<Commande>();
            Commandes = new List<Commande>();
            foreach (var commande in allCommandes)
            {
                if (commande.fournisseur != null)
                {
                    Achats.Add(commande);
                }
                else if (commande.client != null)
                {
                    Commandes.Add(commande);
                }
            }

            MenuButtons =
            [
                DashboardButton,
                ArticlesButton,
                ClientsButton,
                CommandesButton,
                FournisseursButton,
                AchatsButton,
                StocksButton
            ];
            MainFrame.Navigate(new DashBoard(Articles));
        }

        private void SetActiveButton(ToggleButton button)
        {
            foreach (var btn in MenuButtons)
            {
                if (btn != button)
                {
                    btn.IsChecked = false;
                }
            }
            button.IsChecked = true;
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashBoard(Articles));
            SetActiveButton(DashboardButton);

        }

        private void ArticlesButton_Click(object sender, RoutedEventArgs e)
        {
            var familleTask = Task.Run(() => Client.GetAsync<List<Famille>>("Familles/get/all"));
            familleTask.Wait();
            var familles = familleTask.Result;
            var maisonTask = Task.Run(() => Client.GetAsync<List<Maison>>("Maisons/get/all"));
            maisonTask.Wait();
            var maisons = maisonTask.Result;
            MainFrame.Navigate(new PageArticles(Articles, familles, maisons));
            SetActiveButton(ArticlesButton);
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(() => Client.GetAsync<List<Client>>("Clients/get/all"));
            task.Wait();
            var clients = task.Result;
            MainFrame.Navigate(new PageClients(clients));
            SetActiveButton(ClientsButton);
        }

        private void CommandesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageCommandes(Commandes));
            SetActiveButton(CommandesButton);
        }

        private void FournisseursButton_Click(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(() => Client.GetAsync<List<Fournisseur>>("Fournisseurs/get/all"));
            task.Wait();
            var fournisseurs = task.Result;

            MainFrame.Navigate(new PageFournisseurs(fournisseurs));
            SetActiveButton(FournisseursButton);
        }

        private void AchatsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageAchats(Achats));
            SetActiveButton(AchatsButton);
        }

        private void StocksButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageStocks(Articles));
            SetActiveButton(StocksButton);
        }
    }

    
}
