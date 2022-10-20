# Circumventing cross-domain policies using JSONP

## Introduction

Until the introduction of [Cross-Origin Resource Sharing (CORS)](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing) (see also: [Adding support for cross-domain calls (CORS) in a client/server OpenSilver solution)](wcf-and-webclient.md#adding-support-for-cross-domain-calls-cors), web service calls from the browser were limited to accessing resources and services that were hosted on the same domain as the webpage requesting them. CORS is now supported in most modern browsers and became a W3C standard in Janaury 2014 but it also needs to be enabled on the server of the service.

Older web services (such as [Bing Maps REST API](https://blogs.bing.com/maps/2015/03/05/accessing-the-bing-maps-rest-services-from-various-javascript-frameworks)), which were released before CORS, used a different technique to make cross-domain calls to services. This technique is named JSONP and circumvents the Same Origin Policy by taking advantage of the fact that web browsers do not enforce the same-origin policy on script tags.

Here is how it works: when using JSONP, we usually add a parameter to the request URL, such as &jsonp=myCallback. Then, we append a script tag to the body of the webpage using this URL. This will cause the browser to call the service thinking that it is loading a JavaScript file. The response is a JSON object wrapped with some code to a function called myCallback, which gets executed when the script is downloaded.

In other words, instead of returning simple JSON (for example: {"id":"mydomelementid","message": "Yeah!"}), the JSONP request returns a JSON-formatted object wrapped inside a function call (for example: myCallback({"id":"mydomelementid","message": "Yeah!"}))

## How to make JSONP calls using OpenSilver:
Normally, to make JSON calls (not JSONP calls) you use the standard WebClient control (see: [Tutorial to create a REST-based client/server app in OpenSilver (Web API))](wcf-and-webclient.md#tutorial-to-easily-create-a-rest-based-clientserver-app-in-opensilver-wep-api).
However, the standard WebClient control won't work with JSONP because:

It makes the call using an XMLHttpRequest object instead of adding a script tag to the page as described above.
It is unable to provide the callback needed by the JSONP result.
Therefore, we provide below a "WebClientJSONP" class that is able to make JSONP calls.

To use it, simply add a new file named "WebClientJSONP.cs" to your OpenSilver project, and copy/paste the following code:
```
using OpenSilver;
using System;
using System.ComponentModel;

namespace JSONP.Example;

public class WebClientJSONP
{
    public event OpenReadCompletedEventHandler OpenReadCompleted;

    public void OpenReadAsync(Uri address)
    {
        Action<object> callback = (result) =>
        {
            if (OpenReadCompleted != null)
            {
                string json = Interop.ExecuteJavaScript("JSON.stringify($0)", result).ToString();
                OpenReadCompleted(this, new OpenReadCompletedEventArgs(json, null, false, null));
            }
        };

        string url = address.ToString();

        // Generate a random name for the JSONP callback method:
        Random rd = new Random();
        int id = rd.Next(100000,999999);
        string callbackMethodName = "callback" + id;

        // Define the JSON callback in JavaScript and call the REST JSONP service:
        Interop.ExecuteJavaScript(@"
            // Make the callback globally accessible so that it can be called by the result of JSONP:
            window[$1] = function(result) { $2(result); }

            // Call the REST JSONP service:
            var script = document.createElement('script');
            script.setAttribute('type', 'text/javascript');
            script.setAttribute('src', $0 + ""&jsonp="" + $1);
            document.body.appendChild(script);
        ", url, callbackMethodName, callback);
    }
}

public delegate void OpenReadCompletedEventHandler(object sender, OpenReadCompletedEventArgs e);

public class OpenReadCompletedEventArgs : AsyncCompletedEventArgs
{
    public OpenReadCompletedEventArgs(string result, Exception error, bool cancelled, object userState)
        : base(error, cancelled, userState)
    {
        this.Result = result;
    }

    public string Result { get; private set; }
}
```

Then, you can call the class by using this code:
```
void MakeTheCall()
{
    WebClientJSONP wc = new WebClientJSONP();
    wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
    wc.OpenReadAsync("http://yourwebservice/specify-your-jsonp-endpoint-here");
}

void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
{
    if (e.Error == null)
    {
        string json = e.Result;
        // Process your json response here.
    }
}
 ```

## Example: the Bing Maps REST API
The following example demonstrates how you can retrieve the latitude and longitude coordinates or any postal address, as well as the country name, using the Bing Maps REST API.

Requirements:

* You need to obtain an application Key from Microsoft in order to use the service.
* You need to define the class "WebClientJSONP" as explained in the paragraph above.
* Use Newtonsoft.Json for the deserialization (https://www.nuget.org/packages/Newtonsoft.Json/).

Then, you can use the following code to make the call and process the response:
```
void Main()
{
    string key = "ENTER YOUR BING MAPS REST API APPLICATION KEY HERE";
    string query = "1 Microsoft Way, Redmond, WA";

    Uri geocodeRequest = new Uri($"http://dev.virtualearth.net/REST/v1/Locations?q={query}&key={key}");

    WebClientJSONP wc = new WebClientJSONP();
    wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
    wc.OpenReadAsync(geocodeRequest);
}

void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
{
    if (e.Error == null)
    {
        dynamic reqRes = JsonConvert.DeserializeObject(e.Result);
        if (reqRes["resourceSets"] is JArray
            && ((JArray)reqRes["resourceSets"]).Count > 0
            && reqRes["resourceSets"][0]["resources"] is JArray
            && ((JArray)reqRes["resourceSets"][0]["resources"]).Count > 0)
        {
            double x = reqRes["resourceSets"][0]["resources"][0]["point"]["coordinates"][0].Value;
            double y = reqRes["resourceSets"][0]["resources"][0]["point"]["coordinates"][1].Value;
            string country = reqRes["resourceSets"][0]["resources"][0]["address"]["countryRegion"].Value;
            MessageBox.Show("Coordinates: " + x.ToString() + "," + y.ToString() + Environment.NewLine + "Country: " + country);
        }
        else
            MessageBox.Show("No Results found");
    }
}
```

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
