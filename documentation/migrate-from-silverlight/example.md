# Tutorial: Migration from Silverlight application to OpenSilver

In this tutorial we are going to migrate an example of **Silverlight** application to **OpenSilver**.\
It is assumed that the steps described in [Environment Setup](environment-setup.md) page are followed.

The migration steps described below will allow us to avoid source file duplication and will give a possibility to share them between separate **.sln** and **.csproj** files.\
The basic idea is to create a separate *OpenSilver* project with the same name and directory structure as *Silverlight* has, then by renaming solution and project names we can just copy the files to actual *Silverlight* project location and use them without manual modifications.

### About the Silverlight application
**CustomerApp** is a simple *Silverlight* application which shows basic customer information in a *Data Grid View* and allows to filter the information by customer name or by membership.

Here is how it looks like.

![The OpenSilver Application](/images/CustomerAppOpensilver.png "The OpenSilver Application")

Directory structure of the *Silverlight* application looks like this.

![Silverlight App Directory Structure](/images/SilverlightAppDirectoryStructure.png "Silverlight App Directory Structure")

So it is a typical *Silverlight* application having an additional *Silverlight Class Library* inside (**CustomerData**)

### Migration Steps

#### 1. Create a new OpenSilver project (Visual Studio 2019)
In this step we are going to create a separate *OpenSilver* project having the same name as *Silverlight* application has but in a different location.

- Go to `File -> New -> Project` menu item and choose the `OpenSilver Application`.

![New OpenSilver Application](/images/NewOpenSilverApplication.png "New OpenSilver Application")

- Type **CustomerApp** as a `Project name`
- Check `Place solution and project in the same directory` and click `Create`

![Application Name](/images/ApplicationName.png "Application Name")

#### 2. Add a new *OpenSilver Class Library*
We need to keep our new *OpenSilver* project consistent with the *Silverlight*. Since the project has a *Silverlight Class Library* we are going to create a new *OpenSilver Class Library* with the same name.

- In `Solution Explorer`, right-click on `Solution -> Add -> New project...`
- Choose `OpenSilver Class Library` and click `Next`
- Make sure the end location is **CustomerApp**
- Type **CustomerData** as `Project name` and click `Create`

![Library Name](/images/LibraryName.png "Library Name")

#### 3. Add CustomerData as a Project Reference

- In `Solution Explorer`, right-click on the Dependencies of CustomerApp project `-> Add Project Reference...`
- Choose `CustomerData` as a reference project and click `OK`

![Project Reference](/images/ProjectReference.png "Project Reference")

#### 4. Remove OpenSilver Nuget package and install OpenSilver.WorkInProgress one
As it was mentioned on the [Compile with OpenSilver](compile-with-opensilver.md) page for best compatibility we are going to use **OpenSilver.WorkInProgress** package instead of **OpenSilver**.

- Uninstall **OpenSilver** Nuget package
```
Uninstall-Package OpenSilver -Project CustomerApp
Uninstall-Package OpenSilver -Project CustomerApp.Browser
Uninstall-Package OpenSilver -Project CustomerData
```

- Install **OpenSilver.WorkInProgress** Nuget package
```
Install-Package OpenSilver.WorkInProgress -Version 1.0.0-alpha-018 -Project CustomerApp
Install-Package OpenSilver.WorkInProgress -Version 1.0.0-alpha-018 -Project CustomerApp.Browser
Install-Package OpenSilver.WorkInProgress -Version 1.0.0-alpha-018 -Project CustomerData
```

#### 5. Rename OpenSilver projects

- Rename `CustomerApp` to `CustomerApp.OpenSilver`
- Rename `CustomerData` to `CustomerData.OpenSilver`
- Rename `CustomerApp` Solution to `CustomerApp.OpenSilver`

![Rename](/images/Rename.png "Rename")

Now we can close **Visual Studio 2019**.

#### 6. Copy files and directories from OpenSilver project to Silverlight project

- Copy **CustomerApp.Browser**, **CustomerApp.Simulator** folders and **CustomerApp.OpenSilver.sln** file to Silverlight's **root** directory
- Copy **CustomerData.OpenSilver.csproj** and **CustomerApp.OpenSilver.csproj** files to Silverlight's according projects directory

![Directory Structure](/images/DirectoryStructure.png "Directory Structure")

#### 7. Compile migrated project with Visual Studio 2019

- Open **CustomerApp.OpenSilver.sln** located in Silverlight's project with *Visual Studio 2019*
- In `Solution Explorer` find `AssemblyInfo.cs` for both CustomerApp and CustomerData projects and exclude them

![Exclude From Project](/images/ExcludeFromProject.png "Exclude From Project")

- Make **CustomerApp.Browser** as a Startup Project

![Set Startup Project](/images/SetStartupProject.png "Set Startup Project")

- Rebuild the solution and just run it

The result should look like this

![Result](/images/Result.png "Result")

And if we click on 'Show Members' checkbox we will see filtered result

![Result With Filter](/images/ResultWithFilter.png "Result With Filter")

Please note that in the example we didn't change a single line of Silverlight's code and we didn't fix any compilation errors. In real and much bigger projects however some errors expected to happen.