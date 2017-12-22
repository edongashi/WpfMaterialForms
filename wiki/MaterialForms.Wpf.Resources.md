## `BindingOptions`

```csharp
public class MaterialForms.Wpf.Resources.BindingOptions

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `CultureInfo` | ConverterCulture |  | 
| `Int32` | Delay |  | 
| `String` | StringFormat |  | 
| `UpdateSourceTrigger` | UpdateSourceTrigger |  | 
| `Boolean` | ValidatesOnDataErrors |  | 
| `Boolean` | ValidatesOnExceptions |  | 
| `Boolean` | ValidatesOnNotifyDataErrors |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Apply(`Binding` binding) |  | 


## `BindingProxy`

Encapsulates an object bound to a resource.
```csharp
public class MaterialForms.Wpf.Resources.BindingProxy
    : Freezable, ISealable, IProxy

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Key |  | 
| `Object` | Value |  | 
| `Action` | ValueChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Freezable` | CreateInstanceCore() |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DependencyProperty` | ValueProperty |  | 


## `BoolProxy`

Encapsulates a string bound to a resource.
```csharp
public class MaterialForms.Wpf.Resources.BoolProxy
    : Freezable, ISealable, IBoolProxy, IProxy

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Value |  | 
| `Action` | ValueChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Freezable` | CreateInstanceCore() |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DependencyProperty` | ValueProperty |  | 


## `BoundExpression`

```csharp
public class MaterialForms.Wpf.Resources.BoundExpression
    : IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsPlainString |  | 
| `Boolean` | IsSingleResource |  | 
| `IReadOnlyList<IValueProvider>` | Resources |  | 
| `String` | StringFormat |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IProxy` | GetProxy(`IResourceContext` context) |  | 
| `IValueProvider` | GetValueProvider() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 
| `IValueProvider` | Simplified() |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `BoundExpression` | Parse(`String` expression) |  | 
| `BoundExpression` | Parse(`String` expression, `IDictionary<String, Object>` contextualResources) |  | 
| `BoundExpression` | Parse(`String` expression, `Func<String, Boolean, String, IValueProvider>` contextualResource) |  | 
| `IValueProvider` | ParseSimplified(`String` expression) |  | 


## `BoundValue`

```csharp
public class MaterialForms.Wpf.Resources.BoundValue
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `Boolean` | OneTimeBinding |  | 
| `String` | PropertyPath |  | 
| `Object` | Source |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `CoercedValueProvider<T>`

```csharp
public class MaterialForms.Wpf.Resources.CoercedValueProvider<T>
    : IValueProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 


## `ContextPropertyBinding`

```csharp
public class MaterialForms.Wpf.Resources.ContextPropertyBinding
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `Boolean` | OneTimeBinding |  | 
| `String` | PropertyPath |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `ConvertedDataBinding`

```csharp
public class MaterialForms.Wpf.Resources.ConvertedDataBinding
    : IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingOptions` | BindingOptions |  | 
| `Func<IResourceContext, IErrorStringProvider>` | ConversionErrorStringProvider |  | 
| `Func<String, CultureInfo, Object>` | Deserializer |  | 
| `String` | PropertyPath |  | 
| `List<IValidatorProvider>` | ValidationRules |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 


## `ConvertedDirectBinding`

```csharp
public class MaterialForms.Wpf.Resources.ConvertedDirectBinding
    : IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingOptions` | BindingOptions |  | 
| `Func<IResourceContext, IErrorStringProvider>` | ConversionErrorStringProvider |  | 
| `Func<String, CultureInfo, Object>` | Deserializer |  | 
| `List<IValidatorProvider>` | ValidationRules |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 


## `DataBinding`

```csharp
public class MaterialForms.Wpf.Resources.DataBinding
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingOptions` | BindingOptions |  | 
| `Boolean` | IsDynamic |  | 
| `String` | PropertyPath |  | 
| `List<IValidatorProvider>` | ValidationRules |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `DeferredProxyResource`

```csharp
public class MaterialForms.Wpf.Resources.DeferredProxyResource
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `Boolean` | OneTimeBinding |  | 
| `String` | PropertyPath |  | 
| `Func<IResourceContext, IProxy>` | ProxyProvider |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `DirectBinding`

```csharp
public class MaterialForms.Wpf.Resources.DirectBinding
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingOptions` | BindingOptions |  | 
| `Boolean` | IsDynamic |  | 
| `List<IValidatorProvider>` | ValidationRules |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `DynamicResource`

```csharp
public class MaterialForms.Wpf.Resources.DynamicResource
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `String` | ResourceKey |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `EnumerableKeyValueProvider`

