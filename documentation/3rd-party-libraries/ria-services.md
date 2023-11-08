# RIA Services

## Status

Good support in OpenSilver

## Notes

We use Open RIA Services, which is the official replacement by Microsoft when RIA Services was discontinued.

- Either use version 5, but there are some API differences compared to the original RIA Services for Silverlight from 2011, and binary serialization is not functional at this point.
- Or use version 4.6 (recommended). This version is very similar to the Silverlight version of WCF RIA Services. It supports binary serialization and code generation.

For code generation to work with version 4.6, please make sure to reference the following package in your OpenSilver project: OpenSilver.OpenRiaServices.CodeGen.4.6

Note: code generation in version 4.6 works only if there is no "custom generator" on the server-side. If this is the case, you can work around the issue by removing the custom generator or copy/pasting the files generated from the Silverlight version of Open RIA Services.

## NuGet Packages

#### Version 4.6 (Recommended)
https://www.nuget.org/packages/OpenSilver.OpenRiaServices.Client.Core.4.6
https://www.nuget.org/packages/OpenSilver.OpenRiaServices.Client.4.6

The client-side OpenSilver project should reference the latest version of the following packages:
* OpenSilver
* OpenSilver.OpenRiaServices.Client.Core.4.6
* OpenSilver.OpenRiaServices.CodeGen.4.6

The server-side project (.Web) usually references the following packages:
* OpenRiaServices.Server (version 4.6.0)
* OpenRiaServices.EntityFramework.EF4 (version 4.6.0)
* EntityFramework (version 6.3.0)

#### Version 5
https://www.nuget.org/packages/OpenSilver.OpenRiaServices.Client.Core/
https://www.nuget.org/packages/OpenSilver.OpenRiaServices.Client/

## Tutorials

