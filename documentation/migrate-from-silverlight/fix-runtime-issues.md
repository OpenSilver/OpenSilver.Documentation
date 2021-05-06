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

### Fixing cookie-related issues

In a process of client/server communication there can be a situations when authentication cookies not sent to the server in subseqent requests by client and as a result server rejects to send data back.\
One of the reasons can be that client and server are running on different ports.


Here is the solution.

#### 1. Add the following line in App.xaml.cs. (Client).

```Application.Current.Host.Settings.DefaultSoapCredentialsMode = System.Net.CredentialsMode.Enabled;```

#### 2. Add the following lines in Global.asax.cs in Application_BeginRequest function

```
HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:55591");
HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
```

55591 is assumed to be a port which client uses

[Here](https://doc.opensilver.net/documentation/in-depth-topics/wcf-and-webclient.html#to-add-cors-to-your-web-service-recommended-simply-follow-these-steps) is how to add Global.asax and CORS.

## (WORK IN PROGRESS DOCUMENTATION, CHECK BACK SOON)