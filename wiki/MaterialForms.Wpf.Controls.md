## `ActionPanel`

```csharp
public class MaterialForms.Wpf.Controls.ActionPanel
    : Panel, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IAddChild

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Size` | ArrangeOverride(`Size` finalSize) |  | 
| `Size` | MeasureOverride(`Size` availableSize) |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DependencyProperty` | PositionProperty |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Position` | GetPosition(`DependencyObject` element) |  | 
| `void` | SetPosition(`DependencyObject` element, `Position` value) |  | 


## `DynamicForm`

```csharp
public class MaterialForms.Wpf.Controls.DynamicForm
    : Control, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IDynamicForm

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Dictionary<String, IDataBindingProvider>` | DataBindingProviders |  | 
| `Dictionary<String, DataFormField>` | DataFields |  | 
| `IResourceContext` | ResourceContext |  | 


Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Context | Gets or sets the context associated with this form.  Models can utilize this property to get data from  outside of their instance scope. | 
| `IFormBuilder` | FormBuilder | Gets or sets the form builder that is responsible for building forms. | 
| `Object` | Model | Gets or sets the model associated with this form.  If the value is a IFormDefinition, a form will be built based on that definition.  If the value is a Type, a form will be built and bound to a new instance of that type.  If the value is a simple object, a single field bound to this property will be displayed.  If the value is a complex object, a form will be built and bound to properties of that instance. | 
| `Object` | Value | Gets the value of the current model instance. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Dictionary<String, DataFormField>` | GetDataFields() |  | 
| `void` | OnApplyTemplate() |  | 
| `void` | ReloadElements() |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `HashSet<DynamicForm>` | ActiveForms |  | 
| `DependencyProperty` | ContextProperty |  | 
| `DependencyProperty` | FormBuilderProperty |  | 
| `DependencyProperty` | ModelProperty |  | 
| `DependencyProperty` | ValueProperty |  | 
| `DependencyPropertyKey` | ValuePropertyKey |  | 


## `IDynamicForm`

```csharp
public interface MaterialForms.Wpf.Controls.IDynamicForm

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Context |  | 
| `Object` | Model |  | 
| `Object` | Value |  | 


## `MaterialDialog`

```csharp
public abstract class MaterialForms.Wpf.Controls.MaterialDialog
    : ContentControl, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IAddChild

```

## `Position`

```csharp
public enum MaterialForms.Wpf.Controls.Position
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | Right |  | 
| `1` | Left |  | 


## `TextProperties`

```csharp
public static class MaterialForms.Wpf.Controls.TextProperties

```

Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DependencyProperty` | HeadingFontSizeProperty |  | 
| `DependencyProperty` | TextFontSizeProperty |  | 
| `DependencyProperty` | TitleFontSizeProperty |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | GetHeadingFontSize(`DependencyObject` element) |  | 
| `Double` | GetTextFontSize(`DependencyObject` element) |  | 
| `Double` | GetTitleFontSize(`DependencyObject` element) |  | 
| `void` | SetHeadingFontSize(`DependencyObject` element, `Double` value) |  | 
| `void` | SetTextFontSize(`DependencyObject` element, `Double` value) |  | 
| `void` | SetTitleFontSize(`DependencyObject` element, `Double` value) |  | 


