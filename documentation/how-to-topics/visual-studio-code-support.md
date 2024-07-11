# Visual Studio Code support
OpenSilver comes with a Visual Studio Code extensions and a dotnet CLI library of templates that provide support for OpenSilver projects. 
The OpenSilver extension, provides IntelliSense-like support for XAML files within Visual Studio Code and more, and OpenSilver Templates nuget package, which allows you to create new OpenSilver projects and items from within Visual Studio Code.

## OpenSilver dotnet CLI templates
OpenSilver support two ways to create a new project starting from a template, the first one is the classic VSIX that install in VisualStudio few ready to use templates. The second option is using the cross-platform .net core CLI interface.

### What is the .net CLI?
The .NET Command-Line Interface (CLI) is a cross-platform toolchain for developing, building, running, and publishing .NET applications1. The .NET CLI is included with the .NET SDK and now OpenSilver supports also this workflow.

That way you can create new projects on any platform like Linux, MacOS or Windows and then use your favorite IDE for development, like VSCode.

### How to intall the CLI templates
.net core CLI has a different, more modern, templating approach than Visual Studio and OpenSilver provides support for this cross platform technology.

Download the OpenSilver Templates package (from the [OpenSilver.Templates nuget download page](https://www.nuget.org/packages/OpenSilver.Templates)) and install it by issuing this command in the CLI:
	
	dotnet new install .\OpenSilver.CLI.Templates.3.0.0.nupkg

From now on you will have three new templates available via the CLI, on any platform.

You can list the available .NET CLI templates by using the command: 

	dotnet new --list or dotnet new -l 

This command will display the pre-installed .NET Core project templates. The list includes details such as the name of the templates, the short name of the template, default programming language, and the template tags. That way you should see three new templates under the category OpenSilver.CLI.Tools

### Create your first OpenSilver project via the CLI

In order to create your first OpenSilver app you have to choose the available template you want to use (WebApp, Business app etc). and then issue the command:

	dotnet new opensilverapp -n MyProject

This will create in the current directory an new OpenSilver web project called "MyProject". Remember that you must use the short name of the template not the full template name. Our templates will also take care of restoring the necessary nuget packages from the default repositories.

For more information about the command line switches you can run this command to get all the information you need for each template.

### Running the project
After you have created your project you can run it and debug in VS Code. Please note that it can require some manual steps depending on your configuration. In any the extension rely on the following extensions provided by Microsoft:

- C# Dev Toolkit
- C# Support
- .net 8 SDK with wasm/blazor workload

Also you require (if not installed the extension will try to install it for you) the **.net framework 8 SDK and the blazor wasm workload**.

A common scenario, after the prerequisites above are installed, includes:

- Go to the debug panel
- Click on run button
- VSCode will ask for which debug adapter to use, select C#
- At that point the build will start and you should see a browser opening

Depending on you specific configuration and environment more steps may be necessary. In case you need help you can contact us at: [https://opensilver.net/contact.aspx](https://opensilver.net/contact.aspx)

## The Visual Studio Code extension
The Visual Studio code extension is designed to enhance productivity and simplify the creation and coding of OpenSilver projects within Visual Studio Code. 
It provides IntelliSense-like support from XAML files within Visual Studio Code and simplifies the creation and coding of OpenSilver projects within Visual Studio Code. The Visual Studio code extension includes OpenSilver CLI templates for C#, Visual Basic, and F#, allowing for easy project creation. Additionally, it offers commands to create new items such as classes and resource files, all within the Visual Studio Code workspace. 

**Note:** In order to be able to create new project within Visual Studio code you need the [OpenSilver CLI Templates package](https://github.com/OpenSilver/OpenSilver.VSIX). Each templates target OpenSilver 3.0.0 and .net version 8.x.

### Commands provided
The extension introduces new commands, such as building the solution (a necessary step to enable IntelliSense-like experience for XAML files) and creating new project items. These commands are grouped under the **OpenSilver** category and can be accessed via the command palette.

The available commands are:

- New project items
  - opensilver.newProject: "Create a new OpenSilver Project"
  - opensilver.newChildWindow: "Create a new OpenSilver child window"
  - opensilver.newUserControl: "Create a new OpenSilver user control"
  - opensilver.newPage: "Create a new OpenSilver page"
  - opensilver.newClass: "Create a new C# class"
  - opensilver.newResource: "Create a new XAML Resource file"
  - opensilver.newDictionary: "Create a new resource dictionary file"
- Other
  - opensilver.compileProject: "Compile the project"
  
**Note:** Each item creation command prompts you for the file name, namespace, and location. The namespace is stored in the Visual Studio cache, so the next time you're prompted for a namespace, your last choice is preserved and proposed. You can always choose to enter a new namespace, your previous input will be discarded.

### Create a project from the homepage
The extension adds a new command button to the homepage. When you open a new instance of Visual Studio Code, you'll notice a new button labeled 'Create OpenSilver Project.' Clicking this button opens the command palette, where you can choose from different templates (such as application, OpenRia-enabled application, and class library).

![New project](/images/vscodeCreateNewProject.png "New project command button")

After selecting a project template, you'll be prompted to enter a solution name and location. A new solution, compatible with Visual Studio and the C# dev toolkit, will then be created.

### Intellisense support
If the project you're working on is compiled the extension will provide IntelliSense-like support for XAML files. This includes code completion, parameter info, quick info, and member lists. The IntelliSense-like support is available for XAML files only.

![VSCode Intellisense in action](/images/vscodeIntellisense.png "VSCode Intellisense in action")

All you have to do is start typing and a list of suggestions will appear much like in Visual Studio. You can use the arrow keys to navigate through the suggestions and press Enter to select one.

#### Known limitations
At the moment the IntelliSense like support has some limitations. For example before he can recognize elements marked with the x:Name attribute require the project to be rebuilt to be available. 

Also static resource names, named colors, custom controls in data template support is not complete and will be improved in a future release.

## What should I do if I have additional questions?
Please contact us for any questions at: [https://opensilver.net/contact.aspx](https://opensilver.net/contact.aspx)