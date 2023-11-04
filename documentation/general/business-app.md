## Creating a Business Application with RIA Services

RIA Services (Rich Internet Application Services) is a powerful framework that simplifies the development of data-driven applications for the web. With RIA Services, you can easily create business applications that offer rich user experiences and efficient data access. This documentation will guide you through the process of creating a business application using OpenRIA Services (the evolved Open Source version of WCF RIA Services) using OpenSilver.

OpenSilver includes a project template that lets you leverage Open RIA Services to easily create complex client/server business applications.

### Table of Contents

[Getting Started](#getting-started)

[Tutorial](#tutorial)

[Source Code](#source-code)

[Troubleshooting and Known Issues](#troubleshooting-and-known-issues)

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

4. Select the "ADO.NET Entity Data Model" template. Change the name to "AdventureWorks", and then click Add.
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
    * Edit it like this:
    ```
    - Replace any double quotes (") with &quot;
    - Replace 'data source' with 'Server'
    ```
    For example, replace the following ConnectionString:
    ```
    metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string="data source=LAPTOP-JJKBBSSK;initial catalog=AdventureWorksLT2019;trusted_connection=true";
    ```
    with this one:
    ```
    metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=LAPTOP-JJKBBSSK;initial catalog=AdventureWorksLT2019;trusted_connection=true&quot;
    ```
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
        <add name="AdventureWorksLT2022Entities" connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=LAPTOP-CACBBSGA;initial catalog=AdventureWorksLT2022;trusted_connection=true&quot;" providerName="System.Data.EntityClient" />
    </connectionStrings>
    ```
        
   Screenshot:  
   ![image](https://github.com/OpenSilver/OpenSilver.Documentation/assets/8248552/bb8a1ab0-0945-4acd-b4ef-4b2a16ad9bb6)

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
* If the Service class on the server-side inherits from `DbDomainService<T>` (which is the case if you used the "Add New Domain Service Class", on the client side), then the Domain `Service` class will have the same name as on the server-side. So the generated client-side `OrganizationService` class will have the same name as the server-side `OrganizationService` class.
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

#### Issues at design-time:
* Make sure to run both the .Web project (for the server side) and .Browser project (for the client side)
* If you have an error that says "**No connection string named '(...)' could be found in the application config file.**", make sure that you have added an entry to the Web.Config file as explained at [this step](#database-connection).
* If your application doesn't run or has some runtime errors, open the browser console (F12) and look at the errors listed there
* If you see an error related to CORS (such as "**Access to fetch at 'http://localhost:54837/...' from origin 'http://localhost:55591' has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource.**"), then [configure CORS on the server-side](https://doc.opensilver.net/documentation/in-depth-topics/wcf-and-webclient.html#adding-support-for-cross-domain-calls-cors), or work around CORS entirely by [running your browser with disabled security](https://simplelocalize.io/blog/posts/what-is-cors/#3-disable-browser-cors-checks)
* If you get some other "Error 500" on the server side, make sure that the ".Web" project is running under the "x64" platform target.
* If you receive `Unable to load DLL 'SQLite.Interop.dll': The specified module could not be found. (Exception from HRESULT: 0x8007007E)` error, this is a known issue where SQLite sometimes does not generate x86/x64 folders inside of the bin folder of the .Web project. Most of the times you just need to Clean and then Build the project until these folders are generated. You can also try stopping IIS Express from the system tray, and restarting the whole IIS by running the following commands in a Command Line with Administrator Privileges: `net stop winnat` and then `net start winnat`
![SQLite Interop Error](/images/ria-business14.png)
* If you don't see the "Add New Domain Service Class" dialog, make sure to install the latest VSIX of OpenSilver.
* If the "Add New Domain Service Class" dialog is empty, make sure to rebuild the solution, and try again.

### See Also
* [OpenSilver Information about Open RIA Services](https://doc.opensilver.net/documentation/3rd-party-libraries/ria-services.html)  
* [OpenSilver SampleCRM Business Demo Application](https://github.com/OpenSilver/SampleCRM)
* [Microsoft original documentation for WCF RIA Services](https://learn.microsoft.com/en-us/previous-versions/dotnet/wcf-ria/ee707344(v=vs.91))
* [Open RIA Services Wiki](https://github.com/OpenRIAServices/OpenRiaServices/wiki)
* [Open RIA Services Documentation](https://openriaservices.gitbook.io/openriaservices/)
