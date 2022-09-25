using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilverWcfConsumer
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...
        }

        ServiceReference1.Service1Client _soapClient =
            new ServiceReference1.Service1Client(
                new CustomBinding(),
                new System.ServiceModel.EndpointAddress(
                    new Uri("http://localhost:65264/Service1.svc")));

        private async Task GetTodosAsync()
        {
            var todos = await _soapClient.GetToDosAsync();
            SoapToDosItemsControl.ItemsSource = todos;
        }
        
        private async Task AddOrUpdateTodosAsync(ServiceReference1.ToDoItem todo)
        {
            await _soapClient.AddOrUpdateToDoAsync(todo);
            await GetTodosAsync();
        }

        private async Task DeleteAsync(ServiceReference1.ToDoItem todo)
        {
            try
            {
                await _soapClient.DeleteToDoAsync(todo);
                await GetTodosAsync();
            }
            catch (System.ServiceModel.FaultException ex)
            {
                // Fault exceptions allow the server to pass information such as "Item not found":
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        
        void ButtonRefreshSoapToDos_Click(object sender, RoutedEventArgs e)
        {
            _ = GetTodosAsync();
        }

        void ButtonAddSoapToDo_Click(object sender, RoutedEventArgs e)
        {
            var todo = new ServiceReference1.ToDoItem()
            {
                Description = SoapToDoTextBox.Text,
                Id = Guid.NewGuid()
            };
            _ = AddOrUpdateTodosAsync(todo);
        }

        void ButtonDeleteSoapToDo_Click(object sender, RoutedEventArgs e)
        {
            var todo = (ServiceReference1.ToDoItem)((Button)sender).DataContext;
            _ = DeleteAsync(todo);
        }
    }
}
