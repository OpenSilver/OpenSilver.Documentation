# Using WCF with VB.Net

## Introduction

VB.Net is only supported in OpenSilver 2.0+ (including 2.0 preview), it is not supported by the earlier versions. The '.Browser' and '.Simulator' projects are built on the C# programming language. However, these are primarily bootstrap helper projects, and in the vast majority of instances, VB developers will not need to modify them.

OpenSilver provides support for web services and HTTP calls in multiple ways, including:

* The *"Add Service Reference"* feature of Visual Studio 2022. Use this feature to implement SOAP based WCF service. However, it's important to note that this tool is currently not available for VB.Net projects.
> :warning: **Important points to notice **:
> * When adding a WCF Service Reference, please be sure to uncheck the option "Reuse types in referenced assemblies".
> * Update the `System.ServiceModel.*` NuGet packages from v4.4 to v4.10.
> * Some code like event handlers may not be generated, in this case include the Silverlight version of Reference.cs file into the project.
> * Binary serialization is not yet fully supported, please adjust new endpoint with `basicHttpBinding` in the `web.config`.
 
**Note**: to enable passing cookies (for credentials/authentication), put the following code in your App.xaml.cs constructor:
```
Application.Current.Host.Settings.DefaultSoapCredentialsMode = System.Net.CredentialsMode.Enabled;
```

## Sample application

You can download a sample application from the [examples](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples) folder:

* [WCF-with-VB.Net](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/WCF-with-VB.Net).

## Communication between WCF SOAP-based service and OpenSilver VB.Net(2.0) as a client


1) In a new instance of Visual Studio, create a new project of type *OpenSilver -> Empty Application*.

2) Add a new project of type "WCF -> *WCF Service Application"* with VB.Net. Let's call it "WcfService1"

**Note**: While the WCF service code highlighted in this article is written in VB.Net, it's important to understand that it could be written in any programming language supported by the .Net framework, including C#.

3) File *IService1.vb* has the following code:

```vb
' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
<ServiceContract()>
Public Interface IService1

    <OperationContract()>
    Function GetData(ByVal value As Integer) As String

    <OperationContract()>
    Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType

' TODO: Add your service operations here

End Interface

' Use a data contract as illustrated in the sample below to add composite types to service operations.

<DataContract()>
Public Class CompositeType

    <DataMember()>
    Public Property BoolValue() As Boolean

    <DataMember()>
    Public Property StringValue() As String

End Class

```

4) File *Service1.svc* has the following code:
```vb
' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.vb at the Solution Explorer and start debugging.
Public Class Service1
    Implements IService1

    Public Sub New()
    End Sub

    Public Function GetData(ByVal value As Integer) As String Implements IService1.GetData
        Return String.Format("You entered: {0}", value)
    End Function

    Public Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType Implements IService1.GetDataUsingDataContract
        If composite Is Nothing Then
            Throw New ArgumentNullException("composite")
        End If
        If composite.BoolValue Then
            composite.StringValue &= "Suffix"
        End If
        Return composite
    End Function

End Class

```

5) Click "Start Debugging" (F5) and record the URL of the service. Typically, the URL follows a structure similar to this: http://localhost:{service-port-number}/Service1.svc. Keep the project running.

6) Add a new ServiceProxy Class Library: Add a new project of type *OpenSilver -> OpenSilver Class Library* in C#(because C# projects have better tooling support in VS like "Add Service Reference" which is not present in VB.Net projects).

7) Click Project -> *Add Service Reference*, and paste the URL of the service that you previously noted, which should look something like: http://localhost:{service-port-number}/Service1.svc. Click GO and then OK (use any meaningful name, here it is "ServiceReference1").

8) Add the following code to ServiceProxy.cs
```c#
using ServiceReference1;
using System;
using System.Threading.Tasks;

namespace OpenSilverServiceProxy
{
    public class ServiceProxy
    {
        private readonly Service1Client _serviceClient;
        public ServiceProxy()
        {
            _serviceClient = new Service1Client();
        }

        public async Task<string> GetDataAsync(Int32 id)
        {
            return await _serviceClient.GetDataAsync(id);
        }
    }
}

```

9) Manually update the `System.ServiceModel.*` NuGet packages from v4.4 to v4.10.

10) Include a project reference to the 'ServiceProxy Class Library' within your OpenSilver VB.Net project.

11) Add the following code to MainPage.xaml.vb 
```vb
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls

Partial Public Class MainPage
    Inherits Page
    Public Sub New()
        Me.InitializeComponent()
        Main()
    End Sub

    Public Async Sub Main()
        Await MyFunc()
    End Sub

    Public Async Function MyFunc() As Task(Of Integer)
        Dim proxy = New OpenSilverServiceProxy.ServiceProxy()

        Dim results = Await proxy.GetDataAsync(1)
        Console.WriteLine(results)
    End Function

End Class
```
12) Launch the project to test your client/server application.


## See Also
* [Accessing a database](../in-depth-topics/accessing-database.md)
* [WebSockets](http://forums.cshtml5.com/viewtopic.php?f=7&t=276)
* [SignalR](http://forums.cshtml5.com/viewtopic.php?f=7&t=8121)
* [JSONP](../in-depth-topics/jsonp.md)
