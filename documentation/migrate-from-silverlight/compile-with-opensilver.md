
# Compiling with OpenSilver

**Note:** This section assumes you plan to perform the migration independently. However, for those who prefer a quicker, more cost-effective migration process, Userware—the company behind OpenSilver—offers expert migration services. This enables your team to focus on other priorities while Userware handles the migration seamlessly. For more details, visit [OpenSilver.net](https://opensilver.net).

## Prerequisites

Before starting the migration process to OpenSilver, ensure the following prerequisites are met:

1. **Complete the Environment Setup**: 
   Ensure your development environment is properly set up. Follow the instructions provided in the "[Environment Setup](environment-setup.md)" guide to install the required tools and dependencies to work with OpenSilver.

2. **Obtain Full Source Code**: 
   You must have access to the complete source code of the legacy application that you intend to migrate. The migration process requires that all source files from the original application are available.

3. **Ensure the Application Compiles Locally**: 
   Verify that the application compiles successfully on your local development machine. This includes confirming that all projects in the solution are loaded correctly and that all referenced libraries are properly resolved. Refer to the "[Environment Setup](environment-setup.md)" page for a detailed list of required software and tools to ensure a smooth compilation process.

4. **Verify Database Connection Strings**: 
   If your application interacts with a database, confirm that the connection strings are accurate:
   - Open the **Server Explorer** tab in Visual Studio.
   - Right-click on **Data Connections** and select **Add Connection…**.
   - Enter the correct server name and select the database, then click **OK**.
   - To view the connection string, check the properties of the database.

5. **Local Execution of Legacy Application**: 
   Although it is not mandatory to run the application on your local machine during the early stages of migration, doing so will be beneficial later on during the process.

## Migration Process Overview

Migrating an application to OpenSilver involves several key steps. The general approach is to create an OpenSilver-specific project for each original project, transfer the source files, and successfully compile the solution. 

Instead of copying files directly, we recommend sharing the files between the original application and the new OpenSilver project. This approach allows for easier maintenance of both versions during the migration, and facilitates quick updates and bug fixes to the application if needed.

To ensure the migration process runs smoothly, avoid manual edits to the **.sln** and **.csproj** (or **.vbproj** or **.fsproj**) files by following these streamlined steps:

### Step-by-Step Instructions

1. **Create the OpenSilver Application**: 
   - Create a new OpenSilver application with the same name as the original application, but place it in a separate directory.

2. **Add OpenSilver Projects**: 
   - For each project in the original solution, add a corresponding OpenSilver project with the same name.
   - If the project is a **Silverlight/WPF Class Library**, create an equivalent **OpenSilver Class Library** project.

3. **Maintain Directory Structure**: 
   - The directory structure in the OpenSilver solution should match the original solution to maintain consistency.

4. **Rename Solution and Projects**: 
   - Rename the OpenSilver solution and associated projects by appending **.OpenSilver** to their names. This will differentiate the migrated OpenSilver version from the original version.

5. **Remove the ".OpenSilver" Suffix from Assembly Names**: 
   - To prevent breaking the **xmlns** references in XAML files, remove **.OpenSilver** from the **Assembly name**:
     - Right-click the project in **Solution Explorer** and select **Properties**.
     - In the **Properties** window, modify the **Assembly name** by removing the **.OpenSilver** suffix.

6. **Copy the Project Files**: 
   - Copy the newly created **.sln** and **.csproj** (or **.vbproj** or **.fsproj**) files from the OpenSilver solution to their corresponding locations in the project folder.

7. **Migrate RIA Services to OpenRiaServices (For Silverlight Application)**:  
   - If the original application uses RIA Services, migrate it to **[OpenRiaServices](../3rd-party-libraries/ria-services.md)**. This is necessary because OpenSilver does not support the original RIA Services, and OpenRiaServices provides a compatible alternative for server-side data access in OpenSilver applications.

8. **Convert Businesslogic to WCF Service/WebAPI (For WPF Application)**: 
   - WPF Applications often include business logic that needs to be converted to a WCF Service or WebAPI. This step is crucial for ensuring that the application can communicate with the server-side logic effectively in the OpenSilver environment.

>Note: If you are using VB.NET then you need to set TargetFramework to .NET7 or later. then only vb.net syntax will be supported in OpenSilver. To do this, right-click on the project in **Solution Explorer**, select **Properties**, and change the **Target Framework** to **.NET 7** or later.


For further assistance, you can refer to this [example](example.md).

## Anticipated Challenges During Migration

While migrating a application to OpenSilver, some compilation errors are to be expected. OpenSilver currently supports only a subset of functionality, so manual interventions will likely be necessary. The following sections outline common issues and offer solutions to address them.

### Errors Related to .xaml.cs (or .xaml.vb or .xaml.fs) Files

Many applications include **.xaml** files, which can result in various compilation errors during migration, particularly when UI controls are involved. A common issue is errors during C# code migration, such as:

![XAML Errors](/images/XamlErrors.png "XAML Errors")

These errors occur when **.g.i.cs** files are not generated. To resolve this issue:

1. Right-click the **.xaml** file and open its properties (press **F4**).
2. In the **Advanced** section, ensure the following settings:
   ```
   Build Action: Content
   Custom Tool: MSBuild:Compile
   ```

![Build Action](/images/BuildAction.png "Build Action")

If your project includes a **Generic.xaml** file for default styles, follow the same steps to avoid issues with missing styles. Although the project may compile successfully, UI components might not render as expected. Be sure to apply these steps to both **Generic.xaml** and other **.xaml** files in the project.

### Ensuring Consistent Build Configurations

In large projects, it is common to encounter inconsistent build configurations or the use of multiple compiler directives. This can lead to confusing compilation errors. To prevent this:

- Compare the build configurations between the original project and the OpenSilver project, ensuring that the compiler directives and build settings are aligned.

### Known Issues and Troubleshooting

For a detailed list of known issues and solutions, refer to the "[Troubleshooting](../troubleshooting/common-issues-and-solutions.md)" page.

## Using Compiler Directives to Ensure Compatibility with the Original Application

### Modifications in C# (or VB.NET or F#) Files

When modifying C# (or VB.NET or F#) files, use the **#if OPENSILVER** and **#if !OPENSILVER** compiler directives to differentiate between code specific to the OpenSilver version and the original version. This allows for easy identification of modifications and ensures that changes made in OpenSilver do not interfere with the functionality of the original application.

If OpenSilver does not yet implement certain required classes or methods, a temporary solution is to create empty classes or methods in the OpenSilver project to ensure the application compiles correctly.

### Modifications in XAML Files

Since compiler directives cannot be applied directly within XAML files, we recommend the following strategies to make changes to XAML code without affecting the original application:

- For small changes, modify the code-behind file (C#, VB.NET, or F#) instead of editing the XAML directly. Add an **x:Name="SOME_NAME"** to the control in XAML, then make the change in the corresponding code-behind file. Use **#if OPENSILVER** directives to ensure the changes only affect the OpenSilver version.
  
- For larger or more complex changes, duplicate the XAML file and reference the new file within the OpenSilver project. The original Silverlight project can continue using the original XAML file. For example, if you modify **App.xaml**, create a copy named **App.OpenSilver.xaml** and reference this new file in the OpenSilver project.

### Detailed Steps for Creating and Using a New XAML File
---
#### 1. Copy the Name of the XAML File to Duplicate

![Copy XAML file](/images/xaml_copy/1.png "Copy XAML file")

#### 2. Add a New Item to the OpenSilver Project

In **Solution Explorer**, right-click the folder where the file is located and select **Add -> New Item...**

![Add new item](/images/xaml_copy/2.png "Add new item")

#### 3. Choose the Appropriate Control Type

Select the control type that corresponds to the Silverlight control. If a matching type is not available, create a new control and modify it as necessary.

![Choose control type](/images/xaml_copy/3.png "Choose control type")

Ensure to rename the new control to match the desired name for the OpenSilver XAML file (e.g., **MyUserControl.OpenSilver.xaml**).

#### 4. Update the Namespace

Modify the namespace in the newly created **.xaml** and **.xaml.cs** (or **.xaml.vb** or **.xaml.fs**) files to match the original code's namespace.

![Change namespace](/images/xaml_copy/4.png "Change namespace")
![Change namespace](/images/xaml_copy/5.png "Change namespace")

#### 5. Exclude the Original Components from the Project

Exclude the original **.xaml** and **.cs** (or **.vb** or **.fs**) files from the project, as the OpenSilver version will now be used. Right-click the file in **Solution Explorer** and choose **Exclude from Project**.

![Exclude project](/images/xaml_copy/6.png "Exclude project")

#### 6. Viewing Excluded Files

To view files that are no longer part of the project, click the **Show All Files** button in **Solution Explorer**.

![Show all files](/images/xaml_copy/8.png "Show all files")

It is also possible to retain the original **.cs** (or **.vb** or **.fs**) files and share them between both the Silverlight and OpenSilver projects. However, this will cause the file to appear detached from the XAML file in **Solution Explorer**.

![Share .cs file](/images/xaml_copy/9.png "Share .cs file")

---

## Assign Class and Name to FrameworkElement in OpenSilver

You can enable a built-in feature in OpenSilver that automatically assigns the `class` and `data-id` attributes to `FrameworkElement` DOM elements, which is useful for styling, testing, or debugging.

---
- `AssignClass`: Assigns the **CSS class** (`class` attribute) based on the element type.
- `AssignName`: Assigns a **data-id** (`data-id` attribute) using the element’s `Name`.
---

In your OpenSilver project, modify the `App.xaml.cs` file and enable the flags directly inside the `App` constructor:

```csharp
public partial class App : Application
{
    public App()
    {
        // Enable DOM feature flags
        OpenSilver.Features.DOM.AssignClass = true;
        OpenSilver.Features.DOM.AssignName = true;
   
        InitializeComponent();
    }
}
```

**Example**:

When you're using OpenSilver and want to prevent users from selecting text inside all TextBlock elements, you can achieve this by adding a specific CSS rule to the index.html file only after enabling both of the above flags:

```html
<style> 
  .System\.Windows\.Controls\.TextBlock { 
      -moz-user-select: none; 
      -ms-user-select: none; 
      -webkit-user-select: none; 
      user-select: none; 
  } 
</style>
```



## Using `app.config` in OpenSilver Projects

OpenSilver does not natively support `.NET`'s `ConfigurationManager`. However, you can implement a simple custom configuration system by embedding an `app.config` file and reading it through a helper class.

---

**Create the Configuration Folder**

In your project:

- Create a folder named **`Opensilver`**.
- Inside it, add a new C# class named `ConfigurationManager.cs`.

---

**Add the Custom `ConfigurationManager` Class**

Paste the following code into the `ConfigurationManager.cs` file:

```csharp
using System.Collections.Generic;
using System.Xml.Linq;

namespace YourProjectNamespace.Opensilver
{
    public static class ConfigurationManager
    {
        static ConfigurationManager()
        {
            AppSettings = new Dictionary<string, string>();
            ReadSettings();
        }

        public static Dictionary<string, string> AppSettings { get; private set; }

        private static void ReadSettings()
        {
            var resourceStream = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("YourProjectName.app.config"); // Replace with actual project name

            using (var stream = new System.IO.StreamReader(resourceStream))
            {
                var document = XDocument.Load(stream);

                foreach (var element in document
                    .Descendants("appSettings")
                    .Elements("add"))
                {
                    var key = element.Attribute("key")?.Value;
                    var value = element.Attribute("value")?.Value;

                    if (!string.IsNullOrEmpty(key) && value != null)
                    {
                        AppSettings[key] = value;
                    }
                }
            }
        }
    }
}
```

> **Replace** `"YourProjectName.app.config"` with the actual name of your embedded resource file.  
> Usually it's in the format: **`<DefaultNamespace>.app.config`**

---

**Add and Embed the `app.config` File**

1. In the root of your project, create an XML file named **app.config**.

2. Right-click on the file → **Properties** → Set:
```
   Build Action: Embedded Resource
```
Example `app.config` File

```xml
<configuration>
  <appSettings>
    <add key="ApiUrl" value="https://api.example.com" />
    <add key="Theme" value="Dark" />
  </appSettings>
</configuration>
```

---

**Access Configuration Settings in Code**

You can now access settings from anywhere in your application like this:

```csharp
string apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
string theme = ConfigurationManager.AppSettings["Theme"];
```

---

## Migrating Duplex Service to OpenSilver Using SignalR

To migrate the **Duplex Service** to an **OpenSilver** project, WCF's duplex communication needs to be replaced with **SignalR**, since WCF duplex channels aren't supported in WebAssembly. The process involves:

1. **Creating a stub client class** replicating the original interface.
2. **Cloning and modifying** types such as `DuplexServiceClient` and `DuplexServiceClientChannel`.
3. **Re-implementing communication** via SignalR to maintain real-time capabilities.

---

**Create a Stub Client Class**

Define a client class with equivalent method signatures and event definitions to mirror the original WCF-based implementation:

```csharp
public class DuplexServiceClient
{
    public string ServerURI { get; set; }
    public DuplexServiceClientChannel InnerChannel { get; internal set; }
    public CommunicationState State { get; internal set; }

    public event EventHandler<NotifyReceivedEventArgs> NotifyReceived;
    public event EventHandler<AsyncCompletedEventArgs> CloseCompleted;

    public DuplexServiceClient(string uri)
    {
        ServerURI = uri;
        InnerChannel = new DuplexServiceClientChannel(this);
    }

    internal void Abort()
    {
        InnerChannel.Abort();
    }

    internal void OnNotifyReceived(NotifyReceivedEventArgs e)
    {
        NotifyReceived?.Invoke(this, e);
    }

    internal void OnClosed()
    {
        CloseCompleted?.Invoke(this, new AsyncCompletedEventArgs(null, false, null));
    }
}
```

---

**Implement SignalR-Based Channel**

Replace the WCF channel with a SignalR-based implementation to support two-way messaging:

```csharp
public class DuplexServiceClientChannel : IDisposable
{
    private HubConnection _connection;
    private readonly DuplexServiceClient _client;

    public EventHandler<EventArgs> Faulted { get; internal set; }

    public DuplexServiceClientChannel(DuplexServiceClient client)
    {
        _client = client;

        _connection = new HubConnectionBuilder()
            .WithUrl($"{client.ServerURI}/duplexService", options =>
            {
                options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
            })
            .ConfigureLogging(logging =>
            {
                logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Debug);
                logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Debug);
            })
            .WithAutomaticReconnect()
            .WithStatefulReconnect()
            .WithKeepAliveInterval(TimeSpan.FromSeconds(15))
            .Build();

        _connection.On<string>("Notify", message =>
        {
            client.OnNotifyReceived(new NotifyReceivedEventArgs { message = message });
        });

        _connection.Reconnecting += OnReconnecting;
        _connection.Reconnected += OnReconnected;
        _connection.Closed += OnClosed;

        _ = _connection.StartAsync();
    }

    private Task OnReconnecting(Exception ex)
    {
        Console.WriteLine("Connection reconnecting: " + ex);
        return Task.CompletedTask;
    }

    private Task OnReconnected(string connectionId)
    {
        Console.WriteLine("Connection reconnected: " + connectionId);
        return Task.CompletedTask;
    }

    private Task OnClosed(Exception ex)
    {
        Console.WriteLine("Connection closed: " + ex);

        if (ex != null)
        {
            _client.State = CommunicationState.Faulted;
            Faulted?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            _client.State = CommunicationState.Closed;
            _client.OnClosed();
        }

        return Task.CompletedTask;
    }

    public void Abort()
    {
        _ = _connection.StopAsync();
    }

    public void Dispose()
    {
        _ = _connection.DisposeAsync();
    }
}
```

---

>**Notes**
>- **Callback Contracts**: Replicate original callback contracts and interface signatures.
>- **Custom Arguments**: Ensure `NotifyReceivedEventArgs` and similar types are also ported.
>- **Production Considerations**: Include exception handling, reconnection policies, and security configurations for a robust deployment.

This migration approach ensures compatibility with existing client logic while transitioning to a modern, WebAssembly-compatible architecture using SignalR.

## Browser Project Web.config Configuration

This guide explains how to set up a `web.config` file with compression and URL rewrite rules for your browser project. It also describes how to ensure that the `web.config` file from a `Configs` folder is copied to the published directory during the publish process.

**Create a `Configs` Folder**

- In the root directory of your project, create a folder named `Configs`.
- Inside the `Configs` folder, place the `web.config` file, which includes the necessary configurations for compression and URL rewriting.

**Example `web.config` File**

Below is an example of a `web.config` file that includes HTTP compression and URL rewrite rules.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
	<system.webServer>
		<staticContent>
			<remove fileExtension=".blat" />
			<remove fileExtension=".dat" />
			<remove fileExtension=".dll" />
			<remove fileExtension=".webcil" />
			<remove fileExtension=".json" />
			<remove fileExtension=".wasm" />
			<remove fileExtension=".woff" />
			<remove fileExtension=".woff2" />
			<mimeMap fileExtension=".blat" mimeType="application/octet-stream" />
			<mimeMap fileExtension=".dll" mimeType="application/octet-stream" />
			<mimeMap fileExtension=".webcil" mimeType="application/octet-stream" />
			<mimeMap fileExtension=".dat" mimeType="application/octet-stream" />
			<mimeMap fileExtension=".json" mimeType="application/json" />
			<mimeMap fileExtension=".wasm" mimeType="application/wasm" />
			<mimeMap fileExtension=".woff" mimeType="application/font-woff" />
			<mimeMap fileExtension=".woff2" mimeType="application/font-woff" />
		</staticContent>
		<httpCompression>
			<dynamicTypes>
				<add mimeType="application/octet-stream" enabled="true" />
				<add mimeType="application/wasm" enabled="true" />
			</dynamicTypes>
		</httpCompression>
		<rewrite>
			<rules>
				<!-- Add Custom Rules Here -->
				<rule name="Serve subdir">
					<match url=".*" />
					<action type="Rewrite" url="wwwroot\{R:0}" />
				</rule>
				<rule name="SPA fallback routing" stopProcessing="true">
					<match url=".*" />
					<conditions logicalGrouping="MatchAll">
						<add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
					</conditions>
					<action type="Rewrite" url="wwwroot\" />
				</rule>
			</rules>
		</rewrite>
	</system.webServer>
</configuration>
```
---

## Use HttpClient Instead of WebClient in Blazor WebAssembly

Blazor WebAssembly, the WebClient class is not supported because it depends on .NET libraries that require full access to the operating system—something that’s not possible in the browser's sandboxed environment.

The recommended alternative is HttpClient, which is fully supported in Blazor WebAssembly and designed for use in browser-based applications.

## Adding support for cross-domain calls (CORS) 

Refer [link](../in-depth-topics/wcf-and-webclient.md) below for adding support for CORS. 

## VirtualizingStackPanel and Inconsistent Item Heights
When using a VirtualizingStackPanel, especially in scenarios like a ListBox or ListView, you may encounter inconsistent item heights. This inconsistency can cause:

- Jumping or jittery scrolling behavior
- Inaccurate scrolling calculations, leading to items being skipped or the viewport showing empty space.

The VirtualizingStackPanel.ScrollAmount property fine-tunes scrolling behavior in virtualized lists with variable item heights.