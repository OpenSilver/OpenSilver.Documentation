# How to interact between HTML / JavaScript and C# / XAML?

OpenSilver contains an easy-to-use API that allows to call JavaScript code from C# code. The opposite is also possible by exposing C# entry points to the JavaScript context.

This API allows to get out of the .NET world in order to interact with the browser, to access low-level functionalities, to extend the framework, to manually manipulate HTML and CSS, and to interact with JavaScript libraries.

This API is widely used for example by the "OpenSilver.Runtime" project, which contains the implementation of controls like TextBox via HTML and CSS.

The general syntax is as follows:
```
var result = Interop.ExecuteJavaScript("any JS code", params);
```

Here is an example showing how to display a native browser dialog:

```
public static void DisplayAlert(string text)
{
    Interop.ExecuteJavaScript("alert($0)", text);
}
```

$0 is replaced by the first argument, $1 is replaced by the second, etc. For strings it is the same as concatenating strings, but the purpose of this syntax is to preserve string typing in the case where the types are more complex, as we will see below.

One of the functionalities supported by this API is the passage of objects from the JavaScript context to the C# context, so as to be able to chain several calls, as shown in the following example:

```
// Let's get the current URL by chaining multiple JavaScript calls in our C#:
object jsWindow = Interop.ExecuteJavaScript("window");
object location = Interop.ExecuteJavaScript("$0.location", jsWindow);
string currentUrl = Interop.ExecuteJavaScript("$0.href", location).ToString();
MessageBox.Show("The current URL is: " + currentUrl);
```

Another feature is support for events, "delegates" and "callbacks". Here is an example showing how to obtain, via the browser asynchronously, the GPS coordinates where the user is located:

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

To allow the call of C# code from JavaScript code, the developer can simply expose entry points by exposing "delegates", as shown in the example above.

Here is another example showing how to expose a  # method taking an argument of type String so that it can be called from JavaScript code:

    Interop.ExecuteJavaScript("window.MyCSharpEntryPoint = $0", (Action<string>)MyCSharpMethod);

Furthermore, the API provides access to the HTML visual tree (the DOM) from C#, in order to manually manipulate HTML and CSS. To do this, developers can call the method "Interop.GetDiv(uielement)", which gives access to the <div> corresponding to the specified XAML element.

Here is an example that shows how to manually change the background color of the DataGrid XAML by manipulating the corresponding HTML and CSS (note: this is not much useful because the DataGrid already has a Background property in C#, but it shows how to use the low-level API):

```
    DataGrid xamlDataGrid = new DataGrid();

    // Let’s subscribe to the “Loaded” event to ensure that the DataGrid is in the HTML tree:
    xamlDataGrid.Loaded += (s, e) =>
    {
        // We get the <div> that corresponds to the DataGrid in the HTML tree:
        object correspondingDiv = Interop.GetDiv(xamlDataGrid);

        // We change the background color via CSS:
        Interop.ExecuteJavaScript("$0.style.backgroundColor = ‘red’ ", correspondingDiv);
    };
```

By the way, it is worth noting that the OpenSilver runtime contains a XAML control called "HtmlPresenter", which allows to easily place and mix HTML code inside XAML code.

All of these features are demonstrated in the sample open-source application called "OpenSilver Showcase" which is available online at https://opensilver.net/gallery/
