# Using Custom Fonts in your app

## Introduction
In an OpenSilver application you can use 3 types of fonts:

* *"Web safe" browser fonts:* those are fonts that are built into most browsers, so you can use them directly. The most common ones are: Arial, Helvetica, Times New Roman, Times, Courier New, Courier, Verdana, Georgia, Palatino, Garamond, Bookman, Comic Sans MS, Trebuchet MS, Arial Black, Impact

Here is an example of use:
`<TextBlock FontFamily="Helvetica" Text="Hello World"/>`

* *System fonts:* those are fonts that may be present on the user's machine so you can attempt to use them directly, but you have no guarantee that they will display properly on all machines. A common example is "Segoe UI", which is bundled in recent versions of Windows but not older ones. If you choose to use such a font, it is recommended that you specify a "fallback" font in case that the font is not available. To do so, just provide a list of fonts separated by a comma.

Here is an example of use, where we fallback to "Helvetica" if "Segoe UI" is not found:
`<TextBlock FontFamily="Segoe UI,Helvetica" Text="Hello World"/>`

* *Custom fonts:* those are fonts for which you must provide a ".ttf" or ".woff" file. Those types of files can be downloaded from online fonts galleries such as [DaFont.com](https://www.dafont.com/fr/) or [Google Fonts](https://fonts.google.com/). Those fonts get embedded/distributed with your application. The section below explains how to use custom fonts.


## How to use custom fonts
Here is how to use custom fonts in a OpenSilver application:

1. Add a ".ttf" or ".woff" font file to your OpenSilver project. (You can download such a file from online fonts galleries such as [DaFont.com](https://www.dafont.com/fr/) or [Google Fonts](https://fonts.google.com/))

2. Set its *"Build Action"* to *"Content"*. (To do so, select the font file in the Solution Explorer, press F4, and change the "Build Action" property)

3. Reference the font in your XAML using one of the two following syntaxes:

`<TextBlock FontFamily="ms-appx:///AssemblyName/Folder/MyFontFileName.ttf" Text="Hello World"/>`

or

`<TextBlock FontFamily="/AssemblyName;component/Folder/MyFontFileName.ttf" Text="Hello World"/>`



## Notes
* For best cross-browser compatibility, use .WOFF fonts (see the [WOFF browser compatibility table](https://caniuse.com/#feat=woff)).
.TTF fonts do not work in Internet Explorer unless they are marked as "installable" (see the [TTF browser compatibility table](https://caniuse.com/#feat=ttf)).

* Fonts *don't get previewed* in the XAML editor.

* The *default font* of a OpenSilver application is defined in the file "opensilver.css" and is equal to the following fallback chain:

   'Segoe UI', Verdana, 'DejaVu Sans', Lucida, 'MS Sans Serif', sans-serif;

* If your font is used in multiple locations, you can declare a <FontFamily> element such as:

`<FontFamily x:Key="MyFontKey">ms-appx:///AssemblyName/Folder/MyFontFileName.ttf</FontFamily>`

You should declare it in a shared XAML location, such as the <App.Resources> section of the file "App.xaml", or a merged XAML resource dictionary.

Then, you can reference it anywhere in your app using the "StaticResource" markupe extensions, like this:

`<TextBlock FontFamily="{StaticResource MyFontKey}" Text="Hello World"/>`

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
