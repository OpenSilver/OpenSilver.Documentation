# Internals - How does OpenSilver work internally?

## In-depth video

> [!Video https://www.youtube.com/embed/xaTtev_VV2k]

## Internals overview

An OpenSilver project is none other than a .NET Standard type project that references the OpenSilver NuGet package. The presence of this NuGet package has several consequences.

### 1. References

First, the NuGet package will add a reference to the "OpenSilver.dll" assembly, which contains the implementation of the XAML classes. For example, the "Button" and "TextBox" classes are located there.
These classes can be seen by going to the "[src/Runtime/Runtime](https://github.com/OpenSilver/OpenSilver/tree/master/src/Runtime/Runtime)" directory of the OpenSilver C# source code on GitHub, at the address: https://github.com/OpenSilver/OpenSilver
#### Interactions with the DOM tree
1. Low-level elements, such as the "TextBox", "MediaElement" or "WebBrowser" controls, contain a method named "CreateDomElement", which specifies how they should render in HTML.

  For example, "TextBox" will create a `<div>` in html, "MediaElement" will create a tag `<video>` in html and "WebBrowser" will create a `<iframe>` in html.

  * For C# Code
  ![Source Code](/images/12.SourceCode.png "The source code of the WebBrowser control")
  
  * For VB.NET Code
    
        Public Overrides Function CreateDomElement(ByVal parentRef As Object, <Out> ByRef domElementWhereToPlaceChildren As Object) As Object
          Dim outerDiv As Object
          Dim outerDivStyle = INTERNAL_HtmlDomManager.CreateDomLayoutElementAppendItAndGetStyle("div", parentRef, Me, outerDiv)
          outerDivStyle.width = "100%"
          outerDivStyle.height = "100%"
      
          Dim iFrameStyle = INTERNAL_HtmlDomManager.CreateDomElementAppendItAndGetStyle("iframe", outerDiv, Me, _iFrame)
          iFrameStyle.width = "100%"
          iFrameStyle.height = "100%"
          iFrameStyle.border = "none"
      
          DisposeJsCallbacks()
          _jsCallbackOnIframeLoaded = JavaScriptCallback.Create(OnIframeLoad, True)
      
          Dim sIFrame As String = INTERNAL_InteropImplementation.GetVariableStringForJS(_iFrame)
          OpenSilver.Interop.ExecuteJavaScriptFastAsync($"{sIFrame}.onload = {INTERNAL_InteropImplementation.GetVariableStringForJS(_jsCallbackOnIframeLoaded)}")
       
  * For F# Code
    
        override this.CreateDomElement(parentRef: obj, domElementWhereToPlaceChildren : byref<obj>) =
            let mutable outerDiv = null
            let outerDivStyle = INTERNAL_HtmlDomManager.CreateDomLayoutElementAppendItAndGetStyle("div", parentRef, this, &outerDiv)
            outerDivStyle.width <- "100%"
            outerDivStyle.height <- "100%"

            let iFrameStyle = INTERNAL_HtmlDomManager.CreateDomElementAppendItAndGetStyle("iframe", outerDiv, this, &_iFrame)
            iFrameStyle.width <- "100%"
            iFrameStyle.height <- "100%"
            iFrameStyle.border <- "none"

            DisposeJsCallbacks()
            _jsCallbackOnIframeLoaded <- JavaScriptCallback.Create(OnIframeLoad, true);

            let sIFrame = INTERNAL_InteropImplementation.GetVariableStringForJS(_iFrame)
            OpenSilver.Interop.ExecuteJavaScriptFastAsync($"{sIFrame}.onload = {INTERNAL_InteropImplementation.GetVariableStringForJS(_jsCallbackOnIframeLoaded)}")

2. In addition, there is a correspondence that is made between certain XAML properties and CSS properties. For example, the property “Foreground” in XAML will have as equivalent the property “color” in CSS. As another example, the "Source" property of the "WebBrowser" control will have as equivalent the "src" property of `<iframe>`.

![Properties Definition](/images/13.ForegroundProperties.png "The definition of the Foreground property")

VB.NET Code example

    ''' <summary>
    ''' Gets or sets a brush that describes the foreground color.
    ''' </summary>
    Public Property Foreground As Brush
        Get
            Return CType(GetValue(ForegroundProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(ForegroundProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Identifies the <seecref="Control.Foreground"/> dependency property.
    ''' </summary>
    Public Shared ReadOnly ForegroundProperty As DependencyProperty = DependencyProperty.Register(NameOf(Foreground), GetType(Brush), GetType(Control), New PropertyMetadata(New SolidColorBrush(Colors.Black)) With {
    
    
    .MethodToUpdateDom2 = UpdateDomOnForegroundChanged
    })

F# Code example

    /// Gets or sets a brush that describes the foreground color.
    member this.Foreground
        with get() = this.GetValue(Control.ForegroundProperty) :?> Brush
        and set(value) = this.SetValue(Control.ForegroundProperty, value :> Brush)

    /// Identifies the <seecref="Control.Foreground"/> dependency property.
    static member val ForegroundProperty = 
        DependencyProperty.Register(
            "Foreground",
            typeof<Brush>,
            typeof<Control>,
            new PropertyMetadata(defaultValue = SolidColorBrush(Colors.Black), MethodToUpdateDom = Control.UpdateDomOnForegroundChanged)
        ) with get, set

### 2. The compiler

Next, the NuGet package adds a few steps to the Msbuild compilation.

This can be seen by opening the contents of the NuGet package where it is installed (usually in C:\Users&#92;%USERNAME%&#92;.Nuget\packages\opensilver\). By opening the "build" directory, it is possible to see the presence of two .target files, which will alter the behavior of Msbuild.

![Folder Build](/images/14.FolderBuild.png "The contents of the Build directory in the NuGet package")

One of the key compilation steps is the conversion of XAML files to auto-generated C# (or VB.NET or F#) files. In fact, the compiler replaces XAML files with strictly equivalent C# (or VB.NET or F#) code. For example, in C#, the following XAML code:

```
<StackPanel HorizontalAlignment="Left">
  <TextBlock Text="Enter some text below:"/>
  <TextBox x:Name="MyTextBox1"/>
  <Button Content="Click me" Click="Button_Click"/>
</StackPanel>
```

is replaced by the following auto-generated code:
```
var stackPanel1 = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Left };
var textBlock1 = new TextBlock() { Text = "Enter some text below:" };
stackPanel1.Children.Add(textBlock1);
var textBox1 = new TextBox() { Name = "MyTextBox1" };
stackPanel1.Children.Add(textBox1);
this.MyTextBox1 = textBox1;
var button1 = new Button() { Content = "Click me" };
button1.Click += Button_Click;
stackPanel1.Children.Add(button1);
```

One can see this auto-generated code by going to the “obj/Debug” directory of the project and by opening the “MainPage.xaml.True.g.cs” file (after compiling the project).

![Generated Code](/images/15.autoGeneratedCode.png "The auto-generated MainPage.xaml.True.g.cs file")

Please note in VB.NET for the above said XAML code, auto-generated code will be at file “MainPage.xaml.True.g.vb” under the “obj/Debug” directory and will look like this (after compiling the project):

        Dim stackPanel1 = New StackPanel()
        stackPanel1.HorizontalAlignment = HorizontalAlignment.Left
        Dim textBlock1 = New TextBlock()
        textBlock1.Text = "Enter some text below:"
        stackPanel1.Children.Add(textBlock1)
        Dim textBox1 = New TextBox()
        textBox1.Name = "MyTextBox1"
        stackPanel1.Children.Add(textBox1)
        Me.MyTextBox1 = textBox1
        Dim button1 = New Button()
        button1.Content = "Click me"
        AddHandler button1.Click, AddressOf Button_Click
        stackPanel1.Children.Add(button1)

Please note in F# for the above said XAML code, auto-generated code will be at file “MainPage.xaml.True.g.fs under the “obj/Debug” directory and will look like this (after compiling the project):

        let stackPanel1 = new StackPanel()
        stackPanel1.HorizontalAlignment <- Left
        let textBlock1 = new TextBlock()
        textBlock1.Text <- @"Enter some text below:"
        stackPanel1.Children.Add(textBlock1) |> ignore
        let textBox1 = new TextBox()
        textBox1.Name <- "MyTextBox1"
        stackPanel1.Children.Add(textBox1) |> ignore
        let button1 = new Button()
        button1.Content <- @"Click me"
        stackPanel1.Children.Add(button1) |> ignore

### 3. The satellite projects

In the Visual Studio solution, in addition to the main application project (the one containing App.xaml), we mentioned two other projects, which serve as entry points:

* The .Browser project is an ASP.NET Client-Side Blazor type project. It references the main project of the application and lets you run it in WebAssembly

* The .Simulator project is a .NET Framework type project whose purpose is to launch the app as a desktop application with a bunch of handy helpers for a developer, including the ability to debug the application.


![OpenSilver solution](/images/3.solutionExplorer.png "The three projects of a typical OpenSilver solution for C# code")
![OpenSilver solution](/images/3.solutionExplorerWithVB.png "The three projects of a typical OpenSilver solution for VB.NET code")

### 4. The Simulator

The Simulator is a tool which is distributed as a NuGet package. Usually it is located in the "C:\Users&#92;%USERNAME%&#92;.Nuget\packages\opensilver.simulator".



It is a WPF-type application that contains an embedded Chromium browser. The goal of the Simulator is to allow debugging the application with the very powerful tools of the .NET Framework. It also allows to inspect the XAML visual tree.

![Simulator Visual Tree](/images/11.VisualTree.png "The Simulator visual tree inspector")

Its internal operation is as follows: when the Simulator launches, it starts the application via an "Activator.CreateInstance". This is possible because the application DLL is .NET Standard compatible. Then, the Simulator instantiates several methods in the application to handle interops and to analyze the components tree. Finally, the application is running in the .NET Framework context.

All HTML and CSS manipulations are redirected by the Simulator to the embedded browser (chromium) via C# <-> JavaScript interops (see corresponding section). JavaScript events are connected to C# events.
