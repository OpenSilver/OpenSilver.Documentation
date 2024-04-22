## Hosting both OpenSilver app and its backend on a single Azure Web App

This guide demonstrates how to deploy an OpenSilver app to the root and the server to a sub-path, such as `/api`, on the same Azure Web App.

1. In the Azure Web App settings, add a virtual directory for the backend. For example, set the virtual path to `/api` and the physical path to `site\api`, with the type set as `Application`.

2. If your `.Browser` project does not have a `web.config` file, add it (you can use an example from [this article](enable-trimming.md)). Then set `<PublishIISAssets>true</PublishIISAssets>` in the `.csproj` file.

3. Insert the following rule in your `web.config` file, to bypass `/api` calls, allowing them to be handled by the backend app:
```
<configuration>
    <system.webServer>
        <rewrite>
            <rules>
                <rule name="API Rule" stopProcessing="true">
                    <match url="api/(.*)" />
                    <action type="None" />
                </rule>
                <!--Other rules-->
            </rules>
        </rewrite>
    </system.webServer>
</configuration>
```

4. Ensure you updated the server URL in the client app to `https://your-site-name.azurewebsites.net/api/`.
5. Publish the `.Browser` project from Visual Studio using the publish profile that you can get on the Azure portal.
6. Publish the backend project by importing the same publish profile, but modify the Site name to `your-site-name/api`.

![VS Publish on Azure](/images/how-to-topics/vs-publish-azure.png)