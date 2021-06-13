Take the following steps to include OpenSilver private package in delivery (offline).

- Create **Nuget.Config** file in the solution folder with the following content.
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <solution>
    <add key="disableSourceControlIntegration" value="true" />
    <add key="dependencyversion" value="Highest" />
  </solution>

  <packageSources>
    <add key="PrivatePackages" value="PrivatePackages" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```
- Create a new folder named **PrivatePackages** in the solution folder.
- Create OpenSilver package following the instructions [here](https://github.com/OpenSilver/OpenSilver#2-if-you-want-to-build-the-whole-nuget-package-including-the-runtime-the-compiler-the-simulator-etc).
	- Use `build-nuget-package-OpenSilver-private.bat` and follow this naming convention: OpenSilver.1.0.0-private-YYYY-MM-DD.nupkg where you should replace YYYY, MM and DD with today's date.
- Install nuget package inside PrivatePackages.
```
nuget.exe add OpenSilver.1.0.0-private-<version>.nupkg -source <Project_Path>\PrivatePackages\
```
- Reference the package.

After **Nuget.Config** creation "Package source" will have one more option - "PrivatePackages".

![Package Sources](/images/package_sources.png "Package Sources")

If the option is selected then **OpenSilver.WorkInProgress** will appear in Browse tab. Make sure 'Include prerelease' is checked.

![Browse](/images/browse.png "Browse")

- Change .gitignore to be able to push to git.

REPLACE:

*.nupkg

WITH:

*.nupkg
!PrivatePackages/*.nupkg