```csharp
public class MaterialForms.Wpf.Resources.EnumerableKeyValueProvider
    : IValueProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 


## `EnumerableStringValueProvider`

```csharp
public class MaterialForms.Wpf.Resources.EnumerableStringValueProvider
    : IValueProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 


## `IBoolProxy`

```csharp
public interface MaterialForms.Wpf.Resources.IBoolProxy

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Value |  | 


## `IFrameworkResourceContext`

```csharp
public interface MaterialForms.Wpf.Resources.IFrameworkResourceContext
    : IResourceContext

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FrameworkElement` | GetOwningElement() |  | 


## `IProxy`

```csharp
public interface MaterialForms.Wpf.Resources.IProxy

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Value |  | 
| `Action` | ValueChanged |  | 


## `IResourceContext`

Bridges form elements with the control that contains them.
```csharp
public interface MaterialForms.Wpf.Resources.IResourceContext

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | AddResource(`Object` key, `Object` value) | Adds a resource to the control's resources. | 
| `Binding` | CreateContextBinding(`String` path) | Creates a new binding to the form context object. | 
| `Binding` | CreateDirectModelBinding() | Creates a binding to the raw model. | 
| `Binding` | CreateModelBinding(`String` path) | Creates a new binding to the form model object. | 
| `Object` | FindResource(`Object` key) | Locates a resource from the control's resources. | 
| `Object` | GetContextInstance() | Gets current context instance. | 
| `Object` | GetModelInstance() | Gets current model instance. | 
| `Object` | TryFindResource(`Object` key) | Tries to locate a resource from the control's resources. | 


## `IStringProxy`

```csharp
public interface MaterialForms.Wpf.Resources.IStringProxy

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Value |  | 


## `IValueProvider`

```csharp
public interface MaterialForms.Wpf.Resources.IValueProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 


## `LiteralValue`

```csharp
public class MaterialForms.Wpf.Resources.LiteralValue
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `Object` | Value |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `LiteralValue` | False |  | 
| `LiteralValue` | Null |  | 
| `LiteralValue` | True |  | 


## `PropertyBinding`

```csharp
public class MaterialForms.Wpf.Resources.PropertyBinding
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `Boolean` | OneTimeBinding |  | 
| `String` | PropertyPath |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `ProxyResource`

```csharp
public class MaterialForms.Wpf.Resources.ProxyResource
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `Boolean` | OneTimeBinding |  | 
| `String` | PropertyPath |  | 
| `IProxy` | Proxy |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `Resource`

```csharp
public abstract class MaterialForms.Wpf.Resources.Resource
    : IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `String` | ValueConverter |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Boolean` | Equals(`Object` obj) |  | 
| `Int32` | GetHashCode() |  | 
| `IValueConverter` | GetValueConverter(`IResourceContext` context) |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 
| `Object` | ProvideValue(`IResourceContext` context) |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Dictionary<String, IValueConverter>` | ValueConverters | Global cache for value converters accessible from expressions. | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | FormatPath(`String` path) |  | 
| `IValueConverter` | GetValueConverter(`IResourceContext` context, `String` valueConverter) |  | 


## `StaticResource`

```csharp
public class MaterialForms.Wpf.Resources.StaticResource
    : Resource, IEquatable<Resource>, IValueProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | IsDynamic |  | 
| `String` | ResourceKey |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Resource` other) |  | 
| `Int32` | GetHashCode() |  | 
| `BindingBase` | ProvideBinding(`IResourceContext` context) |  | 


## `StringProxy`

Encapsulates a string bound to a resource.
```csharp
public class MaterialForms.Wpf.Resources.StringProxy
    : Freezable, ISealable, IStringProxy, IProxy

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Key |  | 
| `String` | Value |  | 
| `Action` | ValueChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Freezable` | CreateInstanceCore() |  | 
| `String` | ToString() |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `DependencyProperty` | KeyProperty |  | 
| `DependencyProperty` | ValueProperty |  | 


## `ValueProviderExtensions`

```csharp
public static class MaterialForms.Wpf.Resources.ValueProviderExtensions

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IProxy` | GetBestMatchingProxy(this `IValueProvider` valueProvider, `IResourceContext` context) |  | 
| `BoolProxy` | GetBoolValue(this `IValueProvider` valueProvider, `IResourceContext` context) |  | 
| `StringProxy` | GetStringValue(this `IValueProvider` valueProvider, `IResourceContext` context) |  | 
| `StringProxy` | GetStringValue(this `IValueProvider` valueProvider, `IResourceContext` context, `Boolean` setKey) |  | 
| `BindingProxy` | GetValue(this `IValueProvider` valueProvider, `IResourceContext` context) |  | 
| `IValueProvider` | Wrap(this `IValueProvider` valueProvider, `String` valueConverter) |  | 


