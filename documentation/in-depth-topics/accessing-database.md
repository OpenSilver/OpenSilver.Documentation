# Accessing a Database
## Overview

For security reasons, accessing a database is always done on the server-side, not the client-side. In fact, the "Connection String", which includes the URL and password of the database, should not be visible to the end-user.

The client-side (ie. the OpenSilver-based front-end) communicates with the server (for example an ASP.NET / WCF Service application) via one of the following:

* [SOAP](wcf-and-webclient.md)
* [REST](wcf-and-webclient.md)
* [WebSockets](http://forums.cshtml5.com/viewtopic.php?f=7&t=276)
* [SignalR](http://forums.cshtml5.com/viewtopic.php?f=7&t=8121)


## Example
You can download a sample client/server application from the following URL:

https://github.com/cshtml5/TestCshtml5WCF

> :warning: **Note**: The sample is made with [CSHTML5](http://cshtml5.com) rather than OpenSilver, but the concepts are the same.

The sample demonstrates how to pass entities between the client and the server.

Although there is no database in the sample (instead, there is static dictionary on the server-side), database access can be easily added by modifying the file "Service1.svc.cs" located in the "WcfService1" project.

Let's assume that you have an SqlServer database which contains a table named "ToDoItemsTable" that has the following columns:

* Description (type: varchar(max))
* ID (type: uniqueidentifier)
* CreationDate (type: date)

To retrieve the list of ToDo items from the database instead of the static dictionary, open the file "Service1.svc.cs" and replace the following code:
```
private static Dictionary<Guid, ToDoItem> _todos = new Dictionary<Guid, ToDoItem>();

public List<ToDoItem> GetToDos()
{
    return _todos.Values.ToList();
}
```
with this one:
```
public List<ToDoItem> GetToDos()
{
    List<ToDoItem> result = new List<ToDoItem>();

    string connectionString = "HERE YOU MUST PUT THE STRING THAT ALLOWS TO CONNECT TO THE DATABASE";

    // In a using statement, acquire the SqlConnection as a resource.
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        // Open the SqlConnection.
        connection.Open();

        // The following code uses an SqlCommand based on the SqlConnection.
        using (SqlCommand command = new SqlCommand("SELECT Description, ID, CreationDate FROM ToDoItemsTable", connection))
        using (SqlDataReader reader = command.ExecuteReader())
        {
            // Traverse each row of the table:
            while (reader.Read())
            {
                // Read the values of the "Description", "ID", and "CreationDate" columns:
                string todoDescription = reader.GetString(0);
                Guid todoId = reader.GetGuid(1);
                DateTime todoCreationDate = reader.GetDateTime(2);

                // Create a ToDo item instance that will be sent to the OpenSilver client-side application:
                var todo = new ServiceReference2.ToDoItem()
                {
                    Description = todoDescription,
                    Id = todoId,
                    CreationDate = todoCreationDate
                };
            }
        }
    }

    return result;
}
```

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
