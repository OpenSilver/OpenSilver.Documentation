## Creating a Business Application with RIA Services

RIA Services (Rich Internet Application Services) is a powerful framework that simplifies the development of data-driven applications for the web. With RIA Services, you can easily create business applications that offer rich user experiences and efficient data access. This documentation will guide you through the process of creating a business application using OpenRIA Services (the evolved Open Source version of WCF RIA Services) using OpenSilver.

OpenSilver includes a project template that lets you leverage Open RIA Services to easily create complex client/server business applications.

### Table of Contents

[Getting Started](#getting-started)

[Tutorial](#tutorial)

[Source Code](#source-code)

[Troubleshooting and Known Issues](#troubleshooting-and-known-issues)

[How to replace SQLite with SQL Server for Authentication/Membership/Roles](#user-content-how-to-replace-sqlite-with-sql-server-for-authenticationmembershiproles)

[See Also](#see-also)

### Getting Started
You will need Visual Studio 2022 (or newer). Install the OpenSilver VSIX Templates from [here](https://opensilver.net/download.aspx). These templates include the OpenSilver Business Application Template. 

### Tutorial

You can see a quick video demo from this tutorial here:
> [!Video https://www.youtube.com/embed/ZyctFzWKda8?start=345&end=462]

#### Detailed Steps

1. Configure the Database  
For this tutorial, we will use the AdventureWorks Lightweight database (AdventureWorksLT2022.bak). You can see instructions on how to download, restore, and setup here:
[https://learn.microsoft.com/en-us/sql/samples/adventureworks-install-configure?view=sql-server-ver16&tabs=ssms](https://learn.microsoft.com/en-us/sql/samples/adventureworks-install-configure?view=sql-server-ver16&tabs=ssms)

2. Create a new "OpenSilver Business Application Project"
![OpenSilver Ria Services Business Application](/images/ria-business01.png)

3. In Visual Studios's Solution Explorer, right-click the .Web project, click Add, and then click "New Item".

4. Select the "ADO.NET Entity Data Model" template.

**Change the name to ```AdventureWorks```** (instead of "Model1"), and then click Add.

![ADO.NET Entity Data Model](/images/ria-business02.png)

5. On the "Choose Model Contents" page, click "EF Designer from database", and then click Next.
![EF Designer from database](/images/ria-business03.png)

6. <a name="database-connection"></a>On the "Choose Your Data Connection" page, create a connection to the "AdventureWorks" database.
![Connection Properties](/images/ria-business04.png)

    * IMPORTANT: Copy the ConnectionString that is shown in the dialog, and save it somewhere (it will be useful in a later step), then click Next:  
    ![Choose Your Data Connection](/images/ria-business05.png)  

7. On the "Choose Your Database Objects and Settings" page, expand the "Tables" node, select everything from the "SalesLT" schema, and click "Finish"

![Choose Your Database Objects](/images/ria-business06.png)

8. The database is configured in the .Web project and the entity data model appears in the designer (Build the solution).
![Data Model](/images/ria-business07.png)

9. Make sure that the ConnectionString is properly configured in the Web.Config file. To do so:
    * Take the connection string that you copied in one of the previous steps.
    * Replace any double quotes (") with ```&quot;```

    For instance, replace the following example connection string:
    ```
    metadata=res://*/AdventureWorks.csdl|res://*/AdventureWorks.ssdl|res://*/AdventureWorks.msl;provider=System.Data.SqlClient;provider connection string="data source=localhost;initial catalog=AdventureWorksLT2019;trusted_connection=true";
    ```
    with this one:
    ```
    metadata=res://*/AdventureWorks.csdl|res://*/AdventureWorks.ssdl|res://*/AdventureWorks.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;initial catalog=AdventureWorksLT2019;trusted_connection=true&quot;
    ```
    (Note that the connection string above is just an example. You may need to adapt it based on your configuration. Refer to the "Choose Your Data Connection" dialog shown before to know what your connection string should look like.)
    
    * Go to Web.config file, and add this new entry into the \<connectionStrings\> section, using the updated connection string from the previous step:
    ```xml
    <add name="AdventureWorksLT2022Entities" 
         connectionString="(paste the edited connectionString here)" 
         providerName="System.Data.EntityClient" />
    ```
    Make sure that the "name" property is correct and that the connection string contains the password.

    Here is an example of valid setting in the Web.Config:

    ```xml
    <connectionStrings>
        <add name="DefaultConnection" connectionString="Data Source=|DataDirectory|database\OpenSilverBusinessApplication1.db;Version=3;New=True;Compress=True;" providerName="System.Data.EntityClient"/>
        <add name="AdventureWorksLT2022Entities" connectionString="metadata=res://*/AdventureWorks.csdl|res://*/AdventureWorks.ssdl|res://*/AdventureWorks.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;initial catalog=AdventureWorksLT2022;trusted_connection=true&quot;" providerName="System.Data.EntityClient" />
    </connectionStrings>
    ```
    (Note that the connection string above is just an example. You may need to adapt it based on your configuration.)

10. Now that we configured the database, let's proceed to the Open RIA configuration. In Solution Explorer, right-click the .Web project, click Add, and then click "New Item", select the "Domain Service" template. Name the new item "OrganizationService".

Note: if you don't see the "Domain Service" item in the "Add New Item" dialog, make sure that you have downloaded the latest OpenSilver Visual Studio Extension (VSIX).

![Add New Domain Item](/images/ria-business08.png)

11. In the `Add New Domain Service Class` dialog box, select Customers from the Entities list.

Note: if the list is empty, please rebuild the solution, and open the dialog again.

Ensure that the "Enable client access" and "Generate associated classes for metadata" check boxes are selected. Click Ok.  
![Add New Domain Service](/images/ria-business09.png)

12. Build the solution.  
Building the solution generates the Domain Context and entities in the client project.  

For your information, in terms of naming, there are 2 cases:
* If the Service class on the server-side inherits from `DbDomainService<T>` (which is the case if you used the "Add New Domain Service Class", on the server side), then the Domain `Service` class will have the same name as on the server-side. So the generated client-side `OrganizationService` class will have the same name as the server-side `OrganizationService` class.
* If the Service classe on the server-side inherits from `DomainService`, then on the client side, the Domain `Service` class will have the suffix `Context`. So the `OrganizationService` becomes `OrganizationContext`.  
![Services and Contexts](/images/ria-business10.png)

13. Open file `Views\Home.xaml`, and include this code inside of the StackPanel:
```xml
<DataGrid x:Name="dataGridCustomers"
          Height="200"/>

<DataForm CurrentItem="{Binding ElementName=dataGridCustomers, Path=SelectedItem}" 
          Height="200"/>
``` 
The result will be like this:
![Xaml Code](/images/ria-business11.png)

13. And in `Views\Home.xaml.cs`, include this private field:
```c#
private readonly OrganizationService _context = new OrganizationService();
```
or the following one if you are in the second case described above:
```c#
private readonly OrganizationContext _context = new OrganizationContext();
```        
and after `InitializeComponent()` call, add this:
```c#
dataGridCustomers.ItemsSource = _context.Customers;
_context.Load(_context.GetCustomersQuery());
```
The result will be like this:  
![C# Code](/images/ria-business12.png)

14. Execute the ".Web" project, then execute the ".Browser" project and check the results:
![Final](/images/ria-business13.png)

### Source Code

You can get the complete source code for that article [here](https://github.com/OpenSilver/OpenSilver.Documentation/tree/master/examples/OpenSilverBusinessApplicationDemo).

### Troubleshooting and Known Issues

#### Issues that you may encounter at runtime:
* Make sure to **run both the .Web project** (for the server side) and **.Browser project** (for the client side).
* If you have an error that says "**No connection string named '(...)' could be found in the application config file.**", make sure that you have added an entry to the Web.Config file as explained at [this step](#database-connection).
* If your application doesn't run or has some runtime errors, **open the browser console (F12)** and look at the errors listed there.
* If you see an error related to CORS (such as "**Access to fetch at 'http://localhost:54837/...' from origin 'http://localhost:55591' has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource.**"), then [configure CORS on the server-side](https://doc.opensilver.net/documentation/in-depth-topics/wcf-and-webclient.html#adding-support-for-cross-domain-calls-cors), or work around CORS entirely by [running your browser with disabled security](https://simplelocalize.io/blog/posts/what-is-cors/#3-disable-browser-cors-checks)
* If you receive the error "**Unable to load DLL 'SQLite.Interop.dll'**: The specified module could not be found. (Exception from HRESULT: 0x8007007E)", this is a known issue where SQLite sometimes does not generate x86/x64 folders inside of the bin folder of the .Web project. Most of the times you just need to Clean and then Build the project until these folders are generated. You can also try stopping IIS Express from the system tray, and restarting the whole IIS by running the following commands in a Command Line with Administrator Privileges: `net stop winnat` and then `net start winnat`
![SQLite Interop Error](/images/ria-business14.png)
* If you get some other "**Error 500**" on the server side, make sure that the ".Web" project is running under the "x64" platform target.
* If you get the other "**Unable to load the specified metadata resource**", it indicates that the Connection String in the Web.Config file is incorrect. Specifically, the "metadata=" part should have a different value. Make sure that you have properly copied the string that was shown when creating the EDMX file (see screenshot below). Note: if you have placed your EDMX file inside a subfolder of the .Web project, you'll need to specify the folder name there too. For example, if your file "AdventureWorks.edmx" is located inside a folder named "MyFolder", your connection string should be like this: metadata=res://*/MyFolder.AdventureWorks.csdl|res://*/MyFolder.AdventureWorks.ssdl|res://*/MyFolder.AdventureWorks.msl;   (...)
![image](https://github.com/OpenSilver/OpenSilver.Documentation/assets/8248552/4a5f5714-22da-44e1-b5bc-4c1704648919)

#### Issues that you may encounter at design-time:
* If you don't see the "Add New Domain Service Class" dialog, make sure to install the latest VSIX of OpenSilver.
* If the "Add New Domain Service Class" dialog is empty, make sure to rebuild the solution, and try again.


### How to replace SQLite with SQL Server for Authentication/Membership/Roles:

The `Business Application` project template comes with built-in Authentication/Membership/Roles management using SQLite. You can replace SQLite with SQL Server or Azure SQL by creating a database that contains the default ASP.NET Membership tables and stored procedures (using Schema version 1) in your instance of SQL Server or Azure SQL, and then changing the file `Web.Config` (located inside the project with the `.Web` suffix) to make the Connection String point to the newly created database. Be sure to also update the sections `<Membership>` and `<Role>` in `Web.Config`. Read below for details:

##### - To properly configure Web.Config:

In the `<ConnectionString>` section, replace the SQLite connection string with the one for SQL Server or Azure SQL. This is what it usually looks like:
```xml
<connectionStrings>
	<add name="AspNetMembershipDBConnection" connectionString="data source=<SERVER_URL_GOES_HERE>;initial catalog=<DATABASE_NAME_GOES_HERE>;persist security info=True;user id=<USER_ID_GOES_HERE>;password=<USER_PASSWORD_GOES_HERE>;MultipleActiveResultSets=True" providerName="System.Data.EntityClient" />
</connectionStrings>
```

Then, delete the existing `<membership>` and `<roleManager>` sections, and replace them with the following ones:
```xml
	<membership defaultProvider="AspNetSqlMembershipProvider">
		<providers>
			<clear />
			<add name="AspNetSqlMembershipProvider"
				type="System.Web.Security.SqlMembershipProvider"
				connectionStringName="AspNetMembershipDBConnection"
				enablePasswordRetrieval="false"
				enablePasswordReset="true"
				requiresQuestionAndAnswer="false"
				requiresUniqueEmail="false"
				maxInvalidPasswordAttempts="5"
				minRequiredPasswordLength="6"
				minRequiredNonalphanumericCharacters="0"
				passwordAttemptWindow="10"
				applicationName="/"/>
		</providers>
	</membership>
	<roleManager enabled="true" defaultProvider="AspNetSqlRoleProvider">
		<providers>
			<clear />
			<add name="AspNetWindowsTokenRoleProvider" type="System.Web.Security.WindowsTokenRoleProvider" applicationName="/" />
			<add name="AspNetSqlRoleProvider"
				type="System.Web.Security.SqlRoleProvider"
				connectionStringName="AspNetMembershipDBConnection"
				applicationName="/" />
		</providers>
	</roleManager>
```

##### - To create the ASP.NET Membership database (with Schema version 1) in your instance of SQL Server:

1. Navigate to the following folder on your disk: `C:\Windows\Microsoft.NET\Framework\v2.0.50727`
2. Execute the file `aspnet_regsql.exe`
![image](https://github.com/OpenSilver/OpenSilver.Documentation/assets/8248552/71cc5e25-9b58-4f82-b1bb-94b202b7dfd4)
3. Follow the wizard to connect to your SQL Server database and click Next until you are done creating the tables and stored procedures
![image](https://github.com/OpenSilver/OpenSilver.Documentation/assets/8248552/3614342a-e2c3-420a-93cc-0345c261136f)

##### - To create the ASP.NET Membership database (with Schema version 1) in Azure SQL:

Since the file `aspnet_regsql.exe` won't work with Azure SQL, a working solution consists in the following:
1. Start by using a local SQL Server instance such as one created with `SQL Server Developer Edition` or just any other one, and follow the instructions above to create the ASP.NET Membership database there.
2. Then, export that database (schema+data) to a format that can be re-imported in Azure SQL. To do so, you can use `SQL Server Management Studio` (SSMS): right-click on the database that you created at the previous step, choose `Tasks` -> `Generate Scripts`:
![image](https://github.com/OpenSilver/OpenSilver.Documentation/assets/8248552/0888ddbd-7e57-4e4b-8630-7d4fad206bf1)
Follow the wizard, specifying that you want to export everything. Make sure to click "Advanced" and choose `Microsoft Azure SQL Database` under `Script for the database engine type`:
![image](https://github.com/OpenSilver/OpenSilver.Documentation/assets/8248552/c8d2a4c4-53f2-4499-8664-2bbff9c06dea)
Also, be sure to choose `Schema and data` under `Types of data to script`:
![image](https://github.com/OpenSilver/OpenSilver.Documentation/assets/8248552/3638b9a7-5468-43cb-9a3c-c5a1e071a08d)
3. Create a new database on Azure SQL and run the SQL script that you exported at the previous step. To do so, you can use `SQL Server Management Studio` (SSMS): start by connecting to Azure DB, then right-click to create a new database, give it the same name as the locally created one, then right-click the new database, click "Run Query..." and run the SQL script that you created at the previous step.

### See Also
* [OpenSilver Information about Open RIA Services](https://doc.opensilver.net/documentation/3rd-party-libraries/ria-services.html)  
* [OpenSilver SampleCRM Business Demo Application](https://github.com/OpenSilver/SampleCRM)
* [Microsoft original documentation for WCF RIA Services](https://learn.microsoft.com/en-us/previous-versions/dotnet/wcf-ria/ee707344(v=vs.91))
* [Open RIA Services Wiki](https://github.com/OpenRIAServices/OpenRiaServices/wiki)
* [Open RIA Services Documentation](https://openriaservices.gitbook.io/openriaservices/)
