## How to enable and run AOT compilation


1. Verify that DotNet6 Preview5 is installed. To do this open CommandPrompt and run the command
```
dotnet --info
```

<img src="/images/how-to-topics/dotnet-info.png" alt="Dotnet info" /><br />

2. Install the .NET WebAssembly build tools:
- If you installed the DotNet6 Preview 4 or older previously, run the following command from an elevated command prompt (Administrative mode).
``` 
dotnet workload update
```
- Else, or if you get an error, run the following command from an elevated command prompt (Administrative mode).

```
dotnet workload install microsoft-net-sdk-blazorwebassembly-aot
```
 - If you still get an error, try uninstalling DotNet6 Preview 5 and reinstalling it, then try again.

 - If it still doesn't work, uninstall DotNet6 Preview 5, install Visual Studio 2022 Preview 1 which you can download [here](https://visualstudio.microsoft.com/vs/preview/vs2022/), then reinstall DotNet6 Preview 5 and run the following command from an elevated command prompt (Administrative mode).

```
dotnet workload install microsoft-net-sdk-blazorwebassembly-aot
```
 - You can check the [ASP.NET Core updates in .NET 6 Preview 5](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-6-preview-5/) for more informations.


3. To enable WebAssembly AOT compilation in your Blazor WebAssembly project, add the following property to your project file.
```
<RunAOTCompilation Condition="'$(IsSecondPass)'=='True'">true</RunAOTCompilation>
```

<img src="/images/how-to-topics/run-aot-compilation.png" alt="Enable AOT compilation" /><br />

4. Verify that your app uses Preview5 packages.

<img src="/images/how-to-topics/verify-preview5.png" alt="Preview"/><br />


5. Create Publish Profile.

<img src="/images/how-to-topics/create-publish-profile.png" alt="Profile" width="700"/><br />

<img src="/images/how-to-topics/choose-folder.png" alt="Choose Folder" width="700"/><br />

Issues can occur if changing this path.

<img src="/images/how-to-topics/folder-location.png" alt="Folder Location" width="700"/><br />

6. Publish. It will take time.

Try to change publishing settings If it does not work.

<img src="/images/how-to-topics/publish.png" alt="Publish" width="700"/><br />


#### How to run published website

Add new Web Site in IIS

https://docs.google.com/document/d/1c4M3chxXxnS7_-NKvSc7TfP04FsHcJtBThWWqwlbJsc/edit

<img src="/images/how-to-topics/run-published.png" alt="Run published" width="700"/><br />

## Known Issues

1. Exit code: -1073741571

In some cases publish fails with an error similar to following.

```
Exit code: -1073741571
C:\Program Files\dotnet\packs\Microsoft.NET.Runtime.WebAssembly.Sdk\6.0.0-preview.5.21301.5\Sdk\WasmApp.targets(145,5): Error : Precompiling failed for 
```

This error happens when **mono-aot-cross.exe** tries to convert llvm method which is very big and most probably it will be one of **InitializeComponent** functions.

One of the techniques to find how many presumably big functions exist in the project would be to search **.xaml.g.cs** generated files that are bigger than 1MB. If there are not many then the solution can be to split a large xaml file by adding UserControls.

2. Not served mime types after publish.

After running published website the following error can appear in browser console.

<img src="/images/how-to-topics/not_served_files.png" alt="Not served error" /><br />

That means some of MIME types are not served by IIS. To solve that go to **MIME Types** and add **.dat** and **.blat** types both **application/octet-stream**.

<img src="/images/how-to-topics/mime_types.png" alt="Not served error" /><br />

<img src="/images/how-to-topics/add_mime_type.png" alt="Not served error" /><br />

