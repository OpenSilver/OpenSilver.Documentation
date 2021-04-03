# RIA Services

## Status

Good support in OpenSilver

## Notes

We use Open RIA Services, which is the official replacement by Microsoft when RIA Services was discontinued.

- Either use version 5, but there are some API differences compared to the original RIA Services for Silverlight from 2011 
- or use version 4.6 (recommended), but code generation is not functional at this time, you need to copy the generated files from your Silverlight solution (for now)

## NuGet Packages

Coming soon

## Source code

https://github.com/OpenSilver/OpenRiaServices

## Roadmap

- Fix code generation in version 4.6 of Open RIA Services

## Instructions

https://github.com/OpenRIAServices/OpenRiaServices/wiki

The original documentation for WCF RIA Services is still relevant and can be found at https://msdn.microsoft.com/en-us/library/ee707344(v=vs.91).aspx . Namespaces and assembly names are no longer correct since they changed with the release of OpenRiaServices.

Documentation for changes since WCF RIA Services can be found under https://github.com/OpenRIAServices/OpenRiaServices/releases)

## A step-by-step guide to migrate WCF RIA Services in OpenSilver project

In [this](https://doc.opensilver.net/documentation/migrate-from-silverlight/example.html) example it is shown how to migrate **Silverlight** application to **OpenSilver**.\
If the project uses **WCF RIA** services then the beginning steps are the same but then compilation errors are expected.

![Domain Services Error](/images/DomainServicesError.png "Domain Services Error")

#### 1. Install nuget packages

There are two types of projects: **Client-side** and **Server-Side**

- For client-side projects install **OpenRiaServices.Client.Core** nuget package.\
We use version **v4.6.3** because **v5.0** drops Silverlight support. Please note that **v4.6.0** could also be used but it doesn't include **OpenRiaServices.DomainServices.Client.Web.dll**.

```
Install-Package OpenRiaServices.Client.Core -Version 4.6.3 -Project My.Project.Name
```

- For server-side projects install **OpenRiaServices.Server** nuget package.\
We use version **v4.6.0** for the same reason as for above.

```
Install-Package OpenRiaServices.Server -Version 4.6.0 -Project My.Project.Name
```

#### 2. Find and replace System.ServiceModel.DomainServices* with OpenRiaServices.DomainServices* everywhere
Use **OPENSILVER** compiler directive to keep original code working.

![Open Ria Namespaces](/images/OpenRiaNamespaces.png "Open Ria Namespaces")

Please note that you can also use "Find and Replace" feature but regular expressions need to be enabled in order to be able to generate multiline code.

#### 3. Use auto-generated files

Visual Studio will auto-generate a file or files under **Generated_Code** directory when compiling Silverlight solution. It is convenient to copy this folder under **Generated_Code.OpenSilver** to make sure that the code won't be regenrated again.\
Exclude **Generated_Code** folder from the OpenSilver project after the copy to not have duplicate classes.

![Generated Code](/images/GeneratedCode.png "Generated Code")

