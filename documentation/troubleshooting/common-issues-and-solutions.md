# Troubleshooting

## Common issues and solutions

### I am getting "Simulator Not Enabled" whenever I launch the simulator project, even though I did open Package Manager Console.

Please fix the path that is in the "Start Program" field of the project properties of the project with the ".Simulator" suffix.

To do so, please right-click on the project "ReptonPoD.Simulator", click "Properties", go to the "Debug" section, and check "Start external program".

Make sure to enter the following path, where you need to replace "YOUR-USER-NAME-GOES-HERE" with your actual Windows user name:
C:\Users\YOUR-USER-NAME-GOES-HERE\.nuget\packages\opensilver.workinprogress\1.0.0-alpha-007\tools\simulator\CSharpXamlForHtml5.Simulator.exe

You will also need to change the version number in the path above to reflect the version of OpenSilver that you are using.

### What if my issue is not listed here?

Please contact us at: https://opensilver.net/contact.aspx

