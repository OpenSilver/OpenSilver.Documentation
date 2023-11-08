# Environment Setup

## Install the required software

### - Software required to create OpenSilver applications:

* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or newer, for Windows
* [OpenSilver extension for Visual Studio](https://opensilver.net/download.aspx). This is a free and full [open-source](https://github.com/OpenSilver/OpenSilver) VSIX that adds some Project Templates to your Visual Studio, so that you can easily create new OpenSilver applications from the "File -> New Project" dialog. It also adds a XAML Editor that allows you to see a preview while you are editing your XAML files.

### - Software required to run legacy Silverlight applications:

When you migrate from Silverlight to OpenSilver, it is recommended that you set up your environment in a way that allows you to run and debug the original Silverlight application.

In fact, you may later want to do step-by-step debugging in both the original application and the migrated application simulataneously to compare the two and find out the cause of the issues that you encounter .

To run the original Silverlight application you will likely need the following software:

- [Visual Studio 2015](https://visualstudio.microsoft.com/vs/older-downloads/) (or [here](https://stackoverflow.com/questions/44290672/how-to-download-visual-studio-community-edition-2015-not-2017)). This is the most recent version of Visual Studio that officially supported Silverlight.

- Silverlight 5 SDK

- Silverlight 5 Toolkit (Dec 2011 release)

- Silverlight 5 Developer Runtime for Windows (64 bit).

It is recommended that the Silverlight development environment is tested by attempting to create a new Silverlight application and by verifying that step-by-step debugging works properly. If it doesn't please make sure that the above components have been installed successfully and also make sure that the **Silverlight** option is checked under the **Debuggers** section of the **.Web** project properties.

![Turn on Silverlight debugging](/images/silverlight_debuggers.png "Turn on Silverlight debugging")

  Notes:
  - In some cases the original application may have been created with an older version of Visual Studio (usually 2010, 2012, or 2013). In such case, you should either install one of those older versions of VS, or attempt to update the Silverlight solution to a more recent version of VS.
  - There is an unofficial VS extension that allows to open and run Silverlight solutions from more recent versions of VS, such as VS 2019. However, it may cause regressions, which is why we do not recommend this approach. It is called "Silverlight for Visual Studio" by Rami Abughazaleh. The VS 2019 version can be found [here](https://marketplace.visualstudio.com/items?itemName=RamiAbughazaleh.SilverlightProjectSystem).

Depending on the libraries that the Silverlight application is using, you may also need the following components:

- RIA Services V1.0 SP2. This is required only if the Silverlight application uses WCF RIA Services.

- Other 3rd party components (Telerik, DevExpress, Syncfusion, ComponentOne...) 


### - Optional software recommended for debugging and investigating issues:

- [Fiddler](https://www.telerik.com/download/fiddler) is a free web debugging proxy by Telerik that captures all HTTP(S) traffic between the client and the server components of your applications so that you can inspect traffic, set breakpoints, and fiddle with requests & responses. It is a great way to investigate client/server communication issues.
- [XAML Spy Express](http://xamlspy.com/learn/xaml-spy-express) is a free tool that allows to inspect the UI visual tree of a Silverlight application to see how UI elements are structured, as well as examine the properties of the UI elements. It is useful to investigate the differences between the UI of the original Silverlight application and the UI of the migrated OpenSilver application. Note: when working on the OpenSilver application, you do not need XAML Spy to inspect the Visual Tree: just run the OpenSilver "Simulator" and click "Inspect Visual Tree".

