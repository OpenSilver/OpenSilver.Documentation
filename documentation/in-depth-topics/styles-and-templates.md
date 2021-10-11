# Styles and Templates

### UPDATE: since this page was originally written, the built-in styles of OpenSilver have been improved to more closely match those of Silverlight. Please check back in the future for updated styles.

## General Concepts

* The way styling works in OpenSilver is the same as in other recent XAML-based platforms such as UWP, Silverlight, WinRT, and Windows Phone.

* Please note that WPF uses Triggers by default (for historical reasons), which are not yet supported. You can easily convert a WPF-like style into a style that uses only the VisualStateManager states. Read below for examples.

* You can use ResourceDictionaries if you wish to organize styles into their own files. You can easily reference a ResourceDictionary by using the "MergedDictionaries" property, as show in the following example (in "App.xaml"):
```
<Application
    x:Class="MyApplication1.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="MyResourceDictionary1.xaml"/>
                <ResourceDictionary Source="Themes/Generic.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

* Here is a short example that demonstrates how to define a simple "round button" style:
```
<Border>
    <Border.Resources>

        <!-- Define a style for creating round buttons: -->
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
                                Fill="{TemplateBinding Background}"
                                HorizontalAlignment="Stretch"
                                VerticalAlignment="Stretch"/>
                            <ContentPresenter
                                Content="{TemplateBinding Content}"
                                HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
                        </Grid>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

    </Border.Resources>

    <!-- Test the above style using the following sample "OK" button: -->
    <Button x:Name="MyTemplatedButton"
            Content="OK"
            Style="{StaticResource MyRoundButtonStyle}"
            Width="50"
            Height="50"
            Background="Blue"
            Foreground="White"/>
</Border>
```

## Styling the Button

To easily customize the appearance of the Button control, you can set Button properties such as: Background, Foreground, Padding, BorderBrush, BorderThickness, Cursor, HorizontalContentAlignment, and VerticalContentAlignment.

If you wish to further customize the Button control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the Button control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the Button parents' resources), and reference it with: <Button Style={StaticResource ButtonStyle1} />
```
<Style x:Key="ButtonStyle1" TargetType="Button">
    <Setter Property="Background" Value="#FFE2E2E2"/>
    <Setter Property="Foreground" Value="Black"/>
    <Setter Property="BorderThickness" Value="0"/>
    <Setter Property="Padding" Value="12,4,12,4"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="HorizontalContentAlignment" Value="Center"/>
    <Setter Property="VerticalContentAlignment" Value="Center"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border x:Name="OuterBorder"
                            Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}">
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup Name="CommonStates">
                            <VisualState Name="Normal">
                            </VisualState>
                            <VisualState Name="PointerOver">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#11000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="Pressed">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#22000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="Disabled">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#33FFFFFF"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                    <Border x:Name="InnerBorder"
                                Background="{TemplateBinding Background}">
                        <ContentPresenter x:Name="ContentPresenter"
                                                ContentTemplate="{TemplateBinding ContentTemplate}"
                                                Content="{TemplateBinding Content}"
                                                Margin="{TemplateBinding Padding}"
                                                HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                                VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
                    </Border>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the ToggleButton

To easily customize the appearance of the ToggleButton control, you can set ToggleButton properties such as: Background, Foreground, Padding, BorderBrush, BorderThickness, Cursor, HorizontalContentAlignment, and VerticalContentAlignment.

