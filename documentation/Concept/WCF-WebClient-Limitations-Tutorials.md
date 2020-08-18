# WCF and WebClient Limitations and Tutorials

## Introduction

OpenSilver provides support for web services and HTTP calls in multiple ways, including:

* The *"Add Service Reference"* feature of Visual Studio. Use this feature to easily communicate with SOAP / WCF web services. See below for [limitations](#limitations-of-the-add-service-reference-support-soap) and a [Tutorial](#tutorial-to-easily-create-a-soap-based-clientserver-app-in-cshtml5-wcf).
Note: to enable passing cookies (for credentials/authentication), put the following code in your App.xaml.cs constructor:
```
Application.Current.Host.Settings.DefaultSoapCredentialsMode = System.Net.CredentialsMode.Enabled;
```

* The *"WebClient"* and *"WebClientWithCredentials"* classes. Use these classs to download strings from the web, as well as to communicate with REST / Web API web services. The *"WebClientWithCredentials"* has a *"CredentialsMode"* property that you can set to "Enabled" to automatically read the authentication cookie and send it back to all further requests. See below for a [Tutorial](#tutorial-to-easily-create-a-soap-based-clientserver-app-in-opensilver-wcf).
* [WebSockets](http://forums.cshtml5.com/viewtopic.php?f=7&t=276)
* [SignalR](http://forums.cshtml5.com/viewtopic.php?f=7&t=8121)
* [JSONP](Circumventing-cross-domain-policies-using-JSONP.html)

## Sample application


You can download a sample SOAP-based client/server application from the following URL:

http://cshtml5.com/downloads/TestCshtml5WCF.zip

The sample shows a basic To-Do management application.

## Limitations of the "Add Service Reference" support (SOAP)

In the current version, C#/XAML for HTML5 has the following limitations regarding the "Add Service Reference" (SOAP) feature:

* Due to JavaScript restrictions in the browser, *cross-domain calls require the server to [implement CORS](#adding-support-for-cross-domain-calls-cors)*. In other words, if your client application is not hosted on the same domain as your WCF web service, you need to add CORS to the web service (or you can use [JSONP](Circumventing-cross-domain-policies-using-JSONP.html) as an alternative to CORS, but it is not recommended for modern services). To add CORS to your web service, follow this [tutorial](#adding-support-for-cross-domain-calls-cors).

* When adding a WCF Service Reference via the "Add Service Reference" command of VS, VS may add unwanted references to your project, such as the "System" reference. *You need to manually remove those references in order to be able to compile the project.*

![Remove reference System](/images/RemoveSystemreference.png "Remove System.* refere,ces")

* When using OpenSilver, configuring a WCF Service Reference requires temporarily adding a reference to the "System.dll" assembly. You should add this reference only while configuring the service, and then you should remove it once the service has been configured.

Furthermore, please note that the following features are NOT yet supported:

* Non-http bindings (only BasicHttpBinding and HTTPS/SSL are currently supported)
* The generic type "FaultException<T>" (only the non-generic FaultException type is currently supported)

We are working to add support for all of the above features as soon as possible. Please make sure to vote for your most wanted features on http://cshtml5.uservoice.com

## Adding support for cross-domain calls (CORS)

Due to the JavaScript restrictions in the browser, cross-domain calls require the server to implement CORS. In other words, if your client application is not hosted on the same domain as your WCF web service, you need to add CORS to the web service (or you can use [JSONP](Circumventing-cross-domain-policies-using-JSONP.html) as an alternative to CORS, but it is not recommended for modern services).

If you see the error *"No 'Access-Control-Allow-Origin' header is present on the requested resource"* in the browser Console window (F12), it is likely that CORS has not been properly configured.

Here are your options:

* Either *host your application on the same domain as your web service*. In this case, CORS won't be required and you won't encounter any cross-domain issues. However this is not practical during development because it requires you to publish your application every time you need to test.

* Or *add CORS to your web service (recommended)*. This is the recommended approach but it assumes that you have access to the source code of your web service and you have the rights to modify it and publish it. See below for a tutorial on how to add CORS to your web service.

* Or *work around the issue by running Chrome without CORS verification*. This is obviously only for development, not for use in production. To do so, open the Windows "Run" dialog (for example by pressing the Windows+R keys combination), and execute the following command:
```
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --disable-web-security --user-data-dir="c:/chromedev"
```

* Or *work around the issue by using Fiddler* to make the browser believe that your server allows cross-domain requests. This is obviously only for development, not for use in production. To do so, follow the steps described [here](fiddler-enable-cors.html).



### To add CORS to your web service (recommended), simply follow these steps:

#### 1) If you are using a SOAP web service:

i) Make sure you have the file *"Global.asax.cs"* in your server-side SOAP web service project. If you don't have that file, right-click the project name in the Solution Explorer, and click "Add" -> "Global Application Class".

ii) Add the following code to the said file:
```
protected void Application_BeginRequest(object sender, EventArgs e)
{
    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

    if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
    {
        //These headers are handling the "pre-flight" OPTIONS call sent by the browser
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, SOAPAction");
        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
        HttpContext.Current.Response.End();
    }
}
```

To see this code in action, follow the SOAP [tutorial below](#tutorial-to-easily-create-a-soap-based-clientserver-app-in-opensilver-wcf).

#### 2) If you are using a REST / Web API web service:

i) Modify Web.Config to add the following code (note: you need to add the code to the right section of Web.Config:

```
<configuration>
  <system.webServer>

    <httpProtocol>
      <customHeaders>
        <add name="Access-Control-Allow-Origin" value="*" />
        <add name="Access-Control-Allow-Headers" value="Content-Type, Content-Length" />
        <add name="Access-Control-Allow-Methods" value="POST,PUT,GET,DELETE,OPTIONS" />
      </customHeaders>
    </httpProtocol>

  </system.webServer>
</configuration>
```

ii) Add the following code to each of your Web API controller classes:

```
// OPTIONS
public HttpResponseMessage Options()
{
    return Request.CreateResponse(HttpStatusCode.OK);
}
```

To see this code in action, follow the REST [tutorial below](#tutorial-to-easily-create-a-rest-based-clientserver-app-in-opensilver-wep-api).

## WCF authentication / cookies / passing credentials

* *SOAP:* to enable passing cookies (for credentials/authentication) in SOAP calls, put the following code in your App.xaml.cs constructor:
Application.Current.Host.Settings.DefaultSoapCredentialsMode = System.Net.CredentialsMode.Enabled;
You may also be interested to read the [SOAP limitations](#limitations-of-the-add-service-reference-support-soap) and a [SOAP tutorial](#tutorial-to-easily-create-a-soap-based-clientserver-app-in-opensilver-wcf).

* *REST:* to enable passing cookies (for credentials/authentication) in REST calls, use the *"WebClientWithCredentials"* class. The *"WebClientWithCredentials"* is similar to the *"WebClient"* class, except that it also has a *"CredentialsMode"* property that can be set to *"Enabled"* to automatically read the authentication cookie and send it back to all further requests. For a tutorial on how to use the *"WebClient"* class, click [here](#tutorial-to-easily-create-a-rest-based-clientserver-app-in-opensilver-wep-api).

## Tips for debugging WCF services

* If you get a Script error when running your C#/XAML for HTML5 application inside the browser, go to the browser "Developer Tools" (usually F12 key) and look at the JavaScript console output to see if there are any error details. If you are using Chrome to debug the JavaScript code, you can check the option "Pause On Caught Exceptions" for easier debugging.

* In Visual Studio, create a standard C#-based "Console" project (based on the latest .NET Framework) and try to reference your web service from that project. This is useful to know if the error is related to C#/XAML for HTML5 or if it is a generic WCF error.

* Install and launch the freeware ["Fiddler"](https://www.telerik.com/fiddler) application to see what information is sent between the client and the server. You can compare this information with the information that is sent to/from the test Console application created above. This is useful to see if there are any differences between the C#/XAML for HTML5 implementation of WCF, and the pure .NET implementation.

## Common issues and solutions

* If you get the error "No 'Access-Control-Allow-Origin' header is present on the requested resource" when looking at the browser Controle log (F12), it means that the client and the server are not on the same domain and therefore you need to add support for CORS on the server. To do so, read the
[CORS-related section of this page](#adding-support-for-cross-domain-calls-cors).

## Tutorial to easily create a SOAP-based client/server app in OpenSilver (WCF)

In this tutorial we are going to create a simple client/server application for managing To-Do items. If you have any feedback regarding this tutorial, please send us an email to support@cshtml5.com

Note: a sample project source code is available [here](#limitations-of-the-add-service-reference-support-soap).

1) Create a new project of type "WCF -> *WCF Service Application"*. Let's call it "WcfService1"

2) Replace *IService1.cs* with the following code:

```
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfService1
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<ToDoItem> GetToDos();

        [OperationContract]
        void AddOrUpdateToDo(ToDoItem toDoItem);

        [OperationContract]
        void DeleteToDo(ToDoItem toDoItem);
    }

    [DataContract]
    public class ToDoItem
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
```

3) Replace *Service1.svc* with the following code:
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WcfService1
{
    public class Service1 : IService1
    {
        private static Dictionary<Guid, ToDoItem> _todos = new Dictionary<Guid, ToDoItem>();

        public List<ToDoItem> GetToDos()
        {
            return _todos.Values.ToList();
        }

        public void AddOrUpdateToDo(ToDoItem toDoItem)
        {
            _todos[toDoItem.Id] = toDoItem;
        }

        public void DeleteToDo(ToDoItem toDoItem)
        {
            if (_todos.ContainsKey(toDoItem.Id))
                _todos.Remove(toDoItem.Id);
            else
                throw new FaultException("ID not found: " + toDoItem.Id);
        }
    }
}
```
4) Create the file *"Global.asax* by right-clicking the project in the Solution Explorer, and clicking Add -> Global Application Class -> OK. Add the following code to it:
```
protected void Application_BeginRequest(object sender, EventArgs e)
{
    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

    if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
    {
        //These headers are handling the "pre-flight" OPTIONS call sent by the browser
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, SOAPAction");
        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
        HttpContext.Current.Response.End();
    }
}
```
5) Click "Start Debugging" (F5) and take note of the URL of the service. It should be something like: http://localhost:4598/Service1.svc
Keep the project running.

6) In a new instance of Visual Studio, create a new project of type *C#/XAML for HTML5 -> Empty Application*.

7) Click Project -> *Add Service Reference*, and paste the URL of the service that you created above.  It should be something like: http://localhost:4598/Service1.svc where you must replace 4958 with your port number. Click GO and then OK (leave the default name "ServiceReference1").

8) Manually *remove the following references* from the project references: System, System.Runtime.Serialization, System.ServiceModel, System.Xml (and any other DLL that starts with "System.").

9) Modify the page *MainPage.XAML* by removing the default TextBlock and replacing it with the following code:
```
<StackPanel>
    <TextBlock Text="CREATE A NEW TO-DO:" Margin="0,20,0,0" Foreground="Black" HorizontalAlignment="Left"/>
    <StackPanel Orientation="Horizontal" Margin="0,10,0,0">
        <TextBox x:Name="SoapToDoTextBox" Width="200" Text="Enter your To-Do here" Foreground="Black" Background="#FFEEEEEE"/>
        <Button Content="Create" Click="ButtonAddSoapToDo_Click" Foreground="White" Background="#FFE44D26" Margin="5,0,0,0"/>
    </StackPanel>
    <TextBlock Text="LIST OF TODO's:" Margin="0,20,0,0" Foreground="Black" HorizontalAlignment="Left"/>
    <ItemsControl x:Name="SoapToDosItemsControl" HorizontalAlignment="Left">
        <ItemsControl.ItemTemplate>
            <DataTemplate>
                <StackPanel Orientation="Horizontal" HorizontalAlignment="Left">
                    <TextBlock Text="{Binding Description}" Foreground="Black"/>
                    <Button Content="Delete" Click="ButtonDeleteSoapToDo_Click" Foreground="White" Background="#FFE44D26" Margin="5,0,0,0"/>
                </StackPanel>
            </DataTemplate>
        </ItemsControl.ItemTemplate>
    </ItemsControl>
    <Button Content="Refresh the list" Foreground="White" Background="#FFE44D26" Click="ButtonRefreshSoapToDos_Click" HorizontalAlignment="Left" Margin="0,10,0,0"/>
</StackPanel>
```
10) Add the following code to MainPage.xaml.cs (IMPORTANT: be sure to *replace the URL in orannge* with the correct one - ie. use the same URL as above):
```
ServiceReference1.Service1Client _soapClient =
        new ServiceReference1.Service1Client(
            new System.ServiceModel.BasicHttpBinding(),
            new System.ServiceModel.EndpointAddress(
                new Uri("http://localhost:4598/Service1.svc")));

void ButtonRefreshSoapToDos_Click(object sender, RoutedEventArgs e)
{
    var todos = _soapClient.GetToDos();
    SoapToDosItemsControl.ItemsSource = todos;
}

void ButtonAddSoapToDo_Click(object sender, RoutedEventArgs e)
{
    var todo = new ServiceReference1.ToDoItem()
    {
        Description = SoapToDoTextBox.Text,
        Id = Guid.NewGuid()
    };
    _soapClient.AddOrUpdateToDo(todo);
    ButtonRefreshSoapToDos_Click(sender, e);
}

void ButtonDeleteSoapToDo_Click(object sender, RoutedEventArgs e)
{
    try
    {
        var todo = (ServiceReference1.ToDoItem)((Button)sender).DataContext;
        _soapClient.DeleteToDo(todo);
        ButtonRefreshSoapToDos_Click(sender, e);
    }
    catch (System.ServiceModel.FaultException ex)
    {
        // Fault exceptions allow the server to pass information such as "Item not found":
        System.Windows.MessageBox.Show(ex.Message);
    }
}
```
11) Click "Start Debugging" to test your client/server To-Do items application.

## Tutorial to easily create a REST-based client/server app in OpenSilver (Wep API)

In this tutorial we are going to create a simple client/server application for managing To-Do items. If you have any feedback regarding this tutorial, please send us an email to support@cshtml5.com

1) Create a new project of type *"Web -> ASP.NET MVC 4 Web Application"* (or newer). Let's call it "MvcApplication1".

When you click OK, a second dialog appears that lets you choose a Project Template. Be sure to select the *"Web API"* project template. Makes sure the option "Create a unit test project" is NOT checked, and click OK.

2) Modify *Web.Config* to add the following code (note: you need to add the code to the right section of Web.Config):

```
<configuration>
  <system.webServer>

    <httpProtocol>
      <customHeaders>
        <add name="Access-Control-Allow-Origin" value="*" />
        <add name="Access-Control-Allow-Headers" value="Content-Type, Content-Length" />
        <add name="Access-Control-Allow-Methods" value="POST,PUT,GET,DELETE,OPTIONS" />
      </customHeaders>
    </httpProtocol>

  </system.webServer>
</configuration>
```

3) Create a new class called *"ToDoItem.cs"* inside the folder named *"Models"*. Copy/paste the following code:
```
using System;

namespace MvcApplication1.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Description { get; set; }
    }
}
```

4) Right-click the folder "Controllers" and click "Add -> Controller...". Enter the name *"ToDoController"* and choose "Empty MVC Controller" in the "Template" drop-down. Then click OK.
Note: the name of the controller is very important because it will have a direct impact on the URL of your REST web service.
```
using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    public class ToDoController : ApiController
    {
        private static Dictionary<Guid, ToDoItem> _todos = new Dictionary<Guid, ToDoItem>();

        // GET api/ToDo
        public IEnumerable<ToDoItem> GetToDos()
        {
            return _todos.Values.ToList();
        }

        // GET api/ToDo/5
        public ToDoItem GetToDo(Guid id)
        {
            if (_todos.ContainsKey(id))
                return _todos[id];
            else
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("ID not found: " + id),
                        ReasonPhrase = "ID not found"
                    });
        }

        // OPTIONS
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/Todo/5
        public HttpResponseMessage PutToDoItem(Guid id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("The ID must be the same as the ID of the todo."),
                    ReasonPhrase = "The ID must be the same as the ID of the todo"
                };

            _todos[toDoItem.Id] = toDoItem;
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Todo
        public HttpResponseMessage PostToDoItem(ToDoItem toDoItem)
        {
            _todos[toDoItem.Id] = toDoItem;

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/Todo/5
        public HttpResponseMessage DeleteToDoItem(Guid id)
        {
            if (_todos.ContainsKey(id))
            {
                var toDoItem = _todos[id];
                _todos.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK, toDoItem);
            }
            else
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("ID not found: " + id),
                        ReasonPhrase = "ID not found"
                    });
        }
    }
}

