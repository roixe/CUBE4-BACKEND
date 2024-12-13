using System.Windows;
using System.Windows.Controls;

namespace JamaisASec.Forms
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class AjouterClientForm : Window
    {
        public Client? ClientAjoute { get; private set; } // Allow ClientAjoute to be nullable
        private List<Client> Clients { get; set; }

        public AjouterClientForm(List<Client> clients)
        {
            InitializeComponent();
            Clients = clients ?? new List<Client>();
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) { return; }

            string nom = clientName.Text;
            string adresse = clientAddress.Text;
            string mail = clientMail.Text;
            string telephone = clientPhoneNumber.Text;

            ClientAjoute = new Client(nom, adresse, mail, telephone);
            Clients.Add(ClientAjoute);

            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(clientName.Text))
            {
                clientName.ErrorMessage = "Ce champ est obligatoire.";
                isValid = false;
            }
            else
            {
                clientName.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(clientAddress.Text))
            {
                clientAddress.ErrorMessage = "Ce champ est obligatoire.";
                isValid = false;
            }
            else
            {
                clientAddress.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(clientMail.Text))
            {
                clientMail.ErrorMessage = "Ce champ est obligatoire.";
                isValid = false;
            }
            else
            {
                clientMail.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(clientPhoneNumber.Text))
            {
                clientPhoneNumber.ErrorMessage = "Ce champ est obligatoire.";
                isValid = false;
            }
            else
            {
                clientPhoneNumber.ErrorMessage = string.Empty;
            }

            return isValid;
        }

    }
}
