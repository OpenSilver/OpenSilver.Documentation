# Migrating from Silverlight to OpenSilver

## Overview

**Before reading this section, please make sure to read the general [OpenSilver overview](../general/overview.md) page first.**

The general principle for migrating a Silverlight application to OpenSilver consists in creating an OpenSilver-type project for each of the original Silverlight projects, then copying / pasting all the files from the original projects to the OpenSilver projects, and finally compiling the solution.

In practice some compilation errors are expected, since OpenSilver currently supports a (fairly large) subset of Silverlight functionality. Particularly in the area of third-party libraries, some manual work may be required if a particular library is not yet supported.

Userware, the company behind the open-source project OpenSilver, is currently working on compatibility with third-party components and has already successfully implemented many components of the Telerik UI suite. Userware has also been able to reach a good level of support for RIA Services, PRISM, MEF, MvvmLight, SharpZipLib, Newtonsoft, and other Silverlight libraries.

When a functionality is not available, the developer has several possibilities: bypass it with alternative C# (or VB.NET or F#) code and XAML, use a .NET Standard library which provides equivalent functionality, [develop it in JavaScript](https://doc.opensilver.net/documentation/general/javascript-interop-and-libraries.html), or [import an existing JavaScript library](https://doc.opensilver.net/documentation/general/javascript-interop-and-libraries.html).

In all cases, developers can [contact Userware](https://www.opensilver.net/contact.aspx) for professional services ranging from the simple development of a functionality to the complete migration of an application. Case studies can be seen on the OpenSilver.NET web site showing applications migrated by Userware. A [paid support package](https://www.opensilver.net/links/migration-package.aspx) is also available for guidance on migrating from Silverlight to OpenSilver.

On the [OpenSilver web site](https://www.opensilver.net), there is also the possibility of [submitting a XAP file](https://www.opensilver.net/migrate/upload-xap.aspx), i.e. the executable of the Silverlight application to be migrated, in order to receive an analysis of the supported and unsupported features, as well as an estimate of the workload to complete the migration of the application.

The advantages of migrating with OpenSilver rather than rewriting with another language can be many. While a reduction in the time and cost of migration seems obvious, other benefits include a result that is closer to the original application both in terms of appearance and functionality, higher post-migration productivity, and fewer risks of regressions because the code has already been tested. In fact, the code of the migrated application is almost identical to the original, so developers who must maintain it have a greater knowledge and mastery of the code compared to if it was rewritten with another programming language.


## Tutorial to migrate from Silverlight to OpenSilver

**Note: this section assumes that you would like to do the migration on your own. Alternatively, you can have your application migrated by Userware - the company behind OpenSilver - in a fast and cost-effective way, so that your resources are free to work on other tasks. Visit [OpenSilver.net](https://opensilver.net) for details.**

Migrating from Silverlight to OpenSilver usually involves the following steps. Click on any step to read the details:
1. [Environment Setup](environment-setup.md)
2. [Compile with OpenSilver](compile-with-opensilver.md)
3. [Fix runtime issues](fix-runtime-issues.md)

For any questions, please feel free to contact the OpenSilver team at: https://opensilver.net/contact.aspx