```
5) Click "Start Debugging" (F5) and take note of the URL of the service. It should be something like: http://localhost:4858
Keep the project running.

6) In a new instance of Visual Studio, create a new project of type *C#/XAML for HTML5 -> Empty Application*.

7) Modify the page MainPage.XAML by removing the default TextBlock and replacing it with the following code:
```
<StackPanel>
    <TextBlock Text="CREATE A NEW TO-DO:" Margin="0,20,0,0" Foreground="Black" HorizontalAlignment="Left"/>
    <StackPanel Orientation="Horizontal" Margin="0,10,0,0">
        <TextBox x:Name="RestToDoTextBox" Width="200" Text="Enter your To-Do here" Foreground="Black" Background="#FFEEEEEE"/>
        <Button Content="Create" Click="ButtonAddRestToDo_Click" Foreground="White" Background="#FFE44D26" Margin="5,0,0,0"/>
    </StackPanel>
    <TextBlock Text="LIST OF TODO's:" Margin="0,20,0,0" Foreground="Black" HorizontalAlignment="Left"/>
    <ItemsControl x:Name="RestToDosItemsControl" HorizontalAlignment="Left">
        <ItemsControl.ItemTemplate>
            <DataTemplate>
                <StackPanel Orientation="Horizontal" HorizontalAlignment="Left">
                    <TextBlock Text="{Binding Description}" Foreground="Black"/>
                    <Button Content="Delete" Click="ButtonDeleteRestToDo_Click" Foreground="White" Background="#FFE44D26" Margin="5,0,0,0"/>
                </StackPanel>
            </DataTemplate>
        </ItemsControl.ItemTemplate>
    </ItemsControl>
    <Button Content="Refresh the list" Foreground="White" Background="#FFE44D26" Click="ButtonRefreshRestToDos_Click" HorizontalAlignment="Left" Margin="0,10,0,0"/>
