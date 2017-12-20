## `DialogBase`

```csharp
public abstract class MaterialForms.Wpf.Forms.Base.DialogBase
    : FormBase, IActionHandler, INotifyPropertyChanged

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Confirmed |  | 
| `String` | Message |  | 
| `String` | NegativeAction |  | 
| `String` | PositiveAction |  | 
| `String` | Title |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | OnAction(`String` action, `Object` parameter) |  | 


## `FormBase`

```csharp
public abstract class MaterialForms.Wpf.Forms.Base.FormBase
    : IActionHandler, INotifyPropertyChanged

```

Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `EventHandler<ActionEventArgs>` | ActionPerformed |  | 
| `PropertyChangedEventHandler` | PropertyChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | HandleAction(`Object` model, `String` action, `Object` parameter) |  | 
| `void` | OnAction(`String` action, `Object` parameter) |  | 
| `void` | OnPropertyChanged(`String` propertyName = null) |  | 
| `String` | ToString() |  | 


