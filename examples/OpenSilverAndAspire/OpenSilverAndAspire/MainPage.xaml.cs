using Microsoft.Extensions.DependencyInjection;
using OpenSilverAndAspire.Models;
using OpenSilverAndAspire.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilverAndAspire
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...
            LoadWeatherForecast();
        }

        private async void LoadWeatherForecast()
        {
            // Show loading and hide error
            LoadingPanel.Visibility = Visibility.Visible;
            ErrorPanel.Visibility = Visibility.Collapsed;
            WeatherItemsControl.ItemsSource = null;

            try
            {
                var httpClient = ServiceLocator.Provider.GetRequiredService<IHttpClientFactory>().CreateClient("apiservice");
                var result = await httpClient.GetStringAsync("weatherforecast");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var forecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(result, options);


                WeatherItemsControl.ItemsSource = forecasts;
                LoadingPanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                LoadingPanel.Visibility = Visibility.Collapsed;
                ErrorPanel.Visibility = Visibility.Visible;
                ErrorMessageText.Text = $"Could not load weather data: {ex.Message}";
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadWeatherForecast();
        }
    }
}
