using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JamaisASec.Forms
{
    /// <summary>
    /// Logique d'interaction pour AjouterFournisseur.xaml
    /// </summary>
    public partial class AjouterFournisseur : Window
    {
        private List<Fournisseur> Fournisseurs { get; set; }
        public AjouterFournisseur(List<Fournisseur> fournisseurs)
        {
            InitializeComponent();
            Fournisseurs = fournisseurs ?? new List<Fournisseur>();
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) { return; }

            string nom = fournisseurName.Text;
            string adresse = fournisseurAddress.Text;
            string mail = fournisseurMail.Text;
            string telephone = fournisseurPhoneNumber.Text;
            string siret = fournisseurSIRET.Text;

            Fournisseurs.Add(new Fournisseur(nom, adresse, mail, telephone, siret));

            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(fournisseurName.Text))
            {
                fournisseurName.ErrorMessage = "Veuillez entrer un nom.";
                isValid = false;
            }
            else
            {
                fournisseurName.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(fournisseurAddress.Text))
            {
                fournisseurAddress.ErrorMessage = "Veuillez entrer une adresse.";
                isValid = false;
            }
            else
            {
                fournisseurAddress.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(fournisseurMail.Text))
            {
                fournisseurMail.ErrorMessage = "Veuillez entrer une adresse e-mail.";
                isValid = false;
            }
            else
            {
                fournisseurMail.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(fournisseurPhoneNumber.Text))
            {
                fournisseurPhoneNumber.ErrorMessage = "Veuillez entrer un numéro de téléphone.";
                isValid = false;
            }
            else
            {
                fournisseurPhoneNumber.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(fournisseurSIRET.Text))
            {
                fournisseurSIRET.ErrorMessage = "Veuillez entrer un numéro de SIRET.";
                isValid = false;
            }
            else
            {
                fournisseurSIRET.ErrorMessage = string.Empty;
            }


            return isValid;
        }
    }
}
