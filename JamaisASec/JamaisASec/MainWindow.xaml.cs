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
            Achats = allCommandes.Where(c => c.clients_ID == null).ToList();
            Commandes = allCommandes.Where(c => c.fournisseurs_ID == null).ToList();

            Familles = 
            [
                new("Vin Rouge"), 
                new("Vin Blanc"),
                new("Vin Rosé"),
                new("Champagne"),
                new("Vin Doux")

            ];
            /*
            Achats =
            [
                new Achat(
                    5,
                    Fournisseurs[1],
                    [
                        new ArticlesCommandes(Articles[1], 20),
                        new ArticlesCommandes(Articles[2], 10)
                    ],
                    new DateTime(2024, 12, 1) ,
                    "En attente"
                ),
                new Achat(
                    4,
                    Fournisseurs[2],
                    [
                        new ArticlesCommandes(Articles[2], 50),
                        new ArticlesCommandes(Articles[5], 100)
                    ],
                    new DateTime(2024, 11, 15),
                    "En attente"
                ),
                new Achat(
                    3,
                    Fournisseurs[0],
                    [
                        new ArticlesCommandes(Articles[4], 5)
                    ],
                    new DateTime(2024, 11, 5),
                    "Receptionné"
                ),
                new Achat(
                    2,
                    Fournisseurs[1],
                    [
                        new ArticlesCommandes(Articles[1], 30),
                        new ArticlesCommandes(Articles[3], 20)
                    ],
                    new DateTime(2024, 10, 25),
                    "Receptionné"
                ),
                new Achat(
                    1,
                    Fournisseurs[2],
                    [
                        new ArticlesCommandes(Articles[2], 75),
                        new ArticlesCommandes(Articles[6], 150)
                    ],
                    new DateTime(2024, 10, 10),
                    "Receptionné"
                ),
            ];*/

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

            MainFrame.Navigate(new PageArticles(Articles, Familles));
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
