# WPF Material Forms
Work in progress of a windows and dialogs library using Material Design In XAML Toolkit and MahApps.Metro.

The dialogs and windows are generated dynamically from data schemas. The API is aimed to be detached from XAML/WPF and the underlying libraries.

## How to use
### In a WPF project
In your App.xaml you need to have the following resources included. If you are using Material Design in XAML for you UI you will have those already declared.
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

If you need a message loop you can call ```cs MaterialApplication.RunDispatcher(); ```

Before stopping your application you need to shut down explicitly:
```cs
MaterialApplication.ShutDownApplication();
```

## Examples

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
