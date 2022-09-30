# How to use the native HTML5 canvas for high performance

## Why use the native HTML5 canvas?

OpenSilver usually renders each XAML object as one or more <div> elements in the HTML page. This enables to achieve most of the layout features of the Microsoft XAML language, including the Horizontal/Vertical alignment, sizing, panels such as StackPanel, Grid, WrapPanel, and more. Furthermore, OpenSilver provides a full implementation of the Dependency Properties system, supporting features such as properties metadata, default values, styles, locally set values, bindings, static resources, "property changed" callbacks, metadata override, value coercion, and more.

However, there are cases where all those features can lead to performance issues, especially if you are developing a very specific control that needs to render thousands of UI elements, such as a Charting or a Calendar control.

To improve performance in those specific cases, it is recommended that you use the native HTML5 &lt;canvas&gt; control named "HtmlCanvas", which is located in the namespace "CSHTML5.Native.Html.Controls".

This document explains what it is and how to use it.



## What is the HtmlCanvas control and how to use it?
The "HtmlCanvas" control is a high performance control that renders all its children as a single native HTML5 &lt;canvas&gt; element. Instead of ending up with thousands of HTML elements, you end up only with a single HTML element.

*IMPORTANT:* The rendering is done when you call the Draw() method.

The HtmlCanvas control has a "Children" property that can contain any of the following elements:

* RectangleElement
* TextElement
* LineElement
* ImageElement
* ContainerElement

