# RIA Services

## Status

Good support in OpenSilver

## Notes

We use Open RIA Services, which is the official replacement by Microsoft when RIA Services was discontinued.

- Either use version 5, but there are some API differences compared to the original RIA Services for Silverlight from 2011 
- or use version 4.6 (recommended), but code generation is not functional at this time, you need to copy the generated files from your Silverlight solution (for now)

## NuGet Packages

#### Version 4.6 (Recommended)
https://www.myget.org/feed/opensilver/package/nuget/OpenSilver.OpenRiaServices.Client.Core.4.6
https://www.myget.org/feed/opensilver/package/nuget/OpenSilver.OpenRiaServices.Client.4.6

#### Version 5
https://www.nuget.org/packages/OpenSilver.OpenRiaServices.Client.Core/
https://www.nuget.org/packages/OpenSilver.OpenRiaServices.Client/

## Example

https://github.com/OpenSilver/OpenRiaServicesSamples/tree/main/CustomEndpoint

## Source code

https://github.com/OpenSilver/OpenRiaServices

## Roadmap

- Fix code generation in version 4.6 of Open RIA Services

## Instructions

https://github.com/OpenRIAServices/OpenRiaServices/wiki

The original documentation for WCF RIA Services is still relevant and can be found at https://msdn.microsoft.com/en-us/library/ee707344(v=vs.91).aspx . Namespaces and assembly names are no longer correct since they changed with the release of OpenRiaServices.

Documentation for changes since WCF RIA Services can be found under https://github.com/OpenRIAServices/OpenRiaServices/releases)

## A step-by-step guide to migrate Silverlight and OpenSilver projects from WCF RIA to OpenRIA Services

In [this](https://doc.opensilver.net/documentation/migrate-from-silverlight/example.html) example it is shown how to migrate **Silverlight** application to **OpenSilver**.\
If the project uses **WCF RIA** services then the beginning steps are the same but then compilation errors are expected.

![Domain Services Error](/images/DomainServicesError.png "Domain Services Error")

It is not required to have **Silverlight** project migrated to OpenRIA Services before **OpenSilver** migration but it can be useful for testing purposes.\
Migration steps are similar for both **Silverlight** and **OpenSilver** projects. The main difference is package versions.\
If nothing is mentioned then the instruction refers to both.

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
	Install-Package OpenRiaServices.OpenSilver.Client -Version 5.0.0-preview0003
	```

- Server-side
    - Silverlight project
	```
    Install-Package OpenRiaServices.Server -Version 4.6.0
    Install-Package OpenRiaServices.EntityFramework.EF4 -Version 4.6.0
	```
	
	- OpenSilver project
	```
	Install-Package OpenRiaServices.Server -Version 5.0.0-preview0003
	Install-Package OpenRiaServices.EntityFramework.EF4 -Version 4.6.0
	```
	
	Depending on project type it might be required to install other packages as well. For example if Soap and Json Ria Endpoints are used then install the following package as well.
	```
	Install-Package OpenRiaServices.Endpoints -Version 5.0.0-preview0003
	```
	
- Projects that are referenced by Server-side

    Remove reference to `System.Data.Entity` and replace that with nuget package:

    ```
    Install-Package EntityFramework -Version 6.0.1
    ```

    Replace `using System.Data` with using `System.Data.Entity.Core`

As you can see for Silverlight projects versions **4.6.0** are used because version **5.0.0** drops Silverlight support.
For OpenSilver project version **5.0.0-preview0003** is used. This is required for code generation and everything to work fine.

#### 2. Find and replace System.ServiceModel.DomainServices* with OpenRiaServices.DomainServices* everywhere
Use **OPENSILVER** compiler directive to keep original code working.

![Open Ria Namespaces](/images/OpenRiaNamespaces.png "Open Ria Namespaces")

Please note that you can also use "Find and Replace" feature but regular expressions need to be enabled in order to be able to generate multiline code.

#### 3. Auto code generation

If all above packages are installed with required versions then code generation will work fine.

#### 4. web.config

If the project contains **web.config**, then go through the web.config and remove any references to System.ServiceModel.DomainServices and add the corresponding references to corresponding OpenRiaServices assemblies instead.

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
If the original Silverlight application has **LinkedServerProject** tag then add **LinkedOpenRiaServerProject** with the correct project name in **PropertyGroup**.
The same applies for **RiaClientUseFullTypesNames** -> **OpenRiaClientUseFullTypeNames**
```
<LinkedOpenRiaServerProject>..\Project.Web\Project.Web.csproj</LinkedOpenRiaServerProject>
<OpenRiaClientUseFullTypeNames>true</OpenRiaClientUseFullTypeNames>
```

#### 6. Add ServerBaseUri

Add the following code in Application startup. For example App.xaml.cs constructor after InitializeComponent().

```
#if OPENSILVER
    DomainContext.DomainClientFactory = new OpenRiaServices.DomainServices.Client.Web.SoapDomainClientFactory()
    {
        ServerBaseUri = new Uri("http://localhost:51157/"),
    };
#endif
```

The Uri is the Web Project Uri.