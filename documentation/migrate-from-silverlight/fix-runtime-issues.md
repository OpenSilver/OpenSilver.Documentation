# Fixing runtime issues

**Note: this section assumes that you would like to do the migration on your own. Alternatively, you can have your application migrated by Userware - the company behind OpenSilver - in a fast and cost-effective way, so that your resources are free to work on other tasks. Visit [OpenSilver.net](https://opensilver.net) for details.**

### Fixing cookie-related issues

In a process of client/server communication there can be a situations when authentication cookies not sent to the server in subseqent requests by client and as a result server rejects to send data back.\
One of the reasons can be that client and server are running on different ports.


Here is the solution.

#### 1. Add the following line in App.xaml.cs. (Client).

```Application.Current.Host.Settings.DefaultSoapCredentialsMode = System.Net.CredentialsMode.Enabled;```

#### 2. Add the following lines in Global.asax.cs in Application_BeginRequest function

```
HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:55591");
HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
```

55591 is assumed to be a port which client uses

[Here](https://doc.opensilver.net/documentation/in-depth-topics/wcf-and-webclient.html#to-add-cors-to-your-web-service-recommended-simply-follow-these-steps) is how to add Global.asax and CORS.

## (WORK IN PROGRESS DOCUMENTATION, CHECK BACK SOON)