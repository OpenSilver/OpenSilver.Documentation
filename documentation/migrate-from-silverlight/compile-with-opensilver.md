# Compile with OpenSilver

**Note: this section assumes that you would like to do the migration on your own. Alternatively, you can have your application migrated by Userware - the company behind OpenSilver - in a fast and cost-effective way, so that your resources are free to work on other tasks. Visit [OpenSilver.net](https://opensilver.net) for details.**

## Pre-requisites

Before starting to migrate to OpenSilver:
- Follow the steps described on the "[Environment Setup](environment-setup.md)" page
- Make sure that you have the full source code of the legacy Silverlight application tthat you wish to migrate to OpenSilver
- **Make sure that the legacy Silverlight application fully compiles on your developer machine**. All the Silverlight projects should be loading and compiling properly, and all the referenced libraries should be properly resolved. Refer to the "[Environment Setup](environment-setup.md)" page for the list of software that you may need to install to get the Silverlight application to compile.
- At this stage you do not need to be able to run the Silverlight application on your local developer machine yet, but this will be useful at a later stage.


## The general principle

The general principle for migrating a Silverlight application to OpenSilver consists of creating an OpenSilver-type project for each of the original Silverlight projects, then copying / pasting all the files from the original projects to the OpenSilver projects, and finally compiling the solution.

In practice however, we do not want to copy/paste the files but rather share them between the original application and the migrated application, so that both applications can be maintained, at least in the short term. This also makes it easier to merge code changes in case that new developments or bug fixes are made on the original Silverlight application while we are still migrating it.

To avoid manual editing of **.sln** and **.csproj** files the following steps can be taken.
- Create an OpenSilver application with the same name as Silverlight application has but in different location
- Add all other Silverlight-type projects the solution has using according names.\
  If the project type is a **Silverlight Class Library** then **OpenSilver Class Library** needs to be created.
  
  Now we have the same directory structure as original Silverlight application has.
- Rename solution and all Silverlight related projects adding **.OpenSilver** at the end
- Copy all **.sln** and **.csproj** files to according Silverlight project locations

You can find more detailed instructions in this [example](example.md).

## Errors are expected

Many compilation errors are expected, because OpenSilver currently only supports a subset of Silverlight functionality, so manual work should be expected. This guide provides tips and guidance to fix those compilation errors.

## Use the "work in progress" version of the OpenSilver NuGet package

By default when you create a new OpenSilver project, it will reference the "OpenSilver" package. For best compatibility, you should replace it with the alternative "OpenSilver.WorkInProgress" package (available on [NuGet.org](https://www.nuget.org/packages/OpenSilver.WorkInProgress/)).

## Use compiler directives to not break the original Silverlight application

#### In C# files:

Every time that we need to make a change to the C# code, we are going to use the #if OPENSILVER and #if !OPENSILVER compiler directives to distinguish between the original code and the migrated one. The goes is to be able to quickly find all the places that have been modified, and also to make changes to the files without breaking the original Silverlight application. (Read above to understand why we want to avoid breaking the original Silverlight application)

#### In XAML files:

As far as XAML files are concerned, since compiler directives do not work in XAML, we recommend these 2 approaches to make changes to the XAML code without breaking the original Silverlight application:
- If it is a small change, try to do it in the C# code-behind file instead of the XAML file. To do so, first add x:Name="SOME_NAME" (in XAML) to the control that you wish to modify, then make the change in the constructor in the C# code-behind, and use #if OPENSILVER compiler directive to make sure that the change does not break the original Silverlight version of the application.
- If the change is extensive or if it cannot be done in the C# code-behind, we recommend you to create a copy of the XAML file, and reference that copy in your OpenSilver project, while the original Silverlight application references the original file. For example, if you need to make a change to "App.xaml", you should create a copy named for example "App.OpenSilver.xaml", and have the OpenSilver project  reference that file instead of "App.xaml". Note: the associated C# code-behind file ("App.xaml.cs") can still be shared between the original and migrated projects.

## (WORK IN PROGRESS DOCUMENTATION, CHECK BACK SOON)