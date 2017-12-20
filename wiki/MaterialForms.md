## `BooleanSchema`

```csharp
public class MaterialForms.BooleanSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `Boolean` | IsCheckBox |  | 
| `Boolean` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `CaptionSchema`

```csharp
public class MaterialForms.CaptionSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 


## `ColorAssist`

```csharp
public class MaterialForms.ColorAssist

```

Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DependencyProperty` | DisabledOpacityProperty |  | 
| `DependencyProperty` | ForegroundProperty |  | 
| `DependencyProperty` | OpacityProperty |  | 


## `CommandArgs`

```csharp
public class MaterialForms.CommandArgs

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `MaterialForm` | CommandForm |  | 
| `CommandSchema` | Sender |  | 
| `Session` | Session |  | 


## `CommandSchema`

```csharp
public class MaterialForms.CommandSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Action<CommandArgs>` | Callback |  | 
| `ICommand` | Command |  | 
| `String` | CommandHint |  | 
| `MaterialForm` | Form |  | 
| `Boolean` | HoldsValue |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 


## `DateSchema`

```csharp
public class MaterialForms.DateSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `Nullable<DateTime>` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `DelegateCommand`

```csharp
public class MaterialForms.DelegateCommand
    : ICommand

```

Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `EventHandler` | CanExecuteChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | CanExecute(`Object` parameter) |  | 
| `void` | Execute(`Object` parameter) |  | 
| `void` | RaiseCanExecuteChanged() |  | 


## `DialogActionListener`

```csharp
public class MaterialForms.DialogActionListener

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ActionPerformed |  | 


## `DialogFactory`

```csharp
public static class MaterialForms.DialogFactory

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `MaterialDialog` | Alert(`String` message) |  | 
| `MaterialDialog` | Alert(`String` message, `String` title) |  | 
| `MaterialDialog` | Alert(`String` message, `String` title, `String` action) |  | 
| `MaterialDialog` | FromSingleSchema(`SchemaBase` schema) |  | 
| `MaterialDialog` | FromSingleSchema(`String` message, `SchemaBase` schema) |  | 
| `MaterialDialog` | FromSingleSchema(`String` message, `String` title, `SchemaBase` schema) |  | 
| `MaterialDialog` | FromSingleSchema(`String` message, `String` title, `String` positiveAction, `SchemaBase` schema) |  | 
| `MaterialDialog` | FromSingleSchema(`String` message, `String` title, `String` positiveAction, `String` negativeAction, `SchemaBase` schema) |  | 
| `MaterialDialog` | Prompt(`String` message) |  | 
| `MaterialDialog` | Prompt(`String` message, `String` title) |  | 
| `MaterialDialog` | Prompt(`String` message, `String` title, `String` positiveAction) |  | 
| `MaterialDialog` | Prompt(`String` message, `String` title, `String` positiveAction, `String` negativeAction) |  | 


## `DialogSession`

```csharp
public class MaterialForms.DialogSession
    : Session

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `MaterialDialog` | Dialog |  | 
| `Task<Nullable<Boolean>>` | Task |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Close(`Nullable<Boolean>` result) |  | 
| `DialogSession` | Show() |  | 


## `DialogTheme`

Represents the color theme of the displayed dialog.
```csharp
public enum MaterialForms.DialogTheme
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | Inherit | Inherits the scheme used by the application. | 
| `1` | Light | Light background with dark foreground color. | 
| `2` | Dark | Dark background with light foreground color. | 


## `DispatcherOption`

```csharp
public enum MaterialForms.DispatcherOption
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | CurrentThread |  | 
| `1` | CurrentApplication |  | 
| `2` | Custom |  | 


## `IntegerSchema`

```csharp
public class MaterialForms.IntegerSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Required |  | 


Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | GreaterThanMaximumMessage |  | 
| `Boolean` | HoldsValue |  | 
| `String` | LessThanMinimumMessage |  | 
| `Int32` | MaxValue |  | 
| `Int32` | MinValue |  | 
| `String` | RequiredMessage |  | 
| `Nullable<Int32>` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `Boolean` | OnValidation() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `IViewProvider`

```csharp
public interface MaterialForms.IViewProvider
    : INotifyPropertyChanged

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | View |  | 


## `KeyValueSchema`

```csharp
public class MaterialForms.KeyValueSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `String` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `MaterialApplication`

```csharp
public static class MaterialForms.MaterialApplication

