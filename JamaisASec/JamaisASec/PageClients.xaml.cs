using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

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
            searchClient.TextChanged += SearchClient_TextChanged;
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

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
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

        private void RemoveClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le client associé à la ligne
            if (sender is Button button && button.DataContext is Client clientSelectionne)
            {
                // Supprimer le client de la liste
                Clients.Remove(clientSelectionne);
                // Rafraîchir la grille après suppression
                ClientsGrid.Items.Refresh();
            }
        }

        private void SearchClient_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchText = searchClient.Text;
            FilterClients(searchText);
        }

        private void HeaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var client in Clients)
                {
                    client.IsSelected = true;
                }
                ClientsGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void HeaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var client in Clients)
                {
                    client.IsSelected = false;
                }
                ClientsGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void FilterClients(string searchText)
        {
            var filteredClients = Clients.Where(c => c.Nom.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                     c.Adresse.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                     c.Mail.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                                     c.Telephone.Contains(searchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
            ClientsGrid.ItemsSource = filteredClients;
        }
    }
}
