# OpenSilver.Documentation

## Set up
Download [docfx](https://github.com/dotnet/docfx/releases)

Add docfx to Path to call the command from everywhere with the console :
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

and the merged command
`docfx build docfx.json -t templates\<template folder name> --serve`

[Docfx Documentation](https://dotnet.github.io/docfx/tutorial/walkthrough/walkthrough_create_a_docfx_project.html)


## Edit Files
To modify the current documentation, one needs to edit the .md file in the master branch.

[Docfx Markdown](https://daringfireball.net/projects/markdown/basics)
[.MD Markdown](https://markdownmonster.west-wind.com/docs/_53e0pnhea.htm)

The files used are referenced in the .json which is at the base of the folder. Every folder has the same structure :

* The MD files that generate HTML pages. They are the ones to edit to change the content of a page.

* The toc.yml file, it references every file of the folder with a name and the path (href). yml files are space sensitive meaning each line must have a one tab indentation only to be properly working.

* the toc file at the base of the project is slightly different because it is used to build the navbar and the sidebar. Name will be the navbar tab name, the homepage will be the .md file linked to the navbar and finally, the href will be where the sidebar (menu) is generated.

## Markdown exemples
Create a title
```
# H1 ##H2 ### H3
```

Emphasis a Text
```
*bold text*
```

Unordered List :
```
* Element 1
* Element 2
* Element 3
```

Ordered List :
```
1. Element 1
2. Element 2
3. Element 3
```

Add an image
```
![Name](/images/img.png "title")
```

add an hyperlink :
```
[Texte](link)
```

add Code :
```
  ``` Code ```  
```

## Link to Source Code
To generate the documentation from the source code, one needs to edit the docfx.json file. In the metadata section, there is the path of the csproj :
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
```
docfx metadata
```
