# WCF and Http Clients

## Introduction

OpenSilver provides support for web services and HTTP calls in multiple ways, including:

* The *"Add Service Reference"* feature of Visual Studio. Use this feature to easily communicate with SOAP / WCF web services. See below for [limitations](#limitations-of-the-add-service-reference-support-soap) and a [Tutorial](#tutorial-to-easily-create-a-soap-based-clientserver-app-in-opensilver-wcf).
> :warning: **Important limitations of the current release**:
> * When adding a WCF Service Reference, please be sure to uncheck the option "Reuse types in referenced assemblies".
> * Update the `System.ServiceModel.*` NuGet packages from v4.4 to v4.10.
> * Some code like event handlers may not be generated, in this case include the Silverlight version of Reference.cs file into the project.
> * Binary serialization is not yet fully supported, please adjust new endpoint with `basicHttpBinding` in the `web.config`.
* [HttpClient](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-6.0)
* The *"WebClient"* class. Use this classs to download strings from the web, as well as to communicate with REST / Web API web services. See below for a [Tutorial](#tutorial-to-easily-create-a-rest-based-clientserver-app-in-opensilver-wep-api). Please pay attention to the fact that System.Net.WebClient is not supported in the Browser for .NET 5+. However, you can use the migrated class OpenSilver.Compatibility.WebClient.
> **Note**: to enable passing cookies (for credentials/authentication), put the following code in your App.xaml.cs constructor:
```
Application.Current.Host.Settings.DefaultSoapCredentialsMode = System.Net.CredentialsMode.Enabled;
```
* [WebSockets](http://forums.cshtml5.com/viewtopic.php?f=7&t=276)
* [SignalR](http://forums.cshtml5.com/viewtopic.php?f=7&t=8121)
* [JSONP](jsonp.md)

## Sample application

You can download sample applications from the [examples](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples) folder:

* [WcfService1](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/WcfService1). This is a sample for a server-side WCF project.
* [OpenSilverWcfConsumer](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/OpenSilverWcfConsumer). This is a sample OpenSilver application, which interacts with the server-side project via WCF services.
* [ToDoWebApi](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/ToDoWebApi). This is a sample for a server-side WebApi project.
* [OpenSilverRestConsumer](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/OpenSilverRestConsumer). This is a sample OpenSilver application, which interacts with the server-side project via REST calls.

Sample shows a basic To-Do management application.

## Limitations of the "Add Service Reference" support (SOAP)

In the current version, OpenSilver has the following limitations regarding the "Add Service Reference" (SOAP) feature:

* Due to JavaScript restrictions in the browser, *cross-domain calls require the server to [implement CORS](#adding-support-for-cross-domain-calls-cors)*. In other words, if your client application is not hosted on the same domain as your WCF web service, you need to add CORS to the web service (or you can use [JSONP](Circumventing-cross-domain-policies-using-JSONP.html) as an alternative to CORS, but it is not recommended for modern services). To add CORS to your web service, follow this [tutorial](#adding-support-for-cross-domain-calls-cors).

* When adding a WCF Service Reference via the "Add Service Reference" command of VS, please be sure to uncheck the option "Reuse types in referenced assemblies", and update the NuGet packages from v4.4 to v4.10.

<!--Furthermore, please note that the following features are NOT yet supported:

* Non-http bindings (only BasicHttpBinding and HTTPS/SSL are currently supported)
* The generic type "FaultException<T>" (only the non-generic FaultException type is currently supported)

We are working to add support for all of the above features as soon as possible. Please make sure to vote for your most wanted features on http://cshtml5.uservoice.com -->

## Adding support for cross-domain calls (CORS)

Due to the JavaScript restrictions in the browser, cross-domain calls require the server to implement CORS. In other words, if your client application is not hosted on the same domain as your WCF web service, you need to add CORS to the web service (or you can use [JSONP](jsonp.md) as an alternative to CORS, but it is not recommended for modern services).

If you see the error *"No 'Access-Control-Allow-Origin' header is present on the requested resource"* in the browser Console window (F12), it is likely that CORS has not been properly configured.

Here are your options:

* Either *host your application on the same domain as your web service(a good choice for the production environment)*. In this case, CORS won't be required and you won't encounter any cross-domain issues. However this is not practical during development because it requires you to publish your application every time you need to test.

* Or *add CORS to your web service (recommended for development environment)*. This is the recommended approach but it assumes that you have access to the source code of your web service and you have the rights to modify it and publish it. See below for a tutorial on how to add CORS to your web service.

* Or *work around the issue by running Chrome without CORS verification*. This is obviously only for development, not for use in production. To do so, open the Windows "Run" dialog (for example by pressing the Windows+R keys combination), and execute the following command:
```
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --disable-web-security --user-data-dir="c:/chromedev" --disable-features=SameSiteByDefaultCookies
```

* Or *work around the issue by using Fiddler* to make the browser believe that your server allows cross-domain requests. This is obviously only for development, not for use in production. To do so, follow the steps described [here](fiddler-enable-cors.html).

It is also possible to get **SameSite** attribute related errors similar to below when the client and web service are not hosted on the same domain.
```
A cookie associated with a cross-site resource at <URL> was set without the 'SameSite' attribute. ...
```
To solve this go to chrome://flags/ and disable **SameSite by default cookies**. But the option might be removed from latest versions of browsers.

[Here](https://doc.opensilver.net/documentation/how-to-topics/browser-with-arguments.html) is how to launch OpenSilver project in Browser using custom arguments


### To add CORS to your web service (recommended for development environment), simply follow these steps:

> :warning: Please, read about [CORS and security reasons of using CORS](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS)

#### 1) If you are using a SOAP web service:

i) Make sure you have the file *"Global.asax.cs"* in your server-side SOAP web service project. If you don't have that file, right-click the project name in the Solution Explorer, and click "Add" -> "Global Application Class".

ii) Add the following code to the said file:
```
protected void Application_BeginRequest(object sender, EventArgs e)
{
    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "/* the-absolute-URL-of-the-client-app */");

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
> **Note**: To use cookies, which is necesary for authentication, there MUST be an `Access-Control-Allow-Origin` header set to the URL of the client application. 
The star wildcard is not sufficcient in this case and the preflight request WILL FAIL with the following error in the browser: 
**Access to XMLHttpRequest at '*server-enpoint*' from origin '*client-endpoint*' has been blocked by CORS policy: Response to preflight request doesn't pass access control check: The value of the 'Access-Control-Allow-Origin' header in the response must not be the wildcard '*' when the request's credentials mode is 'include'. The credentials mode of requests initiated by the XMLHttpRequest is controlled by the withCredentials attribute.**

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

Please, replace * by the domain of your OpenSilver application.

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

<!--* *REST:* to enable passing cookies (for credentials/authentication) in REST calls, use the *"WebClientWithCredentials"* class. The *"WebClientWithCredentials"* is similar to the *"WebClient"* class, except that it also has a *"CredentialsMode"* property that can be set to *"Enabled"* to automatically read the authentication cookie and send it back to all further requests. For a tutorial on how to use the *"WebClient"* class, click [here](#tutorial-to-easily-create-a-rest-based-clientserver-app-in-opensilver-wep-api).-->

## Tips for debugging WCF services

* In Visual Studio, create a standard C#-based "Console" project (based on the latest .NET Framework) and try to reference your web service from that project. This is useful to know if the error is related to OpenSilver or if it is a generic WCF error.

* Install and launch the freeware ["Fiddler"](https://www.telerik.com/fiddler) application to see what information is sent between the client and the server. You can compare this information with the information that is sent to/from the test Console application created above. This is useful to see if there are any differences between the OpenSilver implementation of WCF, and the pure .NET implementation.

## Common issues and solutions

* If you get the error "No 'Access-Control-Allow-Origin' header is present on the requested resource" when looking at the browser Controle log (F12), it means that the client and the server are not on the same domain and therefore you need to add support for CORS on the server. To do so, read the
[CORS-related section of this page](#adding-support-for-cross-domain-calls-cors).

## Tutorial to easily create a SOAP-based client/server app in OpenSilver (WCF)

In this tutorial we are going to create a simple client/server application for managing To-Do items. If you have any feedback regarding this tutorial, please send us an email to contact@opensilver.net

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
4) Create the file *"Global.asax* by right-clicking the project in the Solution Explorer, and clicking Add -> New Item -> Global Application Class -> OK. Add the following code to it:
```
protected void Application_BeginRequest(object sender, EventArgs e)
{
    //You need to replace URL of a client app later
    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:55591");

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

6) In a new instance of Visual Studio, create a new project of type *OpenSilver -> Empty Application*.

7) Click Project -> *Add Service Reference*, and paste the URL of the service that you created above.  It should be something like: http://localhost:4598/Service1.svc where you must replace 4958 with your port number. Click GO and then OK (leave the default name "ServiceReference1").

8) Manually update the NuGet packages from v4.4 to v4.10.

9) Modify the page *MainPage.XAML* by removing the default TextBlock and replacing it with the following code:
```
<StackPanel>
    <TextBlock Text="CREATE A NEW TO-DO:" Margin="0,20,0,0" HorizontalAlignment="Left"/>
    <StackPanel Orientation="Horizontal" Margin="0,10,0,0">
        <TextBox x:Name="SoapToDoTextBox" Width="200" Text="Enter your To-Do here"/>
        <Button Content="Create" Click="ButtonAddSoapToDo_Click" Margin="5,0,0,0"/>
    </StackPanel>
    <TextBlock Text="LIST OF TODO's:" Margin="0,20,0,0" HorizontalAlignment="Left"/>
    <ItemsControl x:Name="SoapToDosItemsControl" HorizontalAlignment="Left">
        <ItemsControl.ItemTemplate>
            <DataTemplate>
                <StackPanel Orientation="Horizontal" HorizontalAlignment="Left">
                    <TextBlock Text="{Binding Description}"/>
                    <Button Content="Delete" Click="ButtonDeleteSoapToDo_Click" Margin="5,0,0,0"/>
                </StackPanel>
            </DataTemplate>
        </ItemsControl.ItemTemplate>
    </ItemsControl>
    <Button Content="Refresh the list" Click="ButtonRefreshSoapToDos_Click" HorizontalAlignment="Left" Margin="0,10,0,0"/>
</StackPanel>
```
10) Add the following code to MainPage.xaml.cs (IMPORTANT: be sure to *replace the URL in orange* with the correct one - ie. use the same URL as above):
```
ServiceReference1.Service1Client _soapClient =
    new ServiceReference1.Service1Client(
        new CustomBinding(),
        new System.ServiceModel.EndpointAddress(
            new Uri("http://localhost:65264/Service1.svc")));

private async Task GetTodosAsync()
{
    var todos = await _soapClient.GetToDosAsync();
    SoapToDosItemsControl.ItemsSource = todos;
}

private async Task AddOrUpdateTodosAsync(ServiceReference1.ToDoItem todo)
{
    await _soapClient.AddOrUpdateToDoAsync(todo);
    await GetTodosAsync();
}

private async Task DeleteAsync(ServiceReference1.ToDoItem todo)
{
    try
    {
        await _soapClient.DeleteToDoAsync(todo);
        await GetTodosAsync();
    }
    catch (System.ServiceModel.FaultException ex)
    {
        // Fault exceptions allow the server to pass information such as "Item not found":
        System.Windows.MessageBox.Show(ex.Message);
    }
}

void ButtonRefreshSoapToDos_Click(object sender, RoutedEventArgs e)
{
    _ = GetTodosAsync();
}

void ButtonAddSoapToDo_Click(object sender, RoutedEventArgs e)
{
    var todo = new ServiceReference1.ToDoItem()
    {
        Description = SoapToDoTextBox.Text,
        Id = Guid.NewGuid()
    };
    _ = AddOrUpdateTodosAsync(todo);
}

void ButtonDeleteSoapToDo_Click(object sender, RoutedEventArgs e)
{
    var todo = (ServiceReference1.ToDoItem)((Button)sender).DataContext;
    _ = DeleteAsync(todo);
}
```
11) Launch the project to test your client/server To-Do items application.

