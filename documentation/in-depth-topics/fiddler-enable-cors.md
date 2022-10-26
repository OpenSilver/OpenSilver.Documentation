# How to use Fiddler to test with servers that do not authorize cross site requests

## Tutorial
1. Install Fiddler2
2. Go to Rules > Customize Rules... or similar
3. Add the following code near the top, next to the other RulesOption declarations:
```
public static RulesOption("Force CORS")

var m_ForceCORS: boolean = true;
```

4. Add the following code at the end of the OnBeforeRequest method:
```
// If it's an OPTIONS request, fake the response and return w/e the client expects.

if (m_ForceCORS && oSession.oRequest.headers.HTTPMethod == "OPTIONS") {

     oSession.utilCreateResponseAndBypassServer();

     oSession.oResponse.headers.Add("Access-Control-Allow-Origin", oSession.oRequest.headers["Origin"]);

     oSession.oResponse.headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

     oSession.oResponse.headers.Add("Access-Control-Allow-Headers", "Content-Type, SOAPAction, Authorization, Accept, Csrf-Token, X-Requested-With, cloudSession, WbeSession, Cookie");

     oSession.oResponse.headers.Add("Access-Control-Max-Age", "1728000");

     oSession.oResponse.headers.Add("Access-Control-Allow-Credentials", "true");

     oSession.responseCode = 200;

}
```
5. Add the following code at the end of the OnBeforeResponse method:
```
 // Also add the headers to any real response with an "Origin:" header set

if (m_ForceCORS && oSession.oRequest.headers.Exists("Origin")) {

     oSession.oResponse.headers.Remove("Access-Control-Allow-Origin");
     oSession.oResponse.headers.Add("Access-Control-Allow-Origin", oSession.oRequest.headers["Origin"]) ;

     oSession.oResponse.headers.Remove("Access-Control-Allow-Methods");
     oSession.oResponse.headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

     oSession.oResponse.headers.Remove("Access-Control-Allow-Headers");
     oSession.oResponse.headers.Add("Access-Control-Allow-Headers", "Content-Type, SOAPAction, Authorization, Accept, Csrf-Token, X-Requested-With, cloudSession, WbeSession, Cookie");

     oSession.oResponse.headers.Remove("Access-Control-Max-Age");
     oSession.oResponse.headers.Add("Access-Control-Max-Age", "1728000");

     oSession.oResponse.headers.Remove("Access-Control-Allow-Credentials");
     oSession.oResponse.headers.Add("Access-Control-Allow-Credentials", "true");

}
```
6. With Fiddler running, go to Rules and check the new menu entry "Force CORS".

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
