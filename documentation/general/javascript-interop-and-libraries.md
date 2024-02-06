# Interop between C# (or VB.NET or F#) and JavaScript, importing JS libraries, and importing TypeScript Definitions

## How to interact between C# (or VB.NET or F#) and JavaScript?

OpenSilver contains an easy-to-use API that allows to call JavaScript code from C# (or VB.NET or F#) code. The opposite is also possible by exposing entry points to the JavaScript context.

This API allows to get out of the .NET world in order to interact with the browser, to access low-level functionalities, to extend the framework, to manually manipulate HTML and CSS, and to interact with JavaScript libraries.

This API is widely used for example by the "[OpenSilver.Runtime](https://github.com/OpenSilver/OpenSilver/tree/master/src/Runtime/Runtime)" project, which contains the implementation of controls like TextBox via HTML and CSS.

The general syntax is as follows:

* C#
```
var result = Interop.ExecuteJavaScript("any JS code", params);
```
* VB.NET
```
Dim result = Interop.ExecuteJavaScript("any JS code", params)
```
* F#
```
let result = Interop.ExecuteJavaScript("any JS code", params)
```
Here is an example showing how to display a native browser dialog:

* C#
```
public static void DisplayAlert(string text)
{
    Interop.ExecuteJavaScript("alert($0)", text);
}
```
* VB.NET
```
Public Shared Sub DisplayAlert(ByVal text As String)
    Interop.ExecuteJavaScript("alert($0)", text)
End Sub
```
* F#
```
static member displayAlert(text: string) =
    Interop.ExecuteJavaScript("alert($0)", text)
End Sub
```

In the JavaScript code passed as first argument, "$0" will be substituted with the first subsequent argument, which in this instance is the variable "text." If there are additional arguments, "$1" will be substituted with the second argument, and so forth. When you use strings as parameters, the result will be the same as replacing the "$0" placeholders with the corresponding strings. The main purpose of this syntax is to preserve strong typing for the cases where the types are more complex, as will be illustrated below.

This API also allows you to retrieve objects from the JavaScript context into the C# (or VB.NET or F#) context. This will in turn let you chain several calls, as shown in the following example:

* C#
```
// Let's get the current URL by chaining multiple JavaScript calls in our C#:
object jsWindow = Interop.ExecuteJavaScript("window");
object location = Interop.ExecuteJavaScript("$0.location", jsWindow);
string currentUrl = Interop.ExecuteJavaScript("$0.href", location).ToString();
MessageBox.Show("The current URL is: " + currentUrl);
```
* VB.NET
```
' Let's get the current URL by chaining multiple JavaScript calls in our VB.NET:
Dim jsWindow As Object = Interop.ExecuteJavaScript("window")
Dim location As Object = Interop.ExecuteJavaScript("$0.location", jsWindow)
Dim currentUrl As String = Interop.ExecuteJavaScript("$0.href", location).ToString()
MessageBox.Show("The current URL is: " & currentUrl)
```
* F#
```
// Let's get the current URL by chaining multiple JavaScript calls in our F#:
let jsWindow = Interop.ExecuteJavaScript("window")
let location = Interop.ExecuteJavaScript("$0.location", jsWindow)
let currentUrl = Interop.ExecuteJavaScript("$0.href", location).ToString()
MessageBox.Show("The current URL is: " + currentUrl) |> ignore
```

Another feature is the support of events, "delegates" and "callbacks". Here is an example showing how to asynchronously obtain the GPS coordinates of the user, via the browser:

