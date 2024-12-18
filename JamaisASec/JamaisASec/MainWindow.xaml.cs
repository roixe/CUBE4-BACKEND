using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace JamaisASec
{
    public partial class MainWindow : Window
    {
        List<ToggleButton> MenuButtons { get; set; }
        List<Article> Articles { get; set; }
        List<Fournisseur> Fournisseurs { get; set; }
        List<Client> Clients { get; set; }
        List<Famille> Familles { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            // Initialiser les produits
            

            ApiClient client = new ApiClient();

            var task = Task.Run(() => client.GetAsync<List<Article>>("Articles/get/all"));
            task.Wait();
            Articles = task.Result;

         



            Fournisseurs =
            [
                new("Domaine Tariquet", "adresse 1", "tariquet@mail.com", "0123456789", "123456789"),
                new("Pelleheaut", "adresse 2", "pelleheaut@mail.com", "0123456789", "123456789"),
                new("Domaine Uby", "adresse 3", "uby@mail.com", "012345678", "123456789")
            ];

            Clients =
            [
                new("Les vins d'ici", "1 rue de la paix", "vinsdici@mail.com", "0123456789"),
                new("Le Chai des Amis", "12 avenue des Cèdres", "chaiamis@mail.com", "0987654321"),
                new("Saveurs de Bacchus", "34 boulevard du Château", "saveursbacchus@mail.com", "0147258369"),
                new("Cave et Terroir", "8 impasse des Vignerons", "caveetterroir@mail.com", "0178349265"),
                new("Les Ceps Dorés", "15 place des Vendanges", "cepsdores@mail.com", "0187654329"),
                new("Au Bon Cru", "23 allée des Sommeliers", "auboncru@mail.com", "0192837465"),
                new("Vigne et Passion", "45 route des Cépages", "vignepassion@mail.com", "0165483297"),
                new("Terres de Vins", "67 chemin des Grappes", "terresdevins@mail.com", "0156748392"),
                new("Le Raisin Bleu", "89 rue des Tonneaux", "raisinbleu@mail.com", "0143967285"),
                new("L'Art du Vin", "101 cours des Sommeliers", "artduvin@mail.com", "0192834765")
            ];


            Familles = 
            [
                new("Vin Rouge"), 
                new("Vin Blanc"),
                new("Vin Rosé"),
                new("Champagne"),
                new("Vin Doux")

            ];

            MenuButtons =
            [
                DashboardButton,
                ArticlesButton,
                FournisseursButton,
                StocksButton,
                ClientsButton,
                CommandesButton
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

        private void FournisseursButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageFournisseurs(Fournisseurs));
            SetActiveButton(FournisseursButton);
        }

        private void StocksButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new PageStocks(Articles));
            SetActiveButton(StocksButton);
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageClients(Clients));
            SetActiveButton(ClientsButton);
        }

        private void CommandesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageCommandes());
            SetActiveButton(CommandesButton);
        }
    }

    
}
