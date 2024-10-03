# JetBrains Rider support
You can now create, build, and debug applications made with OpenSilver. Our templates, paired with Rider, allow you to create new projects, build, and run them on any platform thanks to the cross-platform nature of OpenSilver.

## Prerequisites
In order to use OpenSilver with rider you need to install our cross-platform .net CLI templates from the [OpenSilver.Templates nuget download page](https://www.nuget.org/packages/OpenSilver.Templates).

### What is the .net CLI?
The .NET Command-Line Interface (CLI) is a cross-platform toolchain for developing, building, running, and publishing .NET applications1. The .NET CLI is included with the .NET SDK and now OpenSilver supports also this workflow.

That way you can create new projects on any platform like Linux, MacOS or Windows and then use JetBrains Rider as your IDE.

### How to install the CLI templates
.net core CLI has a different, more modern, templating approach than Visual Studio and OpenSilver provides support for this cross platform technology.

Download the OpenSilver Templates package (from the [OpenSilver.Templates nuget download page](https://www.nuget.org/packages/OpenSilver.Templates)) and install it by issuing this command in the CLI:
	
	dotnet new install .\OpenSilver.CLI.Templates.3.0.0.nupkg

From now on you will have three new templates available via the CLI, on any platform.

You can list the available .NET CLI templates by using the command: 

	dotnet new --list or dotnet new -l 

This command will display the pre-installed .NET Core project templates. The list includes details such as the name of the templates, the short name of the template, default programming language, and the template tags. That way you should see three new templates under the category OpenSilver.CLI.Tools

### Create your first OpenSilver project via the CLI
Once the templates are installed, Rider will recognize them immediately without any manual adjustments. Simply run Rider and create a new solution. In the bottom left corner, you’ll see the templates. Choose the one you want, name the project, and you’re ready to go.

![New OpenSilver Rider project](/images/rider-new-solution-dialog.png "Create new OpenSilver project dialog in JetBranins Rider")

Any option available via the command line, like the language or .net version are available as well in Rider. Once the project is expanded you can see the structure of the project and start coding.

![Rider IDE with an OpenSilver project loaded](/images/rider-ide-with-opensilver.png "Example of Rider IDE with an OpenSilver project loaded")

Also the debug of the project is already set.

### Running the project
As long as you have the .net 8 SDK with wasm/blazor workload installed you're able to run the project right away.

![Rider IDE with an OpenSilver running](/images/rider-ide-with-opensilver.png "An example of OpenSilver application running in Rider")

### Debugging the project
Debugging an OpenSilver app in Rider is as simple as running it. Just set a breakpoint and run the project in debug mode. The debugger will stop at the breakpoint and you can inspect the variables and the call stack.

![Rider IDE debugging a OpenSilver application](/images/rider-debug-opensilver.png "Rider IDE debugging a OpenSilver application")

## What should I do if I have additional questions?
Please contact us for any questions at: [https://opensilver.net/contact.aspx](https://opensilver.net/contact.aspx)