# How to Call JavaScript from C#
## General Concepts

* A OpenSilver project is mainly written in C# and XAML. When you compile the project, your OpenSilver code gets automatically compiled into WebAssembly.

* If you want, you can place JavaScript code directly inside your C# code by calling the "Interop.ExecuteJavaScript" method.

<!--* For example, the following C# code:
`MessageBox.Show("The current URL is: " + CSHTML5.Interop.ExecuteJavaScript("location.toString()"));`
gets compiled into the following JavaScript code:
`alert('The current URL is: ' + location.toString());`-->

* The following C# example shows how to retrieve the current Width of the browser window by reading the JavaScript "screen.width" property:
`double screenWidth = Convert.ToDouble(CSHTML5.Interop.ExecuteJavaScript("screen.width"));`

* You can pass C# variables to your JavaScript code by passing them as additional arguments to the "Interop.ExecuteJavaScript" method, and using the placeholders $0, $1, $2.... Here is an example:
```
void DisplayAMessage(string messageToDisplay)
{
   CSHTML5.Interop.ExecuteJavaScript("alert($0)", messageToDisplay);
}
```

* You can also do it the other way round, that is, call C# from within your JavaScript code.

To call C# from JavaScript, you need to pass a C#-based callback to your "ExecuteJavaScript" code, like in the following C# example:
```
void Main()
{
    CSHTML5.Interop.ExecuteJavaScript(@"

        // This is JavaScript code
        alert('Hello from JavaScript');

        // Let's call the C# method 'MyCSharpMethod' from this JavaScript code. Due to the fact that it is passed as the first parameter to this ExecuteJavaScript call, we can access it with $0:
        $0();

    ", (Action)MyCSharpMethod);
}

void MyCSharpMethod()
{
    System.Windows.MessageBox.Show("Hello from C#");
}
```

