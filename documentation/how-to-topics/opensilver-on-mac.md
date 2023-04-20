# How to Run an OpenSilver App on Mac

This guide will help you run an OpenSilver application on a Mac using the `dotnet` CLI or an IDE like Visual Studio Code.
## Prerequisites

To run an OpenSilver application on Mac, you need to have the latest .NET SDK installed. At the time of writing this guide, the latest version is .NET 7, which has been tested and works well with OpenSilver. You can download the .NET SDK [here](https://dotnet.microsoft.com/en-us/download) and follow the installation instructions provided on the website.

Use an existing OpenSilver application for this guide. We will use an example located [here](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/OpenSilverForMac).

## Running the App with dotnet CLI

Follow these steps to run the OpenSilver application using the `dotnet` CLI:

1. Open Terminal and navigate to the Browser folder:
```
cd /Users/user/Downloads/OpenSilverForMac/OpenSilver.Samples.Showcase.Browser/OpenSilverForMac.Browser
```

2. Restore all dependencies:
```
dotnet restore
```

3. Build the project:
```
dotnet build
```

4. Run the project:
```
dotnet run
```

5. Open your favorite browser and navigate to http://localhost:55592 (or use the URL from the output of the previous command).

## Running the App with Visual Studio Code

You can also run the OpenSilver application using Visual Studio Code. Follow these steps:

1. Download and install Visual Studio Code from [here](https://code.visualstudio.com/download).

2. Install the following extensions in Visual Studio Code:

* .NET Install Tool for Extension Authors
* C#
* Microsoft.AspNetCore.Razor.VSCode.BlazorWasmDebuggingExtension

3. Add a `launch.json` file in the `.vscode` folder at the root of the solution with the following content:
```
{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Launch OpenSilver App",
            "type": "blazorwasm",
            "request": "launch",
            "timeout": 60000,
            "url": "http://localhost:55592",
            "cwd": "${workspaceFolder}/OpenSilverForMac.Browser"
        }
    ]
}
```

Adjust the cwd path to match your solution and update the url if another URL is used in the Properties/launchSettings.json.

4. Run the OpenSilver app using the Visual Studio Code interface:
![Run via VS Code](/images/how-to-topics/run-via-vs-code.png "Run via VS Code")
