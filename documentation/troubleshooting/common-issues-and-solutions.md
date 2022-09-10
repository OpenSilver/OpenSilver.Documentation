# Troubleshooting

## Known issues and tips:

### Compilation errors

- To improve the accuracy of the "Error List" window, select "Build only", as shown in the screenshot below:

![Select 'Build Only' to see only relevant errors](/images/view-only-build-errors-small.png "Select 'Build Only' to see only relevant errors")

- The "Error List" window may display misleading errors. Please only refer to the content of the "Output" window (Ctrl+W, O) to know if a compilation has succeeded. If there are compilation errors when rebuilding a project, please search for the very first error that appears in the "Output" window (Ctrl+W, O).

- The XAML editor sometimes displays misleading errors during design-time, which do not affect the ability to compile and run the application.

- If you have trouble rebuilding the solution, please delete the 'obj' folders and try again.

- All XAML files in the project need to have the following properties: Build Action = Content, CustomTool = MSBuild:Compile

- Killing "msbuild.exe" process may fix some rare issues that appear after updating to a newer OpenSilver NuGet package or when working with multiple instances of Visual Studio.

### Debugging

- If your breakpoints are not hit, try disabling "Enable just my code" (uncheck the option in Tools->Options->Debugging->General->Enable Just My Code)

- Disabling code optimisation may help if you experience debugging issues (right click the project->Properties->Build->uncheck "optimize code")

### Design-time

- XAML auto-completion and intellisense may sometime not show up.

- If you only change XAML files without changing C# files, you need to manually Rebuild the solution to see the changes reflected in your app.

### WCF and client/server communication

- The browser security context may require that you enable cross-domain calls by [configuring CORS](../in-depth-topics/wcf-and-webclient.html#adding-support-for-cross-domain-calls-cors) and the SameSite attribute.

- When adding a [WCF Service Reference](../in-depth-topics/wcf-and-webclient.html), please uncheck the option "Reuse types in referenced assemblies", and update the NuGet packages from v4.4 to v4.7. When configuring a WCF Service Reference, temporarily add a reference to the "System.dll" assembly, then configure the service, and remove the reference after you are done.

- To enable passing cookies (eg. for credentials/authentication), set the "[DefaultSoapCredentialsMode](../in-depth-topics/wcf-and-webclient.html)" parameter.

#### Code trimming and WCF/SOAP data contract serialization
Code trimming is an optimization that can reduce the size of a published Open Silver application, thus improving both performance and start-up time. For more information you can read the official documentation [here](https://docs.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/configure-trimmer?view=aspnetcore-6.0).

However, in some scenarios, is it possible that code trimming breaks .NET serialization and/or deserialization that rely heavily on Reflection.

If you receive an error on data contract serialization or deserialization (or if you use Reflection in your application) you can disable code trimming.

To do so, just edit the “.Browser” project file and make sure that the **PublishTrimmed** flag is set to False. Below you will find an example.

![image](https://user-images.githubusercontent.com/93962591/158984083-c10f6e4c-0147-434f-b977-97c2ac268dfe.png)

If disabling trimming helps to solve the issue, please, consider reverting trimming and configure trimming rules. To do this, please, follow the instructions [here](https://github.com/dotnet/linker/blob/main/docs/data-formats.md#descriptor-format).

### Performance

- Performance has been greatly improved in the past few months and will keep improving with every upcoming release. Please keep an eye on the [commits history](https://github.com/OpenSilver/OpenSilver/commits/develop) for updates.


## FAQ - Other issues and solutions


### > Where is the "WorkInProgress" package?

The "WorkInProgress" package has been merged into the OpenSilver package and will no longer be updated ([learn more](https://www.opensilver.net/permalinks/wip_discontinued.aspx)).

### > I am getting "Error MSB4018: The "ResolveBlazorRuntimeDependencies" task failed unexpectedly." when I compile.

Make sure that all the projects in your solution use the exact same OpenSilver NuGet package name and version.

For example, this error may occur if one project references the "OpenSilver" package while another project references the "OpenSilver.WorkInProgress" package.

### > How to see more detailed logs?

Build output verbosity can be changed from `Tools -> Options -> Projects and Solutions -> Build and Run`\
Choose an option from **MSBuild project build output verbosity**





### > What if my issue is not listed here?

Please contact us at: https://opensilver.net/contact.aspx

