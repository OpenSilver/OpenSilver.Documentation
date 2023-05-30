# Enabling Application Trimming

By enabling application trimming, you can reduce the size of your app while simultaneously boosting its performance.

To activate trimming, you'll need to adjust the csproj file of your Browser project by setting `<PublishTrimmed>true</PublishTrimmed>`.

![csproj file adjustment](/images/how-to-topics/csproj-file-adjustment-trimming.png)

Remember, trimming is only executed during the publishing stage of the application, and it may consume some time.

Post-trimming, it's crucial to test your application as trimming may lead to unexpected issues. Particularly in applications using reflection, the trimmer might inadvertently remove types required at runtime. If your application uses reflection, you must provide the trimmer with information about these necessary types, both in your application's code and its dependencies.

Therefore, it's essential to configure the trimmer to retain these elements.

## Configuring the Trimmer

Here's how to do it:

1. Add `ILLink.Descriptors.xml` as an embedded resource to the Browser project.

2. Append necessary items according to the instructions available [here](https://github.com/dotnet/linker/blob/main/docs/data-formats.md#descriptor-format).

For instance:
```xml
<?xml version="1.0" encoding="UTF-8" ?>
<!--
  This file specifies the components of the BCL or Blazor packages that should not be
  removed by the IL Linker, even if they aren't directly referenced by user code.
  For more details, visit: 
  https://github.com/dotnet/linker/blob/main/docs/data-formats.md#descriptor-format
-->
<linker>
  <assembly fullname="Microsoft.AspNetCore.Components.Web">
    <type fullname="Microsoft.AspNetCore.Components.Forms.EditContextFieldClassExtensions" preserve="methods" />
  </assembly>

  <assembly fullname="Microsoft.Extensions.Configuration.FileExtensions">
    <type fullname="Microsoft.Extensions.Configuration.FileConfigurationExtensions" preserve="methods" />
  </assembly>
</linker>
```
![ILLink.Descriptors.xml example](/images/how-to-topics/illink-descriptors-example.png)

## Mark as Trimmable
By default, the trimmer operates solely on assemblies labelled as trimmable. Thus, it will most likely trim only assemblies from .NET.

However, it might be beneficial to enable trimming for assemblies from your own project if it's sizable. Similarly, activating trimming for third-party libraries, loaded as dependencies, can be advantageous if you're confident they're not being used in your project.

For instance, suppose your project utilizes the Telerik Library, but doesn't specifically use the MediaPlayer component. In such a case, you can safely eliminate `Telerik.Windows.Controls.MediaPlayer.dll` from the output by enabling trimming for `Telerik.Windows.Controls.MediaPlayer`:
![Enabling trimming for Telerik.Windows.Controls.MediaPlayer](/images/how-to-topics/enable-trimming-mediaplayer.png)
