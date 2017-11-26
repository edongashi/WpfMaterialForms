## `BindingProvider`

Default implementation of .
```csharp
public abstract class MaterialForms.Wpf.Fields.BindingProvider
    : Control, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IResourceContext` | Context | Gets the context associated with the form control. | 
| `IDictionary<String, IValueProvider>` | FieldResources | Gets the field resources identified by name. | 
| `IDictionary<String, IValueProvider>` | FormResources | Gets the form resources identified by name. | 
| `BindingProxy` | Item | Returns a  bound to the value returned by . | 
| `Boolean` | ThrowOnNotFound | Gets whether this object will throw when a resource is not found. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | BindingCreated(`BindingExpressionBase` expression, `String` resource) |  | 
| `Object` | ProvideValue(`String` name) | Resolves the value for the specified resource.  The result may be a  or a literal value. | 


## `DataFormField`

Base class for all input fields.
```csharp
public abstract class MaterialForms.Wpf.Fields.DataFormField
    : FormField

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingOptions` | BindingOptions |  | 
| `Boolean` | CreateBinding |  | 
| `IValueProvider` | DefaultValue | Gets or sets the default value for this field. | 
| `Boolean` | IsDirectBinding |  | 
| `IValueProvider` | IsReadOnly |  | 
| `Type` | PropertyType |  | 
| `IValueProvider` | SelectOnFocus |  | 
| `List<IValidatorProvider>` | Validators |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Freeze() |  | 
| `Object` | GetDefaultValue(`IResourceContext` context) |  | 


## `FormBindingExtension`

Markup extension for creating deferred bindings.
```csharp
public class MaterialForms.Wpf.Fields.FormBindingExtension
    : MarkupExtension

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Converter |  | 
| `String` | Name |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | ProvideValue(`IServiceProvider` serviceProvider) |  | 


## `FormDefinition`

```csharp
public class MaterialForms.Wpf.Fields.FormDefinition
    : IFormDefinition

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `List<FormRow>` | FormRows |  | 
| `Double[]` | Grid |  | 
| `Type` | ModelType |  | 
| `IDictionary<String, IValueProvider>` | Resources |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | CreateInstance(`IResourceContext` context) |  | 
| `void` | Freeze() |  | 


## `FormElement`

Represents a form element, which is not necessarily an input field.
```csharp
public abstract class MaterialForms.Wpf.Fields.FormElement

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IValueProvider` | IsVisible | Gets or sets the bool resource that determines whether this element will be visible. | 
| `Position` | LinePosition |  | 
| `IDictionary<String, IValueProvider>` | Resources |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 
| `void` | Freeze() |  | 


## `FormElementContainer`

```csharp
public class MaterialForms.Wpf.Fields.FormElementContainer

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Int32` | Column |  | 
| `Int32` | ColumnSpan |  | 
| `List<FormElement>` | Elements |  | 


## `FormElementsAlignment`

```csharp
public enum MaterialForms.Wpf.Fields.FormElementsAlignment
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | Stretch |  | 
| `1` | Left |  | 
| `2` | Right |  | 


## `FormField`

Base class for all form field definitions.
```csharp
public abstract class MaterialForms.Wpf.Fields.FormField
    : FormElement

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IValueProvider` | Icon | Gets or sets the field's PackIconKind resource. Not all controls may display an icon. | 
| `String` | Key | Gets or sets the unique name of this field. | 
| `IValueProvider` | Name | Gets or sets the string expression of the field's title. | 
| `IValueProvider` | ToolTip | Gets or sets the string expression of the field's tooltip. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Freeze() | Finalizes the field state by adding all appropriate values as resources.  Changing properties after this method has been called is strongly discouraged. | 


## `FormRow`

```csharp
public class MaterialForms.Wpf.Fields.FormRow

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `List<FormElementContainer>` | Elements |  | 
| `Int32` | RowSpan |  | 
| `Boolean` | StartsNewRow |  | 


## `IBindingProvider`

Provides bindings by resource name.
```csharp
public interface MaterialForms.Wpf.Fields.IBindingProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingProxy` | Item | Returns a  bound to the value returned by . | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | BindingCreated(`BindingExpressionBase` expression, `String` resource) | Gets called when a binding expression is resolved. | 
| `Object` | ProvideValue(`String` name) | Resolves the value for the specified resource.  The result may be a  or a literal value. | 


## `IDataBindingProvider`

```csharp
public interface MaterialForms.Wpf.Fields.IDataBindingProvider
    : IBindingProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | ClearBindings() |  | 
| `IEnumerable<BindingExpressionBase>` | GetBindings() |  | 


## `IFormDefinition`

```csharp
public interface MaterialForms.Wpf.Fields.IFormDefinition

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IReadOnlyList<FormRow>` | FormRows |  | 
| `Double[]` | Grid |  | 
| `Type` | ModelType |  | 
| `IDictionary<String, IValueProvider>` | Resources |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | CreateInstance(`IResourceContext` context) |  | 


## `StringTypeConverter`

```csharp
public class MaterialForms.Wpf.Fields.StringTypeConverter
    : IValueConverter

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Convert(`Object` value, `Type` targetType, `Object` parameter, `CultureInfo` culture) |  | 
| `Object` | ConvertBack(`Object` value, `Type` targetType, `Object` parameter, `CultureInfo` culture) |  | 


## `ValueBindingProvider`

Single source data binding provider that captures resources with name "Value".
```csharp
public class MaterialForms.Wpf.Fields.ValueBindingProvider
    : BindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider, IDataBindingProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingExpressionBase` | CurrentBindingExpression |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | BindingCreated(`BindingExpressionBase` expression, `String` resource) |  | 
| `void` | ClearBindings() |  | 
| `IEnumerable<BindingExpressionBase>` | GetBindings() |  | 


