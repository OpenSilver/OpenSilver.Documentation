# MAUI Hybrid Launcher for OpenSilver

Starting with **OpenSilver 3.2**, you can now run your OpenSilver applications not only in the browser but also as semi-native apps on **macOS, Windows, Android, and iOS**. Additionally, you can publish these apps to major app stores, including: **Google Play, Apple App Store, and Windows Store**. This multi-platform capability is enabled by our support for the new **MAUI Hybrid Launcher**.

---

## Overview

The MAUI Hybrid Launcher bridges OpenSilver with the native capabilities provided by .NET MAUI.

This means you get the best of both worlds:

- **Web-based UI:** Develop with familiar OpenSilver paradigms. The UI is rendered via a WebView, ensuring pixel-perfect consistency accross all platforms. An additional benefit of the WebView is the ability to mix and match XAML and HTML/JS-based UI components, allowing to leverage the large JS ecosystem in addition to the C#/XAML one.
- **Native integration:** Leverage platform-specific features and publish to app stores. The C# code is compiled to native, allowing to call native platform APIs without the need for interops.

It is the same approach that is used by Blazor Hybrid, except that the UI code is XAML/C# instead of HTML/CSS/C#.

---

## Prerequisites

Before you get started, ensure you have the following installed:

1. **Visual Studio 2022 or later** with the ".NET Multi-platform App UI development" component installed.
   ![Visual Studio 2022 MAUI Component](/images/how-to-topics/visual-studio-2022-maui-component.png)

