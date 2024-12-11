# Installation Guide for OpenSilver Development Environment

Below, you'll find a detailed document about how to set up your Development Environment when using OpenSilver.

## Contents
1. Install the Latest Version of Visual Studio 2022
2. Install the OpenSilver Plugin for Visual Studio
3. Download .NET SDK
4. Install WebAssembly Tools for .NET
5. Download and Install the IIS URL Rewrite Module

## 1. Install the Latest Version of Visual Studio 2022

### Step-by-Step Instructions:
1. **Navigate to the Visual Studio Website:**
   - Open your web browser and go to the official Visual Studio site: [Visual Studio Official Site](https://visualstudio.microsoft.com/vs/).

2. **Download the Installer:**
   - Click on the "Download" button for Visual Studio 2022 Community Edition, which is free for individual developers, open-source projects, academic research, education, and small professional teams.

3. **Run the Installer:**
   - Once the download is complete, run the installer file from your Downloads folder.

4. **Install Visual Studio 2022:**
   - When prompted, select the workloads you wish to install. Make sure you have at least: “.NET desktop development” and “ASP.NET and web development” selected.
   - Click "Install" and wait for the installation to complete.

5. **Complete the Setup:**
   - After installation, launch Visual Studio and sign in with your Microsoft account (optional but recommended for syncing settings).

## 2. Install the OpenSilver Plugin for Visual Studio

### Step-by-Step Instructions:
1. **Download:**
   - Get it from [Opensilver download page](https://forms.opensilver.net/download.aspx)

2. **Install:**
   - Close any Visual Studio instance you may have running
   - Double click it and follow the steps.
   - Make sure you select "Visual Studio 2022" as the version of VS to install the plugin for
   - When you'll restart Visual Studio 2022, the OpenSilver plugin will be installed.

## 3. Download .NET SDK

### Step-by-Step Instructions:
1. **Go to the .NET Download Page:**
   - Access the .NET 9 official download page at [Download .NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).

2. **Select the Appropriate SDK:**
   - Choose the SDK according to your Windows operating system.

3. **Download and Install the SDK:**
   - Click the download link and run the installer after the download completes.

## 4. Install WebAssembly Tools for .NET

### Step-by-Step Instructions:
1. **Open a Command Prompt or Terminal:**
   - On Windows, you can search for “cmd” or “Command Prompt” in the Start menu.

2. **Install the WebAssembly Tools:**
   - Type the following command and press Enter:
     ```bash
     dotnet workload install wasm-tools
     ```

## 5. Download and Install the IIS URL Rewrite Module

### Step-by-Step Instructions:
1. **Visit the IIS URL Rewrite Module Download Page:**
   - Navigate to the official Microsoft IIS page to download the URL Rewrite Module: [IIS URL Rewrite Module Official Page](https://www.iis.net/downloads/microsoft/url-rewrite).

2. **Download the Module:**
   - Select the version appropriate for your system and click the download link.

3. **Install the Module:**
   - Run the downloaded installer and follow the on-screen instructions to install the module.

4. **Verify Installation:**
   - Open the IIS Manager from the Control Panel or by searching in the Start menu to ensure that the URL Rewrite module is installed.

