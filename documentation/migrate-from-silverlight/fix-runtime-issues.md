### Errors from .xaml.cs sources
Many different compilation errors related to UI controls can occur if the project consists of **.xaml** files (which Silverlight application usually does).

![xaml Errors](/images/XamlErrors.png "xaml Errors")

This is because **.g.i.cs** files not generated and to fix this click on **.xaml** file and in advanced section choose
```
Build Action - Content
Custom Tool - MSBuild:Compile
```

![Build Action](/images/BuildAction.png "Build Action")

## (WORK IN PROGRESS DOCUMENTATION, CHECK BACK SOON)