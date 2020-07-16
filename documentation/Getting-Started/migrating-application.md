# How to migrate an existing Silverlight application?

The general principle for migrating a Silverlight application to OpenSilver consists of creating an OpenSilver-type project for each of the original Silverlight projects, then copying / pasting all the files from the original projects to the OpenSilver projects, and finally compiling the solution.

In practice many compilation errors are expected, since OpenSilver currently supports a subset of Silverlight functionality. Particularly in the area of third-party libraries, such as Telerik UI, Syncfusion, DevExpress or ComponentOne, there are some missing pieces.

Userware, the company behind the open-source OpenSilver project, is currently working on compatibility with these third-party components and has already successfully implemented many components of the Telerik UI suite. Userware is also currently working on the support for RIA Services, PRISM, MEF, and other Silverlight libraries.

When a functionality is not available, the developer has several possibilities: bypass it with alternative C# code and XAML, use a .NET Standard library which provides equivalent functionality, develop it in JavaScript (see the corresponding paragraph below), or import an existing JavaScript library.

In all cases, developers can contact Userware for professonal services ranging from the simple development of a functionality to the complete migration of an application. Case studies can be seen on the OpenSilver.NET web site showing applications migrated by Userware.

On the OpenSilver web site there is also the possibility of submitting a XAP file, i.e. the executable of the Silverlight application to be migrated, in order to receive an analysis of the supported and unsupported features, as well as an estimate of the workload to complete the migration of the application.

The advantages of migrating with OpenSilver rather than rewriting with another language can be many. While a reduction in the time and cost of migration seems obvious, other benefits include a result that is closer to the original application both in terms of appearance and functionality, as well as higher post-migration productivity. In fact, the code of the migrated application is almost identical to the original, so developers who must maintain it have a greater knowledge and mastery of the code compared to if it was rewritten with another programming language.