If you wish to further customize the ToggleButton control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the ToggleButton control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the ToggleButton parents' resources), and reference it with: <ToggleButton Style={StaticResource ToggleButtonStyle1} />
```
<Style x:Key="ToggleButtonStyle1" TargetType="ToggleButton">
    <Setter Property="Background" Value="#FFE2E2E2"/>
    <Setter Property="Foreground" Value="Black"/>
    <Setter Property="BorderThickness" Value="0"/>
    <Setter Property="Padding" Value="12,4,12,4"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="HorizontalContentAlignment" Value="Center"/>
    <Setter Property="VerticalContentAlignment" Value="Center"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="ToggleButton">
                <Border x:Name="OuterBorder"
                            Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}">
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup Name="CommonStates">
                            <VisualState Name="Normal">
                            </VisualState>
                            <VisualState Name="PointerOver">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#11000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="Pressed">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#44000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="Disabled">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="Transparent"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="Checked">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#33000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="CheckedPointerOver">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#22000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="CheckedPressed">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#44000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="CheckedDisabled">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#33000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="Indeterminate">
                            </VisualState>
                            <VisualState Name="IndeterminatePointerOver">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#11000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="IndeterminatePressed">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#22000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="IndeterminateDisabled">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="Transparent"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                    <Border x:Name="InnerBorder"
                                Background="{TemplateBinding Background}">
                        <ContentPresenter x:Name="ContentPresenter"
                                                ContentTemplate="{TemplateBinding ContentTemplate}"
                                                Content="{TemplateBinding Content}"
                                                Margin="{TemplateBinding Padding}"
                                                HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                                VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
                    </Border>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the Expander
To easily customize the appearance of the Expander control, you can set Expander properties such as: Background, Foreground, Padding, BorderBrush, and BorderThickness.

If you wish to further customize the Expander control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the Expander control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the Expander parents' resources), and reference it with: <Expander Style={StaticResource ExpanderStyle1} Header="MyHeader" Content="MyContent" />
```
<Style x:Key="ExpanderStyle1" TargetType="Expander">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Expander">
                <Border Background="{TemplateBinding Background}"
                                BorderBrush="{TemplateBinding BorderBrush}"
                                BorderThickness="{TemplateBinding BorderThickness}"
                                CornerRadius="3">
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup x:Name="CommonStates">
                            <VisualState x:Name="Normal"/>
                            <VisualState x:Name="Disabled"/>
                        </VisualStateGroup>
                        <VisualStateGroup x:Name="FocusStates">
                            <VisualState x:Name="Focused"/>
                            <VisualState x:Name="Unfocused"/>
                        </VisualStateGroup>
                        <VisualStateGroup x:Name="ExpansionStates">
                            <VisualState x:Name="Collapsed"/>
                            <VisualState x:Name="Expanded">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Duration="0" Storyboard.TargetName="ExpandSite" Storyboard.TargetProperty="Visibility">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="Visible"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                    <StackPanel>
                        <ToggleButton x:Name="ExpanderButton"
                                                Padding="{TemplateBinding Padding}"
                                                Margin="1"
                                                Content="{TemplateBinding Header}"
                                                ContentTemplate="{TemplateBinding HeaderTemplate}"
                                                Cursor="Hand">
                            <ToggleButton.Template>
                                <ControlTemplate TargetType="ToggleButton">
                                    <Border Padding="{TemplateBinding Padding}">
                                        <VisualStateManager.VisualStateGroups>
                                            <VisualStateGroup x:Name="CommonStates">
                                                <VisualState x:Name="Normal"/>
                                                <VisualState x:Name="Checked">
                                                    <Storyboard>
                                                        <ObjectAnimationUsingKeyFrames Duration="0" Storyboard.TargetName="arrow" Storyboard.TargetProperty="Data">
                                                            <DiscreteObjectKeyFrame KeyTime="0" Value="M 1,1.5 L 4.5,5 L 8,1.5"/>
                                                        </ObjectAnimationUsingKeyFrames>
                                                    </Storyboard>
                                                </VisualState>
                                            </VisualStateGroup>
                                            <VisualStateGroup x:Name="FocusStates">
                                                <VisualState x:Name="Focused"/>
                                                <VisualState x:Name="Unfocused"/>
                                            </VisualStateGroup>
                                        </VisualStateManager.VisualStateGroups>
                                        <StackPanel Orientation="Horizontal" Margin="5,0,0,0" >
                                            <Path x:Name="arrow" Visibility="Visible" Stroke="#FF555555" Width="9" Height="9" Margin="0,0,3,0" StrokeThickness="3" HorizontalAlignment="Center" VerticalAlignment="Center" Stretch="Fill" Data="M 2,1 L 5.5,4.5 L 2,8"/>
                                            <ContentPresenter x:Name="header" Margin="4,0,0,0" HorizontalAlignment="Stretch" VerticalAlignment="Center" Content="{TemplateBinding Content}" ContentTemplate="{TemplateBinding ContentTemplate}"/>
                                        </StackPanel>
                                    </Border>
                                </ControlTemplate>
                            </ToggleButton.Template>
                        </ToggleButton>
                        <ContentControl x:Name="ExpandSite"
                                                Visibility="Collapsed"
                                                Margin="{TemplateBinding Padding}"
                                                Content="{TemplateBinding Content}"
                                                ContentTemplate="{TemplateBinding ContentTemplate}" />
                    </StackPanel>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

#Styling the TabControl and TabItem

To easily customize the appearance of the TabControl, you can:

* set *TabControl* properties such as: Background, Foreground, Padding, BorderBrush, BorderThickness, HorizontalContentAlignment, VerticalContentAlignment

