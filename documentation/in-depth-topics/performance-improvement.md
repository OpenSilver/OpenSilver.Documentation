# Performance Improvements

OpenSilver is getting more performant with each release. However, you can improve the responsiveness and performance of your app by making some modifications to the client code.

Before making changes, measure the performance. In some cases, you can make your code less readable, but the gain from optimization is insignificant. See how to profile the performance in the [Simulator](../how-to-topics/performance-profiler.md) and in the [Browser](../how-to-topics/performance-profiler-browser.md).

## Do not load hidden UI elements 

By default, controls with `Collapsed` Visibility are added to the HTML DOM, but CSS styles are not applied until the control becomes visible. This can impact performance, especially if there are a lot of hidden controls. To address this, add the following line to `App.xaml.cs` constructor: 

`Host.Settings.EnableOptimizationWhereCollapsedControlsAreNotLoaded = true;`

This will cause the DOM elements for hidden controls to be created only when the control becomes visible. 

Note that this setting can break the UI in some cases. For example, background controls like `Border` or `Rectangle` can overflow other controls. To resolve this, try to change the `Canvas.ZIndex` property. 

The above setting will not work if the Visibility is bound to a property that has `Collapsed` Visibility by default and the DataContext is not available at the moment of rendering. The control will be rendered because the default value for Visibility is `Visible`. In that case, set the `FallbackValue` property in the binding.

`<Grid Visibility="{Binding MyObject.Visibility, FallbackValue=Collapsed}" />`

Another option is to set the `EnableOptimizationWhereCollapsedControlsAreLoadedLast` property to `true`. This will cause the browser to wait a few moments before creating the DOM elements for the UI controls that are not visible. This gives the browser engine time to draw the visible controls first, which can improve perceived performance. 

## Enable virtualization

Wherever you have a high number of controls with a scroll bar, make sure that virtualization is enabled. Virtualization will ensure that only the elements that are in the visible area of the scrollable container will be rendered. `DataGrid` control uses virtualization by default. 

To check whether virtualization is enabled: 

- An easy way to know whether virtualization is enabled, check if the rendering time depends on the number of items: when virtualization is enabled, the rendering time is independent of the number of items. 

- Use the F12 developer tools to inspect the DOM tree: find the element that has the scrollbars, see what's inside it: if there is no `<div>` that has `VirtualizingStackPanel` in its `class` property, or if you see that all the elements are in the DOM tree in spite of them not being in the visible area, it could be that virtualization is not enabled. 

- Use the "Inspect XAML tree" feature of the Simulator to check whether a `VirtualizingStackPanel` is present.

To enable virtualization for a `ListBox` or another control that derives from `ItemsControl`, change the `ItemsPanelTemplate` to add a `VirtualizingStackPanel` instead of the default `StackPanel`. 

        <ListBox> 
            <ListBox.ItemsPanel> 
                <ItemsPanelTemplate> 
                    <VirtualizingStackPanel/> 
                </ItemsPanelTemplate> 
            </ListBox.ItemsPanel> 
        </ListBox> 

 Performance gains can be significant depending on the number of items. It is roughly proportional. For example, if 100 items visible on screen out of 1000, we will get a 10x performance improvement. Performance gains can even be better than proportional if by enabling virtualization we don't hit certain memory consumption thresholds that cause a high number of memory swaps. 

## Simplify the visual tree

You can look at the visual tree: 

- Either using the "inspect" feature of the F12 developer tools when running in the browser (the `class` attribute of DIVs will tell you the name of the corresponding XAML type) 

- Or by clicking "Inspect Visual Tree" when running the app in the Simulator

The more XAML elements are in the visual tree, the slower the app will perform. 

Every page of the app and every control has XAML Styles and ControlTemplates. Many of those can be simplified, such as removing unused storyboards or replacing slow-performing controls with better performing ones.

Some controls are very slow, like `WriteableBitmap` and `Shapes`. For example, a `Rectangle` and `Ellipse` could be replaced with a `Border`. `Viewbox` cause a redraw of the UI and can have performance issues. Another example is a `TabControl` style, which might have elements for all possible positions of the tab headers (left, bottom, etc.), but if in your app you know that you only use top-positioned tab headers, you can remove the other items from the `ControlTemplate`.

Simplify the ControlTemplates involved in the rendering of the `DataGrid`. For example, the Cell template might have a lot of unnecessary or poorly performing UI elements in the visual tree. 

Performance gains are roughly proportional to the % of visible items that you remove. Hidden items have a smaller impact, but it can still be noticeable. 

If it is acceptable to lose minor functionality in the app, or if it is acceptable to slightly change the look of the app, we can remove minor UI elements in the styles (such as elements for mouse hover, elements for keyboard focus, storyboard, etc.), reduce the depth of the visual tree, use "flat" design instead of gradients and shades, etc. 

## Replace Telerik UI controls with the built-in ones

Usually, built-in controls are more performant than Telerik controls. But keep in mind that some features might be missing.

For example, replace the `RadGridView` with the built-in `DataGrid`, `RadTabControl` with `TabControl`, `RadTileView` with just a simple `Grid`. 

## Replace some controls with native HTML controls 

You can try to replace the OpenSilver controls with the html elements and use css+javascript to style them, so they look/work like the original controls. This requires more works and html/css/js knowledge, but the performance gain will be very huge (like 30x better performance in some cases).  

It can be done for `ComboBox`, `TextBox`, etc. See https://github.com/OpenSilver/OpenSilver.ControlsKit/ or controls in `CSHTML5.Native.Html.Controls` namespace. 

## Use SVG instead of XAML images 

Currently, `Shape` controls like `Line`, `Path`, `Polygon` and so on are being rendered via html canvas, and it is quite slow. Try to replace them either by creating SVG images or converting XAML to SVG. Use `HtmlPresenter` control to place SVG inside it.

Use a tool like Inkscape to open XAML and then save it as Plain SVG. But be aware that you might need to correct the converted SVG manually, especially if the image is complex. 

## Use Progressive Rendering

Progressive rendering is a technique that allows you to render parts of your app before the entire app is loaded. This can improve perceived performance, as the user can start interacting with the app before it is fully loaded.

Set `Host.Settings.ProgressiveRenderingChunkSize` to enable progressive rendering. Be aware, if you set a small value, close to 1, it can break UI. Optimal value is close to 10. 

Also, you can enable progressive loading for `DataGrid` control by setting `GlobalProgressiveLoadingChunkSize` or `ProgressiveLoadingRowChunkSize`. So that rows will start appearing immediately one after the other, instead of appearing all at once after a delay. Although the total time to load the `DataGrid` remains the same, the perception of performance is improved. 

## Avoid using Reflection and Expressions

Reflection and Expressions are very slow in Blazor. For example, replace `OnPropertyChanged(x => x.PropertyName)` with `OnPropertyChanged(nameof(PropertyName)`.

## Client/server communication

Binary serialization is faster than text serialization, but currently some features are missing. Using ProtoBuf can improve performance significantly.  

## Do not load all the data at the same time

Show the view/page with minimal data. Then use `Task.Delay` in C# to load the heavy data and show loading indicator. This does not reduce the total loading time, but it makes the app more responsive, as it's better if a user sees something and wait for loading other than nothing displayed. 

## Set fixed Width/Height in more places

Fixed size will prevent additional calculations when the layout is updated.

## Page caching

If the `Frame` control is used, set `Page.NavigationCacheMode = NavigationCacheMode.Enabled` to reuse cached pages and reduce memory usage.

## Host the app in HTTPS rather than HTTP

In some scenarios, cachine of the application files will work only if the application is hosted in HTTPS. 
