# WPF Material Forms
A windows and dialogs library using Material Design In XAML Toolkit and MahApps.Metro.

[NuGet](https://www.nuget.org/packages/WpfMaterialForms) - ```Install-Package WpfMaterialForms```

The dialogs and windows are generated dynamically from data schemas. The API is aimed to be detached from XAML/WPF and the underlying libraries.

Check out MaterialForms.WpfDemo for easy to follow examples.

## How to use
### In a WPF project

In your App.xaml you need to have the following resources included. If you are using Material Design in XAML for you UI you will have those already declared (the color theme does not matter).
```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Yellow.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

### Outside of WPF (WinForms, Console)
You need to call this method only once before creating any Material window or dialog (in Main or somewhere during startup):
```cs
MaterialApplication.CreateApplication();
```

If you need a message loop you can call ```MaterialApplication.RunDispatcher();```

Before stopping your application you need to shut down explicitly:
```cs
MaterialApplication.ShutDownApplication();
```

### API guide
The ```MaterialDialog``` class describes displayed dialog contents ```{ Title, Message, Form, PositiveAction, NegativeAction, ... }```

The ```MaterialForm``` class is a list of data schemas that display as controls inside the dialogs. Schemas can hold values and can be given keys. You can access schema values in different ways:
```cs
string username = (string)Form["username"];
Dictionary<string, object> dictionary = Form.GetValuesDictionary();
List<object> schemaValues = Form.GetValuesList();
```

Dialogs can be hosted in two contexts: in a new WPF Window or in an existing DialogHost.

To host within a DialogHost, call ```await dialog.Show("DialogIdentifier")``` where "DialogIdentifier" is the ```DialogHost.Identifer``` that you specify in your window.

To host within a new Window, create a ```new MaterialWindow(dialog)```. MaterialWindow abstracts WPF Window properties and binds them automatically ```{ Title, Width, Height, ShowCloseButton, ... }```. To show the created window call ```await window.Show()```.

The async Show() method returns a bool? value. Usually true represents positive action click, false negative action click, and null that the dialog has been closed by other means.

The ShowTracked() method returns a ```Session``` object, which you can use to close the dialog host from code. If a dialog has been shown using ShowTracked, you can await its session.Task.

## Examples
### Basic dialogs
```cs
await WindowFactory.Alert("Hello World!").Show();
bool? result = await WindowFactory.Prompt("Delete item?").Show();
```

### Customized dialogs
```cs
{
    Title = "Please log in to continue",
    PositiveAction = "LOG IN",
    Form = new MaterialForm
    {
        new StringSchema
        {
            Name = "Username",
            IconKind = IconKind.Account
        },
        new PasswordSchema
        {
            Name = "Password",
            IconKind = IconKind.Key
        },
        new BooleanSchema
        {
            Name = "Remember me",
            IsCheckBox = true
        }
    }
}
```

![login](https://github.com/EdonGashi/WpfMaterialForms/blob/master/doc/login.png)

```cs
{
    Title = "Settings",
    Form = new MaterialForm
    {
        new CaptionSchema
        {
            Name = "Connectivity"
        },
        new BooleanSchema
        {
            Name = "WiFi",
            IconKind = IconKind.Wifi,
            Value = true
        },
        new BooleanSchema
        {
            Name = "Mobile Data",
            IconKind = IconKind.Signal
        },
        new CaptionSchema
        {
            Name = "Device"
        },
        new NumberRangeSchema
        {
            Name = "Volume",
            IconKind = IconKind.VolumeHigh,
            MinValue = 0,
            MaxValue = 10,
            Value = 5
        },
        new KeyValueSchema
        {
            Name = "Ringtone",
            Value = "Over the horizon",
            IconKind = IconKind.MusicNote
        }
    }
}
```

![settings](https://github.com/EdonGashi/WpfMaterialForms/blob/master/doc/settings.png)

```cs
{
    Title = "Send e-mail",
    PositiveAction = "SEND",
    Form = new MaterialForm
    {
        new StringSchema
        {
            Name = "To",
            IconKind = IconKind.Email
        },
        new StringSchema
        {
            Name = "Message",
            IsMultiLine = true,
            IconKind = IconKind.Comment
        }
    }
}
```

![email](https://github.com/EdonGashi/WpfMaterialForms/blob/master/doc/email.png)

```cs
{
    Message = "Discard draft?",
    PositiveAction = "DISCARD"
}
```

![dialog](https://github.com/EdonGashi/WpfMaterialForms/blob/master/doc/dialog.png)