```

Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Dispatcher` | CustomDispatcher |  | 
| `DispatcherOption` | DefaultDispatcher |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | CheckDispatcherAccess() |  | 
| `Boolean` | CheckDispatcherAccess(`DispatcherOption` dispatcherOption) |  | 
| `void` | CreateApplication() |  | 
| `Dispatcher` | GetCustomDispatcher() |  | 
| `Dispatcher` | GetDispatcher(`DispatcherOption` dispatcherOption) |  | 
| `void` | LoadMaterialDesign(`Application` application) |  | 
| `void` | RunDispatcher() |  | 
| `void` | SetDefaultDispatcher(`DispatcherOption` dispatcherOption) |  | 
| `void` | ShutDownApplication() |  | 
| `void` | ShutDownCustomDispatcher() |  | 


## `MaterialDialog`

```csharp
public class MaterialForms.MaterialDialog
    : IViewProvider, INotifyPropertyChanged

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | AuxiliaryAction | Gets or sets the text that appears on the auxiliary dialog button. A null or empty value hides the button. | 
| `MaterialForm` | Form |  | 
| `String` | Message | Gets or sets the message that appears in the dialog. | 
| `String` | NegativeAction | Gets or sets the text that appears on the negative dialog button. A null or empty value hides the button. | 
| `FormActionCallback` | OnAuxiliaryAction | Gets or sets the callback that will be invoked when the auxiliary button is clicked by the user. | 
| `FormActionCallback` | OnNegativeAction | Gets or sets the callback that will be invoked when the negative button is clicked by the user.  When this property is not null, the dialog must be closed explicitly using Session.Close(result). | 
| `FormActionCallback` | OnPositiveAction | Gets or sets the callback that will be invoked when the positive button is clicked by the user.  When this property is not null, the dialog must be closed explicitly using Session.Close(result). | 
| `String` | PositiveAction | Gets or sets the text that appears on the positive dialog button. A null or empty value hides the button. | 
| `Boolean` | ShowsProgressOnPositiveAction | Gets or sets whether to show a progress indicator while performing the positive dialog action.  Applies only when a custom handler is assigned to the PositiveAction property. | 
| `DialogTheme` | Theme | Gets or sets the dialog theme. | 
| `String` | Title | Gets or sets the title that appears in the dialog. This is not the same as the window title. | 
| `Boolean` | ValidatesOnPositiveAction | Gets or sets whether to perform form validation before handling the positive dialog action. | 
| `UserControl` | View | Gets a new instance of a DialogView bound to this object. | 


Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `PropertyChangedEventHandler` | PropertyChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | OnPropertyChanged(`String` propertyName = null) |  | 
| `Task<Nullable<Boolean>>` | Show(`String` dialogIdentifier) |  | 
| `Task<Nullable<Boolean>>` | Show(`String` dialogIdentifier, `Double` width) |  | 
| `DialogSession` | ShowTracked(`String` dialogIdentifier) |  | 
| `DialogSession` | ShowTracked(`String` dialogIdentifier, `Double` width) |  | 
| `Boolean` | Validate() |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DialogTheme` | DefaultDialogTheme | Represents the default theme used by new dialogs. | 


## `MaterialForm`

```csharp
public class MaterialForms.MaterialForm
    : ObservableCollection<SchemaBase>, IList<SchemaBase>, ICollection<SchemaBase>, IEnumerable<SchemaBase>, IEnumerable, IList, ICollection, IReadOnlyList<SchemaBase>, IReadOnlyCollection<SchemaBase>, INotifyCollectionChanged, INotifyPropertyChanged

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Int32` | FocusedSchema | Gets or sets the value that indicates which form element should receive focus  when the form is displayed. A negative value disables this feature. | 
| `Object` | Item | Gets or sets the value of the schema with the specified key. | 
| `UserControl` | View | Gets a new view bound to the current state of the object. Use this if you need to host your forms manually. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `T` | Bind() |  | 
| `T` | Bind(`T` instance) |  | 
| `SchemaBase` | GetSchema(`String` key) |  | 
| `Dictionary<String, Object>` | GetValuesDictionary() | Gets all form key-value pairs as a dictionary. Schemas that have no key or cannot hold values are excluded. | 
| `Dictionary<String, Object>` | GetValuesDictionaryFromNames() | Gets all form name-value pairs as a dictionary. Schemas that have no name or cannot hold values are excluded. | 
| `Object` | GetValuesDynamic() | Gets all form key-value pairs as a dynamic object with keys as property names. Schemas that have no key or cannot hold values are excluded. | 
| `List<Object>` | GetValuesList() | Gets all form values as an indexed list. Schemas that cannot hold values are excluded. | 
| `Boolean` | Validate() |  | 
| `IEnumerable<SchemaBase>` | ValidKeySchemas() |  | 