</StackPanel>
```
8) Add the following code to MainPage.xaml.cs (*IMPORTANT*: be sure to *replace the URL in orange* with the correct one - ie. use the same URL as above):
```
const string BaseUrl = "http://localhost:4858";

void ButtonRefreshRestToDos_Click(object sender, RoutedEventArgs e)
{
    var webClient = new WebClient();
    webClient.Encoding = Encoding.UTF8;
    webClient.Headers[HttpRequestHeader.Accept] = "application/xml";
    string response = webClient.DownloadString(BaseUrl + "//api/Todo");
    var dataContractSerializer = new DataContractSerializer(typeof(List<ToDoItem>));
    List<ToDoItem> toDoItems = (List<ToDoItem>)dataContractSerializer.DeserializeFromString(response);
    RestToDosItemsControl.ItemsSource = toDoItems;
}

void ButtonAddRestToDo_Click(object sender, RoutedEventArgs e)
{
    string data = string.Format(@"{{""Id"": ""{0}"",""Description"": ""{1}""}}", Guid.NewGuid(), RestToDoTextBox.Text.Replace("\"", "'"));
    var webClient = new WebClient();
    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
    webClient.Encoding = Encoding.UTF8;
    string response = webClient.UploadString(BaseUrl + "/api/Todo/", "POST", data);
    ButtonRefreshRestToDos_Click(sender, e);
}

void ButtonDeleteRestToDo_Click(object sender, RoutedEventArgs e)
{
    ToDoItem todo = (ToDoItem)((Button)sender).DataContext;
    var webClient = new WebClient();
    string response = webClient.UploadString(BaseUrl + "/api/Todo/" + todo.Id.ToString(), "DELETE", "");
    ButtonRefreshRestToDos_Click(sender, e);
}

public class ToDoItem
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Description { get; set; }
}
```
9) Click "Start Debugging" to test your client/server To-Do items application.



## See Also
* [Accessing a database](Accessing-database.html)
* [Circumventing cross-domain policies using JSONP](Circumventing-cross-domain-policies-using-JSONP.html)


## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
