# OpenSilver.Blazor (Preview)

---

## 1. Introduction

**OpenSilver.Blazor** is a new library that enables you to seamlessly embed [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) components within your OpenSilver applications. Blazor is a powerful framework with a vibrant community and a rich ecosystem of ready-made components. By integrating Blazor into OpenSilver, we unlock the ability to reuse existing Blazor assets while leveraging the shared .NET technology stack.

> **Note:**
> OpenSilver.Blazor is currently released as a **Preview**. Expect breaking changes, evolving APIs, and incomplete features as we work towards the first stable release. Feedback is welcome!

---

## 2. Key Features

* **Embed Razor Code Directly in XAML**

  Place Razor code blocks directly within XAML using the `<RazorComponent>` tag.

* **Reference External Razor Components**

  Use Razor components defined in separate `.razor` files via the `ComponentType` attribute:

  ```xml
  <RazorComponent ComponentType="{x:Type local:Counter}" />
  ```

* **Support for 3rd Party Blazor Libraries**

  Integrate popular Blazor UI libraries (such as **Blazorise**, **MudBlazor**, and **Radzen**) seamlessly into your OpenSilver app.

* **XAML Bindings in Razor Code**

  Use XAML data bindings within Razor code.

* **Multi-Launcher Compatibility**

  Works with `.Browser`, `.MauiHybrid`, and `.Simulator` launchers.

  > **Most stable experience is currently for `.Browser` launcher.**

---

## 3. Getting Started & Project Configuration

### Prerequisites

* Latest **Preview** versions of OpenSilver and OpenSilver.Blazor packages
* .NET 8 or .NET 9

### Installation Steps

1. **Add the MyGet feed** (if not already present):
   All newly created OpenSilver projects include
   `https://www.myget.org/F/opensilver/api/v3/index.json`
   by default.
   [Read more about preview feeds here.](../how-to-topics/get-latest-preview-version.md)

2. **Install the NuGet Package:**
   Add the `OpenSilver.Blazor` NuGet package to any project where you want to use Blazor components.

3. **Update your project SDK:**
   In your `.csproj`, change:

   ```xml
   <Project Sdk="Microsoft.NET.Sdk">
   ```

   to

   ```xml
   <Project Sdk="Microsoft.NET.Sdk.Razor">
   ```

4. **Add 3rd Party Blazor Libraries (Optional):**
   Install any Blazor library (e.g., Blazorise, MudBlazor, Radzen) following their own installation instructions.

---

## 4. Usage Examples

Below are a few usage examples.

### **A. Simple Embedded Razor Code with Data Binding**

#### **1. XAML Integration (`Message_Demo.xaml`)**

```xml
<UserControl
    x:Class="MyApp.Message_Demo"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:razor="clr-namespace:OpenSilver.Blazor;assembly=OpenSilver.Blazor"
    xmlns:local="clr-namespace:MyApp">

    <razor:RazorComponent>
        <h3>Hello, Blazor!</h3>

        <p>Message from the Model: "{Binding Message, Type=String}"</p>
    </razor:RazorComponent>
</UserControl>
```

#### **2. Code-behind (`Message_Demo.xaml.cs`)**

```csharp
public partial class Message_Demo : UserControl
{
    public Message_Demo()
    {
        this.InitializeComponent();

        this.DataContext = new Context();
    }
}

public class Context
{
    public string Message { get; set; } = "Hi!";
}
```

### **B. Using a 3rd Party Blazor Button (Radzen Example)**

This example demonstrates how to use a Radzen Blazor button inside a XAML UI, including the required setup for 3rd-party Blazor libraries.

#### **1. XAML Integration (`RadzenButton_Demo.xaml`)**

```xml
<UserControl
    x:Class="MyApp.RadzenButton_Demo"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:razor="clr-namespace:OpenSilver.Blazor;assembly=OpenSilver.Blazor"
    xmlns:local="clr-namespace:MyApp">

    <StackPanel>
        <razor:RazorComponent Margin="0,20,0,0" Height="70">
            @using Radzen
            @using Radzen.Blazor

            <RadzenButton Text="Click me!" Click="{Binding ButtonClickDel, Type=Action}" />
        </razor:RazorComponent>
    </StackPanel>
</UserControl>
```

