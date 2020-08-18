# RelativeSource

## Introduction

The "RelativeSource" keyword is used in XAML to specify the source of a binding.

The following RelativeSource modes are currently supported:

* [RelativeSourceMode.Self](#relativesourcemodeself)
* [RelativeSourceMode.TemplatedParent](#relativesourcemodetemplatedparent)


## RelativeSourceMode.Self
Use "Self" if you want to bind one property of an element to another property on the same element. With this mode, the element on which the binding is applied will be the source of the binding.

Here is an example:

`<TextBlock Text="{Binding Width, RelativeSource={RelativeSource Mode=Self}}" x:Name="TextBlock1" Width="200"/>`

In this example, the TextBlock will display the text "200", because the Text property is bound to the Width property.

Please note that this is *NOT the same as*:

`<TextBlock Text="{Binding Width}" x:Name="TextBlock1" Width="200"/>`

The line above will NOT work because it will be looking for a property "Width" on the DataContext. By *"DataContext"* we mean the content of the property "TextBlock1.DataContext". The DataContext is an inherited property, which means that "TextBlock1.DataContext" will have the same value as the DataContext property of the parent of the TextBlock.

An alternative way to achieve the same result as "Selft" is to use "ElementName". With *"ElementName"* you can tell it to use the element itself as the source of the binding. Here is an example:

<TextBlock Text="{Binding Width, ElementName=TextBlock1}" x:Name="TextBlock1" Width="200"/>


## RelativeSourceMode.TemplatedParent

Use "TemplatedParent" if you are *inside a ControlTemplate* and you want to *bind a property of an element to a property of the templated control.*

For example, consider the following XAML code:
```
<Border>
    <Border.Resources>
        <Style x:Key="MyRoundButtonStyle" TargetType="Button">

            <!-- Set the default value for some properties of the button: -->
            <Setter Property="Background" Value="Gray"/>
            <Setter Property="Foreground" Value="Black"/>
            <Setter Property="Padding" Value="12"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="HorizontalContentAlignment" Value="Center"/>
            <Setter Property="VerticalContentAlignment" Value="Center"/>

            <!-- Define how the button will be rendered: -->
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="Button">
                        <Grid>
                            <Ellipse
                                Fill="{Binding Background, RelativeSource={RelativeSource Mode=TemplatedParent}}"
                                HorizontalAlignment="Stretch"
                                VerticalAlignment="Stretch"/>
                            <ContentPresenter
                                Content="{Binding Content, RelativeSource={RelativeSource Mode=TemplatedParent}}"
                                HorizontalAlignment="{Binding HorizontalContentAlignment, RelativeSource={RelativeSource Mode=TemplatedParent}}"
                                VerticalAlignment="{Binding VerticalContentAlignment, RelativeSource={RelativeSource Mode=TemplatedParent}}"/>
                        </Grid>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
    </Border.Resources>
    <Button x:Name="MyTemplatedButton"
            Content="OK"
            Style="{StaticResource MyRoundButtonStyle}"
            Width="50"
            Height="50"
            Background="Blue"
            Foreground="White"/>
</Border>
```

In the code above, we create a round button by creating a new style using a ControlTemplate. The "Fill" property of the ellipse has a binding to the "Background" property of the templated button.

Please note that the two following lines are strictly equivalent:

`<Ellipse Fill="{Binding Background, RelativeSource={RelativeSource Mode=TemplatedParent}}"/>`

and

`<Ellipse Fill="{TemplateBinding Background}"/>`

The main advantage of the "Binding" syntax compared to the "TemplateBinding" syntax is that the former allows you to specify a "Converter" to convert the bound value via a class that inherits from *IValueConverter*.

## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