* C#
```
void GetGpsPosition()
{
    Interop.ExecuteJavaScript(@"navigator.geolocation.getCurrentPosition($0);", (Action<object>)GpsReceived);
}
void GpsReceived(object jsEventArgs)
{
    double lat = Convert.ToDouble(Interop.ExecuteJavaScript("$0.coords.latitude", jsEventArgs));
    double lon = Convert.ToDouble(Interop.ExecuteJavaScript("$0.coords.longitude", jsEventArgs));
    MessageBox.Show("Latitude: " + lat.ToString() + "  Longitude: " + lon.ToString());
}
```
* VB.NET
```
Private Sub GetGpsPosition()
    Interop.ExecuteJavaScript("navigator.geolocation.getCurrentPosition($0);", CType(AddressOf GpsReceived, Action(Of Object)))
End Sub

Private Sub GpsReceived(ByVal jsEventArgs As Object)
    Dim lat = Convert.ToDouble(Interop.ExecuteJavaScript("$0.coords.latitude", jsEventArgs))
    Dim lon = Convert.ToDouble(Interop.ExecuteJavaScript("$0.coords.longitude", jsEventArgs))
    MessageBox.Show("Latitude: " & lat.ToString() & "  Longitude: " & lon.ToString())
End Sub
```
* F#
```
member this.getGpsPosition() =
    let gpsReceivedDelegate : Delegate = upcast Func<obj, unit>(fun obj -> this.GpsReceived(obj))
    Interop.ExecuteJavaScript(@"navigator.geolocation.getCurrentPosition($0);", gpsReceivedDelegate)

member this.GpsReceived(jsEventArgs: obj) =
    let lat = Convert.ToDouble(Interop.ExecuteJavaScript("$0.coords.latitude", jsEventArgs))
    let lon = Convert.ToDouble(Interop.ExecuteJavaScript("$0.coords.longitude", jsEventArgs))
    MessageBox.Show(sprintf "Latitude: %f  Longitude: %f" lat lon) |> ignore
```

To allow the call of C# (or VB.NET or F#) code from JavaScript, the developer can simply expose entry points by exposing "delegates", as shown in the example above.

Here is another example showing how to expose a C# (or VB.NET or F#) method taking an argument of type String so that it can be called from JavaScript:

* C#
```
Interop.ExecuteJavaScript("window.MyCSharpEntryPoint = $0", (Action<string>)MyMethod);
```
* VB.NET
```
Interop.ExecuteJavaScript("window.MyCSharpEntryPoint = $0", CType(AddressOf MyMethod, Action(Of String)))
```
* F#
```
let actionDelegate : Delegate = upcast Func<string, unit>(fun str -> this.MyMethod(str))
Interop.ExecuteJavaScript("window.MyCSharpEntryPoint = $0", actionDelegate);
```

Furthermore, the API provides access to the HTML visual tree (the DOM) from C# (or VB.NET or F#), in order to manually manipulate HTML and CSS. To do this, developers can call the method `Interop.GetDiv(UIElement)`, which gives access to the corresponding `div` of the specified XAML element.

Here is an example that shows how to manually change the background color of the XAML DataGrid by manipulating the corresponding HTML and CSS (note: this is not very useful because the DataGrid already has a Background property, but it is suitable for demonstration purposes):

* C#
```
var xamlDataGrid = new DataGrid();

// Let's subscribe to the "Loaded" event to ensure that the DataGrid is in the HTML tree:
xamlDataGrid.Loaded += (s, e) =>
{
    // We get the <div> that corresponds to the DataGrid in the HTML tree:
    object correspondingDiv = Interop.GetDiv(xamlDataGrid);

    // We change the background color via CSS:
    Interop.ExecuteJavaScript("$0.style.backgroundColor = 'red' ", correspondingDiv);
};
```
* VB.NET
```
Dim xamlDataGrid As DataGrid = New DataGrid()

' Let's subscribe to the "Loaded" event to ensure that the DataGrid is in the HTML tree:
AddHandler xamlDataGrid.Loaded, Sub(s, e)
                                    ' We get the <div> that corresponds to the DataGrid in the HTML tree:
                                    Dim correspondingDiv As Object = Interop.GetDiv(xamlDataGrid)

                                    ' We change the background color via CSS:
                                    Interop.ExecuteJavaScript("$0.style.backgroundColor = 'red' ", correspondingDiv)
                                End Sub
```
* F#
```
let xamlDataGrid = DataGrid()

// Let's subscribe to the "Loaded" event to ensure that the DataGrid is in the HTML tree:
xamlDataGrid.Loaded.Add(fun e ->
    // We get the <div> that corresponds to the DataGrid in the HTML tree:
    let correspondingDiv = Interop.GetDiv(xamlDataGrid)

    // We change the background color via CSS:
    Interop.ExecuteJavaScript("$0.style.backgroundColor = 'red'", correspondingDiv) |> ignore
)
```