There are working examples for both of these projects:
* [WcfService1](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/WcfService1)
* [OpenSilverWcfConsumer](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/OpenSilverWcfConsumer)

## Tutorial to easily create a REST-based client/server app in OpenSilver (Wep API)

In this tutorial we are going to create a simple client/server application for managing To-Do items. If you have any feedback regarding this tutorial, please send us an email to contact@opensilver.net

1) Create a new project of type *"ASP.NET Core Web API"*. Let's call it "ToDoWebApi".

2) Modify *Program.cs* to add the following code:

```
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

const string corsApp = "corsapp";
//services cors
builder.Services.AddCors(p => p.AddPolicy(corsApp, b =>
{
    //You need to replace URL of a client app later
    b.WithOrigins("http://localhost:55591/").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

//app cors
app.UseCors(corsApp);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

```

3) Create a new class called *"ToDoItem.cs"* inside the folder named *"Models"*. Copy/paste the following code:
```
namespace ToDoWebApi.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string? Description { get; set; }
    }
}
```

4) Right-click the folder "Controllers" and click "Add -> Controller...". Enter the name *"ToDoController"* and choose "Empty MVC Controller" in the "Template" drop-down. Then click OK.
Note: the name of the controller is very important because it will have a direct impact on the URL of your REST web service.
```
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private static readonly Dictionary<Guid, ToDoItem> ToDos = new();

        // GET api/ToDo
        [HttpGet]
        public IEnumerable<ToDoItem> GetToDos()
        {
            return ToDos.Values.ToList();
        }

        // GET api/ToDo/5
        [HttpGet("{id}")]
        public ActionResult GetToDo(Guid id)
        {
            if (ToDos.ContainsKey(id))
            {
                return Ok(ToDos[id]);
            }

            return NotFound("ID not found: " + id);
        }

        // PUT api/Todo/5
        [HttpPut("{id}")]
        public ActionResult PutToDoItem(Guid id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest("The ID must be the same as the ID of the todo");
            }

            ToDos[toDoItem.Id] = toDoItem;
            return Ok();
        }

        // POST api/Todo
        [HttpPost]
        public ActionResult PostToDoItem(ToDoItem toDoItem)
        {
            ToDos[toDoItem.Id] = toDoItem;

            return Ok();
        }

        // DELETE api/Todo/5
        [HttpDelete("{id}")]
        public ActionResult DeleteToDoItem(Guid id)
        {
            if (!ToDos.ContainsKey(id))
            {
                return NotFound("ID not found: " + id);
            }

            ToDos.Remove(id);
            return Ok();
        }
    }
}

```
5) Click "Start Debugging" (F5) and take note of the URL of the service. It should be something like: http://localhost:4858
Keep the project running.

