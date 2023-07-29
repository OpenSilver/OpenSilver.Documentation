using System.Windows;
using System.Windows.Controls;

namespace OpenSilverSeleniumDemo
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewItem.Text)) return;

            listItems.Items.Add(txtNewItem.Text.Trim());
        }
    }
}