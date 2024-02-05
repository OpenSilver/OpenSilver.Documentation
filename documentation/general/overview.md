# OpenSilver Overview

## What is OpenSilver?

OpenSilver is an open-source replacement for Microsoft Silverlight. It is a development tool allowing developers to write web applications using .NET, C#(or VB.NET or F#), and XAML, like Silverlight was. A notable difference with Silverlight is that applications created with OpenSilver run on all current browsers, including Chrome, Edge, Firefox and Safari, without requiring any plugin.

The OpenSilver project was first announced in March 2020 (Technical Preview) by Userware, a French company founded in 2007 specializing in Microsoft development technologies, which had already been working since 2014 on the conversion from C # / XAML to HTML / JavaScript, as part of its other product called [CSHTML5](http://cshtml5.com).
OpenSilver can be downloaded from [OpenSilver.NET](https://OpenSilver.NET). The site also contains sample applications and case studies.

The full OpenSilver source code is available under the MIT license on GitHub at [github.com/OpenSilver](https://github.com/OpenSilver)

## Why the need to replace Silverlight?

In 2007 Microsoft released Silverlight, a plugin for Internet browsers that revolutionized both the browsing experience for users and the development experience for programmers.

On the user side, Silverlight enabled the display of rich content in web pages, much like the competing Adobe Flash plugin at the time. Thanks to Silverlight, it was possible to have very complex applications that ran entirely inside the browser, with an advanced graphical interface and performance similar to that of native Windows applications.

On the developer side, Silverlight enabled the creation of web applications using .NET, C#(or VB.NET or F#) and XAML, that is to say very powerful technologies which until then had been reserved for Windows application development.

With the advent and rapid spread of smartphones (iPhone, Android, etc.) and tablets, it has become increasingly difficult to install plugins on Internet browsers. So, little by little, even the Windows versions of the main Internet browsers decided to stop supporting plugins. That was the end for Adobe Flash and Microsoft Silverlight, which today no longer run on Safari, Chrome, Edge nor Firefox. The last notable version of Silverlight, version 5, was released in December 2011. Then, in 2012, Microsoft announced that Silverlight would only be supported for another ten years, until October 12, 2021, and would only be supported on Microsoft Internet Explorer.

This has been a major problem for many companies that had invested several years of development in their Silverlight applications, particularly in the area of ​​enterprise applications. The need has arisen to find a replacement for Silverlight so as not to throw away all these years of development, hence the birth of OpenSilver.

## What are the differences between OpenSilver and Silverlight?

On the user side, Silverlight requires the installation of a plugin in order to work in Internet browsers. OpenSilver on the contrary exploits modern and standardized technologies integrated into browsers, in particular HTML5, CSS3 and WebAssembly, to be able to run on all current browsers: Chrome, Edge, Firefox, Safari.

On the developer side, OpenSilver supports .NET Standard, the latest version of the C#(or VB.NET or F#) languages (whereas Silverlight was stuck with C# 6 and was not compatible with .NET Standard) and the latest version of Visual Studio. However, some advanced features of Silverlight are not yet supported, such as XNA or Smooth Streaming, and some third-party UI components, such as Telerik UI, have some known issues which are being addressed by Userware. Furthermore, while OpenSilver lets developers directly reference .NET Standard assemblies without recompiling them, it is not possible to reference a Silverlight assembly directly from an OpenSilver project: it must be recompiled with OpenSilver from its source code.

Internally, the implementation of OpenSilver is completely different from that of Silverlight. For example, Silverlight uses Windows technologies for rendering, while OpenSilver creates an HTML element and applies CSS styles for each element of the XAML. The end result is almost identical, but the approach is very different.

## How to migrate an existing Silverlight application?

The general principle for migrating a Silverlight application to OpenSilver consists in creating an OpenSilver-type project for each of the original Silverlight projects, then copying / pasting all the files from the original projects to the OpenSilver projects, and finally compiling the solution.

In practice some compilation errors are expected, since OpenSilver currently supports a (fairly large) subset of Silverlight functionality. Particularly in the area of third-party libraries, some manual work may be required if a particular library is not yet supported.

Userware, the company behind the open-source project OpenSilver, is currently working on compatibility with third-party components and has already successfully implemented many components of the Telerik UI suite. Userware has also been able to reach a good level of support for RIA Services, PRISM, MEF, MvvmLight, SharpZipLib, Newtonsoft, and other Silverlight libraries.

When a functionality is not available, the developer has several possibilities: bypass it with alternative C#(or VB.NET or F#) code and XAML, use a .NET Standard library which provides equivalent functionality, [develop it in JavaScript](javascript-interop-and-libraries.md), or [import an existing JavaScript library](javascript-interop-and-libraries.md#how-to-import-javascript-libraries).

In all cases, developers can [contact Userware](https://www.opensilver.net/contact.aspx) for professonal services ranging from the simple development of a functionality to the complete migration of an application. Case studies can be seen on the OpenSilver.NET web site showing applications migrated by Userware. A [paid support package](https://www.opensilver.net/links/migration-package.aspx) is also available for guidance on migrating from Silverlight to OpenSilver.

On the [OpenSilver web site](https://www.opensilver.net) there is also the possibility of [submitting a XAP file](https://www.opensilver.net/migrate/upload-xap.aspx), i.e. the executable of the Silverlight application to be migrated, in order to receive an analysis of the supported and unsupported features, as well as an estimate of the workload to complete the migration of the application.

The advantages of migrating with OpenSilver rather than rewriting with another language can be many. While a reduction in the time and cost of migration seems obvious, other benefits include a result that is closer to the original application both in terms of appearance and functionality, higher post-migration productivity, and fewer risks of regressions because the code has already been tested. In fact, the code of the migrated application is almost identical to the original, so developers who must maintain it have a greater knowledge and mastery of the code compared to if it was rewritten with another programming language.

## The OpenSilver promise

OpenSilver addresses the need to maintain Silverlight applications which have sometimes required several years of development. But the promise of OpenSilver goes far beyond that, because it aims to revive the simplicity and the efficiency of the development of rich web applications that Silverlight proposed, by leveraging the Silverlight legacy, modernizing the Silverlight technology through WebAssembly, and pushing the boundaries of what it can do.
