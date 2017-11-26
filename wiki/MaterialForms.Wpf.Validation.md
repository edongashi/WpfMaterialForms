## `ComparisonValidator`

```csharp
public abstract class MaterialForms.Wpf.Validation.ComparisonValidator
    : FieldValidator

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IProxy` | Argument |  | 


## `ConversionValidator`

```csharp
public class MaterialForms.Wpf.Validation.ConversionValidator
    : ValidationRule

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `ValidationResult` | Validate(`Object` value, `CultureInfo` cultureInfo) |  | 


## `EmptyValidator`

```csharp
public class MaterialForms.Wpf.Validation.EmptyValidator
    : FieldValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `ErrorStringProvider`

```csharp
public class MaterialForms.Wpf.Validation.ErrorStringProvider
    : IErrorStringProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | GetErrorMessage(`Object` value) |  | 


## `ExistsInValidator`

```csharp
public class MaterialForms.Wpf.Validation.ExistsInValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `FalseValidator`

```csharp
public class MaterialForms.Wpf.Validation.FalseValidator
    : FieldValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `FieldValidator`

```csharp
public abstract class MaterialForms.Wpf.Validation.FieldValidator
    : ValidationRule

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IErrorStringProvider` | ErrorProvider |  | 
| `IBoolProxy` | IsEnforced |  | 
| `Boolean` | StrictValidation |  | 
| `ValidationPipe` | ValidationPipe |  | 
| `IValueConverter` | ValueConverter |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `ValidationResult` | Validate(`Object` value, `CultureInfo` cultureInfo) |  | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `GreaterThanEqualValidator`

```csharp
public class MaterialForms.Wpf.Validation.GreaterThanEqualValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `GreaterThanValidator`

```csharp
public class MaterialForms.Wpf.Validation.GreaterThanValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `IErrorStringProvider`

```csharp
public interface MaterialForms.Wpf.Validation.IErrorStringProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | GetErrorMessage(`Object` value) |  | 


## `IValidatorProvider`

```csharp
public interface MaterialForms.Wpf.Validation.IValidatorProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FieldValidator` | GetValidator(`IResourceContext` context, `ValidationPipe` pipe) |  | 


## `LessThanEqualValidator`

```csharp
public class MaterialForms.Wpf.Validation.LessThanEqualValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `LessThanValidator`

```csharp
public class MaterialForms.Wpf.Validation.LessThanValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `MatchPatternValidator`

```csharp
public class MaterialForms.Wpf.Validation.MatchPatternValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `MethodInvocationValidator`

```csharp
public class MaterialForms.Wpf.Validation.MethodInvocationValidator
    : FieldValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `NotEmptyValidator`

```csharp
public class MaterialForms.Wpf.Validation.NotEmptyValidator
    : FieldValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `NotEqualsValidator`

```csharp
public class MaterialForms.Wpf.Validation.NotEqualsValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `NotExistsInValidator`

```csharp
public class MaterialForms.Wpf.Validation.NotExistsInValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `NotMatchPatternValidator`

```csharp
public class MaterialForms.Wpf.Validation.NotMatchPatternValidator
    : ComparisonValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `NotNullValidator`

```csharp
public class MaterialForms.Wpf.Validation.NotNullValidator
    : FieldValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `NullValidator`

```csharp
public class MaterialForms.Wpf.Validation.NullValidator
    : FieldValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `PlainErrorStringProvider`

```csharp
public class MaterialForms.Wpf.Validation.PlainErrorStringProvider
    : IErrorStringProvider

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | ErrorMessage |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | GetErrorMessage(`Object` value) |  | 


## `TrueValidator`

```csharp
public class MaterialForms.Wpf.Validation.TrueValidator
    : FieldValidator

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | ValidateValue(`Object` value, `CultureInfo` cultureInfo) |  | 


## `ValidationContext`

Gets passed to method validators.
```csharp
public class MaterialForms.Wpf.Validation.ValidationContext

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `CultureInfo` | CultureInfo | Gets the  in which this validation is performed. | 
| `Object` | Model | Gets the model that contains the property that is being validated. | 
| `Object` | ModelContext | Gets the model's context at the time of validation. | 
| `String` | PropertyName | Gets the name of the property that is being validated. | 
| `Object` | PropertyValue | Gets the value of the property that is being validated. | 
| `Boolean` | WillCommit | Gets whether the value will be written to the  property regardless of validation result. | 


## `ValidationPipe`

```csharp
public class MaterialForms.Wpf.Validation.ValidationPipe
    : ValidationRule

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Error |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `ValidationResult` | Validate(`Object` value, `CultureInfo` cultureInfo) |  | 


## `ValueErrorStringProvider`

```csharp
public class MaterialForms.Wpf.Validation.ValueErrorStringProvider
    : IErrorStringProvider

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | GetErrorMessage(`Object` value) |  | 


