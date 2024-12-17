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
            ClientsGrid.ItemsSource = Clients;

            controlsClient.AddItem += ControlsClient_AjouterItem;
        }

        private void ControlsClient_AjouterItem(object sender, RoutedEventArgs e)
        {
            AjouterClientButton_Click(sender, e);
        }

        private void AjouterClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre AjouterClientForm
            var ajouterClientForm = new Forms.ClientForm(Clients);
            ajouterClientForm.ShowDialog();
            // Rafraîchir la grille après ajout d'un client
            ClientsGrid.Items.Refresh();
        }

        private void EditProduitButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le client associé à la ligne
            if (sender is Button button && button.DataContext is Client clientSelectionne)
            {
                // Ouvrir la fenêtre de modification
                var modifierProduitForm = new Forms.ClientForm(Clients, clientSelectionne);

                // Afficher la fenêtre en mode dialogue
                modifierProduitForm.ShowDialog();

                // Rafraîchir la grille après modification
                ClientsGrid.Items.Refresh();
            }
        }

    }
}
