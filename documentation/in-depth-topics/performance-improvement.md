# Performance Improvements

OpenSilver is getting more performant with each release. However, you can improve the responsiveness and performance of your app by making some modifications to the client code.

Before making changes, measure the performance. In some cases, you can make your code less readable, but the gain from optimization is insignificant. See how to profile the performance in the [Simulator](../how-to-topics/performance-profiler.md) and in the [Browser](../how-to-topics/performance-profiler-browser.md).

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

Some controls are very slow, like `WriteableBitmap`. For example, `Viewbox` cause a redraw of the UI and can have performance issues. Another example is a `TabControl` style, which might have elements for all possible positions of the tab headers (left, bottom, etc.), but if in your app you know that you only use top-positioned tab headers, you can remove the other items from the `ControlTemplate`.

Simplify the ControlTemplates involved in the rendering of the `DataGrid`. For example, the Cell template might have a lot of unnecessary or poorly performing UI elements in the visual tree. 

Performance gains are roughly proportional to the % of visible items that you remove. Hidden items have a smaller impact, but it can still be noticeable. 

If it is acceptable to lose minor functionality in the app, or if it is acceptable to slightly change the look of the app, we can remove minor UI elements in the styles (such as elements for mouse hover, elements for keyboard focus, storyboard, etc.), reduce the depth of the visual tree, use "flat" design instead of gradients and shades, etc. 

## Replace Telerik UI controls with the built-in ones

Usually, built-in controls are more performant than Telerik controls. But keep in mind that some features might be missing.

For example, replace the `RadGridView` with the built-in `DataGrid`, `RadTabControl` with `TabControl`, `RadTileView` with just a simple `Grid`. 

## Replace some controls with native HTML controls 

You can try to replace the OpenSilver controls with the html elements and use css+javascript to style them, so they look/work like the original controls. This requires more works and html/css/js knowledge, but the performance gain will be very huge (like 30x better performance in some cases).  

It can be done for `ComboBox`, `TextBox`, etc. See https://github.com/OpenSilver/OpenSilver.ControlsKit/ or controls in `CSHTML5.Native.Html.Controls` namespace. 

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

In some scenarios, caching of the application files will work only if the application is hosted in HTTPS. 

## Enable WASM SIMD

If you are sure that your users have an up-to-date browser (read note below), you can improve the performance of your application by enabling WASM SIMD.

To do so, edit the ".CSPROJ" file of the project that has the ".Browser" suffix, and replace the line <WasmEnableSIMD>false</WasmEnableSIMD> with <WasmEnableSIMD>true</WasmEnableSIMD>

Note that this setting will prevent the application from running on older browsers. Specifically, the application will not run on old versions of Edge, Chrome or Firefox released prior to 2021, and Safari versions released prior to 2023.


