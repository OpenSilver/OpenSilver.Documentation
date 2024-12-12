## How to enable and run AOT compilation

1. Verify that .NET 7.0 (or higher) is installed. To do this, open a command prompt and run the command
```
dotnet --info
```

<img src="/images/how-to-topics/dotnet-info.png" alt="Dotnet info" /><br />

2. Install the .NET WebAssembly build tools.
Run the following command from an elevated command prompt (Administrative mode).
``` 
dotnet workload install wasm-tools
```

3. To enable WebAssembly AOT compilation in your Blazor WebAssembly project, add the following property to your project file.

```
<RunAOTCompilation>true</RunAOTCompilation>
```

<img src="/images/how-to-topics/run-aot-compilation.png" alt="Enable AOT compilation" /><br />

4. Verify that your app uses correct packages.

<img src="/images/how-to-topics/verify-packages.png" alt="Preview"/><br />


5. Create Publish Profile.

<img src="/images/how-to-topics/create-publish-profile.png" alt="Profile" width="700"/><br />

<img src="/images/how-to-topics/choose-folder.png" alt="Choose Folder" width="700"/><br />

<img src="/images/how-to-topics/folder-location.png" alt="Folder Location" width="700"/><br />

6. Publish. It will take time.

Try to change publishing settings If it does not work.

<img src="/images/how-to-topics/publish.png" alt="Publish" width="700"/><br /><br />


[Here](add-site-to-iis.md) is how to add and run published website.

To verify that your application is actually running in AOT, please open the browser developer tools (F12), go to the Network tab, force refresh (Ctrl + F5) and check the size of the **dotnet.wasm** file. It is supposed to be several times bigger when running in AOT (non AOT wasm size is < 3MB). If that's not the case then most probably it is not running in AOT.

## A quick recap about AOT

The concept of "AOT" exists in 2 contexts: `desktop/mobile` applications (CoreRT, NativeAOT, etc.) and `Blazor WebAssembly`. The 2 work in a very different way. Here is the explanation how both work.

#### When running on Windows or as desktop/mobile apps:

- Without AOT:

VS will compile the C# to IL (Intermediate Language) (.DLLs)
When app is launched, the JIT ("Just-In-Time" compilation) (which comes from .NET Framework runtime, or NET Core...) will convert IL into binary executable code.

- With AOT:

VS will compile the C# to binary executable code.

=> No much difference in performance during application execution (on Windows) between non-AOT and AOT because JIT will compile to binary code on Windows when the application starts.

#### When running in the browser with Blazor WebAssembly:

- Without AOT:

VS will compile the C# to IL (Intermediate Language) (.DLLs).
There is no JIT in the browser.
"dotnet.wasm" is the only binary executable that gets downloaded. "dotnet.wasm" loads the IL of the DLLs and "interprets" them.
Negative impact on performance because interpretion layer.

- With AOT:

VS will compile the C# to binary executable code (files are bigger than IL).

=> Much better performance with AOT during application execution. As far as initial loading is concerned, AOT needs to download bigger files, so initial load is slower, but then the files are cached by the browser, so when coming back go the app, initial time is the same as non-AOT.

Note: when C# reflection is used in the application, or "dynamic" types, the execution will temporaroly fall back to "interpreted" mode, which means that the runtime performance of AOT will be nearly the same as non-AOT. We are working to remove all those types, so as to really benefit from AOT.

We recommend clients to already publish the application in AOT mode so that you can see the current state with AOT. Please keep in mind that further AOT performance improvement will come as we remove more types and we make further optimizations, to leverage AOT and not fall back to "interpreted" mode too often.

For AOT to work you need to publish your app (not merely starting it with Visual Studio). You can publish it to a local folder and point IIS to it (make sure to select "self-contained" in the publishing options).

## Known Issues

### 1. Exit code: -1073741571

In some cases publish fails with an error similar to following.

```
Exit code: -1073741571
C:\Program Files\dotnet\packs\Microsoft.NET.Runtime.WebAssembly.Sdk\6.0.0-preview.5.21301.5\Sdk\WasmApp.targets(145,5): Error : Precompiling failed for 
```

This error happens when **mono-aot-cross.exe** tries to convert llvm method which is very big and most probably it will be one of **InitializeComponent** functions.

One of the techniques to find how many presumably big functions exist in the project would be to search **.xaml.g.cs** generated files that are bigger than 1MB. The solution can be to split **InitializeComponent** method to several methods, exclude **.xaml** file and include generated **.xaml.g.cs** one one.

To find out which exact function causes the issue follow the steps.
- Set MSBuild output verbosity to `Diagnostic` level\
  Tools -> Options -> Build And Run
- Publish the project again and wait until it fails.
- Find the last execution of **mono-aot-cross.exe** and run it with **cmd.exe** adding **-v** (verbosity) option.

