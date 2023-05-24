# How to Enable Windows Authentication on OpenSilver App

To enable windows authentication on Opensilver Application update `"windowsAuthentication": true,` and `"anonymousAuthentication": false,` under `iisSettings` section in `launchSettings.json`.

Update `launchSettings.json` with the following content:
```
{
  "iisSettings": {
    "windowsAuthentication": true,
    "anonymousAuthentication": false,
    "iisExpress": {
      "applicationUrl": "http://localhost:55591/",
      "sslPort": 0
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "WindowsAuthSample.Browser": {
      "commandName": "Project",
      "launchBrowser": true,
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "http://localhost:55592/"
    }
  }
}
```
### IIS Settings

Double click on Authentication in IIS.

<img src="/images/how-to-topics/windows-auth1.png" alt="windows" title="IIS-settings.png" style="border: 2px solid #555;" /><br />

Make sure only "Basic authentication" is enabled.

<img src="/images/how-to-topics/windows-auth2.png" alt="windows" title="IIS-settings.png" style="border: 2px solid #555;" /><br />

### Access UserName

Add the following packages to the project:

<img src="/images/how-to-topics/windows-auth3.png" alt="windows" title="Required-packages.png" style="border: 2px solid #555;" /><br />

Change program.cs to the following:

```
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace WindowsAuthSample.Browser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // Add this code
            builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
            // --------------------------------------
            var host = builder.Build();
            await host.RunAsync();
        }

        public static void RunApplication()
        {
            Application.RunApplication(() =>
            {
                var app = new WindowsAuthSample.App();
            });
        }
    }
}

```

Add following to the `web.config` file of project:
```
  <system.web>
    <authorization>
        <allow users ="*"/>
    </authorization>
     <authentication mode="Windows" />
    <identity impersonate="true" />
    <httpModules>
      <add name="DomainServiceModule" type="OpenRiaServices.DomainServices.Hosting.DomainServiceHttpModule, OpenRiaServices.DomainServices.Hosting" />
    </httpModules>
    <compilation debug="true" targetFramework="4.5.1">
      <assemblies>
        <add assembly="System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
      </assemblies>
    </compilation>
    <pages controlRenderingCompatibilityVersion="4.0" />
  </system.web>
```
Using DomainService

To access the username in the DomainService, run this code:

```
    using OpenRiaServices.DomainServices.EntityFramework;
    public partial class DomainService : UserDomainService<>
    {
        .
        .
        var user = this.DomainServiceContext.User.Identity.Name;
        .
        .
    }

```