* set *TabItem* properties such as: Background, Foreground, SelectedBackground, SelectedForeground, SelectedAccent, Padding, BorderBrush, BorderThickness, Cursor, Margin, HorizontalContentAlignment, VerticalContentAlignment

If you wish to further customize the TabControl, you can apply a custom ControlTemplate to both the TabControl and the TabItem.

Below you will find a sample Style and ControlTemplate for the TabControl and the TabItem controls. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the Expander parents' resources), and reference it with: <TabControl Style={StaticResource TabControlStyle1}> and <TabItem Style={StaticResource TabItemStyle1} />
```
<Style x:Key="TabControlStyle1" TargetType="TabControl">
    <Setter Property="Background" Value="White"/>
    <Setter Property="BorderBrush" Value="#FFDDDDDD"/>
    <Setter Property="BorderThickness" Value="1,1,1,1"/>
    <Setter Property="Padding" Value="5"/>
    <Setter Property="HorizontalContentAlignment" Value="Stretch"/>
    <Setter Property="VerticalContentAlignment" Value="Stretch"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="TabControl">
                <Border>
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup x:Name="CommonStates">
                            <VisualState x:Name="Normal" />
                            <VisualState x:Name="Disabled">
                            </VisualState>
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>

                    <Grid x:Name="TemplateTop" Visibility="Collapsed">
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="*"/>
                        </Grid.RowDefinitions>

                        <TabPanel x:Name="TabPanelTop" />

                        <Border
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"
                            Background="{TemplateBinding Background}"
                            Grid.Row="1"  
                            CornerRadius="0,0,3,3">
                            <ContentPresenter
                                    x:Name="ContentTop"
                                    HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                    VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
                        </Border>
                    </Grid>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
<Style x:Key="TabItemStyle1" TargetType="TabItem">
    <Setter Property="Background" Value="White"/>
    <Setter Property="BorderBrush" Value="#FFDDDDDD"/>
    <Setter Property="BorderThickness" Value="2"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="Foreground" Value="Black"/>
    <Setter Property="HorizontalContentAlignment" Value="Stretch"/>
    <Setter Property="VerticalContentAlignment" Value="Stretch"/>
    <Setter Property="Margin" Value="0,0,5,0"/>
    <Setter Property="Padding" Value="6,0,6,3"/>
    <Setter Property="SelectedBackground" Value="White"/>
    <Setter Property="SelectedForeground" Value="Black"/>
    <Setter Property="SelectedAccent" Value="Blue"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="TabItem">
                <StackPanel x:Name="Root">
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup x:Name="CommonStates">
                            <VisualState x:Name="Normal" />
                            <VisualState x:Name="PointerOver">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Duration="0" Storyboard.TargetName="PointerOverBorder" Storyboard.TargetProperty="Background">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#FFCFCFCF"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState x:Name="Disabled">
                            </VisualState>
                        </VisualStateGroup>
                        <VisualStateGroup x:Name="SelectionStates">
                            <VisualState x:Name="Unselected"/>
                            <VisualState x:Name="Selected" />
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                    <Border
                            x:Name="TemplateTopSelected" Visibility="Collapsed"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="1,0,1,0"
                            Background="{TemplateBinding SelectedBackground}"
                            CornerRadius="3,3,0,0"
                            Cursor="Arrow">
                        <StackPanel>
                            <Border Height="3" CornerRadius="3,3,0,0" Background="{TemplateBinding SelectedAccent}"/>
                            <ContentControl x:Name="HeaderTopSelected"
                                        Foreground="{TemplateBinding SelectedForeground}"
                                        Margin="{TemplateBinding Padding}"
                                        />
                        </StackPanel>
                    </Border>
                    <Border x:Name="TemplateTopUnselected" Visibility="Collapsed"
                                BorderBrush="{TemplateBinding BorderBrush}"
                                BorderThickness="1,1,1,0"
                                Background="{TemplateBinding Background}"
                                CornerRadius="3,3,0,0"
                                Cursor="{TemplateBinding Cursor}">
                        <StackPanel>
                            <Border x:Name="PointerOverBorder" Height="2" CornerRadius="3,3,0,0" Background="Transparent"/>
                            <ContentControl x:Name="HeaderTopUnselected"
                                        Foreground="{TemplateBinding Foreground}"
                                        Margin="{TemplateBinding Padding}"/>
                        </StackPanel>
                    </Border>
                </StackPanel>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the CheckBox

By default, the CheckBox uses the native HTML appearance.

If you wish to customize its appearance, you can apply a Style with a ControlTemplate.

To do so, you can copy the XAML code for the ToggleButton above, and just change TargetType="ToggleButton" into TargetType="CheckBox" (2 occurrences!). This will work because CheckBox inherits from ToggleButton.



## Styling the ComboBox

By default, the ComboBox is rendered using the standard HTML appearance. This has the advantage of rendering differently depending on the browser. For example, on mobile browsers, a touch-based value picker will appear.

If you want to customize the appearance of the ComboBox control, you need to *set the "UseNativeComboBox" property to "False"*. Here is an example:
```
<ComboBox UseNativeComboBox="False" />
```

You can then apply a Style and even custom ControlTemplate.

Below you will find an example of Style and ControlTemplate for the ComboBox control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the ComboBox parents' resources), and reference it with: <ComboBox UseNativeComboBox="False" Style={StaticResource ComboBoxStyle1}/>
```
<Style x:Key="ComboBoxStyle1" TargetType="ComboBox">
    <Setter Property="Background" Value="White"/>
    <Setter Property="Foreground" Value="Black"/>
    <Setter Property="Padding" Value="6,2,30,2"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="ComboBox">
                <StackPanel>
                    <VisualStateManager.VisualStateGroups>
                    </VisualStateManager.VisualStateGroups>
                    <Border x:Name="OuterBorder"
                            Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}">
                        <Grid>
                            <ToggleButton x:Name="DropDownToggle" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" HorizontalContentAlignment="Right" VerticalContentAlignment="Center">
                                <Path x:Name="arrow" Visibility="Visible" Stroke="Black" Width="13" Height="9" StrokeThickness="3" HorizontalAlignment="Center" VerticalAlignment="Center" Stretch="None" Data="M 1.5,2.25 L 6.75,7.5 L 12,2.25"/>
                            </ToggleButton>
                            <ContentPresenter x:Name="ContentPresenter"
                                              IsHitTestVisible="False"
                                              Margin="{TemplateBinding Padding}"
                                              MinHeight="24"/>
                        </Grid>
                    </Border>
                    <Popup x:Name="Popup" IsOpen="False">
                        <Border Background="White" BorderBrush="Black" BorderThickness="1">
                            <ScrollViewer MaxHeight="{TemplateBinding MaxDropDownHeight}" VerticalScrollBarVisibility="Auto" HorizontalScrollBarVisibility="Disabled">
                                <ItemsPresenter x:Name="ItemsHost" />
                            </ScrollViewer>
                        </Border>
                    </Popup>
                </StackPanel>

            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the ChildWindow

