## How to publish OpenSilver project using command line

#### 1. Install Visual Studio Build Tools 2019. You can find it here - https://visualstudio.microsoft.com/downloads/

![Install Build Tools](/images/how-to-topics/install_build_tools.png "Install Build Tools")

#### 2. Install .NET SDK

![Modify Build Tools](/images/how-to-topics/install_net_sdk.png "Modify Build Tools")
![Choose .NET SDK](/images/how-to-topics/choose_net_sdk.png "Choose .NET SDK")

#### 3. Install DotNet6 Preview4 SDK from  https://dotnet.microsoft.com/download/dotnet/6.0
#### 4. Add file global.json. This file must go at the level of your sln or higher in the directory tree. Content of the file:
```
{
  "sdk": {
    "allowPrerelease": true
  }
}
```

#### 5. Restore Nuget packages. For example with this command:
```
dotnet restore OpenSilverApplication.sln
```

#### 6. Go to Solution folder and run:
```
msbuild OpenSilverApplication.sln /t:OpenSilverApplication_Browser:Rebuild /p:DeployOnBuild=true
```
