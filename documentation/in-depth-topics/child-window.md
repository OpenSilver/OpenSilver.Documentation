# How to use the ChildWindow

## How to create and display a ChildWindow

1. Create a new ChildWindow by right-clicking on your project in the Solution Explorer, and clicking Add => New Item => OpenSilver => Child Window.

2. To make the ChildWindow appear, use the following code:
```
var childWindow1 = new ChildWindow1();
childWindow1.Show();
```

## Notes and other features

* If you want, you can give a title to your ChildWindow by setting it in XAML, like so: <ChildWindow Title="My Child Window" ...

* It is recommended that you give a fixed size (in pixels) to your ChildWindow, otherwise it may not show properly. You can do so by setting the size in XAML, like this: `<ChildWindow Width="300" Height="200"...`

* you want, you can register the "Closed" event to retrieve the dialog result. Here is how:
```
childWindow1.Closed += new EventHandler(ChildWindow1_Closed);

...
void ChildWindow1_Closed(object sender, EventArgs e)
{
    var childWindow1 = (ChildWindow1)sender;
    MessageBox.Show("The result is: " + childWindow1.DialogResult.ToString());
}
```

* You can set the result from within the ChildWindow using the following code:
`this.DialogResult = true; // This will automatically close the ChildWindow.`

* If you want to pass a value other than boolean, you can add a property to your ChildWindow implementation and set it to the desired value.
If you want, in your ChildWindow code, you can register the "Closing" event to check for data validity, and abort the closing operation by setting e.Cancel = true;

* You can customize the appearance of the ChildWindow by custimizing its style and template. You can find the default style and template, [here](styles-and-templates.html#styling-the-childwindow). For more information on how to set the style and template of a control,[ follow this link](styles-and-templates.html).


## Related Topics
[Styles and Templates](styles-and-templates.html)


## Contact Us
Please [click here](https://opensilver.net/contact.aspx) for contact information.
