## How to get the latest preview version of OpenSilver

The CI/CD pipeline is run automatically after each commit to the develop branch in the OpenSilver repository.
This pipeline builds OpenSilver and Simulator packages and uploads it to the publicly available MyGet feed.
As a result, anybody can consume the latest version with minimal effort.

### Steps

Add the nuget.config file with the following content on the same level with the .sln file:
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <add key="MyGet" value="https://www.myget.org/F/opensilver/api/v3/index.json" />
    </packageSources>
</configuration>
```
<img src="/images/how-to-topics/nuget-config.png" alt="Add nuget.config"/><br />
<br />
Restart your Visual Studio.<br /><br />
Go to "Manage NuGet Packages for Solution...":
<img src="/images/how-to-topics/manage-nuget-packages.png" alt="Manage nuget packages"/><br />
<br />
Uninstall current version of OpenSilver:
<img src="/images/how-to-topics/uninstall-opensilver.png" alt="Uninstall OpenSilver"/><br />
<br />
Choose the MyGet as a package source and install the latest Preview version:
<img src="/images/how-to-topics/install-opensilver.png" alt="Install OpenSilver"/><br />
<br />
Repeat steps with the OpenSilver.Simulator package.
<br /><br />
Restart your Visual Studio and rebuild the solution.