You can see another example in the source code of the [WebSocket extension](http://forums.cshtml5.com/viewtopic.php?f=7&t=276) (where the JavaScript-based websocket calls the C#-based OnOpenCallback method) or the [File Open Dialog extension](http://forums.cshtml5.com/viewtopic.php?f=7&t=522) (where the Open Dialog of the browser calls the OnFileOpened method of C#).


## How to embed JavaScript/CSS files in your OpenSilver project

You can add JS/CSS files to your OpenSilver project. Those files will be automatically copied "as is" to the output folder.

You can then load them by calling ["Interop.LoadJavaScriptFile()"](#await-interoploadjavascriptfilestring-url) and ["Interop.LoadCssFile()"](#await-interoploadcssfilestring-url).



## "I have a JavaScript library that needs &lt;div&gt; or another DOM element in order to render stuff. How can I obtain it?"

You can use the method [CSHTML5.Interop.GetDiv(FrameworkElement)](#interopgetdivframeworkelement-fe) in order to get the DIV associated to a XAML element. For this method to succeed, the XAML element must be in the Visual Tree. To ensure that it is in the Visual Tree, you can read the IsLoaded property, or you can place your code in the "Loaded" event handler. This approach works best with simple XAML elements, such as Border or Canvas.

Alternatively, you can use the [HtmlPresenter control](html-presenter.md)  to put arbitrary HTML/CSS code in your XAML, and then read the ".DomElement" property of the HtmlPresenter control to get a reference to the instantiated DOM element in order to pass it to the JavaScript library.



## How to create C#/XAML-based wrappers around JS libraries
A C#/XAML-based wrapper around a JavaScript library is useful because it allows the person who uses the wrapper to consume the JavaScript library in pure C# and XAML, without ever having to write any JavaScript code.

For example, the [ZIP Compression](http://forums.cshtml5.com/viewtopic.php?f=7&t=521) extension is a C#-based wrapper around the [JSZip](https://stuk.github.io/jszip/) JavaScript library. It encapsulates all the interop between JS and C# so that the rest of the application can interact with the JavaScript library using only pure C# code.

To create such a wrapper, follow these steps:

* Find the .JS files that correspond to the library. You can either point to an online location where those JS files are hosted (such as those referenced on [CDN JS](https://cdnjs.com/)) or you can download the JS files and add them to your OpenSilver project.

* In your OpenSilver project, create a new C# class and write the ["Interop.LoadJavaScriptFile()"](#await-interoploadjavascriptfilestring-url) code that will load the JS file.

* Now use the ["Interop.ExecuteJavaScript()"](#interopexecutejavascript) method to interact with the JavaScript library from within your C# code.

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
Use this method to call JavaScript code from within your C# code.

For example, the following C# code will display the current URL:

`MessageBox.Show("The current URL is: " + CSHTML5.Interop.ExecuteJavaScript("location.toString()"));`

The following C# code will retrieve the current Width of the browser window by reading the JavaScript "screen.width" property:

`double screenWidth = Convert.ToDouble(CSHTML5.Interop.ExecuteJavaScript("screen.width"));`

You can pass C# variables to your JavaScript code by passing them as additional arguments to the "Interop.ExecuteJavaScript" method, and using the placeholders $0, $1, $2.... Here is an example:
```
void DisplayAMessage(string messageToDisplay)
{
   CSHTML5.Interop.ExecuteJavaScript("alert($0)", messageToDisplay);
}
```
You can also do it the other way round, that is, call C# from within your JavaScript code

To call C# from JavaScript, you need to pass a C#-based callback to your "ExecuteJavaScript" code, like in the following C# example:
```
void Main()
{
    CSHTML5.Interop.ExecuteJavaScript(@"

        // This is JavaScript code
        alert('Hello from JavaScript');

        // Let's call the C# method 'MyCSharpMethod' from this JavaScript code. Due to the fact that it is passed as the first parameter to this ExecuteJavaScript call, we can access it with $0:
        $0();

    ", (Action)MyCSharpMethod);
}

void MyCSharpMethod()
{
    System.Windows.MessageBox.Show("Hello from C#");
}
```

You can see another example in the source code of the [WebSocket extension](http://forums.cshtml5.com/viewtopic.php?f=7&t=276) (where the JavaScript-based websocket calls the C#-based OnOpenCallback method) or the [File Open Dialog extension](http://forums.cshtml5.com/viewtopic.php?f=7&t=522) (where the Open Dialog of the browser calls the OnFileOpened method of C#).


## await Interop.LoadJavaScriptFile(string url)
Use this method to load a JavaScript file from either an online location (http/https) or the local project.

The following C# example loads the "FileSaver.js" file from an online location:

`await CSHTML5.Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js");`

To load a file that you have added locally to your OpenSilver project, use any of the two following URL syntaxes:

* ms-appx:///AssemblyName/Folder/FileName.js

* /AssemblyName;component/Folder/FileName.js
Here is an example:

`await CSHTML5.Interop.LoadJavaScriptFile("ms-appx:///MyProject/FileSaver.min.js");`



## Interop.LoadJavaScriptFilesAsync(IEnumerable<string> urls, Action callback)
Use this method to load a list of JavaScript files from either an online location (http/https) or the local project.

Unlike the method [LoadJavaScriptFile](#await-interoploadjavascriptfilestring-url), this one does not use the async/await pattern. Instead, the provided "callback" is called when the scripts have been loaded.

The following C# example loads two JavaScript files from an online location and displays a message when done:
```
CSHTML5.Interop.LoadJavaScriptFilesAsync(new string[]
{
    @"https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.2/jspdf.min.js",
    @"https://cdnjs.cloudflare.com/ajax/libs/kendo-ui-core/2014.1.416/js/kendo.core.min.js"
},
() =>
{
    MessageBox.Show("The scripts have been loaded.");
});
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
public partial class MainPage : Page
{
  public MainPage()
  {
    this.InitializeComponent();

    this.Loaded += MainPage_Loaded;
  }

  void MainPage_Loaded(object sender, RoutedEventArgs e)
  {
    object rootDiv = CSHTML5.Interop.GetDiv(this);

    MessageBox.Show("The width of the root div is: " + Convert.ToDouble(CSHTML5.Interop.ExecuteJavaScript("$0.offsetWidth", rootDiv)).ToString());
  }
}
```
You can see a more advanced example by looking at the source code of the [File Open Dialog extension](http://forums.cshtml5.com/viewtopic.php?f=7&t=522).

You may also be interested by the [HtmlPresenter control](html-presenter.md).



## Interop.IsRunningInTheSimulator
Returns "True" if the application is running in C# inside the Simulator, and "False" if the application is running in the web browser.

This property is useful when you need to do something different depending on whether the application is running in the browser or in the Simulator, due to the fact that some JavaScript features are not possible in the Simulator.

For example, if you want to open a new browser window, which cannot be done in the Simulator, you may want to display a "Not supported" message if the value of this property is True.



## FrameworkElement.IsLoaded
Returns True if the FrameworkElement is in the Visual Tree, and False otherwise.

This property is particularly useful when used in conjunction with the [Interop.GetDiv](#interopgetdivframeworkelement-fe) method, which only works when the FrameworkElement is in the Visual Tree.

When this property returns True, it also means that the FrameworkElement is present on the HTML DOM tree.

Note: you can listen to the Loaded and Unloaded events to be notified when this property changes.



## See Also
* [How to use the HtmlPresenter to put HTML/CSS in your XAML](html-presenter.md)
* [Importing TypeScript Definitions](Importing-typescript-definitions.html)

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
