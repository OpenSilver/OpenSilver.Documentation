## How to enable and run AOT compilation


1. Verify that DotNet6 Preview4 is installed. To do this open CommandPrompt and run the command
```
dotnet --info
```

<img src="/images/how-to-topics/dotnet-info.png" alt="Dotnet info" width="700"/><br />

2. To install the .NET WebAssembly build tools, run the following command from an elevated command prompt (Administrative mode).
```
dotnet workload install microsoft-net-sdk-blazorwebassembly-aot
```


3. To enable WebAssembly AOT compilation in your Blazor WebAssembly project, add the following property to your project file.
```
<RunAOTCompilation>true</RunAOTCompilation>
```

<img src="/images/how-to-topics/run-aot-compilation.png" alt="Enable AOT compilation" width="700"/><br />

4. Verify that your app uses Preview4 packages.

<img src="/images/how-to-topics/verify-preview4.png" alt="Preview" width="700"/><br />


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