Please refer to the ["Reference"](#reference) section below for details on each element.

Note: for performance reasons, those elements do not support binding, and they are always positioned relative to their container element, using (X,Y) coordinates.



## Example1
```
<native:HtmlCanvas x:Name="HtmlCanvas1" xmlns:native="using:CSHTML5.Native.Html.Controls">
    <native:RectangleElement X="200" Y="42" Width="100" Height="50" FillColor="Blue"/>
</native:HtmlCanvas>
```
Then, in your C# code (for example in the Page_Loaded event), be sure to call:
  *HtmlCanvas1.Draw();*

Note: it is important that you call the "Draw()" method only AFTER the HtmlCanvas has been added to the Visual Tree. To know whether the control is in the visual tree, you can read the "IsLoaded" property, which should be equal to "true".



## Example2
Use the code below to display 10,000 colored rectangles.

Note: this example is intended to be tested in the browser rather than the Simulator because the Simulator is much slower at working with the HtmlCanvas control due to underlying C#/JS interop calls.
```
private void Button_Click(object sender, RoutedEventArgs e)
{
    if (!CSHTML5.Interop.IsRunningInTheSimulator)
    {
        Random rand = new Random();
        double x, y, w, h;
        CSHTML5.Native.Html.Controls.RectangleElement r;
        double canvasWidth = HtmlCanvas1.ActualWidth;
        double canvasHeight = HtmlCanvas1.ActualHeight;
        HtmlCanvas1.Children.Clear();
        for (int i = 0; i < 10000; i += 1)
        {
            w = rand.Next(10, 100);
            h = rand.Next(10, 100);
            x = rand.Next((int)canvasWidth - (int)w);
            y = rand.Next((int)canvasHeight - (int)h);
            r = new CSHTML5.Native.Html.Controls.RectangleElement(x, y, w, h);
            HtmlCanvas1.Children.Add(r);
            r.FillColor = Color.FromArgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
        }
        HtmlCanvas1.Draw();
    }
    else
    {
        System.Windows.MessageBox.Show("This demo is too slow to run in the Simulator. Please run in the browser instead.");
    }
}
```
XAML code:
```
     <Grid>
        <native:HtmlCanvas Name="HtmlCanvas1" xmlns:native="using:CSHTML5.Native.Html.Controls"/>
        <Button Content="Click to render" HorizontalAlignment="Center" VerticalAlignment="Center" Click="Button_Click"/>
    </Grid>
```

Example3 (full project)
</br>
![canvasDemo](../../images/htmlcanvasdemo1_screenshot.png "HtmlCanvas Demo 1 Screenshot")


<!--You can run the sample online [here](http://cshtml5.com/samples/htmlcanvas1/index.html?20170316).

You can download the full source code from GitHub at the following URL (just click the green button named "Clone or download"):
https://github.com/cshtml5/CSHTML5.Demos.HtmlCanvasPerformance1 -->



## Reference

* *HtmlCanvas:* this is the main XAML control that you should place in your XAML tree. Its purpose is to generate a native html &lt;canvas&gt; tag in the output html page. This control is a DependencyObject and has all the same positioning properties as the other XAML controls (HorizontalAlignment, VerticalAlignment, Width, Height, etc.).

Example in XAML:
```
<native:HtmlCanvas Width="100" Height="100" xmlns:native="using:CSHTML5.Native.Html.Controls">
     <!-- Place children here -->
</native:HtmlCanvas>
```
Example in C#:
```
HtmlCanvas myCanvas = new HtmlCanvas();
myCanvas.Width = 100;
myCanvas.Height = 100;
```
The following elements that can be added to the "HtmlCanvas.Children" collection:

* *RectangleElement:* this will draw a rectangle at the specified (X,Y) position relative to its container, with the specified (Width, Height) and FillColor.

Example in XAML:
```
<native:HtmlCanvas Width="1000" Height"500" xmlns:native="using:CSHTML5.Native.Html.Controls">
    <native:RectangleElement X="200" Y="42" Width="100" Height="50" FillColor="Blue"/>
</native:HtmlCanvas>
```
* *TextElement:* this will draw some text at the specified (X,Y) position relative to its container. It contains the properties "Text" and "Font" (note: if you set the "Font" property, it needs to be specified in the html format).

Example in XAML:
```
<native:HtmlCanvas Width="1000" Height"500" xmlns:native="using:CSHTML5.Native.Html.Controls">
    <native:TextElement X="200" Y="42" Text="I am the text" Font="20px Arial"/>
</native:HtmlCanvas>
```
* *LineElement:* this will draw a line that goes from coordinates (X1,Y1) to (X2,Y2) relative to its container.

Example in XAML:
```
<native:HtmlCanvas Width="1000" Height="500" xmlns:native="using:CSHTML5.Native.Html.Controls">
    <native:LineElement X1="200" Y1="42" X2="100" Y2="50" StrokeColor="Red" LineWidth="2"/>
</native:HtmlCanvas>
```
* *ImageElement:* this will draw an image at the specified (X,Y) position relative to its container. The image filename can be specified by setting the "Source" property. You can use any of the two following syntaxes:

* ms-appx:///AssemblyName/FolderName/FileName.png
* /AssemblyName;component/FolderName/FileName.png

Example in XAML:
```
<native:HtmlCanvas Width="1000" Height"500" xmlns:native="using:CSHTML5.Native.Html.Controls">
    <native:ImageElement Source="ms-appx:///MyAssembly/MyFolder/Image.png" X="200" Y="42" Width="100" Height="50"/>
</native:HtmlCanvas>
```
* *ContainerElement:* this element is meant to contain other elements via its "Children" property. You can also nest ContainerElements inside other ContainerElements. The purpose of a ContainerElement is to enable its children to be positioned relative to the container, as well as to encapsulate portions of the rendering.

Here is how this "encapsulation" concept works:

You can create classes that inherit from the "ContainerElement" class in order to encapsulate and reuse portions of the rendering, such as sprites in a game or business objects in a business application. For example, if you are developing a Calendar control using a single HtmlCanvas control, you may want to create a "DayElement" class that is in charge of rendering a single day, by making the "DayElement" class inherit from the "ContainerElement" class. In the constructor of the "DayElement" class, you may create all the elements needed to render a single day - such as a "TextElement" for the day number - and add them to the "Children" collection of the "DayElement" class. Then, to render the whole calendar, you may instantiate as many instances of the "DayElement" class as there are days in the calendar, and add them to the "Children" collection of the "HtmlCanvas" control.


Example in XAML:
```
<native:HtmlCanvas Width="1000" Height"500" xmlns:native="using:CSHTML5.Native.Html.Controls">
    <native:ContainerElement X="200" Y="42" Width="100" Height="50" FillColor="Blue">
        <!-- Place children here -->
    </native:ContainerElement>
</native:HtmlCanvas>
A more complete example of use of the ContainerElement can be found at:
   https://github.com/cshtml5/CSHTML5.Demos.HtmlCanvasPerformance1
```

*IMPORTANT: The rendering is done only when you call the "HtmlCanvas.Draw()" method.*

The following *properties* are available to all the elements above:

* FillColor
* StrokeColor
* ShadowColor
* ShadowBlur
* ShadowOffsetX
* ShadowOffsetY
* LineCap
* LineJoin
* MeterLimit
* ToolTip
* ContextMenu

The following methods are available to all the elements above:

* MoveTo(X,Y): This method will position the element at the specified (X,Y) coordinates relative to its container.
* Move(DeltaX, DeltaY): This method will add the passed (DeltaX,DeltaY) coordinates to the current (X,Y) coordinates.

The following *events* are available to all the elements above:

* PointerPressed
* PointerReleased
* PointerMoved
* PointerEntered
* PointerExited
* RightTapped

Please note that, if you make changes to the UI of the HtmlCanvas, you need to call the "Draw()" method to refresh. For convenience, the class *"HtmlCanvasPointerRoutedEventArgs"* passed to the event handlers contains a *reference to the parent HtmlCanvas control*.



## See Also
* [How to use the HtmlPresenter control](htmp-presenter.md)

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
