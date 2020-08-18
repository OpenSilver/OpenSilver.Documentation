# Importing TypeScript Definitions

## Introduction
This feature lets you import and use thousands of JavaScript libraries as if they were written in C#.

Any JavaScript library that comes with a "TypeScript Definition" file can now be used from within a C#/XAML project without writing JavaScript code.

"TypeScript Definition" files are files that have the ".d.ts" extension. Their purpose is to provide strong typing (aka "type safety") to existing JavaScript libraries.

A repository of "TypeScript Definition" files for popular libraries can be found at: https://github.com/DefinitelyTyped/DefinitelyTyped



## Understanding what a "TypeScript Definition" file is
Let's consider for example the JavaScript library ["clipboard.js" by lgarron](https://github.com/lgarron/clipboard-polyfill).

Its goal is let JavaScript developers easily interact with the system clipboard by providing programmatic Copy/Paste functionality.

The library comes in the form of a JS file named "clipboard.js" that looks like this:

(note: what you see below is only a small extract of the much larger original library)
```
// JavaScript library clipboard-js 0.3.1 (extract)

(function (name, definition) {

    var clipboard = {};

    clipboard.copy = function (data) {
        return new Promise(function (resolve, reject) {
            if (typeof data !== "string" && !("text/plain" in data)) {
                throw new Error("You must provide a text/plain type.");
            }
            var strData = (typeof data === "string" ? data : data["text/plain"]);
            var copySucceeded = window.clipboardData.setData("Text", strData);
            if (copySucceeded) {
                resolve();
            } else {
                reject(new Error("Copying was rejected."));
            }
        });
    };

    clipboard.paste = function () {
        return new Promise(function (resolve, reject) {
            var strData = window.clipboardData.getData("Text");
            if (strData) {
                resolve(strData);
            } else {
                // The user rejected the paste request.
                reject(new Error("Pasting was rejected."));
            }
        });
    };

    return clipboard;
}));
```
By looking at the code above, you can see that the JavaScript library creates an object named "clipboard" that contains the methods "copy(data)" and "paste()".

As you can see in the code above, the object is not strongly typed, meaning that the compiler has no way to know what type is expected by the "copy" method, and what type is returned by the "paste" method.

It is possible to use the above library directly "as is", but you will get no intellisense, and no compile-time errors in case of mistakes. Furthermore, if you want to use the above library from your C# code in your OpenSilver project, you will need to make a lot of calls to ["Interop.ExecuteJavaScript(...)"](call-javascript-from-csharp.html)) because no C# objects exist.

However, if the library comes with a "TypeScript Definition" file, or if you can create one, those issues are solved. In fact, the "TypeScript Definition" file will add strong-typing to the library, making it possible to have intellisense, compile-time error checking, and even direct calls to the library from C# thanks to the OpenSilver processing explained below.

The "TypeScript Definition" file for the library mentioned above does exist, it is named "clipboad-js.d.ts", and it can be downloaded from: https://github.com/DefinitelyTyped/DefinitelyTyped/blob/354cec620daccfa0ad167ba046651fb5fef69e8a/types/clipboard-js/index.d.ts

This is what it looks like:
```
// Type definitions for clipboard-js 0.3.1

declare namespace clipboard {

    interface IClipboardJsStatic {
        copy(val: string | Element): Promise<void>;
        paste(): Promise<string>;
    }
}

declare var clipboard: clipboard.IClipboardJsStatic;

declare module 'clipboard-js' {
    export = clipboard;
}
```
By looking at the code above, you can see that the "TypeScript Definition" file tells the compiler that the "clipboard" object has two methods, that the method "Copy" expects a string, and that the "Paste" method returns a Promise<string>.