2. One of the following OpenSilver tools:

   - [OpenSilver VSIX for Visual Studio 2022 (or later)](https://forms.opensilver.net/download.aspx)
   - [OpenSilver VSIX for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=userware.vscode-opensilver)
   - [CLI OpenSilver.Templates package](https://www.nuget.org/packages/OpenSilver.Templates)

---

## Creating an OpenSilver Application with MAUI Hybrid Launcher

You can create a new OpenSilver application with the MAUI Hybrid Launcher using either the command line, Visual Studio 2022, or Visual Studio Code.

### 1. Via Command Line

Open your terminal and run one of the following commands:

- **For all supported MAUI Platforms:**

  ```bash
  dotnet new opensilverapp -o MyAppName --mauiPlatforms all
  ```

- **For specific MAUI Platforms (e.g., Android and Windows):**

  ```bash
  dotnet new opensilverapp -o MyAppName --mauiPlatforms android windows
  ```

> **Note:** The Browser launcher is always included by default.

After running the command, a new OpenSilver application will be generated with an additional `.MauiHybrid` launcher project. You can then open and continue developing your application in your favorite IDE.

### 2. Via Visual Studio 2022

1. Install the OpenSilver extension.
2. Start by creating a new OpenSilver project.
3. In the project creation wizard, you will see an additional configuration panel to select the desired MAUI Platforms.
4. Choose the platforms you wish to target and complete the wizard.

### 3. Via Visual Studio Code

1. Install the OpenSilver extension.
2. Initiate the creation of a new application.
3. When prompted, select the MAUI Platforms you want to include.
4. If you choose not to select any platforms, the generated OpenSilver application will not include the MAUI Hybrid Launcher.
   ![Create a new OpenSilver application in VS Code](/images/how-to-topics/visual-studio-code-new-with-maui.png)

---

## Running the MAUI Hybrid Launcher

To run your OpenSilver application using the MAUI Hybrid Launcher, follow these steps:

- **Set the Startup Project:** Choose the `.MauiHybrid` launcher as the startup project in your solution.
- **Select the Target Platform:** Ensure you have selected the appropriate target platform before running the application.
- **IDE Recommendations:**
  - For **macOS**, we recommend using **Visual Studio Code** with the OpenSilver extension, along with .NET Meteor or .NET MAUI extensions for enhanced development experience.
  - For **Windows**, we recommend using **Visual Studio 2022** with the OpenSilver extension.

### Running on Windows (Example)

To run your generated application as a native Windows desktop app:

1. **Set the Startup Project:**
   - In your solution, choose the `.MauiHybrid` project as the startup project.
2. **Select the Target Platform:**
   - Make sure that "Windows" is selected as your target platform.
3. **Run the Application:**
   - Click the run button (or press F5) to launch the app.

  ![Running MAUI Hybrid in Visual Studio 2022](/images/how-to-topics/vs-2022-run-maui-hybrid.png)
---

## Leveraging MAUI Native Features

One of the strengths of the MAUI Hybrid Launcher is that it gives you access to native features via .NET MAUI. For example, the **Microsoft.Maui.Essentials** package provides APIs for accessing device hardware and native functions.

> **Note:** Ensure that the package version aligns with your .NET version. For example, if your application targets **.NET 8**, use **Microsoft.Maui.Essentials 8.0.100**.

### Thread Considerations

OpenSilver’s core logic runs on its own dedicated thread, while many native operations require execution on the MAUI UI thread. Use `MainThread.BeginInvokeOnMainThread` from the `Microsoft.Maui.Essentials` package to marshal calls onto the MAUI UI thread.

### Example: Accessing User Location

Below is an example demonstrating how to request location permissions, obtain the user’s location, and update the UI:

```csharp
// Run the native API calls on the MAUI UI thread.
MainThread.BeginInvokeOnMainThread(async () =>
{
    // Check the current location permission status.
    var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

    // If permission is not granted, request it.
    if (status != PermissionStatus.Granted)
    {
        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
    }

    // If permission is granted, fetch the location.
    if (status == PermissionStatus.Granted)
    {
        var request = new GeolocationRequest(GeolocationAccuracy.Medium);
        var location = await Geolocation.Default.GetLocationAsync(request);

        if (location != null)
        {
            // Switch back to the OpenSilver thread to update the UI.
            Dispatcher.BeginInvoke(() =>
            {
                TB.Text = location.ToString();
            });
        }
    }
    else
    {
        // Inform the user if permission was denied.
        Dispatcher.BeginInvoke(() =>
        {
            MessageBox.Show("Location permission denied.");
        });
    }
});
```

> **Important:** Ensure proper permissions are configured for all platforms. For example, for **Android**, add the following permissions to `AndroidManifest.xml`:
>
> - `ACCESS_COARSE_LOCATION`
> - `ACCESS_FINE_LOCATION`

![Location permissions for Android](/images/how-to-topics/android-location-permissions.png)
---

## Publishing to App Stores

Since OpenSilver leverages the official MAUI Hybrid technology, you can follow the standard publishing processes provided by Microsoft. For detailed steps, refer to the official MAUI deployment guides:

- **Android:** [MAUI Android Deployment](https://learn.microsoft.com/en-us/dotnet/maui/android/deployment/?view=net-maui-9.0)
- **iOS:** [MAUI iOS Deployment](https://learn.microsoft.com/en-us/dotnet/maui/ios/deployment/?view=net-maui-9.0)
- **macOS:** [MAUI Mac Catalyst Deployment](https://learn.microsoft.com/en-us/dotnet/maui/mac-catalyst/deployment/?view=net-maui-9.0)
- **Windows:** [MAUI Windows Deployment Overview](https://learn.microsoft.com/en-us/dotnet/maui/windows/deployment/overview?view=net-maui-9.0)

---

## Known Issues

### Path Length Limitations

- There is a known issue related to the internal path size limitations in MAUI. This can result in build failures due to the generated temporary file paths exceeding 256 characters.  
**Workaround:** Place your project folder as close to the root directory as possible (for example, `C:\MyProject`) to reduce the overall path length.

### Windows Launch Failure (Code 3221226356 / 0xc0000374)

- Sometimes, when attempting to run the application on Windows, it may fail with exit code **3221226356 (0xc0000374)**.
**More details:** [Official issue on GitHub](https://github.com/dotnet/maui/issues/25837).  
**Workaround:** Try running the application again.

---

## Conclusion

The MAUI Hybrid Launcher extends OpenSilver applications into semi-native experiences across multiple platforms. Whether you’re building for Windows, macOS, Android, or iOS, you now have a streamlined way to leverage both OpenSilver’s rich UI capabilities and MAUI’s native functionalities.

