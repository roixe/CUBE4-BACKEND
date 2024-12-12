using System.Windows.Controls;

namespace JamaisASec
{
    /// <summary>
    /// Interaction logic for PageClients.xaml
    /// </summary>
    public partial class PageClients : Page
    {
        public PageClients(List<Client> clients)
        {
            InitializeComponent();
            ClientsGrid.ItemsSource = clients;
        }

    }
}
