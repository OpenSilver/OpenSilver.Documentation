# Integrating OpenSilver with .NET Aspire

## Introduction

**OpenSilver** is a modern, cross-platform frontend framework based on .NET and XAML, designed to build powerful and flexible web applications. For comprehensive web solutions, developers often pair OpenSilver frontends with robust backend services. **.NET Aspire** simplifies this integration by providing unified infrastructure for deploying, managing, and scaling .NET backend applications.

This article will guide you through the process of integrating an OpenSilver frontend application with backend services managed by .NET Aspire, leveraging their combined strengths to streamline development and deployment.

---

## What is .NET Aspire?

**.NET Aspire** is a Microsoft infrastructure framework tailored for hosting, deploying, and managing backend .NET applications. It provides:

* Unified configuration and automatic service discovery
* Seamless scaling and load balancing
* Built-in observability (monitoring and diagnostics)
* Robust, resilient service management

With .NET Aspire, developers can focus on business logic while benefiting from modern DevOps practices “out-of-the-box.”

---

## Why Integrate OpenSilver Frontend with .NET Aspire Backend?

### 1. Easy Integration

.NET Aspire streamlines the connection between OpenSilver frontend apps and backend APIs, enabling fast, secure, and reliable communication.

### 2. Simplified Deployment

Frontends and backends can be deployed together, reducing manual setup and ensuring consistent, reproducible environments.

### 3. Automatic Scaling

Backend services scale automatically in response to frontend demand, maintaining performance and responsiveness.

### 4. Built-in Observability

Aspire provides out-of-the-box monitoring, logging, and diagnostics—allowing you to quickly gain insights and diagnose issues.

### 5. Enhanced Reliability

With managed service lifecycles and resilience features, Aspire ensures high backend availability, creating a stable platform for OpenSilver applications.

---

## Example Integration: OpenSilver with .NET Aspire

To illustrate, we’ve developed a practical example, available on [GitHub](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/OpenSilverAndAspire).

### Solution Overview

The example solution consists of:

* **OpenSilverAndAspire.ApiService:** A minimal API service providing weather data
* **OpenSilverAndAspire.Browser:** The OpenSilver (Blazor WebAssembly) app displaying weather data
* **OpenSilverAndAspire.WasmHost:** An ASP.NET Core app hosting the OpenSilver frontend and bridging Aspire configuration
* **OpenSilverAndAspire.AppHost:** The “orchestrator” that coordinates startup and connections between services using Aspire
* **OpenSilverAndAspire.ServiceDefaults:** Aspire service utility code
* **OpenSilverAndAspire:** The core OpenSilver project with frontend logic

---

### WASM Host: Bridging OpenSilver and Aspire

Although OpenSilver is, under the hood, a Blazor WebAssembly application, simply adding it to Aspire as a static web app offers limited benefits. A key feature of Aspire is automatic management of service URLs. To leverage this, we introduce a thin ASP.NET Core hosting layer—**OpenSilverAndAspire.WasmHost**—which serves the OpenSilver app and passes service configuration at runtime.

The WasmHost is responsible for:

* Serving the OpenSilver frontend
* Exposing Aspire configuration to the frontend

---

### AppHost: The Orchestration Layer

The orchestration of projects is handled in `Program.cs` within **OpenSilverAndAspire.AppHost**. Here’s the essence:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder
    .AddProject<Projects.OpenSilverAndAspire_ApiService>("apiservice")
    .WithExternalHttpEndpoints();

builder
    .AddProject<Projects.OpenSilverAndAspire_WasmHost>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
```

This configuration:

* Registers the API service and the WasmHost (frontend)
* Ensures the frontend depends on and waits for the backend API
* Enables Aspire’s dashboard to monitor both services

We can now run the `.AppHost` project and view the configuration in the Aspire dashboard:
![Aspire Dashboard](/images/how-to-topics/aspire-dashboard.png "Aspire Dashboard")

---

### Passing Service Configuration to the Frontend

To call the API from the OpenSilver frontend, the frontend needs to know the backend’s URL. Aspire provides this information to the WasmHost, which then must relay it to the OpenSilver client.

In **OpenSilverAndAspire.WasmHost/Program.cs**, we added the following minimal endpoint:

```csharp
app.MapGet("/aspire-config", (IConfiguration config) => new
{
    services = config.GetSection("services").Get<Dictionary<string, Dictionary<string, string[]>>>()
});
```

This endpoint returns Aspire’s service config, making it accessible to the frontend.

---

### Consuming Configuration in the OpenSilver Browser Project

In **OpenSilverAndAspire.Browser/Program.cs**, read the configuration as follows:

```csharp
var http = new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};
using var responseStream = await http.GetStreamAsync("aspire-config");
builder.Configuration.AddJsonStream(responseStream);
```

This loads service configuration at runtime, enabling dynamic service discovery.

---

### Enabling Service Discovery and HTTP Client Configuration

With the configuration in place, use the `Microsoft.Extensions.ServiceDiscovery` NuGet package:

```csharp
builder.Services.AddServiceDiscovery();
builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddServiceDiscovery();
});

builder.Services.AddHttpClient("apiservice", client =>
{
    client.BaseAddress = new Uri("https://apiservice");
});
```

Now, calling your backend is simple:

```csharp
var httpClient = host.Services.GetRequiredService<IHttpClientFactory>().CreateClient("apiservice");
var result = await httpClient.GetStringAsync("weatherforecast");
```

You can verify in browser dev tools that requests are automatically routed to the correct backend URL.

---

### Handling CORS in ApiService

A common issue at this stage is **CORS** (Cross-Origin Resource Sharing). For simplicity, in this example, all origins are allowed (in production, restrict origins for security):

**OpenSilverAndAspire.ApiService/Program.cs:**

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
          .AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});
```

And:

```csharp
app.UseCors("AllowAll");
```

Now, requests from the frontend will succeed.

![Weather Forecast](/images/how-to-topics/weather-forecast.png "Weather Forecast")
---

### Alternative: Proxy Approach for CORS

Instead of enabling CORS on the API, another approach is to route all API calls through the ASP.NET Core host (WasmHost). This way, the frontend calls the same origin as itself, and the host proxies requests to the backend API, sidestepping CORS altogether. This can improve security and simplify frontend code.

---

## Conclusion

Integrating OpenSilver with .NET Aspire unlocks a productive workflow for building, deploying, and operating modern .NET web applications. Aspire’s powerful service orchestration, configuration management, and observability features simplify backend management—while OpenSilver enables you to deliver rich, responsive web frontends.

Try the [sample project](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/OpenSilverAndAspire) to get started, and bring your .NET solutions to the next level!

---

**Feedback, questions, or suggestions?** [Open an issue on GitHub](https://github.com/OpenSilver/OpenSilver/issues) or join the community discussion.

---

**Further Reading:**

* [.NET Aspire documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
* [OpenSilver documentation](https://doc.opensilver.net/documentation/general/overview.html)
