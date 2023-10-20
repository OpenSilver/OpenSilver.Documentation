# Interop between VB.NET and JavaScript, importing JS libraries, and importing TypeScript Definitions

## General concepts

OpenSilver provides an easy-to-use API that enables you to call JavaScript code from VB.NET code and vice versa. You can expose VB.NET entry points to the JavaScript context and access them from JavaScript code.

This API lets you interact with the browser, access low-level features, extend the framework, manipulate HTML and CSS manually, and use JavaScript libraries. You can go beyond the .NET world and leverage the power of web technologies.

This API is widely used for example by the "[OpenSilver.Runtime](https://github.com/OpenSilver/OpenSilver/tree/master/src/Runtime/Runtime)" project, which contains the implementation of controls like TextBox via HTML and CSS.

The general syntax is as follows:
```
Dim result = Interop.ExecuteJavaScript("any JS code", params)
```

The first argument is you JavaScript code and the second is a parameter passed to the JavaScript (we tal more about parameters after).

Here is an example showing how to display a native browser dialog passing a custom text:

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

Another powerful feature is support for events, "delegates" and "callbacks". Here is an example showing how to obtain, via the browser asynchronously, the GPS coordinates where the user is located:

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



## "I have a JavaScript library that needs &lt;div&gt; or another DOM element in order to render stuff. How can I obtain it?"

