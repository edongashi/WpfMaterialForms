## `ActionEventArgs`

```csharp
public class MaterialForms.Wpf.Forms.ActionEventArgs
    : EventArgs

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Action |  | 


## `Alert`

```csharp
public class MaterialForms.Wpf.Forms.Alert
    : DialogBase, IActionHandler, INotifyPropertyChanged

```

## `Confirm`

```csharp
public class MaterialForms.Wpf.Forms.Confirm
    : DialogBase, IActionHandler, INotifyPropertyChanged

```

## `Prompt<T>`

```csharp
public class MaterialForms.Wpf.Forms.Prompt<T>
    : DialogBase, IActionHandler, INotifyPropertyChanged

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Nullable<PackIconKind>` | Icon |  | 
| `String` | Name |  | 
| `String` | ToolTip |  | 
| `T` | Value |  | 


