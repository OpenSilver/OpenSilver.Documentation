## Use Visual Studio Performance Profiler for OpenSilver projects.

.NET profiling tools can be used to analyze OpenSilver application performance including CPU usage, Memory Usage and other available analysis Visual Studio provides.

#### 1. Install profiling tools

By default profiling tools are installed with **Visual Studio**. If that's not the case then it can be installed using **Visual Studio Installer**.
 
<img src="/images/how-to-topics/install-net-profiling.png" alt="Install NET profiling tools" /><br />

#### 2. Use latest Simulator project

.Simulator project needs to be set as **Startup Project** in order to be able to use profiler. As an addition simulator project needs to be updated to the latest version which means it needs to run as a standalone executable **Windows Application**.

[Here](https://opensilver.net/permalinks/update/alpha19.aspx) are full instructions.

#### 3. Run profiler

If the above mentioned points are satisfied then to run profiler go to **Debug -> Performance Profiler...**

<img src="/images/how-to-topics/performance-profiler-menu.png" alt="Performance Profiler Menu" width="400" /><br />

<img src="/images/how-to-topics/profiler-tools.png" alt="Profiler tools" width="900" /><br />

Select profiling tool and click **Start**. Diagnostics information will be generated after closing the simulator.

Here are some examples of CPU Usage diagnostics.

<img src="/images/how-to-topics/profiling-result.png" alt="Profiling result" width="800" /><br />

<img src="/images/how-to-topics/cpu-usage.png" alt="CPU usage" width="800" /><br />

#### 4. Specify .pdb files location

If the result doesn't show OpenSilver external calls then most probably profiler couldn't find .pdb files for OpenSilver.

[Follow](https://docs.microsoft.com/en-us/visualstudio/debugger/specify-symbol-dot-pdb-and-source-files-in-the-visual-studio-debugger?view=vs-2019) the instructions to specify .pdb files location in Visual Studio. One other option would be to copy OpenSilver.pdb in Simulator Debug folder.

#### Useful links

This document describes all profiling tools in depth.\
[Measure app performance in Visual Studio](https://docs.microsoft.com/en-us/visualstudio/profiling/?view=vs-2019)
 