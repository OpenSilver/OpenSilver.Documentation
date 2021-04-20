### Errors from .xaml.cs sources
Many different compilation errors related to UI controls can occur if the project consists of **.xaml** files (which Silverlight application usually does).

![xaml Errors](/images/XamlErrors.png "xaml Errors")

This is because **.g.i.cs** files not generated and to fix this click on **.xaml** file and in advanced section choose
```
Build Action - Content
Custom Tool - MSBuild:Compile
```

![Build Action](/images/BuildAction.png "Build Action")

Please note that if the project includes **Generic.xaml** file for default styles then most probably no errors will be encountered during compilation but some screens will not be shown as expected at the end. So the above steps need to be followed for **Generic.xaml** and for all other **.xaml** files.

### Make build configurations consistent
Some large projects might have different build configurations with many compiler directives. As a result a lot of confusing errors might be encountered during compilation.
It is a good practice to check **Silverlight** project and see if the build configurations are consistent with **OpenSilver** project.

## (WORK IN PROGRESS DOCUMENTATION, CHECK BACK SOON)