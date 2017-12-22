## `ActionAlignerElement`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.ActionAlignerElement

```

## `ActionElement`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.ActionElement
    : ContentElement

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IValueProvider` | ActionName |  | 
| `IValueProvider` | ActionParameter |  | 
| `IValueProvider` | ClosesDialog |  | 
| `IValueProvider` | IsEnabled |  | 
| `IValueProvider` | IsReset |  | 
| `IValueProvider` | Validates |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 


## `ActionPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.ActionPresenter
    : BindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `ICommand` | Command |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DependencyProperty` | CommandProperty |  | 


## `BooleanField`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.BooleanField
    : DataFormField

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsSwitch |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 


## `BreakElement`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.BreakElement
    : FormElement

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IValueProvider` | Height |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 
| `void` | Freeze() |  | 


## `BreakPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.BreakPresenter
    : BindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider

```

## `CardElement`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.CardElement
    : FormElement

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 


## `CardPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.CardPresenter
    : BindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider

```

## `CheckBoxPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.CheckBoxPresenter
    : ValueBindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider, IDataBindingProvider

```

## `ContentElement`

```csharp
public abstract class MaterialForms.Wpf.Fields.Defaults.ContentElement
    : FormElement

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IValueProvider` | Content |  | 
| `IValueProvider` | Icon |  | 
| `IValueProvider` | IconPadding |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Freeze() |  | 


## `ConvertedField`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.ConvertedField
    : DataFormField

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Func<IResourceContext, IErrorStringProvider>` | ConversionErrorMessage |  | 
| `Func<String, CultureInfo, Object>` | Deserializer |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 
| `void` | Freeze() |  | 


## `ConvertedPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.ConvertedPresenter
    : ValueBindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider, IDataBindingProvider

```

## `DateTimeField`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.DateTimeField
    : DataFormField

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 
| `void` | Freeze() |  | 


## `DateTimePresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.DateTimePresenter
    : ValueBindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider, IDataBindingProvider

```

## `DividerElement`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.DividerElement
    : FormElement

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IValueProvider` | HasMargin |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 
| `void` | Freeze() |  | 


## `DividerPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.DividerPresenter
    : BindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider

```

## `HeadingElement`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.HeadingElement
    : ContentElement

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 


## `HeadingPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.HeadingPresenter
    : BindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider

```

## `SelectionField`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.SelectionField
    : DataFormField

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IValueProvider` | DisplayPath |  | 
| `IValueProvider` | ItemsSource |  | 
| `IValueProvider` | ItemStringFormat |  | 
| `IValueProvider` | SelectionType |  | 
| `IValueProvider` | ValuePath |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 
| `void` | Freeze() |  | 


## `SelectionPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.SelectionPresenter
    : ValueBindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider, IDataBindingProvider

```

## `StringField`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.StringField
    : DataFormField

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IValueProvider` | IsMultiline |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 
| `void` | Freeze() |  | 


## `StringPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.StringPresenter
    : ValueBindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider, IDataBindingProvider

```

## `SwitchPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.SwitchPresenter
    : ValueBindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider, IDataBindingProvider

```

## `TextElement`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.TextElement
    : ContentElement

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 


## `TextPresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.TextPresenter
    : BindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider

```

## `TitleElement`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.TitleElement
    : ContentElement

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IBindingProvider` | CreateBindingProvider(`IResourceContext` context, `IDictionary<String, IValueProvider>` formResources) |  | 


## `TitlePresenter`

```csharp
public class MaterialForms.Wpf.Fields.Defaults.TitlePresenter
    : BindingProvider, IResource, IAnimatable, IInputElement, IFrameworkInputElement, ISupportInitialize, IHaveResources, IQueryAmbient, IBindingProvider

```