* [Tutorial to create a new OpenSilver business application with Open RIA Services](https://doc.opensilver.net/documentation/general/business-app.html)
* [Tutorial to migrate Silverlight and OpenSilver projects from WCF RIA Services to Open RIA Services](#Migrate-from-WCF-RIA-to-Open-RIA)

## Examples

There are many examples:

* For a real-world, open-source sample OpenSilver application that uses Open RIA Services, check the "SampleCRM" at the following URL:
https://github.com/OpenSilver/SampleCRM

* You can also create a new OpenSilver client/server application with RIA Services by using the "OpenSilver Business Application" project template present in OpenSilver 2.0 and newer.

* For another example, look at the "4_6" branch of the following repository for a working client-server example:
https://github.com/OpenSilver/OpenRiaServicesDemo

Note: make sure to run the 2 following projects simultaneously:
* SilverlightWCFRIA.Browser (the front-end)
* SilverlightWCFRIA.Web (the back-end)

Other sample application:
https://github.com/OpenSilver/OpenRiaServicesSamples/tree/main/CustomEndpoint

## Source code

https://github.com/OpenSilver/OpenRiaServices

## Roadmap

- Provide an example for the version 5.0 of Open RIA Services
- Support server-side "custom generators"
- Support binary serialization in version 5.0 (note: it is supported in v4.6)

## Instructions

Tutorials related to Open RIA Services:
https://github.com/OpenRIAServices/OpenRiaServices/wiki

The original documentation for WCF RIA Services is still relevant and can be found at https://msdn.microsoft.com/en-us/library/ee707344(v=vs.91).aspx . Namespaces and assembly names are no longer correct since they changed with the release of OpenRiaServices.

Documentation for changes since WCF RIA Services can be found under https://github.com/OpenRIAServices/OpenRiaServices/releases

Refer to the "Example" section above for downloadable source code.


## <a name="Migrate-from-WCF-RIA-to-Open-RIA"></a>A step-by-step guide to migrate Silverlight and OpenSilver projects from WCF RIA to OpenRIA Services

In [this](https://doc.opensilver.net/documentation/migrate-from-silverlight/example.html) example we show you how to migrate a **Silverlight** application to **OpenSilver**.\
If the project uses **WCF RIA** services, the beginning steps are the same but compilation errors are expected.

![Domain Services Error](/images/DomainServicesError.png "Domain Services Error")

It is not required to have the **Silverlight** project migrated to OpenRIA Services before the **OpenSilver** migration but it can be useful for testing purposes.\
Migration steps are similar for both **Silverlight** and **OpenSilver** projects. The main difference is package versions.\
If not specified, the instruction applies to both.

#### 1. Install nuget packages

There are two types of projects: **Client-side** and **Server-Side**

- Client-side

    - Silverlight project
	```
	Install-Package OpenRiaServices.Client.Core -Version 4.6.0
	Install-Package OpenRiaServices.Silverlight.CodeGen -Version 4.6.0
	```
	
    - OpenSilver project
	```
	Install-Package OpenSilver.OpenRiaServices.Client.Core.4.6
	```

- Server-side
    - Silverlight project
	```
        Install-Package OpenRiaServices.Server -Version 4.6.0
        Install-Package OpenRiaServices.EntityFramework.EF4 -Version 4.6.0
	```
	
	- OpenSilver project
	```
	Install-Package OpenRiaServices.Server -Version 4.6.0
	Install-Package OpenRiaServices.EntityFramework.EF4 -Version 4.6.0
	```
	
	Depending on the project type, it might be required to install other packages as well. For example, if Soap and Json Ria Endpoints are used, then install the following package as well.
	```
	Install-Package OpenRiaServices.Endpoints -Version 4.6.0
	```
	
- Projects that are referenced by Server-side

    Remove references to `System.Data.Entity` and replace it with the nuget package:

    ```
    Install-Package EntityFramework -Version 6.3.0
    ```

    (or version 6.0.1)

    Replace `using System.Data` with using `System.Data.Entity.Core`

As you can see, versions **4.6.0** are used because version **5.0.0** drops Silverlight support.

#### 2. Find and replace System.ServiceModel.DomainServices* with OpenRiaServices.DomainServices* everywhere
Use **OPENSILVER** compiler directive to keep original code working.

![Open Ria Namespaces](/images/OpenRiaNamespaces.png "Open Ria Namespaces")

Please note that you can also use "Find and Replace" feature but regular expressions need to be enabled in order to be able to generate multiline code.

#### 3. Auto code generation

If all the above packages are installed with the required versions then the code generation will work fine. In the OpenSilver version, the code generator will work only if  the server side has no "custom generators". You should remove those custom generators or copy/paste the code generated from the Silverlight version of Open RIA Services.

#### 4. web.config

If the project contains **web.config**, then go through the web.config file and remove any references to System.ServiceModel.DomainServices and add the corresponding references to corresponding OpenRiaServices assemblies instead.

Here are common entries

WCF RIA
```
<section name="domainServices" type="System.ServiceModel.DomainServices.Hosting.DomainServicesSection, System.ServiceModel.DomainServices.Hosting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" allowDefinition="MachineToApplication" requirePermission="false" />
```

OpenRIA
```
<section name="domainServices" type="OpenRiaServices.DomainServices.Hosting.DomainServicesSection, OpenRiaServices.DomainServices.Hosting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=2e0b7ccb1ae5b4c8" allowDefinition="MachineToApplication" requirePermission="false" />
```


WCF RIA
```
<add name="DomainServiceModule" type="System.ServiceModel.DomainServices.Hosting.DomainServiceHttpModule, System.ServiceModel.DomainServices.Hosting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
```

OpenRIA
```
<add name="DomainServiceModule" type="OpenRiaServices.DomainServices.Hosting.DomainServiceHttpModule, OpenRiaServices.DomainServices.Hosting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=2e0b7ccb1ae5b4c8" />
```


WCF RIA
```
<add name="DomainServiceModule" preCondition="managedHandler" type="System.ServiceModel.DomainServices.Hosting.DomainServiceHttpModule, System.ServiceModel.DomainServices.Hosting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
```

OpenRIA
```
<add name="DomainServiceModule" preCondition="managedHandler" type="OpenRiaServices.DomainServices.Hosting.DomainServiceHttpModule, OpenRiaServices.DomainServices.Hosting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=2e0b7ccb1ae5b4c8" />
```

#### 5. Add LinkedOpenRiaServerProject tag
If the original Silverlight application has the **LinkedServerProject** tag then add **LinkedOpenRiaServerProject** with the correct project name in **PropertyGroup**.
The same applies for **RiaClientUseFullTypesNames** -> **OpenRiaClientUseFullTypeNames**
```
<LinkedOpenRiaServerProject>..\Project.Web\Project.Web.csproj</LinkedOpenRiaServerProject>
<OpenRiaClientUseFullTypeNames>true</OpenRiaClientUseFullTypeNames>
```

#### 6. Add ServerBaseUri

Add the following code in the Application startup. For example in the "App.xaml.cs" constructor after InitializeComponent().

- If you are using Open RIA version 5, use the following code:
  
```
#if OPENSILVER
    DomainContext.DomainClientFactory = new OpenRiaServices.DomainServices.Client.Web.SoapDomainClientFactory()
    {
        ServerBaseUri = new Uri("http://localhost:51157/"),
    };
#endif
```

where you should replace the URI with the one of the Web Project (.Web).

- If you are using Open RIA version 4.6, use the following code:

```
((DomainClientFactory)DomainContext.DomainClientFactory).ServerBaseUri = new Uri("http://localhost:51157/");
```

where you should replace the URI with the one of the Web Project (.Web).

You may also need to add the business entity types as "known types", like in this example:

```
KnownTypesHelper.AddKnownType(typeof(student));
```