Note: to understand what the word "Promise" means in JavaScript, you can read [this guide](https://web.dev/promises/).



## How can I use a TypeScript Definition file in my OpenSilver project?

Simply add the TypeScript Definition file to your OpenSilver project and the compiler will automatically take it into account.

 Usually you want to add both:

* the original JavaScript library (for example "clipboard.js")
* AND the corresponding TypeScript Definition file (for example "clipboard-js.d.ts")


## Sample project
Let's create a sample project to demonstrate the use of the "clipboard.js" and "clipboard-js.d.ts" files, as explained above.

This is what the sample project looks like:
<br>

![solutionExplorer](../../images/typescript_importer_solution_explorer.png "Screenshot of Solution Explorer")

This is the content of MainPage.xaml:
```
<Page
    x:Class="TestOpenSilverTypeScriptClipboard.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <StackPanel>
        <Button
            Content="Click to copy some text to the clipboard"
            Click="ButtonCopy_Click"
            Margin="5"
            HorizontalAlignment="Left"/>
        <Button
            Content="Click to paste from the clipboard"
            Click="ButtonPaste_Click"
            Margin="5"
            HorizontalAlignment="Left"/>
    </StackPanel>
</Page>
```
And this is the content of MainPage.xaml.cs:
```
using CSHTML5;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TestOpenSilverTypeScriptClipboard
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }
        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the JavaScript library:
            await Interop.LoadJavaScriptFile("ms-appx:///TestOpenSilverTypeScriptClipboard/clipboard.js");
        }
        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            // Copy (may not function in the Simulator):
            clipboard_js.clipboard_jsClass.clipboard.copy("copy succeeded");
        }
        private void ButtonPaste_Click(object sender, RoutedEventArgs e)
        {
            // Paste (may not function in the Simulator):
            clipboard_js.clipboard_jsClass.clipboard.paste().then(
                onFulfilled: (result) =>
                {
                    MessageBox.Show("The text pasted from the clipboard is the following: " + result);
                },
                onRejected: (error) =>
                {
                    MessageBox.Show(error.ToString());
                });
        }
    }
}
```
As you can see, the JavaScript-based clipboard library is being used as if it was a C#-based library.



## "I have a JavaScript library that needs a &lt;div&gt; or another DOM element in order to render stuff. How can I obtain it?"

You can use the method [CHSTML5.Interop.GetDiv(FrameworkElement)](call-javascript-from-csharp.html#interopgetdivframeworkelement-fe) in order to get the DIV associated to a XAML element. For this method to succeed, the XAML element must be in the Visual Tree. To ensure that it is in the Visual Tree, you can read the [IsLoaded](call-javascript-from-csharp.html#frameworkelementisloaded) property, or you can place your code in the "Loaded" event handler. This approach works best with simple XAML elements, such as Border or Canvas.

Alternatively, you can use the [HtmlPresenter control](html-presenter.md) to put arbitrary HTML/CSS code in your XAML, and then read the ".DomElement" property of the HtmlPresenter control to get a reference to the instantiated DOM element in order to pass it to the JavaScript library.



## What happens exactly when I add a TypeScript Definition file to my OpenSilver project?

When you add a "TypeScript Definition" file to your OpenSilver project, the compiler will automatically analyze it and generate some hidden C# classes that provide a strongly-typed bridge to the JavaScript library.

The generated C# classes are located in the folder "obj\Debug\FILENAME\" (where "FILENAME" is the name of the TypeScript Definition file).

Here is an example of hidden C# file that is automatically generated from the TypeScript Definition file "clipboard-js.d.ts" mentioned above:


```
//------------------------------------------------------------------------------
// <auto-generated>
//     Changes to this file will be
//     lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace clipboard_js
{
    public static class clipboard_jsClass
    {
        public static clipboard.IClipboardJsStatic clipboard
        {
            get
            {
                var jsObj = CSHTML5.Interop.ExecuteJavaScript("clipboard");
                return new clipboard.IClipboardJsStatic(jsObj);
            }
        }
    }
}

namespace clipboard_js.clipboard
{
    public class IClipboardJsStatic
    {
        private object _jsInstance { get; set; }
        public IClipboardJsStatic()
        {
            this._jsInstance = CSHTML5.Interop.ExecuteJavaScript("new clipboard.IClipboardJsStatic()");
        }
        public IClipboardJsStatic(object jsObj)
        {
            this._jsInstance = jsObj;
        }
        public object ToJavaScriptObject()
        {
            return this._jsInstance;
        }
        public Promise copy(MultiType<string, object> val)
        {
            return new Promise(CSHTML5.Interop.ExecuteJavaScript("$0.copy($1)", this._jsInstance, val.ToJavaScriptObject()));
        }
        public Promise<string> paste()
        {
            return new Promise<string>(CSHTML5.Interop.ExecuteJavaScript("$0.paste()", this._jsInstance));
        }
    }
}
```
As you can see, the generated code encapsulates the JavaScript library and provides a sort of "wrap" that redirects every C# call to the underlying JavaScript library.

You can read more about the method "Interop.ExecuteJavaScript" at: [How to call JavaScript from C#](call-javascript-from-csharp.html).



## Notes and Tips
* To force re-generate the C# files from the TypeScript Definition, manually delete the file "TypeScriptDefInfos.xml" that is located in the "obj\Debug" sub-folder of your project folder, and re-build the project.

* If the TypeScript Definition file is very big, it may take several minutes to compile. This is normal and only happens during the first compilation. You can comment out unused portions of the file to speed the compilation up.

* When a TypeScript Definition file is open in a tab in Visual Studio, many misleading errors may be displayed, and some TypeScript code may be underlined even though it is perfectly correct. To see only the "real" compilation errors, be sure to always close the TypeScript Definition file and re-compile.

* If you encounter any issues during the compilation of a TypeScript Definition file, please contact support. As a temporary workaround, you can comment out the portions of the TypeScript Definition file that do not compile properly, and try again.


## See Also
* [How to call JavaScript from C#](call-javascript-from-csharp.html)

* [How to use the HtmlPresenter to put HTML/CSS in your XAML](html-presenter.md)

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
