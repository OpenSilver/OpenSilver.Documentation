# How to import JavaScript libraries?

It is possible to import JavaScript libraries into an OpenSilver project. This allows to extend the functionality of OpenSilver without much effort. It provides an alternative to importing.NET Standard libraries.

As the ecosystem of JavaScript libraries is very rich and active, thousands of third-party components are immediately accessible from OpenSilver projects.

The most "low level" way to interact with these JavaScript libraries is to use the "Interop" API described above, supplemented by the method "LoadJavaScriptFile(url)". Here is an example:
```
await Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js");
```
The .js file can be located online, as in the example above, or it can be placed inside the application project (of type OpenSilver). In the latter case, it will be accessible using any of the following URL formats:

* ms-appx:///AssemblyName/Folder/FileName.js
* /AssemblyName;component/Folder/FileName.js

Here is an example:
```
await Interop.LoadJavaScriptFile("ms-appx:///MyProject/FileSaver.min.js");
```
There are also other methods like “Interop.LoadCSSFile”, which are documented on the site and demonstrated in the sample application called "OpenSilver Showcase".

Userware, the company behind the open-source OpenSilver project, is currently working on providing NuGet packages specific to well-known JavaScript components, allowing to use them directly from XAML and C# without having to manually code JavaScript calls.

For example, packages for Telerik Kendo UI, Syncfusion Essential JS and DevExpress DevExtreme are already available and can be seen in the "Showcase" application on the OpenSilver website. They contain some of the main controls, such as the DataGrid, the RichTextEditor and the Spreadsheet component. Their source code is on GitHub.
