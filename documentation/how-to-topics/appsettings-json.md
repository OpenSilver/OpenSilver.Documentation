## How to add Application Settings

Blazor supports the file appsettings.json out of the box. As a result, you can use it for your migrations too.

Steps:

1. Add appsettings.json in the "wwwroot" folder. During the build, Blazor detects this file and loads it automatically.
2. Change Pages/index.cs to the following:
```
using DotNetForHtml5;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OpenSilverApplication.Browser.Interop;

namespace OpenSilverApplication.Browser.Pages
{
    [Route("/")]
    public class Index : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Cshtml5Initializer.Initialize(new UnmarshalledJavaScriptExecutionHandler(JSRuntime));
            Program.RunApplication(Configuration);
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private IConfiguration Configuration { get; set; }
    }
}
```
The difference is a new property Configuration.

3. Change program.cs to the following:
```
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace OpenSilverApplication.Browser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var host = builder.Build();
            await host.RunAsync();
        }

        public static void RunApplication(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Application.RunApplication(() =>
            {
                var app = new OpenSilverApplication46.App(configuration);
            });
        }
    }
}
```
4. Add the ["Microsoft.Extensions.Configuration.Abstractions"](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Abstractions/) package to the OpenSilver class library project.
5. Add a constructor to App.xaml.cs and handle logic with your Configuration there:
```
public App(Microsoft.Extensions.Configuration.IConfiguration configuration)
{
    this.InitializeComponent();

    // Enter construction logic here...
    // Handle your configuration somehow here

    var mainPage = new MainPage();
    Window.Current.Content = mainPage;
}

public App() : this(null)
{
}
```

The working example is available [here](https://github.com/jacob-l/OpenSilverApplicationAppSettings).