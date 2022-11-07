using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OpenSilver;

namespace OpenSilverTypeScriptJsUrl
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...
            this.Loaded += MainPage_Loaded;
        }
        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the JavaScript library:
            await Interop.LoadJavaScriptFile("ms-appx:///OpenSilverTypeScriptJsUrl/js-url.js");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var hostname = js_url.js_urlClass.url.Invoke("hostname");
            MessageBox.Show("Current hostname: " + hostname);
        }
    }
}