The command will look similar to this
```
"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Runtime.AOT.win-x64.Cross.browser-wasm\6.0.0-preview.5.21301.5\Sdk\..\tools\mono-aot-cross.exe" -v --debug --llvm "--aot=no-opt,static,direct-icalls,deterministic,dwarfdebug,llvm-path=C:\Program Files\dotnet\packs\Microsoft.NET.Runtime.Emscripten.2.0.12.Sdk.win-x64\6.0.0-preview.5.21281.1\tools\bin\,llvmonly,interp,asmonly,llvm-outfile=MyLib.dll.bc" MyLib.dll
```

There is also **MONO_PATH** environment variable that need to be set before running the command. It will be shown in Visual Studio output as well.

If command succeeded you will see some statistic about converted methods etc. If it fails you will see the last method it was trying to convert and fails.
```
converting llvm method void MyApp.Views.MyEditServiceCodeDetailsView:InitializeComponent ()
```

### Solution
It turns out that the bug is not directly related to the function size but the amount of lines that are adding objects to the List

```
var parents_8ce7371e02ef489e9893174ae5411181 = new global::System.Collections.Generic.List<global::System.Object>();
parents_8ce7371e02ef489e9893174ae5411181.Add(TextBlock_c0999e8200734224a58a6861f2e68cfb);
parents_8ce7371e02ef489e9893174ae5411181.Add(StackPanel_8465a697e0a94b7ca24107bb115330f6);
parents_8ce7371e02ef489e9893174ae5411181.Add(StackPanel_c7b62394168e4c7da9eabfbeb9d6a833);
```

Having many .Add lines in generated .g.cs code increases probability for mono-aot-cross.exe to fail.

As a workaround, OpenSilver compiler can be adapted to generate local function inside InitializeComponent() and add list items inside the function.

```
var parents_8ce7371e02ef489e9893174ae5411181 = new global::System.Collections.Generic.List<global::System.Object>();
void parents_8ce7371e02ef489e9893174ae5411181_func() {
    parents_8ce7371e02ef489e9893174ae5411181.Add(TextBlock_c0999e8200734224a58a6861f2e68cfb);
    parents_8ce7371e02ef489e9893174ae5411181.Add(StackPanel_8465a697e0a94b7ca24107bb115330f6);
    parents_8ce7371e02ef489e9893174ae5411181.Add(StackPanel_c7b62394168e4c7da9eabfbeb9d6a833);
};
parents_8ce7371e02ef489e9893174ae5411181_func();
```

To achieve that we need 3 lines of code.
- Open `GeneratingCSharpCode.cs` file
- Find the following line

```
while (elementForSearch != null && elementForSearch.Parent != null)`
```
- Add this line before while statement

```
stringBuilder.AppendLine("void " + nameForParentsCollection + "_func() {");
```
- Add this two lines after while statment

```
stringBuilder.AppendLine("};");
stringBuilder.AppendLine(nameForParentsCollection + "_func();");
```

### 2. Not served mime types after publish.

After running published website the following error can appear in browser console.

<img src="/images/how-to-topics/not_served_files.png" alt="Not served error" /><br />

That means some of MIME types are not served by IIS. To solve that go to **MIME Types** and add **.dat** and **.blat** types both **application/octet-stream**.

<img src="/images/how-to-topics/mime_types.png" alt="Not served error" /><br />

<img src="/images/how-to-topics/add_mime_type.png" alt="Not served error" /><br />

### A quick recap about AOT

The concept of "AOT" exists in 2 contexts: `desktop/mobile` applications (CoreRT, NativeAOT, etc.) and `Blazor WebAssembly`. The 2 work in a very different way. Here is the explanation how both work.

#### When running on Windows or as desktop/mobile apps:

- Without AOT:

VS will compile the C# to IL (Intermediate Language) (.DLLs)
When app is launched, the JIT ("Just-In-Time" compilation) (which comes from .NET Framework runtime, or NET Core...) will convert IL into binary executable code.

- With AOT:

VS will compile the C# to binary executable code.

=> No much difference in performance during application execution (on Windows) between non-AOT and AOT because JIT will compile to binary code on Windows when the application starts.

#### When running in the browser with Blazor WebAssembly:

- Without AOT:

VS will compile the C# to IL (Intermediate Language) (.DLLs).
There is no JIT in the browser.
"dotnet.wasm" is the only binary executable that gets downloaded. "dotnet.wasm" loads the IL of the DLLs and "interprets" them.
Negative impact on performance because interpretion layer.

- With AOT:

VS will compile the C# to binary executable code (files are bigger than IL).

=> Much better performance with AOT during application execution. As far as initial loading is concerned, AOT needs to download bigger files, so initial load is slower, but then the files are cached by the browser, so when coming back go the app, initial time is the same as non-AOT.

Note: when C# reflection is used in the application, or "dynamic" types, the execution will temporaroly fall back to "interpreted" mode, which means that the runtime performance of AOT will be nearly the same as non-AOT. We are working to remove all those types, so as to really benefit from AOT.

We recommend clients to already publish the application in AOT mode so that you can see the current state with AOT. Please keep in mind that further AOT performance improvement will come as we remove more types and we make further optimizations, to leverage AOT and not fall back to "interpreted" mode too often.