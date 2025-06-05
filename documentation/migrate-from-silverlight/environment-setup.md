
# Software Requirements for OpenSilver Applications

## 1. Prerequisites for Developing OpenSilver Applications
To begin developing OpenSilver applications, the following software components are essential:

- **[Visual Studio 2022 or newer](https://visualstudio.microsoft.com/downloads/)** (for Windows): This is the required version of Visual Studio to build and manage OpenSilver applications effectively.
- **[OpenSilver Extension for Visual Studio](https://opensilver.net/download.aspx)**: A robust and fully [open-source](https://github.com/OpenSilver/OpenSilver)  VSIX extension that enhances Visual Studio by adding essential project templates. This extension enables seamless creation of OpenSilver applications from the "File -> New Project" dialog. Additionally, it includes a sophisticated XAML Editor, which supports live previews as you modify XAML files, ensuring a smooth development experience.

**Note**: For a complete guide including step-by-step installation, environment configuration, Please refer this [documentation](../general/development-setup.md) for more details.

## 2. Prerequisites for Running Legacy Silverlight Applications
As part of the migration process from Silverlight to OpenSilver, it's crucial to establish an environment where you can run and debug the original Silverlight application. This ensures a comprehensive comparison and simultaneous debugging of both the original and migrated applications.

The following software is required to run the legacy Silverlight application:

- **[Visual Studio 2015](https://visualstudio.microsoft.com/vs/older-downloads/)** (or a compatible older version): The last supported [Visual Studio](https://stackoverflow.com/questions/44290672/how-to-download-visual-studio-community-edition-2015-not-2017) version for Silverlight.
- **Silverlight 5 SDK**: Critical for building and running Silverlight applications.
- **Silverlight 5 Toolkit (Dec 2011 release)**: Provides essential controls and features for Silverlight applications.
- **Silverlight 5 Developer Runtime for Windows (64-bit)**: Required to execute Silverlight applications on a Windows development machine.

### Ensuring the Environment is Properly Set Up
1. **Test Silverlight Debugging**: It is strongly recommended to verify the Silverlight environment by creating a new Silverlight application and testing step-by-step debugging. If debugging fails, ensure that all the necessary components are installed correctly, and confirm that the **"Silverlight"** option is selected under the **Debuggers** section of the **.Web** project properties.

![Turn on Silverlight debugging](/images/silverlight_debuggers.png "Turn on Silverlight debugging")

> **Note**: If the original application was developed with an earlier version of Visual Studio (e.g., 2010, 2012, or 2013), you may need to install that specific version or upgrade the project to a more recent version. Note that while an unofficial extension, **["Silverlight for Visual Studio"](https://marketplace.visualstudio.com/items?itemName=RamiAbughazaleh.SilverlightProjectSystem)** by Rami Abughazaleh, allows for running Silverlight solutions in newer versions (such as VS 2019), its usage is not recommended due to potential regressions.

2. **Additional Components**: Depending on the libraries and features used within your Silverlight application, the following components may also be necessary:
   - **RIA Services V1.0 SP2**: Required if the application utilizes WCF RIA Services.
   - **Third-Party Libraries**: Such as Telerik, DevExpress, Syncfusion, or ComponentOne.

## 3. Recommended Tools for Debugging and Issue Resolution

To facilitate debugging and resolving any issues during migration, the following tools are highly recommended:

- **[Fiddler](https://www.telerik.com/download/fiddler)**: A powerful, free web debugging proxy by Telerik that captures and inspects all HTTP(S) traffic between the client and server. Fiddler allows you to inspect requests, set breakpoints, and manipulate traffic, making it an invaluable tool for diagnosing client-server communication issues.

- **[XAML Spy Express](http://xamlspy.com/learn/xaml-spy-express)**: A highly effective, free tool that allows you to inspect the UI visual tree of Silverlight applications. It provides a detailed view of how UI elements are structured, as well as insights into their properties. This tool is particularly useful for comparing the UI between the original Silverlight application and the migrated OpenSilver version.

>  **Note**: When working with OpenSilver applications, XAML Spy is not required to inspect the Visual Tree. Simply use the built-in "Simulator" in OpenSilver and utilize the "Inspect Visual Tree" feature for direct insights.
