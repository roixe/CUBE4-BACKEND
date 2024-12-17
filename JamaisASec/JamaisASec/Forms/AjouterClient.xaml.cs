using System.Windows;
using System.Windows.Controls;

namespace JamaisASec.Forms
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class AjouterClient : Window
    {
        private List<Client> Clients { get; set; }

        public AjouterClient(List<Client> clients)
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

            Clients.Add(new Client(nom, adresse, mail, telephone));

            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(clientName.Text))
            {
                clientName.ErrorMessage = "Veuillez entrer un nom.";
                isValid = false;
            }
            else
            {
                clientName.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(clientAddress.Text))
            {
                clientAddress.ErrorMessage = "Veuillez entrer une adresse.";
                isValid = false;
            }
            else
            {
                clientAddress.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(clientMail.Text))
            {
                clientMail.ErrorMessage = "Veuillez entrer une adresse e-mail.";
                isValid = false;
            }
            else
            {
                clientMail.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(clientPhoneNumber.Text))
            {
                clientPhoneNumber.ErrorMessage = "Veuillez entrer un numéro de téléphone.";
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
