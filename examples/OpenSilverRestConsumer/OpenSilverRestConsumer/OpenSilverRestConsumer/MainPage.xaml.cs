using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilverRestConsumer
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...
        }

        const string BaseUrl = "https://localhost:7037";
        private static readonly HttpClient Client = new HttpClient();

        private async Task DownloadAsync()
        {
            using (var response = await Client.GetAsync(BaseUrl + "/ToDo"))
            using (var content = response.Content)
            {
                var json = await content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var items = JsonSerializer.Deserialize<IEnumerable<ToDoItem>>(json, options).ToList();
                RestToDosItemsControl.ItemsSource = items;
            }
        }

        private async Task AddAsync(ToDoItem toDoItem)
        {
            var content = new StringContent(JsonSerializer.Serialize(toDoItem), Encoding.UTF8,
                "application/json");

            await Client.PostAsync(BaseUrl + "/ToDo", content);
            await DownloadAsync();
        }

        private async Task DeleteAsync(ToDoItem toDoItem)
        {
            await Client.DeleteAsync(BaseUrl + "/ToDo/" + toDoItem.Id);
            await DownloadAsync();
        }

        void ButtonRefreshRestToDos_Click(object sender, RoutedEventArgs e)
        {
            _ = DownloadAsync();
        }

        void ButtonAddRestToDo_Click(object sender, RoutedEventArgs e)
        {
            var toDoItem = new ToDoItem
            {
                Id = Guid.NewGuid(),
                Description = RestToDoTextBox.Text
            };

            _ = AddAsync(toDoItem);
        }

        void ButtonDeleteRestToDo_Click(object sender, RoutedEventArgs e)
        {
            var todo = (ToDoItem)((Button)sender).DataContext;
            _ = DeleteAsync(todo);
        }

        public class ToDoItem
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public string Description { get; set; }
        }
    }
}