To easily customize the appearance of the ChildWindow control, you can set ChildWindow properties such as: OverlayBrush, OverlayOpacity, Background, Foreground, BorderBrush, BorderThickness, HorizontalContentAlignment, and VerticalContentAlignment.

If you wish to further customize the ChildWindow control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the ChildWindow control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the ChildWindow parents' resources), and reference it with: <ChildWindow Style={StaticResource ChildWindowStyle1} />
```
<Style x:Key="ChildWindowStyle1" TargetType="ChildWindow">
    <Setter Property="HorizontalAlignment" Value="Center" />
    <Setter Property="VerticalAlignment" Value="Center" />
    <Setter Property="HorizontalContentAlignment" Value="Stretch" />
    <Setter Property="VerticalContentAlignment" Value="Stretch" />
    <Setter Property="BorderThickness" Value="1" />
    <Setter Property="BorderBrush" Value="#FFE2E2E2" />
    <Setter Property="OverlayBrush" Value="#7F000000" />
    <Setter Property="OverlayOpacity" Value="1" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="ChildWindow">
                <Grid x:Name="Root">
                    <Grid x:Name="Overlay" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Margin="0" Background="{TemplateBinding OverlayBrush}" Opacity="{TemplateBinding OverlayOpacity}" />
                    <Border x:Name="ContentRoot" HorizontalAlignment="{TemplateBinding HorizontalAlignment}" VerticalAlignment="{TemplateBinding VerticalAlignment}" RenderTransformOrigin="0.5,0.5" Height="{TemplateBinding Height}" Width="{TemplateBinding Width}">
                        <Border x:Name="ContentContainer" Background="#FFF6F6F6" BorderThickness="{TemplateBinding BorderThickness}" BorderBrush="{TemplateBinding BorderBrush}" CornerRadius="2">
                            <Grid>
                                <Grid.RowDefinitions>
                                    <RowDefinition Height="Auto" />
                                    <RowDefinition />
                                </Grid.RowDefinitions>
                                <Border x:Name="Chrome" Width="Auto" BorderBrush="#FFFFFFFF" BorderThickness="0,0,0,1" Background="#FFEEEEEE">
                                    <Grid Height="Auto" Width="Auto">
                                        <Grid.ColumnDefinitions>
                                            <ColumnDefinition />
                                            <ColumnDefinition Width="30" />
                                        </Grid.ColumnDefinitions>
                                        <ContentControl Content="{TemplateBinding Title}" FontWeight="Bold" HorizontalAlignment="Stretch" VerticalAlignment="Center" Margin="6,0,6,0" />
                                        <Button x:Name="CloseButton" Content="X" Grid.Column="1" HorizontalAlignment="Center" VerticalAlignment="Center" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Width="15" Padding="0" />
                                    </Grid>
                                </Border>
                                <Border Background="{TemplateBinding Background}" Margin="7" Grid.Row="1">
                                    <ContentPresenter x:Name="ContentPresenter" Content="{TemplateBinding Content}" ContentTemplate="{TemplateBinding ContentTemplate}" HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}" VerticalAlignment="{TemplateBinding VerticalContentAlignment}" />
                                </Border>
                            </Grid>
                        </Border>
                    </Border>
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the ToolTip

To easily customize the appearance of the ToolTip control, you can set ToolTip properties such as: Background, Foreground, Padding, BorderBrush, BorderThickness, and FontSize.

If you wish to further customize the ToolTip control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the ToolTip control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the ToolTip parents' resources), and reference it with: <ToolTip Style={StaticResource ToolTipStyle1} />
```
<Style x:Key="ToolTipStyle1" TargetType="ToolTip">
    <Setter Property="Foreground" Value="#FF666666"/>
    <Setter Property="Background" Value="White"/>
    <Setter Property="BorderBrush" Value="Gray"/>
    <Setter Property="BorderThickness" Value="2"/>
    <Setter Property="FontSize" Value="12"/>
    <Setter Property="Padding" Value="10,6,10,7"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="ToolTip">
                <Border x:Name="LayoutRoot" BorderBrush="{TemplateBinding BorderBrush}" BorderThickness="{TemplateBinding BorderThickness}" Background="{TemplateBinding Background}">
                    <ContentPresenter ContentTemplate="{TemplateBinding ContentTemplate}" Content="{TemplateBinding Content}" Margin="{TemplateBinding Padding}"/>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the Thumb
