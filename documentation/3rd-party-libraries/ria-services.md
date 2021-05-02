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
	
As you can see for Silverlight projects versions **4.6.0** are used because version **5.0.0** drops Silverlight support.
For OpenSilver project version **5.0.0-preview0003** is used. This is required for code generation and everything to work fine.

#### 2. Find and replace System.ServiceModel.DomainServices* with OpenRiaServices.DomainServices* everywhere
Use **OPENSILVER** compiler directive to keep original code working.

![Open Ria Namespaces](/images/OpenRiaNamespaces.png "Open Ria Namespaces")

Please note that you can also use "Find and Replace" feature but regular expressions need to be enabled in order to be able to generate multiline code.

#### 3. Auto code generation

If all above packages are installed with required versions then code generation will work fine.