You can use the method [CSHTML5.Interop.GetDiv(FrameworkElement)](#interopgetdivframeworkelement-fe) in order to get the DIV associated to a XAML element. For this method to succeed, the XAML element must be in the Visual Tree. To ensure that it is in the Visual Tree, you can read the IsLoaded property, or you can place your code in the "Loaded" event handler. This approach works best with simple XAML elements, such as Border or Canvas.

Alternatively, you can use the [HtmlPresenter control](html-presenter.md)  to put arbitrary HTML/CSS code in your XAML, and then read the ".DomElement" property of the HtmlPresenter control to get a reference to the instantiated DOM element in order to pass it to the JavaScript library.



## How to create VB/XAML-based wrappers around JS libraries
A VB/XAML-based wrapper around a JavaScript library is useful because it allows the person who uses the wrapper to consume the JavaScript library in pure VB and XAML, without ever having to write any JavaScript code.

For example, the [ZIP Compression](http://forums.cshtml5.com/viewtopic.php?f=7&t=521) extension is a C#-based wrapper around the [JSZip](https://stuk.github.io/jszip/) JavaScript library. It encapsulates all the interop between JS and C# so that the rest of the application can interact with the JavaScript library using only pure C# code.

To create such a wrapper in VB, follow these steps:

* Find the .JS files that correspond to the library. You can either point to an online location where those JS files are hosted (such as those referenced on [CDN JS](https://cdnjs.com/)) or you can download the JS files and add them to your OpenSilver project.

* In your OpenSilver project, create a new VB class and write the ["Interop.LoadJavaScriptFile()"](#await-interoploadjavascriptfilestring-url) code that will load the JS file.

* Now use the ["Interop.ExecuteJavaScript()"](#interopexecutejavascript) method to interact with the JavaScript library from within your VB code.

* Read the other sections of this page to learn more, look at some[ examples](#examples), or [contact us](https://opensilver.net/contact.aspx).

* You can automate the creation of such a wrapper by [importing TypeScript Definition files](Importing-typescript-definitions.html).


## Examples
<table>
<thead>
	<tr>
		<th>Example</th>
		<th>Source Code	</th>
		<th>How it works</th>
	</tr>
</thead>
<tbody>
	<tr>
		<td>WebSockets extension</td>
		<td><a href="http://forums.cshtml5.com/viewtopic.php?f=7&t=276">Link</a></td>
		<td>It wraps the native JavaScript "WebSocket" class into a C#-based class that can be consumed by OpenSilver applications</td>
	</tr>
	<tr>
		<td>Print extension</td>
		<td><a href="http://forums.cshtml5.com/viewtopic.php?f=7&t=275">Link</a></td>
		<td>It extracts the HTML DOM element that corresponds to the specified FrameworkElement, copies it into a new browser window, and then calls the browser Print command on the new window.</td>
	</tr>
  <tr>
    <td>File Open Dialog extension</td>
    <td><a href="http://forums.cshtml5.com/viewtopic.php?f=7&t=522">Link</a></td>
    <td>It adds the <code>&lt;input type='file'&gt;</code> tag to the HTML DOM, and listens to the JavaScript "Change" event to return a JS blob or text.</td>
  </tr>
  <tr>
    <td>File Save extension</td>
    <td><a href="http://forums.cshtml5.com/viewtopic.php?f=7&t=520">Link</a></td>
    <td>It wraps the open-source JavaScript ["FileSaver.js"](https://github.com/eligrey/FileSaver.js/) library into a C#/XAML class for consumption by OpenSilver-based apps.</td>
  </tr>
  <tr>
    <td>Unofficial ArcGIS Mapping Control for OpenSilver</td>
    <td><a href="http://forums.cshtml5.com/viewtopic.php?f=7&t=272">Link</a></td>
    <td>It wraps the JavaScript-based version of the [ArcGIS mapping library](https://developers.arcgis.com/javascript/) into a C#/XAML-based control that can be consumed from pure C#/XAML code.</td>
  </tr>
  <tr>
    <td>ZIP Compression extension</td>
    <td><a href="http://forums.cshtml5.com/viewtopic.php?f=7&t=521">Link</a></td>
    <td>	It wraps the open-source JavaScript ["JSZip.js"](https://stuk.github.io/jszip/) library into a C#/XAML class that mimics the method signatures of Ionic.Zip (DotNetZip).</td>
  </tr>
</tbody>
</table>

## Reference
### Interop.ExecuteJavaScript(...)
Use this method to call JavaScript code from within your VB code.

For example, the following VB code will display the current URL:

`MessageBox.Show("The current URL is: " & CSHTML5.Interop.ExecuteJavaScript("location.toString()"))`

The following C# code will retrieve the current Width of the browser window by reading the JavaScript "screen.width" property:

`dim screenWidth as Double = Convert.ToDouble(CSHTML5.Interop.ExecuteJavaScript("screen.width"))`

You can pass VB.net variables to your JavaScript code by passing them as additional arguments to the "Interop.ExecuteJavaScript" method, and using the placeholders $0, $1, $2.... Here is an example:
```
Private Sub DisplayAMessage(ByVal messageToDisplay As String)
    CSHTML5.Interop.ExecuteJavaScript("alert($0)", messageToDisplay)
End Sub
```
You can also do it the other way round, that is, call VB.net from within your JavaScript code

To call VB.net from JavaScript, you need to pass a VB.net-based callback to your "ExecuteJavaScript" code, like in the following C# example:
```
Class SurroundingClass
    Private Sub Main()
        CSHTML5.Interop.ExecuteJavaScript("

        // This is JavaScript code
        alert('Hello from JavaScript');

        // Let's call the C# method 'MyCSharpMethod' from this JavaScript code. Due to the fact that it is passed as the first parameter to this ExecuteJavaScript call, we can access it with $0:
        $0();

    ", CType(AddressOf MyCSharpMethod, Action))
    End Sub

    Private Sub MyCSharpMethod()
        System.Windows.MessageBox.Show("Hello from VB")
    End Sub
End Class
```

You can see another example in the source code of the [WebSocket extension](http://forums.cshtml5.com/viewtopic.php?f=7&t=276) (where the JavaScript-based websocket calls the C#-based OnOpenCallback method) or the [File Open Dialog extension](http://forums.cshtml5.com/viewtopic.php?f=7&t=522) (where the Open Dialog of the browser calls the OnFileOpened method of C#).


## await Interop.LoadJavaScriptFile(string url)
Use this method to load a JavaScript file from either an online location (http/https) or the local project.

The following C# example loads the "FileSaver.js" file from an online location:

`Private Function CSHTML5.Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js") As Await`

To load a file that you have added locally to your OpenSilver project, use any of the two following URL syntaxes:

* ms-appx:///AssemblyName/Folder/FileName.js

* /AssemblyName;component/Folder/FileName.js
Here is an example:

`Private Function CSHTML5.Interop.LoadJavaScriptFile("ms-appx:///MyProject/FileSaver.min.js") as Await`



## Interop.LoadJavaScriptFilesAsync(IEnumerable<string> urls, Action callback)
Use this method to load a list of JavaScript files from either an online location (http/https) or the local project.

Unlike the method [LoadJavaScriptFile](#await-interoploadjavascriptfilestring-url), this one does not use the async/await pattern. Instead, the provided "callback" is called when the scripts have been loaded.

The following VB example loads two JavaScript files from an online location and displays a message when done:
```
CSHTML5.Interop.LoadJavaScriptFilesAsync(New String() {
    "https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.2/jspdf.min.js", 
    "https://cdnjs.cloudflare.com/ajax/libs/kendo-ui-core/2014.1.416/js/kendo.core.min.js"}, 

    Function()
        MessageBox.Show("The scripts have been loaded.")
    End Function)
```

To load a file that you have added locally to your OpenSilver project, use any of the two following URL syntaxes:

* ms-appx:///AssemblyName/Folder/FileName.js

* /AssemblyName;component/Folder/FileName.js


## await Interop.LoadCssFile(string url)
This method works the same as [Interop.LoadJavaScriptFile()](#await-interoploadjavascriptfilestring-url)


## Interop.LoadCssFilesAsync(string url, Action callback)
This method works the same as [Interop.LoadJavaScriptFilesAsync()](#interoploadjavascriptfilesasyncienumerable-urls-action-callback)


## Interop.GetDiv(FrameworkElement fe)
This method returns the HTML DOM element that corresponds to the specified FrameworkElement.

*Important note:* the FrameworkElement must be in the Visual Tree for this method to succeed. To ensure that the element is in the Visual Tree, you can read the [IsLoaded](#frameworkelementisloaded) property, or you can place your code in the "Loaded" event handler.

Here is an example:
```
Public Partial Class MainPage
    Inherits Page

    Public Sub New()
        Me.InitializeComponent()
        Me.Loaded += AddressOf MainPage_Loaded
    End Sub

    Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim rootDiv As Object = CSHTML5.Interop.GetDiv(Me)
        MessageBox.Show("The width of the root div is: " & Convert.ToDouble(CSHTML5.Interop.ExecuteJavaScript("$0.offsetWidth", rootDiv)).ToString())
    End Sub
End Class
```
You can see a more advanced example by looking at the source code of the [File Open Dialog extension](http://forums.cshtml5.com/viewtopic.php?f=7&t=522).

You may also be interested by the [HtmlPresenter control](html-presenter.md).



## Interop.IsRunningInTheSimulator
Returns "True" if the application is running in VB inside the Simulator, and "False" if the application is running in the web browser.

This property is useful when you need to do something different depending on whether the application is running in the browser or in the Simulator, due to the fact that some JavaScript features are not possible in the Simulator.

For example, if you want to open a new browser window, which cannot be done in the Simulator, you may want to display a "Not supported" message if the value of this property is True.



## FrameworkElement.IsLoaded
Returns True if the FrameworkElement is in the Visual Tree, and False otherwise.

This property is particularly useful when used in conjunction with the [Interop.GetDiv](#interopgetdivframeworkelement-fe) method, which only works when the FrameworkElement is in the Visual Tree.

When this property returns True, it also means that the FrameworkElement is present on the HTML DOM tree.

Note: you can listen to the Loaded and Unloaded events to be notified when this property changes.

## How to import “TypeScript Definition” files?
To avoid having to manually make calls to the "Interop" API to interact with JavaScript libraries, OpenSilver includes the possibility of importing "TypeScript Definition" files, the extension of which is ".d.ts". These are relatively short files that accompany most JavaScript libraries and whose purpose is to provide strong typing to the libraries in question.

Normally these files are intended for developers of TypeScript applications, but OpenSilver has diverted their use in order to auto-generate strongly typed VB.NET wrapper classes that allow to interact with JavaScript libraries in pure VB.NET, that is to say without any manual call to JavaScript.

To use these files, developers can simply copy/paste them to an OpenSilver project and compile the project. The auto-generated files can be seen in the “obj/Debug” subfolder of the project.

In the current version, some advanced features of TypeScript are not yet supported, so a little cleaning up inside the TypeScript Definition file is often necessary to keep only the essentials.

Many examples are available in the GitHub of CSHTML5, which is a sister product also maintained by Userware. Its GitHub is accessible at the following address: https://github.com/cshtml5?tab=repositories


## See Also
* [How to use the HtmlPresenter to put HTML/CSS in your XAML](html-presenter.md)
* [Importing TypeScript Definitions](Importing-typescript-definitions.html)

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
