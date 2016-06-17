# WPF Material Forms
Work in progress of a windows and dialogs library using Material Design In XAML Toolkit and MahApps.Metro.

The dialogs and windows are generated dynamically from data schemas. The API is aimed to be detached from XAML/WPF and the underlying libraries.

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
    Subheading = "Discard draft?",
    PositiveAction = "DISCARD"
}
```

![dialog](https://github.com/EdonGashi/WpfMaterialForms/blob/master/doc/dialog.png)
