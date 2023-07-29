## Build OpenSilver project using command-line


We can use **msbuild (MSBuild.exe)** to build OpenSilver projects using command line.

```
msbuild TestProject.sln
```

This will build the project with default configuration. To use a particular configuration `-p` option can be used.

```
msbuild TestProject.sln -p:Configuration=Release
```

Use the following command to see full list of msbuild options.

```
msbuild -help
```
