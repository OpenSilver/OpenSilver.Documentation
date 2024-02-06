# Compiling with OpenSilver

**Note: this section assumes that you would like to do the migration on your own. Alternatively, you can have your application migrated by Userware - the company behind OpenSilver - in a fast and cost-effective way, so that your resources are free to work on other tasks. Visit [OpenSilver.net](https://opensilver.net) for details.**

## Pre-requisites

Before starting to migrate to OpenSilver:
- Follow the steps described on the "[Environment Setup](environment-setup.md)" page
- Make sure that you have the full source code of the legacy Silverlight application that you wish to migrate to OpenSilver
- **Make sure that the legacy Silverlight application fully compiles on your developer machine**. All the Silverlight projects should be loading and compiling properly, and all the referenced libraries should be properly resolved. Refer to the "[Environment Setup](environment-setup.md)" page for the list of software that you may need to install to get the Silverlight application to compile.
- If the Silverlight application has database connections make sure that the connection strings are correct.
    - Go to **Server Explorer** tab
	- Right click on **Data Connections** -> **Add Connection...**
	- Enter the Server name then select database and click OK. the database will appear under **Data Connections**
	- To see the connection string, check the database's properties.
- You do not need to be able to run the Silverlight application on your local developer machine yet, but it will be useful at a later stage.


## The general principle

The general principle for migrating a Silverlight application to OpenSilver consists of creating an OpenSilver-type project for each of the original Silverlight projects, then copying / pasting all the files from the original projects to the OpenSilver projects, and finally compiling the solution.

In practice however, we do not want to copy/paste the files but rather share them between the original application and the migrated application, so that both applications can be maintained, at least in the short term. This also makes it easier to merge code changes in case that new developments or bug fixes are made on the original Silverlight application while we are still migrating it.

To avoid manual editing of **.sln** and **.csproj** (or **.vbproj** or **.fsproj**) files the following steps can be taken.
- Create an OpenSilver application with the same name as the Silverlight application's but in a different location
- Add an OpenSilver-type project in the OpenSilver solution for every project of the Silverlight solution, using the same names.\
  If the project type is a **Silverlight Class Library** then an **OpenSilver Class Library** needs to be created.

  Now we have the same directory structure as the original Silverlight application's.
- Rename the OpenSilver solution and all its Silverlight related projects by adding **.OpenSilver** at the end
- Remove **.OpenSilver** from the **Assembly name**
  In the Solution Explorer, right-click on the project -> Properties\
  This needs to be done in order to not break the "xmlns" references in XAML files.
- Copy all newly created **.sln** and **.csproj** (or **.vbproj** or **.fsproj**) files to their corresponding location in the Silverlight project's folder.

You can find more detailed instructions in this [example](example.md).

## Errors are expected

Some compilation errors are expected, because OpenSilver currently only supports a (fairly large) subset of Silverlight functionality, so manual work should be expected. This guide provides tips and guidance to fix those compilation errors.

### Errors from .xaml.cs (or .xaml.vb or .xaml.fs) sources
Many different compilation errors related to UI controls can occur if the project contains **.xaml** files (which is common in Silverlight applications). For example, errors while migrating C# code :

![xaml Errors](/images/XamlErrors.png "xaml Errors")

This error is due to the files **.g.i.cs** not being generated. To fix it, click on the **.xaml** file, go to its properties (F4), and in the advanced section choose:
```
Build Action: Content
Custom Tool: MSBuild:Compile
```

![Build Action](/images/BuildAction.png "Build Action")

Please note that if the project includes the **Generic.xaml** file for default styles, then it is likely that the project will be built with no error, but some components will not look like they are expected to. So the above steps need to be followed for both **Generic.xaml** and all other **.xaml** files.

### Make build configurations consistent
Some large projects might have different build configurations with many compiler directives. As a result a lot of confusing errors might be encountered during compilation.
It is a good practice to check the original **Silverlight** project and see if the build configurations are consistent with the **OpenSilver** project.

### Known issues

Visit the "[Troubleshooting](../troubleshooting/common-issues-and-solutions.md)" page for known issues and tips.

## Use compiler directives to not break the original Silverlight application

#### In C# (or VB.NET or F#) files:

Every time we need to make a change to the C# (or VB.NET or F#) code, we are going to use the #if OPENSILVER and #if !OPENSILVER compiler directives to distinguish between the original code and the migrated one. The goal is to be able to quickly find all the places that have been modified, and also to make changes to the files without breaking the original Silverlight application (for side by side comparison).

OpenSilver may not have some required classes or methods implemented yet. A temporary solution for compiling the project would be to add empty classes/methods in the OpenSilver project.

#### In XAML files:

As far as XAML files are concerned, since compiler directives do not work in XAML, we recommend these 2 approaches to make changes to the XAML code without breaking the original Silverlight application:
- If it is a small change, try to do it in the C# (or VB.NET or F#) code-behind file instead of the XAML file. To do so, first add x:Name="SOME_NAME" (in XAML) to the control that you wish to modify, then make the change in the constructor in the C# (or VB.NET or F#) code-behind, and use a #if OPENSILVER compiler directive to make sure that the change does not break the original Silverlight version of the application.
- If the change is extensive or if it cannot be done in the C# (or VB.NET or F#) code-behind, we recommend that you create a copy of the XAML file, and reference that copy in your OpenSilver project, while the original Silverlight application references the original file. For example, if you need to make a change to "App.xaml", you should create a copy named for example "App.OpenSilver.xaml", and have the OpenSilver project  reference that file instead of "App.xaml". Note: the associated C# (or VB.NET or F#) code-behind file ("App.xaml.cs/App.xaml.vb/App.xaml.fs") can still be shared between the original and migrated projects.

#### Here are the steps to copy and use a new XAML file:

#### 1. Copy the name of the XAML file that needs to be duplicated

![Copy XAML file](/images/xaml_copy/1.png "Copy XAML file")

#### 2. Add a new item to the OpenSilver project

In the **Solution Explorer** right-click on the folder where the file is located and choose **Add -> New item ...**

![Add new item](/images/xaml_copy/2.png "Add new item")

#### 3. Choose the item type

Most of the time xaml will represent different Silverlights controls - **UserControl, ChildWindow, Page** etc. In that case we can create the exact alternative OpenSilver provides.

![Choose control type](/images/xaml_copy/3.png "Choose control type")

If it is not the case then we can simply create any of given controls and then rename and modify as needed.\
**Do not forget to change the new control's name to the one you want for your OpenSilver xaml file (here, we picked "MyUserControl.OpenSilver.xaml")**

#### 4. Change the namespace

Visual Studio will automatically generate a namespace which might not be the one we wanted. Copy the namespace from the original .cs (or .vb or .fs) file and replace it in the newly created **.xaml** and **.xaml.cs** (or **.xaml.vb** or **.xaml.fs**) files.\
Here is an example from C#:

![Change namespace](/images/xaml_copy/4.png "Change namespace")
![Change namespace](/images/xaml_copy/5.png "Change namespace")

#### 5. Exclude the original component from the project

We can exclude the original **.xaml** and **.cs**(or **.vb** or **.fs**) files from project because now we have new ones. The steps below show you how to exclude a **.cs** file. The same steps can be followed for a **.vb** or **.fs** file.

![Exclude project](/images/xaml_copy/6.png "Exclude project")

Here is how it looks like afterwards

![Excluded project](/images/xaml_copy/7.png "Excluded project")

To see the files that are not part of the project click **Show All Files** button in **Projects Explorer**

![Show all files](/images/xaml_copy/8.png "Show all files")

Please note that it is also possible to replace only the .xaml file and share the original .cs (or .vb or .fs) file.
For example, in C# migration, we can just exlude (or remove) **.OpenSilver.xaml.cs** from the project and include the original **.xaml.cs**

It will however look odd  because the .xaml.cs file will no longer be shown as a direct child of the .xaml file in the Solution Explorer.

![Share .cs file](/images/xaml_copy/9.png "Share .cs file")