#### **2. Code-behind (`RadzenButton_Demo.xaml.cs`)**

```csharp
public partial class RadzenButton_Demo : UserControl
{
    public RadzenButton_Demo()
    {
        InitializeComponent();
        DataContext = new TestButtonClickClass();
    }
}

public class TestButtonClickClass
{
    public Action ButtonClickDel { get; }

    public TestButtonClickClass()
    {
        ButtonClickDel = ButtonClick;
    }

    private void ButtonClick()
    {
        MessageBox.Show("You clicked me!");
    }
}
```

#### **3. Additional Configuration for Radzen.Blazor**

To use Radzen components, some additional setup is required in your `.Browser` project:

* **a. Add the Radzen JS Script**

  In `index.html` (inside the `<head>` section):

  ```html
  <script src="_content/Radzen.Blazor/Radzen.Blazor.js"></script>
  ```

* **b. Register Radzen Services**

  In `Program.cs`, add the following before `builder.Build();`:

  ```csharp
  builder.Services.AddRadzenComponents();
  ```

#### **4. Notes**

* Ensure you’ve installed the `Radzen.Blazor` NuGet package in your project.
* The example uses XAML data binding to connect the button click event to a C# handler via `ButtonClickDel`.
* If you use other 3rd-party Blazor libraries (e.g., MudBlazor, Blazorise), consult their documentation for similar setup requirements.


### **C. Using a Ready Razor Component from Another File**

#### **1. XAML Integration (`MyControl.xaml`)**

```xml
<UserControl
    x:Class="MyApp.MyControl"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:razor="clr-namespace:OpenSilver.Blazor;assembly=OpenSilver.Blazor"
    xmlns:local="clr-namespace:MyApp">

    <razor:RazorComponent ComponentType="{x:Type local:Counter}" />
</UserControl>
```

#### **2. Blazor Component (`Counter.razor`)**

```razor
<h3 style="margin-top:0">Counter</h3>

<p>Current count: @currentCount</p>

<button class="btn btn-primary" onclick="@IncrementCount">Click me</button>

@code {
    int currentCount = 0;

    void IncrementCount()
    {
        currentCount++;
    }
}
```

---

## 5. Threading Considerations

### Running Blazor Code on the Correct Thread

When running OpenSilver.Blazor in certain environments, specifically **MAUI Hybrid**, the **Simulator**, and the **XAML Designer**, Blazor components and logic must be executed on a dedicated Blazor thread. This is required due to differences in the threading models of these platforms.

To ensure **maximum compatibility and a consistent experience** across all launchers, we highly recommend that any code interacting with Blazor components is always executed on the Blazor thread. This approach avoids threading issues and ensures your UI updates work reliably everywhere.

### The `BlazorThread` Utility

The `OpenSilver.Blazor.Threading.BlazorThread` class provides methods for running code on the Blazor thread. You should use these methods whenever you need to update Blazor components or call APIs such as `StateHasChanged` from outside of Razor code.

**Example:**
To safely call the Blazor `StateHasChanged` method, use:

```csharp
OpenSilver.Blazor.Threading.BlazorThread.BeginInvokeOnMainThread(this.StateHasChanged);
```

This guarantees that `StateHasChanged` runs on the Blazor thread, preventing cross-thread exceptions and UI update issues.

> **Tip:**
> For best results and future compatibility, always use `BlazorThread` for any code that updates or interacts with Blazor components from outside of their normal lifecycle methods.

---

## 6. Known Limitations & Preview Notes

* **Launcher Support:**
  `.Photino` launcher is **not supported**. Behavior may differ across `.Browser`, `.MauiHybrid`, and `.Simulator`.

* **Designer Support:**
  The XAML Designer may not reliably preview Razor components.

* **Stability:**
  Breaking changes are expected in this Preview.

* **Troubleshooting:**
  Please [report issues](https://github.com/OpenSilver/OpenSilver/issues) on GitHub.

---

## 7. Roadmap

The first official release of OpenSilver.Blazor is planned for **Autumn 2025**. Expect further features and improved stability.

---