To easily customize the appearance of the Thumb control, you can set Thumb properties such as: Background, BorderBrush, and BorderThickness.

If you wish to further customize the Thumb control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the Thumb control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the Thumb parents' resources), and reference it with: <Thumb Style={StaticResource ThumbStyle1} />
```
<Style x:Key="ThumbStyle1" TargetType="Thumb">
    <Setter Property="Background" Value="#FFE2E2E2"/>
    <Setter Property="BorderThickness" Value="0" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Thumb">
                <Grid>
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup x:Name="CommonStates">
                            <VisualState x:Name="Normal"/>
                            <VisualState x:Name="PointerOver">
                                <Storyboard>
                                    <DoubleAnimation Duration="0" To="1" Storyboard.TargetProperty="Opacity" Storyboard.TargetName="BackgroundPointerOver"/>
                                    <DoubleAnimation Duration="0" To="0" Storyboard.TargetProperty="Opacity" Storyboard.TargetName="Background" />
                                </Storyboard>
                            </VisualState>
                            <VisualState x:Name="Pressed">
                                <Storyboard>
                                    <DoubleAnimation Duration="0" To="1" Storyboard.TargetProperty="Opacity" Storyboard.TargetName="BackgroundPressed"/>
                                    <DoubleAnimation Duration="0" To="0" Storyboard.TargetProperty="Opacity" Storyboard.TargetName="Background" />
                                </Storyboard>
                            </VisualState>
                            <VisualState x:Name="Disabled">
                                <Storyboard>
                                    <DoubleAnimation Duration="0" To="1" Storyboard.TargetProperty="Opacity" Storyboard.TargetName="BackgroundDisabled"/>
                                    <DoubleAnimation Duration="0" To="0" Storyboard.TargetProperty="Opacity" Storyboard.TargetName="Background" />
                                </Storyboard>
                            </VisualState>
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                    <Border x:Name="Background"
                            Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"/>
                    <Border x:Name="BackgroundPointerOver"
                            Background="#11000000"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"
                            Opacity="0"/>
                    <Border x:Name="BackgroundPressed"
                            Background="#22000000"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"
                            Opacity="0"/>
                    <Border x:Name="BackgroundDisabled"
                            Background="#33FFFFFF"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"
                            Opacity="0"/>
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the ScrollBar
To easily customize the appearance of the ScrollBar control, you can set ScrollBar properties such as: Background and Foreground.

If you wish to further customize the ScrollBar control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the ScrollBar control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the ScrollBar parents' resources), and reference it with: <ScrollBar Style={StaticResource ScrollBarStyle1} />
```
<Style x:Key="ScrollBarStyle1" TargetType="ScrollBar">
    <Setter Property="Background" Value="#FFF1F1F1" />
    <Setter Property="Foreground" Value="#FF333333" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="ScrollBar">
                <Grid x:Name="Root">
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup x:Name="CommonStates">
                            <VisualState x:Name="Normal" />
                            <VisualState x:Name="PointerOver" />
                            <VisualState x:Name="Disabled" />
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                    <Canvas x:Name="HorizontalRoot" Visibility="Collapsed" Background="{TemplateBinding Background}">
                        <Button x:Name="HorizontalSmallDecrease" Padding="0" Cursor="Arrow">
                            <Path x:Name="ArrowLeft" Fill="#FF555555" Width="9" Height="9" Margin="0,0,3,0" StrokeThickness="3" HorizontalAlignment="Center" VerticalAlignment="Center" Stretch="Fill" Data="M 2,4.5 L 5.5,1 L 5.5,8"/>
                        </Button>
                        <Button x:Name="HorizontalSmallIncrease" Padding="0" Cursor="Arrow">
                            <Path x:Name="ArrowRight" Fill="#FF555555" Width="9" Height="9" Margin="0,0,3,0" StrokeThickness="3" HorizontalAlignment="Center" VerticalAlignment="Center" Stretch="Fill" Data="M 2,1 L 5.5,4.5 L 2,8"/>
                        </Button>
                        <Button x:Name="HorizontalLargeDecrease" Padding="0" Cursor="Arrow" />
                        <Button x:Name="HorizontalLargeIncrease" Padding="0" Cursor="Arrow" />
                        <Thumb x:Name="HorizontalThumb" Cursor="Arrow">
                            <Thumb.Template>
                                <ControlTemplate>
                                    <Border Background="#FFBBBBBB"/>
                                </ControlTemplate>
                            </Thumb.Template>
                        </Thumb>
                    </Canvas>
                    <Canvas x:Name="VerticalRoot" Visibility="Collapsed" Background="{TemplateBinding Background}">
                        <Button x:Name="VerticalSmallDecrease" Padding="0" Cursor="Arrow">
                            <Path x:Name="ArrowTop" Fill="#FF555555" Width="9" Height="9" Margin="0,0,3,0" StrokeThickness="3" HorizontalAlignment="Center" VerticalAlignment="Center" Stretch="Fill" Data="M 4.5,2 L 1,5.5 L 8,5.5"/>
                        </Button>
                        <Button x:Name="VerticalSmallIncrease" Padding="0" Cursor="Arrow">
                            <Path x:Name="ArrowBottom" Fill="#FF555555" Width="9" Height="9" Margin="0,0,3,0" StrokeThickness="3" HorizontalAlignment="Center" VerticalAlignment="Center" Stretch="Fill" Data="M 1,2 L 4.5,5.5 L 8,2"/>
                        </Button>
                        <Button x:Name="VerticalLargeDecrease" Padding="0" Cursor="Arrow" />
                        <Button x:Name="VerticalLargeIncrease" Padding="0" Cursor="Arrow" />
                        <Thumb x:Name="VerticalThumb" Background="#FFBBBBBB" Cursor="Arrow">
                            <Thumb.Template>
                                <ControlTemplate>
                                    <Border Background="#FFBBBBBB"/>
                                </ControlTemplate>
                            </Thumb.Template>
                        </Thumb>
                    </Canvas>
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the MenuItem
To easily customize the appearance of the MenuItem control, you can set MenuItem properties such as: Background, Foreground, Padding, BorderBrush, BorderThickness, HorizontalContentAlignment, and VerticalContentAlignment.

