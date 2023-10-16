# Interop between VB.NET and JavaScript, importing JS libraries, and importing TypeScript Definitions

## How to interact between VB.NET/XAML and HTML/JavaScript?

OpenSilver contains an easy-to-use API that allows to call JavaScript code from VB.NET code. The opposite is also possible by exposing VB.NET entry points to the JavaScript context.

This API allows to get out of the .NET world in order to interact with the browser, to access low-level functionalities, to extend the framework, to manually manipulate HTML and CSS, and to interact with JavaScript libraries.

This API is widely used for example by the "[OpenSilver.Runtime](https://github.com/OpenSilver/OpenSilver/tree/master/src/Runtime/Runtime)" project, which contains the implementation of controls like TextBox via HTML and CSS.

The general syntax is as follows:
```
Dim result = Interop.ExecuteJavaScript("any JS code", params)
```

Here is an example showing how to display a native browser dialog:

```
Public Shared Sub DisplayAlert(ByVal text As String)
    Interop.ExecuteJavaScript("alert($0)", text)
End Sub

```

$0 is replaced by the first argument, $1 is replaced by the second, etc. For strings it is the same as concatenating strings, but the purpose of this syntax is to preserve strong typing in the case where the types are more complex, as we will see below.

One of the functionalities supported by this API is the passage of objects from the JavaScript context to the VB.NET context, so as to be able to chain several calls, as shown in the following example:

```
// Let's get the current URL by chaining multiple JavaScript calls in our VB.NET:
    Dim jsWindow As Object = Interop.ExecuteJavaScript("window")
    Dim location As Object = Interop.ExecuteJavaScript("$0.location", jsWindow)
    Dim currentUrl As String = Interop.ExecuteJavaScript("$0.href", location).ToString()
    MessageBox.Show("The current URL is: " & currentUrl)
```

Another feature is support for events, "delegates" and "callbacks". Here is an example showing how to obtain, via the browser asynchronously, the GPS coordinates where the user is located:

```
Private Sub GetGpsPosition()
    Interop.ExecuteJavaScript("navigator.geolocation.getCurrentPosition($0);", CType(GpsReceived, Action(Of Object)))
End Sub

Private Sub GpsReceived(ByVal jsEventArgs As Object)
    Dim lat = Convert.ToDouble(Interop.ExecuteJavaScript("$0.coords.latitude", jsEventArgs))
    Dim lon = Convert.ToDouble(Interop.ExecuteJavaScript("$0.coords.longitude", jsEventArgs))
    MessageBox.Show("Latitude: " & lat.ToString() & "  Longitude: " & lon.ToString())
End Sub

```

To allow the call of VB.NET code from JavaScript code, the developer can simply expose entry points by exposing "delegates", as shown in the example above.

Here is another example showing how to expose a VB.NET method taking an argument of type String so that it can be called from JavaScript code:

    Interop.ExecuteJavaScript("window.MyCSharpEntryPoint = $0", CType(MyCSharpMethod, Action(Of String)))

Furthermore, the API provides access to the HTML visual tree (the DOM) from VB.NET, in order to manually manipulate HTML and CSS. To do this, developers can call the method "Interop.GetDiv(uielement)", which gives access to the corresponding `div` of the specified XAML element.

Here is an example that shows how to manually change the background color of the DataGrid XAML by manipulating the corresponding HTML and CSS (note: this is not much useful because the DataGrid already has a Background property in VB.NET, but it shows how to use the low-level API):

```
   Dim xamlDataGrid As DataGrid = New DataGrid()

    ' Let’s subscribe to the “Loaded” event to ensure that the DataGrid is in the HTML tree:
    xamlDataGrid.Loaded += Sub(s, e)
                               ' We get the <div> that corresponds to the DataGrid in the HTML tree:
                               Dim correspondingDiv As Object = Interop.GetDiv(xamlDataGrid)

                               ' We change the background color via CSS:
                               Interop.ExecuteJavaScript("$0.style.backgroundColor = 'red' ", correspondingDiv)
                           End Sub
```

By the way, it is worth noting that the OpenSilver runtime contains a XAML control called "HtmlPresenter", which allows to easily place and mix HTML code inside XAML code.

All of these features are demonstrated in the sample open-source application called "OpenSilver Showcase" which is available online at https://opensilver.net/gallery/

## How to import JavaScript libraries?

It is possible to import JavaScript libraries into an OpenSilver project. This allows to extend the functionality of OpenSilver without much effort. It provides an alternative to importing.NET Standard libraries.

As the ecosystem of JavaScript libraries is very rich and active, thousands of third-party components are immediately accessible from OpenSilver projects.

The most "low level" way to interact with these JavaScript libraries is to use the "Interop" API described above, supplemented by the method "LoadJavaScriptFile(url)". Here is an example:
```
Await Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js");
```
The .js file can be located online, as in the example above, or it can be placed inside the application project (of type OpenSilver). In the latter case, it will be accessible using any of the following URL formats:

* ms-appx:///AssemblyName/Folder/FileName.js
* /AssemblyName;component/Folder/FileName.js

Here is an example:
```
Await Interop.LoadJavaScriptFile("ms-appx:///MyProject/FileSaver.min.js");
```
There are also other methods like “Interop.LoadCSSFile”, which are documented on the site and demonstrated in the sample application called "OpenSilver Showcase".

Userware, the company behind the open-source OpenSilver project, is currently working on providing NuGet packages specific to well-known JavaScript components, allowing to use them directly from XAML and VB.NET without having to manually code JavaScript calls.

For example, packages for Telerik Kendo UI, Syncfusion Essential JS and DevExpress DevExtreme are already available and can be seen in the "Showcase" application on the OpenSilver website. They contain some of the main controls, such as the DataGrid, the RichTextEditor and the Spreadsheet component. Their source code is on GitHub.

## How to import “TypeScript Definition” files?

To avoid having to manually make calls to the "Interop" API to interact with JavaScript libraries, OpenSilver includes the possibility of importing "TypeScript Definition" files, the extension of which is ".d.ts". These are relatively short files that accompany most JavaScript libraries and whose purpose is to provide strong typing to the libraries in question.

Normally these files are intended for developers of TypeScript applications, but OpenSilver has diverted their use in order to auto-generate strongly typed VB.NET wrapper classes that allow to interact with JavaScript libraries in pure VB.NET, that is to say without any manual call to JavaScript.

To use these files, developers can simply copy/paste them to an OpenSilver project and compile the project. The auto-generated files can be seen in the “obj/Debug” subfolder of the project.

In the current version, some advanced features of TypeScript are not yet supported, so a little cleaning up inside the TypeScript Definition file is often necessary to keep only the essentials.

Many examples are available in the GitHub of CSHTML5, which is a sister product also maintained by Userware. Its GitHub is accessible at the following address: [https://github.com/cshtml5?tab=repositories](https://github.com/cshtml5?tab=repositories)
