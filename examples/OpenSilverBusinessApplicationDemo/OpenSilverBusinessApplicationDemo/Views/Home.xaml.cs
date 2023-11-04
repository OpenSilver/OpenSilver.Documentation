using System.Windows.Controls;
using System.Windows.Navigation;
using OpenSilverBusinessApplicationDemo.Web;

namespace OpenSilverBusinessApplicationDemo.Views
{
    public partial class Home : Page
    {
        private readonly OrganizationContext _context = new OrganizationContext();

        public Home()
        {
            InitializeComponent();

            dataGridCustomers.ItemsSource = _context.Customers;
            _context.Load(_context.GetCustomersQuery());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}