If you wish to further customize the MenuItem control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the MenuItem control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the MenuItem parents' resources), and reference it with: <MenuItem Style={StaticResource MenuItemStyle1} />
```
<Style x:Key="MenuItemStyle1" TargetType="MenuItem">
    <Setter Property="Background" Value="#FFE2E2E2"/>
    <Setter Property="Foreground" Value="Black"/>
    <Setter Property="BorderThickness" Value="0"/>
    <Setter Property="Padding" Value="12,4,12,4"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="HorizontalContentAlignment" Value="Center"/>
    <Setter Property="VerticalContentAlignment" Value="Center"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="MenuItem">
                <Border x:Name="OuterBorder"
                            Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}">
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup Name="CommonStates">
                            <VisualState Name="Normal">
                            </VisualState>
                            <VisualState Name="PointerOver">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#11000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="Pressed">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#22000000"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState Name="Disabled">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="Background" Storyboard.TargetName="InnerBorder">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="#33FFFFFF"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                    <Border x:Name="InnerBorder"
                                Background="{TemplateBinding Background}">
                        <StackPanel Orientation="Horizontal" Margin="{TemplateBinding Padding}">
                            <ContentPresenter x:Name="IconPresenter"
                                                  Content="{TemplateBinding Icon}"
                                                  Margin="0,0,10,0"
                                                  VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
                                                  Visibility="{TemplateBinding INTERNAL_IconVisibility}"
                                              />
                            <ContentPresenter x:Name="ContentPresenter"
                                                  ContentTemplate="{TemplateBinding ContentTemplate}"
                                                  Content="{TemplateBinding Content}"
                                                  HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                                  VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
                        </StackPanel>
                    </Border>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the GridSplitter
To easily customize the appearance of the GridSplitter control, you can set GridSplitter properties such as Background.

If you wish to further customize the GridSplitter control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the GridSplitter control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the GridSplitter parents' resources), and reference it with: <GridSplitter Style={StaticResource GridSplitterStyle1} />
```
<Style x:Key="GridSplitterStyle1" TargetType="GridSplitter">
    <Setter Property="Background" Value="#FFF0F0F0"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="GridSplitter">
                <Grid Background="{TemplateBinding Background}">
                    <Thumb x:Name="PART_Thumb" Opacity="0" /> <!-- Note: the thumb control is here only in OpenSilver -->
                    <ContentPresenter Content="{TemplateBinding Element}" IsHitTestVisible="False" />
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the NumericUpDown
To customize the NumericUpDown control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the NumericUpDown control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the NumericUpDown parents' resources), and reference it with: <NumericUpDown Style={StaticResource NumericUpDownStyle1} />
```
<Style x:Key="NumericUpDownStyle1"
    TargetType="NumericUpDown">
    <Setter
        Property="Template">
        <Setter.Value>
            <ControlTemplate
                TargetType="NumericUpDown">
                <Border
                    Background="{TemplateBinding Background}"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding BorderThickness}"
                    VerticalAlignment="Top">
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition />
                            <ColumnDefinition
                                Width="Auto" />
                            <ColumnDefinition
                                Width="Auto" />
                        </Grid.ColumnDefinitions>
                        <Grid
                            Grid.Column="0">
                            <Rectangle
                                x:Name="PART_ValueBar"
                                IsHitTestVisible="False"
                                Fill="#FFFFFFFF" />
                            <UpDownTextBox
                                x:Name="PART_ValueTextBox"
                                FontFamily="{TemplateBinding FontFamily}"
                                FontSize="{TemplateBinding FontSize}"
                                Foreground="{TemplateBinding Foreground}"
                                HorizontalAlignment="Stretch"
                                    />
                        </Grid>
                        <RepeatButton
                            Grid.Column="1"
                            x:Name="PART_DecrementButton"
                            Margin="4,0,0,0"
                            Content="&#x2796;" />
                        <RepeatButton
                            Grid.Column="2"
                            x:Name="PART_IncrementButton"
                            Margin="3,0,0,0"
                            Content="&#x2795;" />
                    </Grid>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

Alternatively, here is a more Silverlight-like style:

```
<Style x:Key="NumericUpDownSilverlightLikeStyle" TargetType="NumericUpDown">
    <Setter Property="Margin" Value="2,0,2,0"/>
    <Setter
        Property="Template">
        <Setter.Value>
            <ControlTemplate
                TargetType="NumericUpDown">
                <Border
                    Background="{TemplateBinding Background}"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding BorderThickness}"
                    VerticalAlignment="Top">
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*" />
                            <ColumnDefinition Width="Auto" />
                        </Grid.ColumnDefinitions>
                        <Grid.RowDefinitions>
                            <RowDefinition Height="0.5*"/>
                            <RowDefinition Height="0.5*"/>
                        </Grid.RowDefinitions>
                        <Grid
                            Grid.Column="0"
                            Grid.RowSpan="2">
                            <UpDownTextBox
                                x:Name="PART_ValueTextBox"
                                FontFamily="{TemplateBinding FontFamily}"
                                FontSize="{TemplateBinding FontSize}"
                                Foreground="{TemplateBinding Foreground}"
                                HorizontalAlignment="Stretch"
                                VerticalAlignment="Top"
                                    />
                        </Grid>
                        <RepeatButton
                            Grid.Column="1"
                            Grid.Row="0"
                            FontSize="10"
                            FontFamily="Verdana"
                            x:Name="PART_IncrementButton"
                            Content="&#x2795;"
                            Interval="50"
                            Padding="2,0,3,0"
                            Margin="2,0,2,0"/>
                        <RepeatButton
                            Grid.Column="1"
                            Grid.Row="1"
                            FontSize="10"
                            FontFamily="Verdana"
                            x:Name="PART_DecrementButton"
                            Content="&#x2796;"
                            Interval="50"
                            Padding="2,0,3,0"
                            Margin="2,2,2,0"/>
                    </Grid>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the DatePicker