6) In a new instance of Visual Studio, create a new project of type *OpenSilver -> Empty Application*.

7) Modify the page MainPage.XAML by removing the default TextBlock and replacing it with the following code:
```
<StackPanel>
    <TextBlock Text="CREATE A NEW TO-DO:" Margin="0,20,0,0" HorizontalAlignment="Left"/>
    <StackPanel Orientation="Horizontal" Margin="0,10,0,0">
        <TextBox x:Name="RestToDoTextBox" Width="200" Text="Enter your To-Do here"/>
        <Button Content="Create" Click="ButtonAddRestToDo_Click" Margin="5,0,0,0"/>
    </StackPanel>
    <TextBlock Text="LIST OF TODO's:" Margin="0,20,0,0" Foreground="Black" HorizontalAlignment="Left"/>
    <ItemsControl x:Name="RestToDosItemsControl" HorizontalAlignment="Left">
        <ItemsControl.ItemTemplate>
            <DataTemplate>
                <StackPanel Orientation="Horizontal" HorizontalAlignment="Left">
                    <TextBlock Text="{Binding Description}"/>
                    <Button Content="Delete" Click="ButtonDeleteRestToDo_Click" Margin="5,0,0,0"/>
                </StackPanel>
            </DataTemplate>
        </ItemsControl.ItemTemplate>
    </ItemsControl>
    <Button Content="Refresh the list" Click="ButtonRefreshRestToDos_Click" HorizontalAlignment="Left" Margin="0,10,0,0"/>
</StackPanel>
```
8) Add the following code to MainPage.xaml.cs (*IMPORTANT*: be sure to *replace the URL in orange* with the correct one - ie. use the same URL as above):
```
const string BaseUrl = "https://localhost:7037";
private static readonly HttpClient Client = new HttpClient();

private async Task DownloadAsync()
{
    using (var response = await Client.GetAsync(BaseUrl + "/ToDo"))
    using (var content = response.Content)
    {
        var json = await content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var items = JsonSerializer.Deserialize<IEnumerable<ToDoItem>>(json, options).ToList();
        RestToDosItemsControl.ItemsSource = items;
    }
}

private async Task AddAsync(ToDoItem toDoItem)
{
    var content = new StringContent(JsonSerializer.Serialize(toDoItem), Encoding.UTF8,
        "application/json");

    await Client.PostAsync(BaseUrl + "/ToDo", content);
    await DownloadAsync();
}

private async Task DeleteAsync(ToDoItem toDoItem)
{
    await Client.DeleteAsync(BaseUrl + "/ToDo/" + toDoItem.Id);
    await DownloadAsync();
}

void ButtonRefreshRestToDos_Click(object sender, RoutedEventArgs e)
{
    _ = DownloadAsync();
}

void ButtonAddRestToDo_Click(object sender, RoutedEventArgs e)
{
    var toDoItem = new ToDoItem
    {
        Id = Guid.NewGuid(),
        Description = RestToDoTextBox.Text
    };

    _ = AddAsync(toDoItem);
}

void ButtonDeleteRestToDo_Click(object sender, RoutedEventArgs e)
{
    var todo = (ToDoItem)((Button)sender).DataContext;
    _ = DeleteAsync(todo);
}

public class ToDoItem
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Description { get; set; }
}
```
9) Launch the project to test your client/server To-Do items application.

*Note:* if there are CORS issues in the console, please update URL of the client app in program.cs for the WebApi project.

There are working examples for both of the described projects:
* [ToDoWebApi](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/ToDoWebApi)
* [OpenSilverRestConsumer](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/OpenSilverRestConsumer)

## See Also
* [Accessing a database](accessing-database.md)
* [Circumventing cross-domain policies using JSONP](jsonp.md)


## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
