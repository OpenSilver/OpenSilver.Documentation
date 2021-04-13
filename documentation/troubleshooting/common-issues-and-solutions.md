# Troubleshooting

## Known issues of the current release

- Performance will be greatly improved (by at least 30 times) when the Mono for WebAssembly team at Microsoft releases AOT compilation. Please keep an eye on the "What's new and Roadmap" page for updates.

- Auto-complete and intellisense are not yet available when editing XAML code. The XAML editor displays misleading errors during design time, which you should ignore.

- The "Error List" window may display misleading errors. Please refer only to the content of the "Output" window (Ctrl+W, O) to know if a compilation has succeeded. If there are compilation errors when rebuilding a project, please search for the very first error that appears in the "Output" window (Ctrl+W, O).

- To improve the accuracy of the "Error List" window, select "Build only", as shown in the screenshot below:

![Select 'Build Only' to see only relevant errors](/images/view-only-build-errors-small.png "Select 'Build Only' to see only relevant errors")

- If you get compilation errors that only appear from time to time and are not consistent, killing the "msbuild.exe" process may fix them. In particular, when updating to a newer OpenSilver NuGet package or working with multiple instances of Visual Studio, you may sometimes need to kill the "msbuild.exe" process to force it to use the current version of the package.

- ResX files are not yet supported.

- If you only change the XAML without changing the C#, you need to Rebuild to see the changes reflected in your app.

- When adding a WCF Service Reference, please uncheck the option "Reuse types in referenced assemblies", and update the NuGet packages from v4.4 to v4.7

## FAQ - Other issues and solutions

### > I am getting "Simulator Not Enabled" whenever I launch the simulator project, even though I did open Package Manager Console.

Please fix the path that is in the "Start Program" field of the project properties of the project with the ".Simulator" suffix.

To do so, please right-click on the project "<PROJECT NAME>.Simulator", click "Properties", go to the "Debug" section, and check "Start external program".

Make sure to enter the following path, where you need to replace "YOUR-USER-NAME-GOES-HERE" with your actual Windows user name, and the package version number with the current version of OpenSilver that you are using:
C:\Users\YOUR-USER-NAME-GOES-HERE\.nuget\packages\opensilver.workinprogress\1.0.0-alpha-007\tools\simulator\CSharpXamlForHtml5.Simulator.exe

### > I am getting "Error MSB4018: The "ResolveBlazorRuntimeDependencies" task failed unexpectedly." when I compile.

Make sure that all the projects in your solution use the exact same OpenSilver NuGet package name and version.

For example, this error may occur if one project references the "OpenSilver" package while another project references the "OpenSilver.WorkInProgress" package.

### > What if my issue is not listed here?

Please contact us at: https://opensilver.net/contact.aspx

