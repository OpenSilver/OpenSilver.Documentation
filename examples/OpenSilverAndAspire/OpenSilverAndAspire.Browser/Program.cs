using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSilverAndAspire.Services;

namespace OpenSilverAndAspire.Browser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Add Configuration from the Server
            var http = new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
            using var responseStream = await http.GetStreamAsync("aspire-config");
            builder.Configuration.AddJsonStream(responseStream);

            builder.Services.AddServiceDiscovery();
            builder.Services.ConfigureHttpClientDefaults(http =>
            {
                http.AddServiceDiscovery();
            });

            builder.Services.AddHttpClient("apiservice", client =>
            {
                client.BaseAddress = new Uri("https://apiservice");
            });

            var host = builder.Build();

            ServiceLocator.Initialize(host.Services);

            await host.RunAsync();
        }
    }
}
