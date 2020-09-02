# Troubleshooting

## Known issues of the current release

- Performance will be greatly improved (by at least 30 times) when the Mono for WebAssembly team at Microsoft releases AOT compilation. Please keep an eye on the "What's new and Roadmap" page for updates.

- Auto-complete and intellisense are not yet available when editing XAML code. The XAML editor displays misleading errors during design time.

- The "Error List" window may display misleading errors. Please refer only to the content of the "Output" window to know if a compilation has succeeded.

- To improve the accuracy of the "Error List" window, select "Build only", as shown in the screenshot below:

![Select 'Build Only' to see only relevant errors](/images/view-only-build-errors-small.png "Select 'Build Only' to see only relevant errors")

- ResX files are not yet supportedIf you only change the XAML without changing the C#, you need to Rebuild to see the changes reflected in your app.

- When adding a WCF Service Reference, please uncheck the option "Reuse types in referenced assemblies", and update the NuGet packages from v4.4 to v4.7

## FAQ - Other issues and solutions

### > I am getting "Simulator Not Enabled" whenever I launch the simulator project, even though I did open Package Manager Console.

Please fix the path that is in the "Start Program" field of the project properties of the project with the ".Simulator" suffix.

To do so, please right-click on the project "ReptonPoD.Simulator", click "Properties", go to the "Debug" section, and check "Start external program".

Make sure to enter the following path, where you need to replace "YOUR-USER-NAME-GOES-HERE" with your actual Windows user name:
C:\Users\YOUR-USER-NAME-GOES-HERE\.nuget\packages\opensilver.workinprogress\1.0.0-alpha-007\tools\simulator\CSharpXamlForHtml5.Simulator.exe

You will also need to change the version number in the path above to reflect the version of OpenSilver that you are using.

### > What if my issue is not listed here?

Please contact us at: https://opensilver.net/contact.aspx