To customize the DatePicker control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the DatePicker control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the DatePicker parents' resources), and reference it with: <DatePicker Style={StaticResource DatePickerStyle1} />
```
<Style x:Key="DatePickerStyle1" TargetType="DatePicker">
    <Setter Property="BorderBrush" Value="Gray"/>
    <Setter Property="BorderThickness" Value="1" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="DatePicker">
                <Grid x:Name="Root">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*"/>
                        <ColumnDefinition Width="Auto"/>
                    </Grid.ColumnDefinitions>
                    <TextBox x:Name="TextBox" Grid.Column="0" Background="{TemplateBinding Background}" BorderBrush="{TemplateBinding BorderBrush}" BorderThickness="{TemplateBinding BorderThickness}" Padding="{TemplateBinding Padding}" />
                    <Button x:Name="Button" Grid.Column="1" Content="▼" Width="30" Foreground="{TemplateBinding Foreground}" BorderBrush="{TemplateBinding BorderBrush}" BorderThickness="{TemplateBinding BorderThickness}" Margin="2,0,2,0"/>
                    <Popup x:Name="Popup" VerticalAlignment="Bottom"/>
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the TimePicker
To customize the TimePicker control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the TimePicker control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the TimePicker parents' resources), and reference it with: <TimePicker Style={StaticResource TimePickerStyle1} />
```
<Style x:Key="TimePickerStyle1" TargetType="TimePicker">
    <Setter Property="BorderBrush" Value="Gray"/>
    <Setter Property="BorderThickness" Value="1" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="TimePicker">
                <Grid x:Name="Root">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*"/>
                        <ColumnDefinition Width="Auto"/>
                    </Grid.ColumnDefinitions>
                    <TextBox x:Name="TextBox" Grid.Column="0" Background="{TemplateBinding Background}" BorderBrush="{TemplateBinding BorderBrush}" BorderThickness="{TemplateBinding BorderThickness}" Padding="{TemplateBinding Padding}" />
                    <Button x:Name="Button" Grid.Column="1" Content="▼" Width="30" Foreground="{TemplateBinding Foreground}" BorderBrush="{TemplateBinding BorderBrush}" BorderThickness="{TemplateBinding BorderThickness}" Margin="2,0,2,0"/>
                    <Popup x:Name="Popup" VerticalAlignment="Bottom"/>
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Styling the HyperlinkButton
To customize the HyperlinkButton control, you can apply a custom ControlTemplate.

Below you will find a sample Style and ControlTemplate for the HyperlinkButton control. To use it, you can place the code in the XAML resources (either App.xaml or anywhere in the TimePicker parents' resources), and reference it with: <HyperlinkButton Style={StaticResource HyperlinkButtonStyle1} />
```
<Style x:Key="HyperlinkButtonStyle1" TargetType="HyperlinkButton">
    <Setter Property="Foreground" Value="#0000EE"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="HorizontalContentAlignment" Value="Center"/>
    <Setter Property="VerticalContentAlignment" Value="Center"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="HyperlinkButton">
                <Border x:Name="OuterBorder"
                            Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}">
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup Name="CommonStates">
                            <VisualState Name="Normal">
                            </VisualState>
                            <VisualState Name="PointerOver">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetProperty="FontWeight" Storyboard.TargetName="ContentPresenter">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="Bold"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                        </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                    <ContentPresenter x:Name="ContentPresenter"
                              ContentTemplate="{TemplateBinding ContentTemplate}"
                              Content="{TemplateBinding Content}"
                              Margin="{TemplateBinding Padding}"
                              HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                              VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>        
</Style>
```



## Contact Us

Please [click here](https://opensilver.net/contact.aspx) for contact information.
