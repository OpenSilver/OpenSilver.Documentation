## How to manage themes

### What do Themes do? 

Themes are a set of styles that will replace those originally associated with the DefaultStyleKey of Controls. This means that these styles will not be treated as implicit but as default. This way, if you want to later create implicit styles to only change some properties defined in your theme styles, you can simply do it normally. 

### Using a theme

You can easily apply a theme on your application in two steps (we will take the example of OpenSilver's Modern theme available on GitHub here: https://github.com/OpenSilver/OpenSilver.Themes.Modern):
1. Add a reference to the project or package which defines the theme
2. Set your theme as the value of Application.Theme:
- In C#:
```
Application.Current.Theme = new ModernTheme();
```
- Or in Xaml:

    In your OpenSilver application, add to App.xaml: 
    ```
    <Application.Theme> 
        <mt:ModernTheme /> 
    </Application.Theme> 
    ```
    And the corresponding namespace definition: xmlns:mt="http://opensilver.net/themes/modern"
    
### How to create a Theme 

#### Requirements: 

For a theme to be used, we strictly need to have: 
- A Xaml Resource Dictionary with the theme's resources (styles) 
- A class for the theme which: 
    - Inherits from OpenSilver.Theming.Theme. 
    - Implements the GenerateResources method. 

#### Quick theme creation steps: 

For a quick creation of a Theme, follow these steps in the OpenSilver Project that should hold your theme definition. 

##### Add theme styles 

First, you need to define resources that will be used by your theme. You can do so by adding  Xaml Resource Dictionaries to your project.  

Create a Xaml Resource Dictionary file for each assembly that defines at least one of the controls for which there is a style in your theme and place the style for each control in its corresponding assembly's file. 

To simplify the code that will be required to load these files, it would be a good idea to give these files a name that only changes by the assembly name. 

 

 

##### Add a class for the theme 

This class will be your theme instance. 
It needs to inherit from OpenSilver.Theming.Theme, and override the GenerateResources method. 

The role of that method is to load the theme resources, limited to those related to the assembly passed as parameter. 

Here is an example of implementation for this method: 
```
protected override ResourceDictionary GenerateResources(Assembly assembly) 
{ 
    string assemblyName = assembly.GetName().Name; 
    if (IsSupportedAssembly(assemblyName)) 
    { 
        return CreateDictionary(assemblyName); 
    } 
    return null; 
} 

private ResourceDictionary CreateDictionary(string assemblyName) 
{ 
    var resources = new ResourceDictionary(); 
    resources.Source = new Uri($"/SomeTheme;component/Themes/{assemblyName}.xaml", UriKind.Relative); 
    Application.LoadComponent(resources, resources.Source); 
    return resources; 
} 
 
private static bool IsSupportedAssembly(string assemblyName) 
{ 
    // Keep only assemblies handled by your theme:
    return assemblyName is 
        "OpenSilver" or 
        "OpenSilver.Controls" or 
        "OpenSilver.Controls.Toolkit" or 
        "OpenSilver.Controls.Layout.Toolkit" or 
        "OpenSilver.Controls.Data.Input" or 
        "OpenSilver.Controls.Input" or 
        "OpenSilver.Controls.Input.Toolkit" or 
        "OpenSilver.Controls.Data" or 
        "OpenSilver.Controls.DataVisualization.Toolkit" or 
        "OpenSilver.Controls.Data.DataForm.Toolkit"; 
}
```

As you can see, this will only load the resource files for the assemblies that are used. 

That's it! Your theme can now be used. 

### Going further 

#### The BasedOn Property 

By using the BasedOn property, you can enrich an already existing Theme by adding styles and resources for Controls located in a different assembly, or by redefining the styles already defined in the base Theme. 

Please note that if GenerateResources returns a non-null value for an Assembly in your theme, that same method will not be called for that assembly on the themes it is based on. 

Key considerations when using the BasedOn property: 

- Ensure that the GenerateResources method of your theme returns a non-null value only for assemblies it is specifically responsible for handling. 
- Returning a non-null value will prevent resource generation from BasedOn themes for the same assemblies, potentially leading to missing resources. 
- If you only intend to modify specific styles from a BasedOn theme, you must either copy all other required styles into your own theme or define the new styles as implicit styles instead of including them directly in your theme. 
- The order in which themes are linked through the BasedOn property determines their priority. The theme set in Application.Theme has the highest priority, followed by its BasedOn theme, then that theme's BasedOn, and so on in a cascading manner. 

#### The Resources property 

The Resources property allows you to add ressources that are non assembly-specific and can therefore be used by all your theme’s resources. You can for example use this property to define a palette of colors or some converters that you can then use throughout your theme. 

You can find an example of using this property in the ModernTheme on GitHub: https://github.com/OpenSilver/OpenSilver.Themes.Modern (see the ModernTheme and the Palette classes).