## `MaterialWindow`

```csharp
public class MaterialForms.MaterialWindow
    : INotifyPropertyChanged

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | CanResize |  | 
| `MaterialDialog` | Dialog |  | 
| `Double` | Height |  | 
| `Boolean` | ShowCloseButton |  | 
| `Boolean` | ShowMaxRestoreButton |  | 
| `Boolean` | ShowMinButton |  | 
| `String` | Title |  | 
| `Double` | Width |  | 


Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `PropertyChangedEventHandler` | PropertyChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | OnPropertyChanged(`String` propertyName = null) |  | 
| `Task<Nullable<Boolean>>` | Show() |  | 
| `Task<Nullable<Boolean>>` | Show(`DispatcherOption` dispatcherOption) |  | 
| `WindowSession` | ShowTracked() |  | 
| `WindowSession` | ShowTracked(`DispatcherOption` dispatcherOption) |  | 


## `MultiSchema`

Allows adding multiple schemas within the same row.
```csharp
public class MaterialForms.MultiSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `IEnumerable<Double>` | RelativeColumnWidths | Gets the star size width of each schema in their respective index.  Default width for each element is one unit. | 
| `SchemaBase[]` | Schemas | Gets the schemas being presented. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `MaterialForm` | GetForm() |  | 
| `Object` | GetValue() |  | 
| `Boolean` | OnValidation() |  | 


## `NumberRangeSchema`

```csharp
public class MaterialForms.NumberRangeSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `Int32` | MaxValue |  | 
| `Int32` | MinValue |  | 
| `Int32` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `PasswordHelpers`

```csharp
public static class MaterialForms.PasswordHelpers

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | ConvertToUnsecureString(`SecureString` securePassword) |  | 


## `PasswordSchema`

```csharp
public class MaterialForms.PasswordSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `SecureString` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 


## `ProgressSchema`

```csharp
public class MaterialForms.ProgressSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `Boolean` | IsIndeterminate |  | 
| `Int32` | Maximum |  | 
| `Double` | Progress |  | 
| `Boolean` | ShowAbsolute |  | 
| `Boolean` | ShowPercentage |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `SchemaBase`

Base class for all schemas. Custom data types and controls can be implemented by extending this class.
```csharp
public abstract class MaterialForms.SchemaBase
    : IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Error |  | 


Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Description | Gets or sets the description of the schema, usually appearing as a tooltip on mouse over. | 
| `Boolean` | HasErrors |  | 
| `Boolean` | HasNoError |  | 
| `Boolean` | HoldsValue |  | 
| `PackIconKind` | IconKind | Gets or sets the icon type shown in the control. Some controls do not display icons. | 
| `Visibility` | IconVisibility |  | 
| `String` | Key | Gets or sets the key that identifies the value in the form. Does not appear in the UI. | 
| `String` | Name | Gets or sets the display name of the schema, usually appearing as a control hint or label. | 
| `Boolean` | ValidatesOnValueChanged |  | 
| `UserControl` | View |  | 


Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `EventHandler<DataErrorsChangedEventArgs>` | ErrorsChanged |  | 
| `PropertyChangedEventHandler` | PropertyChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `IEnumerable` | GetErrors(`String` propertyName) |  | 
| `Object` | GetValue() |  | 
| `void` | Invalidate(`String` message) |  | 
| `Boolean` | IsValid() |  | 
| `void` | OnErrorsChanged(`String` propertyName) |  | 
| `void` | OnPropertyChanged(`String` propertyName = null) |  | 
| `Boolean` | OnValidation() |  | 
| `void` | SetValue(`Object` obj) |  | 
| `Boolean` | Validate() |  | 


## `SelectionSchema`

```csharp
public class MaterialForms.SelectionSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `Boolean` | IsEditable |  | 
| `ObservableCollection<Object>` | Items |  | 
| `Object` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `Session`

```csharp
public abstract class MaterialForms.Session

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `MaterialDialog` | Dialog | Gets the dialog object for which this session exists. | 
| `Int32` | Id | Gets the unique ID associated with this session. | 
| `Object` | Item | Gets or sets the value of the form schema with the specified key. | 
| `Task<Nullable<Boolean>>` | Task | Gets the task that represents the dialog's session lifecycle. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Close(`Nullable<Boolean>` result) | Closes the session unconditionally. | 
| `void` | Invalidate(`String` key, `String` message) | Displays an error message for the specified schema. | 
| `void` | Lock() |  | 
| `void` | Unlock() |  | 


## `SessionAssist`

```csharp
public static class MaterialForms.SessionAssist

