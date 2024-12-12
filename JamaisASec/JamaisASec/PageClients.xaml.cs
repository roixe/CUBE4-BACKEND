using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    /// <summary>
    /// Interaction logic for PageClients.xaml
    /// </summary>
    public partial class PageClients : Page
    {
        private List<Client> Clients { get; set; }
        public PageClients(List<Client> clients)
        {
            InitializeComponent();
            Clients = clients;
            ClientsGrid.ItemsSource = clients;

            controlsClient.AddItem += ControlsClient_AjouterItem;
        }

        private void ControlsClient_AjouterItem(object sender, RoutedEventArgs e)
        {
            AjouterClientButton_Click(sender, e);
        }

        private void AjouterClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterClientForm
            var ajouterClientForm = new Forms.AjouterClientForm(Clients);
            ajouterClientForm.ShowDialog();
            // Rafraîchir la grille après ajout d'un client
            ClientsGrid.Items.Refresh();
        }

    }
}
