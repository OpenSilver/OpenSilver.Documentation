# OpenSilver.Documentation

## Set up
Download [docfx](https://github.com/dotnet/docfx/releases)

Add docfx to Path in order to call the command from everywhere with the console :
 `set PATH=%PATH%;D:\docfx\`

[Add a PATH with windows Interface](https://www.computerhope.com/issues/ch000549.htm#:~:text=In%20the%20Advanced%20section%2C%20click,want%20the%20computer%20to%20access.)

To build a project, use the command :
`docfx docfx.json`

To try in local, use the command :
`docfx serve _site`
The pages will be uploaded on your port 8080 by default (http://localhost:8080), you can change the port with the -p such as :
`docfx serve _site -p 8081`

finally, you can merge the 2 commands with :
`docfx docfx.json --serve`

Apply a template :
`docfx build docfx.json -t templates\<template folder name>`


[Docfx Documentation](https://dotnet.github.io/docfx/tutorial/walkthrough/walkthrough_create_a_docfx_project.html)


## Edit Files
To modify the current documentation, one needs to edit the .md file in the master branch.

[Docfx Markdown](https://daringfireball.net/projects/markdown/basics)
[.MD Markdown](https://markdownmonster.west-wind.com/docs/_53e0pnhea.htm)

To change

## Update Github Pages

## Link to source Code
In order to generate the documentation from the source code, one needs to edit the docfx.json file. In the metadata section, there is the path of the csproj :
```
"metadata": [
  {
    "src": [
      {
        "files": [
          "../../OpenSilverDocumentation/OpenSilver/src/CSHTML5.Runtime/**.csproj"
        ]
      }
    ],
```

and use the command to read only the metadata configuration
`docfx metadata`
