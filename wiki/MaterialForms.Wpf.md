## `IActionHandler`

```csharp
public interface MaterialForms.Wpf.IActionHandler

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | HandleAction(`Object` model, `String` action, `Object` parameter) |  | 


## `ModelState`

Provides utilities for bound models.
```csharp
public static class MaterialForms.Wpf.ModelState

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | ClearValidationErrors(`Object` model) | Clear validation errors from source properties. | 
| `void` | ClearValidationErrors(`Object` model, `String[]` properties) | Clear validation errors from source properties. | 
| `Boolean` | IsModel(`Object` value) |  | 
| `Boolean` | Reset(`Object` model, `String[]` properties) | Attempts to reset object properties to default values.  Does nothing if object is not part of any generated form. | 
| `Boolean` | Reset(`Object` model) | Attempts to reset object properties to default values.  Does nothing if object is not part of any generated form. | 
| `void` | UpdateFields(`Object` model) | Updates form fields with model values.  Has a similar effect to . | 
| `void` | UpdateFields(`Object` model, `String[]` properties) | Updates form fields with model values.  Has a similar effect to . | 
| `Boolean` | Validate(`Object` model) | Validates source by flushing bindings. | 
| `Boolean` | Validate(`Object` model, `String[]` properties) | Validates source by flushing bindings. | 


