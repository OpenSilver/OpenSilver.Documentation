# Visual Studio Code support

OpenSilver comes with a Visual Studio Code extension and a dotnet CLI library of templates that provide support for OpenSilver projects.
The OpenSilver extension simplifies working with XAML files within Visual Studio Code, while the OpenSilver Templates NuGet package allows you to create new OpenSilver projects and items from within Visual Studio Code.

## Prerequisites

We highly recommend using the [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) for an enhanced development experience in Visual Studio Code.

## OpenSilver dotnet CLI templates

OpenSilver supports two ways to create a new project starting from a template.
The first is the classic VSIX that installs a few ready-to-use templates in Visual Studio.
The second option is using the cross-platform .NET CLI interface.

### What is the .NET CLI?

The .NET Command-Line Interface (CLI) is a cross-platform toolchain for developing, building, running, and publishing .NET applications.
The .NET CLI is included with the .NET SDK, and now OpenSilver also supports this workflow.

This allows you to create new projects on any platform like Linux, macOS, or Windows, and then use your favorite IDE for development, like VS Code.

### How to install the CLI templates

The .NET CLI has a different, more modern, templating approach than Visual Studio, and OpenSilver provides support for this cross-platform technology.

Simply run the following command in the CLI to install the OpenSilver Templates:

```
dotnet new install OpenSilver.Templates
```

From now on, you will have three new templates available via the CLI on any platform.

You can list the available .NET CLI templates by using the command:

```
dotnet new --list
```

This command will display the pre-installed .NET Core project templates.
The list includes details such as the name of the templates, the short name of the template, default programming language, and the template tags.
You should see three new templates listed among the available .NET templates.

### Create your first OpenSilver project via the CLI

To create your first OpenSilver app, choose the available template you want to use (e.g., WebApp, Business app) and issue the command:

```
dotnet new opensilverapp -n MyProject
```

This will create a new OpenSilver web project called "MyProject" in the current directory.
Remember to use the short name of the template, not the full template name.
The templates will also take care of restoring the necessary NuGet packages from the default repositories.

For more information about the command-line switches, you can run the following command:

```
dotnet new opensilverapp --help
```

### Running the project

After creating your project, you can run it and debug in VS Code.
Please note that some manual steps may be required depending on your configuration.
The extension relies on the following extensions provided by Microsoft:

- C# Dev Toolkit
- C# Support

You will also need the **.NET SDK and the Blazor WASM workload** (the extension will try to install these if they are not already installed).

A common scenario after the prerequisites are installed includes:

1. Go to the debug panel.
2. Click the run button.
3. VS Code will ask which debug adapter to use; select C#.
4. The build will start, and you should see a browser opening.

Depending on your specific configuration and environment, more steps may be necessary.
If you need help, you can contact us at: [https://opensilver.net/contact.aspx](https://opensilver.net/contact.aspx).

## The Visual Studio Code extension

The extension for VS Code can be downloaded from: [https://opensilver.net/download](https://opensilver.net/download).

The Visual Studio Code extension is designed to enhance productivity by simplifying the creation and coding of OpenSilver projects within Visual Studio Code.
The built-in XAML Designer tool simplifies changing the XAML files. The extension includes OpenSilver CLI templates for C#, Visual Basic, and F#, allowing for easy project creation.
Additionally, it offers commands to create new items such as classes and resource files, all within the Visual Studio Code workspace. The extension also includes a powerful XAML UI Designer, allowing developers to visually build XAML pages with ease.

**Note:** In order to create new projects within Visual Studio Code, you need the [OpenSilver CLI Templates package](https://www.nuget.org/packages/OpenSilver.Templates).

### Commands provided

The extension introduces new commands, such as building the solution and creating new project items.
These commands are grouped under the **OpenSilver** category and can be accessed via the command palette.

The available commands are:

- New project items
  - opensilver.newApplication: "Create a new OpenSilver Application"
  - opensilver.newProject: "Add a new OpenSilver Class Library"
  - opensilver.newChildWindow: "Create a new OpenSilver Child Window"
  - opensilver.newUserControl: "Create a new OpenSilver User Control"
  - opensilver.newPage: "Create a new OpenSilver Page"
  - opensilver.newClass: "Create a new Class"
  - opensilver.newResourceFile: "Create a new Resource(.resx) file"
  - opensilver.newXamlResourceDictionary: "Create a new XAML Resource Dictionary file"
- Other
  - opensilver.compileSolution: "Compile the Application"
  - opensilver.showXamlDesigner: "Show the Xaml Designer"
  - opensilver.formatXaml: "Format the XAML file"

### XAML UI Designer

One of the key features of this extension is the XAML UI Designer. This tool provides a visual interface for designing XAML pages, allowing developers to drag and drop components, modify properties, and see immediate previews of their changes. The designer offers support for editing existing XAML files, building new pages from scratch, and ensures all changes are synced directly with the code, improving development efficiency.

![VS Code with OpenSilver UI Designer](/images/how-to-topics/VSCodeDesigner.png "VS Code with OpenSilver UI Designer")

### Create an Application from the homepage

The extension adds a new command button to the Explorer panel homepage.
When you open a new instance of Visual Studio Code, you'll notice a new button labeled "Create OpenSilver Application".
Clicking this button opens the command palette, where you can choose from different templates.

After selecting a project template, you'll be prompted to enter a solution name and location.
A new solution, compatible with Visual Studio and the C# Dev Toolkit, will then be created.

![VS Code Homepage with OpenSilver buttons](/images/how-to-topics/VSCodeHomepage.png "Ability to open or create an OpenSilver application from the homepage")

## What should I do if I have additional questions?

Please contact us for any questions at: [https://opensilver.net/contact.aspx](https://opensilver.net/contact.aspx).
