# Storyboards and Animations

## Introduction
With OpenSilver you can create Storyboards and Animations either programmatically or in XAML.

## In XAML
Here is some sample XAML code for animations:

```
<Canvas x:Name="CanvasForAnimationsDemo" Width="350" Height="50" Margin="0,10,0,0">
    <Canvas.Resources>
        <Storyboard x:Key="AnimationToOpen">
            <DoubleAnimation Duration="0:0:1" To="70" Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.TranslateX)" Storyboard.TargetName="Item1">
                <DoubleAnimation.EasingFunction>
                    <QuarticEase EasingMode="EaseOut"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Duration="0:0:1" To="140" Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.TranslateX)" Storyboard.TargetName="Item2">
                <DoubleAnimation.EasingFunction>
                    <QuarticEase EasingMode="EaseOut"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Duration="0:0:1" To="210" Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.TranslateX)" Storyboard.TargetName="Item3">
                <DoubleAnimation.EasingFunction>
                    <QuarticEase EasingMode="EaseOut"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Duration="0:0:1" To="280" Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.TranslateX)" Storyboard.TargetName="Item4">
                <DoubleAnimation.EasingFunction>
                    <QuarticEase EasingMode="EaseOut"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
        </Storyboard>
        <Storyboard x:Key="AnimationToClose">
            <DoubleAnimation Duration="0:0:1" To="0" Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.TranslateX)" Storyboard.TargetName="Item1">
                <DoubleAnimation.EasingFunction>
                    <QuarticEase EasingMode="EaseOut"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Duration="0:0:1" To="0" Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.TranslateX)" Storyboard.TargetName="Item2">
                <DoubleAnimation.EasingFunction>
                    <QuarticEase EasingMode="EaseOut"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Duration="0:0:1" To="0" Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.TranslateX)" Storyboard.TargetName="Item3">
                <DoubleAnimation.EasingFunction>
                    <QuarticEase EasingMode="EaseOut"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Duration="0:0:1" To="0" Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.TranslateX)" Storyboard.TargetName="Item4">
                <DoubleAnimation.EasingFunction>
                    <QuarticEase EasingMode="EaseOut"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
        </Storyboard>
    </Canvas.Resources>
    <Button x:Name="Item1" Content="Item1" Width="70" Height="50" Foreground="White" Background="#FFE44D26" RenderTransformOrigin="0.5,0.5">
        <Button.RenderTransform>
            <CompositeTransform/>
        </Button.RenderTransform>
    </Button>
    <Button x:Name="Item2" Content="Item2" Width="70" Height="50" Foreground="White" Background="#FFE44D26" RenderTransformOrigin="0.5,0.5">
        <Button.RenderTransform>
            <CompositeTransform/>
        </Button.RenderTransform>
    </Button>
    <Button x:Name="Item3" Content="Item3" Width="70" Height="50" Foreground="White" Background="#FFE44D26" RenderTransformOrigin="0.5,0.5">
        <Button.RenderTransform>
            <CompositeTransform/>
        </Button.RenderTransform>
    </Button>
    <Button x:Name="Item4" Content="Item4" Width="70" Height="50" Foreground="White" Background="#FFE44D26" RenderTransformOrigin="0.5,0.5">
        <Button.RenderTransform>
            <CompositeTransform/>
        </Button.RenderTransform>
    </Button>
    <Button x:Name="ButtonToStartAnimationClose" Content="Close" Visibility="Collapsed" Width="70" Height="50" Click="ButtonToStartAnimationClose_Click" Foreground="White" Background="#FFE44D26" />
    <Button x:Name="ButtonToStartAnimationOpen" Content="Start" Width="70" Height="50" Click="ButtonToStartAnimationOpen_Click" Foreground="White" Background="#FFE44D26" />
</Canvas>
```
Here is the code-behind:

```
void ButtonToStartAnimationOpen_Click(object sender, RoutedEventArgs e)
{
    var storyboard = (Storyboard)CanvasForAnimationsDemo.Resources["AnimationToOpen"];
    storyboard.Begin();
    ButtonToStartAnimationOpen.Visibility = Visibility.Collapsed;
    ButtonToStartAnimationClose.Visibility = Visibility.Visible;
}

void ButtonToStartAnimationClose_Click(object sender, RoutedEventArgs e)
{
    var storyboard = (Storyboard)CanvasForAnimationsDemo.Resources["AnimationToClose"];
    storyboard.Begin();
    ButtonToStartAnimationOpen.Visibility = Visibility.Visible;
    ButtonToStartAnimationClose.Visibility = Visibility.Collapsed;
}
```

## Programmatically:
Here is some sample C# code for creating and starting Storyboards programmatically (no XAML):

```
DoubleAnimation doubleAnimation = new DoubleAnimation();
doubleAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 3, 0));
Storyboard storyBoard = new Storyboard();
storyBoard.Children.Add(doubleAnimation);
Storyboard.SetTarget(doubleAnimation, ELEMENT_TO_ANIMATE_GOES_HERE);
Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(FrameworkElement.WidthProperty));
doubleAnimation.To = 500;
storyBoard.Begin();
storyBoard.Completed += StoryBoard_Completed;
void StoryBoard_Completed(object sender, EventArgs e)
{
    MessageBox.Show("The Storyboard has finished running.");
}
```


## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
