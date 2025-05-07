When OpenSilver application is published, by default the output files are compressed using gzip or brotli compression. However IIS needs to be configured via `web.config` to serve the compressed assets.

To verify if the application is using compression, check the Network tab in a browser's developer tools and make sure `Content-Encoding` is either `br` or `gz`.

<img src="/images/how-to-topics/compression1.png" alt="Content-Encoding" title="Content-Encoding" style="border: 2px solid #555;" /><br />

Here is the case where the compression is not used.

<img src="/images/how-to-topics/compression2.png" alt="Content-Encoding" title="Content-Encoding" style="border: 2px solid #555;" /><br />

For usual OpenSilver application the following `web.config` will work without any modifications. If your application uses more resource types, you should add the corresponding mime types to the `staticContent` section of the `web.config` file.

>Starting from OpenSilver 3.1, make sure to include the `<add input="{QUERY_STRING}" pattern=".+?" negate="true" />` lines in the `Rewrite gzip file` and `Rewrite brotli file` rules. This is important to serve OpenSilver files with a query string in the URL.

```
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
  <system.webServer>
    <staticContent>
      <remove fileExtension=".dll" />
      <remove fileExtension=".json" />
      <remove fileExtension=".woff" />
      <remove fileExtension=".woff2" />
      <remove fileExtension=".dat" />
      <remove fileExtension=".wasm" />
      <remove fileExtension=".blat" />
      <mimeMap fileExtension=".json" mimeType="application/json" />
      <mimeMap fileExtension=".woff" mimeType="font/woff" />
      <mimeMap fileExtension=".woff2" mimeType="font/woff2" />
      <mimeMap fileExtension=".dat" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".dll" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".wasm" mimeType="application/wasm" />
      <mimeMap fileExtension=".blat" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".js.gz" mimeType="application/javascript" />
      <mimeMap fileExtension=".dat.gz" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".dll.gz" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".json.gz" mimeType="application/json" />
      <mimeMap fileExtension=".wasm.gz" mimeType="application/wasm" />
      <mimeMap fileExtension=".blat.gz" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".html.gz" mimeType="text/html" />
      <mimeMap fileExtension=".css.gz" mimeType="text/css" />
      <mimeMap fileExtension=".ico.gz" mimeType="image/x-icon" />
      <mimeMap fileExtension=".svg.gz" mimeType="image/svg+xml" />
      <mimeMap fileExtension=".js.br" mimeType="application/javascript" />
      <mimeMap fileExtension=".dat.br" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".dll.br" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".json.br" mimeType="application/json" />
      <mimeMap fileExtension=".wasm.br" mimeType="application/wasm" />
      <mimeMap fileExtension=".blat.br" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".html.br" mimeType="text/html" />
      <mimeMap fileExtension=".css.br" mimeType="text/css" />
      <mimeMap fileExtension=".ico.br" mimeType="image/x-icon" />
      <mimeMap fileExtension=".svg.br" mimeType="image/svg+xml" />
    </staticContent>
    <httpCompression>
      <dynamicTypes>
        <remove mimeType="text/*" />
        <remove mimeType="application/javascript" />
        <remove mimeType="image/svg+xml" />
      </dynamicTypes>
      <staticTypes>
        <remove mimeType="text/*" />
        <remove mimeType="application/javascript" />
        <remove mimeType="image/svg+xml" />
      </staticTypes>
    </httpCompression>
    <rewrite>
      <outboundRules rewriteBeforeCache="true">
        <rule name="Add Vary Accept-Encoding" preCondition="PreCompressedFile" enabled="true">
          <match serverVariable="RESPONSE_Vary" pattern=".*" />
          <action type="Rewrite" value="Accept-Encoding" />
        </rule>
        <rule name="Add Encoding Brotli" preCondition="PreCompressedBrotli" enabled="true" stopProcessing="true">
          <match serverVariable="RESPONSE_Content_Encoding" pattern=".*" />
          <action type="Rewrite" value="br" />
        </rule>
        <rule name="Add Encoding Gzip" preCondition="PreCompressedGzip" enabled="true" stopProcessing="true">
          <match serverVariable="RESPONSE_Content_Encoding" pattern=".*" />
          <action type="Rewrite" value="gzip" />
        </rule>
        <preConditions>
          <preCondition name="PreCompressedFile">
            <add input="{HTTP_URL}" pattern="\.(gz|br)$" />
          </preCondition>
            <preCondition name="PreCompressedBrotli">
            <add input="{HTTP_URL}" pattern="\.br$" />
          </preCondition>
          <preCondition name="PreCompressedGzip">
            <add input="{HTTP_URL}" pattern="\.gz$" />
          </preCondition>
        </preConditions>
      </outboundRules>
      <rules>
        <rule name="Serve subdir">
          <match url=".*" />
          <action type="Rewrite" url="wwwroot\{R:0}" />
        </rule>
        <rule name="Rewrite brotli file" stopProcessing="true">
          <match url="(.*)"/>
          <conditions>
            <add input="{HTTP_ACCEPT_ENCODING}" pattern="br" />
            <add input="{REQUEST_FILENAME}" pattern="\.(js|dat|dll|json|wasm|blat|htm|html|css|ico|svg)$" />
            <add input="{QUERY_STRING}" pattern=".+?" negate="true" />
            <add input="{REQUEST_FILENAME}.br" matchType="IsFile" />
          </conditions>
          <action type="Rewrite" url="{R:1}.br" />
        </rule>
        <rule name="Rewrite gzip file" stopProcessing="true">
          <match url="(.*)"/>
          <conditions>
            <add input="{HTTP_ACCEPT_ENCODING}" pattern="gzip" />
            <add input="{REQUEST_FILENAME}" pattern="\.(js|dat|dll|json|wasm|blat|htm|html|css|ico|svg)$" />
            <add input="{QUERY_STRING}" pattern=".+?" negate="true" />
            <add input="{REQUEST_FILENAME}.gz" matchType="IsFile" />
          </conditions>
          <action type="Rewrite" url="{R:1}.gz" />
        </rule>
        <rule name="SPA fallback routing" stopProcessing="true">
          <match url=".*" />
          <conditions logicalGrouping="MatchAll">
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
          </conditions>
          <action type="Rewrite" url="wwwroot\" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
```

Here is a more detailed explanation [How to Host and deploy ASP.NET Core Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/webassembly?view=aspnetcore-6.0)

#### Publish web.config

If you need to publish custom `web.config` file automatically, you can add it to .Browser project and set `<PublishIISAssets>true</PublishIISAssets>` in the project file.

#### Make sure compression is enabled from IIS

Double click on compression in IIS.

<img src="/images/how-to-topics/compression4.png" alt="Compression" title="Compression" style="border: 2px solid #555;" /><br />

Make sure both dynamic and static content compressions are enabled.

<img src="/images/how-to-topics/compression5.png" style="border: 2px solid #555;" /><br />

If one of them is disabled it needs to be enabled from "Turn Windows features on or off".

<img src="/images/how-to-topics/compression6.png" style="border: 2px solid #555;" /><br />