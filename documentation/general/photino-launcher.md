# Using the Photino Launcher with OpenSilver

Starting from version **3.2**, OpenSilver supports building cross-platform applications with **MAUI Hybrid**, allowing deployment to:

- **Windows**
- **macOS**
- **iOS**
- **Android**

However, **MAUI Hybrid does not support Linux**. To bridge this gap, OpenSilver integrates support for the **Photino Launcher** ([https://www.tryphotino.io/](https://www.tryphotino.io/)), enabling OpenSilver applications to run seamlessly on **Linux**, as well as **Windows** and **macOS**.

You now have the flexibility to build fully cross-platform OpenSilver applications:

- **MAUI Hybrid**: Windows, macOS, iOS, Android
- **Photino**: Linux, Windows, macOS

---

## Getting Started

To utilize the Photino Launcher with OpenSilver, ensure you have one of these development environments:

- **Visual Studio 2022** with the [OpenSilver Extension](https://marketplace.visualstudio.com/items?itemName=userware.OpenSilverSDK)
- **Visual Studio Code** with the [OpenSilver Extension](https://marketplace.visualstudio.com/items?itemName=userware.vscode-opensilver)
- The **.NET CLI** with the [OpenSilver.Templates](https://www.nuget.org/packages/OpenSilver.Templates) installed

---

## Creating an OpenSilver Application with Photino Support

You can create a new OpenSilver application with Photino support using the command line, Visual Studio 2022, or Visual Studio Code.

### 1. Using the Command Line

Open a terminal and execute:

```bash
dotnet new opensilverapp -o MyAppName --usePhotino
```

> 📌 **Note**: The Browser launcher is always included by default. The `--usePhotino` option adds a dedicated `MyAppName.Photino` project to your solution.

Open the generated solution in Visual Studio, Visual Studio Code, or any text editor.

---

### 2. Using Visual Studio 2022

1. Ensure the **OpenSilver Extension** is installed from the Visual Studio Marketplace.
2. Create a new **OpenSilver Application** project.
3. In the wizard's **Platform Selection** section, select **Linux (Photino)**.
   ![New Project Wizard](/images/new-app-wizard.png)
4. Complete the wizard. Your solution will include the browser launcher and a `MyAppName.Photino` project.

---

### 3. Using Visual Studio Code

1. Install the **OpenSilver Extension**.
2. Run the **"Create OpenSilver Application"** command.
3. Select **Linux (Photino)** when prompted for platforms.
   ![Photino in Visual Studio Code](/images/vscode-photino.png)
4. The generated solution includes the `MyAppName.Photino` project.

---

## Running an OpenSilver Photino Application on Linux

### Option 1: Command Line

Navigate to the Photino project directory and run:

```bash
cd MyAppName.Photino
dotnet run
```

This command launches your OpenSilver application within a native Photino window.

---

### Option 2: Visual Studio Code

1. Ensure both **OpenSilver** and **C# Extensions** are installed.
2. Open your project folder in VS Code.
3. Navigate to `Program.cs` within the `MyAppName.Photino` project.
4. Press **F5** to initiate debugging.
5. If prompted, set `MyAppName.Photino` as the startup project to auto-generate `launch.json`. Press **F5** again to run your application.

![Running Photino application](/images/vscode-photino-running.png)

---

## Publishing Your Photino Application

Publishing an OpenSilver application with Photino is straightforward and preconfigured.

### Publishing for Linux

The `MyAppName.Photino.csproj` file is already set up for publishing. Simply execute:

```bash
dotnet publish
```

The output will be a self-contained, Linux-ready application located in:

```
MyAppName.Photino/bin/Release/TARGET_FRAMEWORK/linux-x64/publish/
```

This folder contains an executable compatible with most popular Linux distributions.

### Packaging

Although the published output can be used directly, it's **recommended** to package the application using a native package manager for your target Linux distribution (e.g., `deb` for Debian/Ubuntu, `rpm` for Fedora/RedHat).

Proper packaging simplifies installation, improves user experience, and integrates the application seamlessly with Linux systems.

For detailed guidance on packaging and publishing, refer to the official Photino guide:  
📄 [Photino Publish Guide](https://github.com/tryphotino/photino.Samples/blob/master/Photino.PublishPhotino/PublishPhotino/README.md)

---

## Summary

The **Photino Launcher** significantly expands OpenSilver’s capabilities by adding robust Linux support. Combined with MAUI Hybrid, OpenSilver applications can now run on **Windows, macOS, iOS, Android, and Linux** from a unified codebase.

- Use **MAUI Hybrid** for Windows, macOS, iOS, Android.
- Use **Photino** for Linux, macOS, and Windows.

For further information, visit:

- [Photino Official Site](https://www.tryphotino.io)
- [OpenSilver Official Site](https://opensilver.net)