It is worth noting that the OpenSilver runtime contains a XAML control called "HtmlPresenter", which allows to easily place and mix HTML code inside XAML code.

All of these features are demonstrated in the sample open-source application called "OpenSilver Showcase" which is available online at https://opensilver.net/gallery/

## How to import JavaScript libraries?

It is possible to go beyond OpenSilver's functionalities effortlessly, by importing Javascript libraries. This provides an alternative to importing .NET Standard libraries.

As the ecosystem of JavaScript libraries is very rich and active, thousands of third-party components are immediately accessible for OpenSilver projects.

The most "low level" way to interact with these JavaScript libraries is to use the "Interop" API described above, supplemented by the method `LoadJavaScriptFile(url)`. Here is an example:

* C#
```
await Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js");
```
* VB.NET
```
Await Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js")
```
* F#
```
let! result = await Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js") |> Async.AwaitTask
```
The .js file can be located online, as in the example above, or it can be placed inside the application project (of type OpenSilver). In the latter case, it will be accessible using any of the following URL formats:

* ms-appx:///AssemblyName/Folder/FileName.js
* /AssemblyName;component/Folder/FileName.js

Here is an example:
* C#
```
await Interop.LoadJavaScriptFile("ms-appx:///MyProject/FileSaver.min.js");
```
* VB.NET
```
Await Interop.LoadJavaScriptFile("ms-appx:///MyProject/FileSaver.min.js")
```
* F#
```
let! result = Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js") |> Async.AwaitTask
```
There are also other methods like `Interop.LoadCSSFile`, which are documented [on the site](https://doc.opensilver.net/reference/OpenSilver.Interop.html) and demonstrated in the sample application called "OpenSilver Showcase".

Userware, the company behind the open-source project OpenSilver, is currently working on providing NuGet packages specific to well-known JavaScript components, allowing to use them directly from XAML and C# (or VB.NET or F#) without having to manually code JavaScript calls.

For example, packages for Telerik Kendo UI, Syncfusion Essential JS and DevExpress DevExtreme are already available and can be seen in the "Showcase" application, which can be found [here](https://opensilver.net/gallery/) on the OpenSilver website. They contain some of the main controls, such as the DataGrid, the RichTextEditor and the Spreadsheet component. Their source code is on GitHub.

## How to import “TypeScript Definition” files?

To avoid having to manually make calls to the `Interop` API to interact with JavaScript libraries, OpenSilver includes the possibility of importing "TypeScript Definition" files, the extension of which is ".d.ts". These are relatively short files that accompany most JavaScript libraries and their purpose is to provide strong typing to the libraries in question.

Normally these files are intended for developers of TypeScript applications, but OpenSilver has diverted their use in order to auto-generate strongly typed C# (or VB.NET or F#) wrapper classes that allow to interact with JavaScript libraries in pure C# (or VB.NET or F#), that is to say without any manual call to JavaScript.

To use these files, developers can simply copy/paste them into an OpenSilver project and compile the project. The auto-generated files can be seen in the “obj/Debug” subfolder of the project.

In the current version, some advanced features of TypeScript are not yet supported, so a little clean up inside the TypeScript Definition file is often necessary to keep only the essentials.

Many examples are available in the GitHub of CSHTML5, which is a sister product also maintained by Userware. Its GitHub is accessible at the following address: [https://github.com/cshtml5?tab=repositories](https://github.com/cshtml5?tab=repositories)
