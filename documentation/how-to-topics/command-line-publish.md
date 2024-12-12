## How to publish OpenSilver project using command line

#### 1. Install Visual Studio Build Tools. You can find it here - https://visualstudio.microsoft.com/downloads/

![Install Build Tools](/images/how-to-topics/install_build_tools.png "Install Build Tools")

#### 2. Install .NET SDK

![Modify Build Tools](/images/how-to-topics/install_net_sdk.png "Modify Build Tools")
![Choose .NET SDK](/images/how-to-topics/choose_net_sdk.png "Choose .NET SDK")

#### 3. Install .NET SDK from  https://dotnet.microsoft.com/download/dotnet

#### 4. Restore Nuget packages. For example with this command:
```
dotnet restore OpenSilverApplication.sln
```

#### 5. Go to Solution folder and run:
```
msbuild OpenSilverApplication.sln /t:Rebuild /p:DeployOnBuild=true
```
