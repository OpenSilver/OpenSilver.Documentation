# How to import “TypeScript Definition” files

To avoid having to manually make calls to the "Interop" API to interact with JavaScript libraries, OpenSilver includes the possibility of importing "TypeScript Definition" files, the extension of which is ".d.ts". These are relatively short files that accompany most JavaScript libraries and whose purpose is to provide strong typing to the libraries in question.

Normally these files are intended for developers of TypeScript applications, but OpenSilver has diverted their use in order to auto-generate strongly typed C# wrapper classes that allow to interact with JavaScript libraries in pure C#, that is to say without any manual call to JavaScript.

To use these files, developers can simply copy/paste them to an OpenSilver project and compile the project. The auto-generated files can be seen in the “obj/Debug” subfolder of the project.

In the current version, some advanced features of TypeScript are not yet supported, so a little cleaning up inside the TypeScript Definition file is often necessary to keep only the essentials.

Many examples are available in the GitHub of CSHTML5, which is a sister product also maintained by Userware. Its GitHub is accessible at the following address: [repositories](https://github.com/cshtml5?tab=repositories)