```

Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DependencyProperty` | HostingSessionProperty |  | 


## `SingleFileSchema`

```csharp
public class MaterialForms.SingleFileSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Filter |  | 
| `Boolean` | HoldsValue |  | 
| `ValidationCallback<String>` | Validation |  | 
| `String` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `Boolean` | OnValidation() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `StringSchema`

```csharp
public class MaterialForms.StringSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `Boolean` | IsMultiLine |  | 
| `Boolean` | IsReadOnly |  | 
| `ValidationCallback<String>` | Validation |  | 
| `String` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `Boolean` | OnValidation() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `TimeSchema`

```csharp
public class MaterialForms.TimeSchema
    : SchemaBase, IViewProvider, INotifyPropertyChanged, INotifyDataErrorInfo

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HoldsValue |  | 
| `Boolean` | Is24Hours |  | 
| `String` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `UserControl` | CreateView() |  | 
| `Object` | GetValue() |  | 
| `void` | SetValue(`Object` obj) |  | 


## `Validators`

```csharp
public static class MaterialForms.Validators

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | IsNotEmpty(`String` value) |  | 
| `String` | OptionalFile(`String` value) |  | 
| `String` | RequiredFile(`String` value) |  | 


## `WindowFactory`

```csharp
public static class MaterialForms.WindowFactory

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `MaterialWindow` | Alert(`String` message) |  | 
| `MaterialWindow` | Alert(`String` message, `String` title) |  | 
| `MaterialWindow` | Alert(`String` message, `String` title, `String` action) |  | 
| `MaterialWindow` | FromSingleSchema(`SchemaBase` schema) |  | 
| `MaterialWindow` | FromSingleSchema(`String` message, `SchemaBase` schema) |  | 
| `MaterialWindow` | FromSingleSchema(`String` message, `String` title, `SchemaBase` schema) |  | 
| `MaterialWindow` | FromSingleSchema(`String` message, `String` title, `String` positiveAction, `SchemaBase` schema) |  | 
| `MaterialWindow` | FromSingleSchema(`String` message, `String` title, `String` positiveAction, `String` negativeAction, `SchemaBase` schema) |  | 
| `MaterialWindow` | Prompt(`String` message) |  | 
| `MaterialWindow` | Prompt(`String` message, `String` title) |  | 
| `MaterialWindow` | Prompt(`String` message, `String` title, `String` positiveAction) |  | 
| `MaterialWindow` | Prompt(`String` message, `String` title, `String` positiveAction, `String` negativeAction) |  | 


## `WindowSession`

Manages the lifecycle of a window displayed to the user.
```csharp
public class MaterialForms.WindowSession
    : Session

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `MaterialDialog` | Dialog | Gets the dialog associated with the MaterialWindow context. | 
| `String` | DialogIdentifier | Gets the unique identifier for the window's dialog host. | 
| `Boolean` | IsClosed | Gets whether the session has finished and the window is closed. | 
| `Boolean` | IsLoaded | Gets whether the window has loaded and has been displayed to the user. A true value does not guarantee that the window is still open. | 
| `Task<Nullable<Boolean>>` | Task | Gets the task representing the window session being displayed. The task returns the window's dialog result. | 
| `MaterialWindow` | Window | Gets the data context of the displayed window. | 


Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `EventHandler` | Loaded | Occurs when the window has loaded and has been displayed to the user. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Task` | Alert(`String` message) |  | 
| `Task` | Alert(`String` message, `String` title) |  | 
| `Task` | Alert(`String` message, `String` title, `String` action) |  | 
| `void` | Close(`Nullable<Boolean>` result) | Closes the displayed window unconditionally. | 
| `Task<Nullable<Boolean>>` | Prompt(`String` message) |  | 
| `Task<Nullable<Boolean>>` | Prompt(`String` message, `String` title) |  | 
| `Task<Nullable<Boolean>>` | Prompt(`String` message, `String` title, `String` positiveAction) |  | 
| `Task<Nullable<Boolean>>` | Prompt(`String` message, `String` title, `String` positiveAction, `String` negativeAction) |  | 
| `WindowSession` | Show() |  | 
| `Task<Nullable<Boolean>>` | ShowDialog(`MaterialDialog` dialog) |  | 
| `Task<Nullable<Boolean>>` | ShowDialog(`MaterialDialog` dialog, `Double` dialogWidth) |  | 
| `DialogSession` | ShowDialogTracked(`MaterialDialog` dialog) |  | 
| `DialogSession` | ShowDialogTracked(`MaterialDialog` dialog, `Double` dialogWidth) |  | 


