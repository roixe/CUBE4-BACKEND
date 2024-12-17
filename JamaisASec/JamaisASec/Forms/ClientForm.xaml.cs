using System.Windows;
using System.Windows.Controls;

namespace JamaisASec.Forms
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class ClientForm : Window
    {
        private List<Client> Clients { get; set; }
        public bool IsEditMode { get; private set; }
        private Client? ClientEnCours { get; set; }

        public ClientForm(List<Client> clients, Client? clientAModifier = null)
        {
            InitializeComponent();
            Clients = clients ?? [];

            IsEditMode = clientAModifier != null;
            ClientEnCours = clientAModifier;
            this.Title = IsEditMode ? "Modifier un client" : "Ajouter un client";

            if (ClientEnCours != null)
            {
                clientName.Text = ClientEnCours.Nom;
                clientAddress.Text = ClientEnCours.Adresse;
                clientMail.Text = ClientEnCours.Mail;
                clientPhoneNumber.Text = ClientEnCours.Telephone;
            }
        }

        private void FormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) { return; }

            string nom = clientName.Text;
            string adresse = clientAddress.Text;
            string mail = clientMail.Text;
            string telephone = clientPhoneNumber.Text;

            if (ClientEnCours != null)
            {
                ClientEnCours.Nom = nom;
                ClientEnCours.Adresse = adresse;
                ClientEnCours.Mail = mail;
                ClientEnCours.Telephone = telephone;
            }
            else
            {
                Clients.Add(new Client(nom, adresse, mail, telephone));
            }

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
