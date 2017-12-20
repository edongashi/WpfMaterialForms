## `ActionAttribute`

```csharp
public class MaterialForms.Wpf.Annotations.ActionAttribute
    : FormContentAttribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | ActionName | Action identifier that is passed to handlers. | 
| `Object` | ClosesDialog | Determines whether this action will close dialogs that host it. | 
| `String` | Content | Displayed content. Accepts a dynamic expression. | 
| `Object` | Icon | Displayed icon. Accepts a PackIconKind or a dynamic resource. | 
| `Boolean` | IsCancel |  | 
| `Boolean` | IsDefault |  | 
| `Object` | IsEnabled | Determines whether this action can be performed.  Accepts a boolean or a dynamic resource. | 
| `Object` | IsReset | Determines whether the model will be reset to default values before the action is executed. | 
| `Object` | Parameter | Action parameter. Accepts a dynamic expression. | 
| `Object` | Validates | Determines whether the model will be validated before the action is executed. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FormElement` | CreateElement() |  | 


## `BindingAttribute`

Specifies additional information about a field's data binding.
```csharp
public class MaterialForms.Wpf.Annotations.BindingAttribute
    : Attribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | ConversionErrorMessage |  | 
| `String` | ConverterCulture | Indicates the culture name to use if this field uses  data conversion. A null value indicates UI culture;  an empty value indicates invariant culture; a string  value will retrieve the culture by name. | 
| `Int32` | Delay |  | 
| `String` | StringFormat |  | 
| `UpdateSourceTrigger` | UpdateSourceTrigger |  | 
| `Boolean` | ValidatesOnDataErrors |  | 
| `Boolean` | ValidatesOnExceptions |  | 
| `Boolean` | ValidatesOnNotifyDataErrors |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Apply(`BindingOptions` bindingOptions) |  | 


## `BreakAttribute`

```csharp
public class MaterialForms.Wpf.Annotations.BreakAttribute
    : FormContentAttribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Height | Height of the break. Accepts a double or a dynamic resource. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FormElement` | CreateElement() |  | 


## `CardAttribute`

```csharp
public class MaterialForms.Wpf.Annotations.CardAttribute
    : FormContentAttribute, _Attribute

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FormElement` | CreateElement() |  | 


## `DefaultFields`

Specifies which fields are displayed by default.
```csharp
public enum MaterialForms.Wpf.Annotations.DefaultFields
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | AllExcludingReadonly | Properties with public getters and setters are displayed by default. | 
| `1` | AllIncludingReadonly | All public properties are displayed by default. | 
| `2` | None | No properties are displayed by default. Use  to add properties. | 


## `DividerAttribute`

```csharp
public class MaterialForms.Wpf.Annotations.DividerAttribute
    : FormContentAttribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | HasMargin |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FormElement` | CreateElement() |  | 


## `EnumDisplayAttribute`

Allows specifying enum display text.
```csharp
public class MaterialForms.Wpf.Annotations.EnumDisplayAttribute
    : Attribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Name | Enumeration member name. Accepts a string or a dynamic expression. | 


## `FieldAttribute`

Indicates that the property is a form field and allows specifying its details.
```csharp
public class MaterialForms.Wpf.Annotations.FieldAttribute
    : Attribute, _Attribute

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | HasName |  | 


Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Int32` | Column | Specifies the column number. Applicable only when  is set. | 
| `Int32` | ColumnSpan | Specifies the column span. Applicable only when  is set. | 
| `Object` | DefaultValue | Determines the default value of this field. Accepts an object of the same type as the property type  or a dynamic expression. Some types such as DateTime and numbers can be deserialized from strings. | 
| `Object` | Icon | The icon associated with the field. Not all field types may support icons.  Accepts a  or a dynamic resource. | 
| `Object` | IsReadOnly | Determines whether the field is editable. Accepts a boolean or a dynamic resource. | 
| `Object` | IsVisible | Determines whether this field will be visible. Accepts a boolean or a dynamic resource. | 
| `String` | Name | The display name of the field. Accepts a string or a dynamic expression. | 
| `Int32` | Position | Determines the relative position of this field in the form.  Fields are sorted based on this value, which has a default value of 0. | 
| `String` | Row | Specifies the row name. Fields sharing the same row name will be aligned in columns when possible. | 
| `String` | ToolTip | The tooltip of the field which shows on hover. Accepts a string or a dynamic expression. | 


## `FieldIgnoreAttribute`

Properties marked with this attribute will never be generated.
```csharp
public class MaterialForms.Wpf.Annotations.FieldIgnoreAttribute
    : Attribute, _Attribute

```

## `FormAttribute`

Allows configuring generated forms.
```csharp
public class MaterialForms.Wpf.Annotations.FormAttribute
    : Attribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double[]` | Grid | Specifies grid column widths. Positive values indicate star widths, negative values indicate pixel widths. | 
| `DefaultFields` | Mode | Specifies default field generation behavior. | 


## `FormContentAttribute`

Represents content attached before or after form elements.
```csharp
public abstract class MaterialForms.Wpf.Annotations.FormContentAttribute
    : Attribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | InsertAfter | If set to true and this attribute is attached to a property, this element will be displayed after the field.  If set to true and this attribute is attached to a class, this element will be displayed after the form contents. | 
| `Object` | IsVisible | Determines whether this element will be visible. Accepts a boolean or a dynamic resource. | 
| `Position` | LinePosition |  | 
| `Int32` | Position | Determines the position relative to other elements added to the attribute target. | 
| `Int32` | RowSpan |  | 
| `Boolean` | ShareLine |  | 
| `Boolean` | StartsNewRow |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FormElement` | CreateElement() | Create a form element corresponding to this object. | 
| `FormElement` | GetElement() |  | 
| `void` | InitializeElement(`FormElement` element) |  | 


## `HeadingAttribute`

Draws accented heading text.
```csharp
public class MaterialForms.Wpf.Annotations.HeadingAttribute
    : TextElementAttribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Icon | Displayed icon. Accepts a PackIconKind or a dynamic resource. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FormElement` | CreateElement() |  | 


## `Must`

Specifies validation type.
```csharp
public enum MaterialForms.Wpf.Annotations.Must
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | BeEqualTo | Property must equal to a value. | 
| `1` | NotBeEqualTo | Property must not equal to a value. | 
| `2` | BeGreaterThan | Property must be greater than a value. | 
| `3` | BeGreaterThanOrEqualTo | Property must be greater than or equal to a value. | 
| `4` | BeLessThan | Property must be less than a value. | 
| `5` | BeLessThanOrEqualTo | Property must be less than or equal to a value. | 
| `6` | BeEmpty | Property must be empty.  A string is empty if it is null or has length 0.  A collection is empty if it is null or has 0 elements. | 
| `7` | NotBeEmpty | Property must not be empty.  A string is empty if it is null or has length 0.  A collection is empty if it is null or has 0 elements. | 
| `8` | BeTrue | Property must be true. | 
| `9` | BeFalse | Property must be false. | 
| `10` | BeNull | Property must be null. | 
| `11` | NotBeNull | Property must not be null. | 
| `12` | ExistIn | Property must exist in a collection. | 
| `13` | NotExistIn | Property must not exist in a collection. | 
| `14` | MatchPattern | Property must match a regex pattern. | 
| `15` | NotMatchPattern | Property must not match a regex pattern. | 
| `16` | SatisfyMethod | Property value must satisfy model's static method of signature: public static bool <Argument>( context).  Throws if no such method is found. | 
| `17` | SatisfyContextMethod | Property value must satisfy context's static method of signature: public static bool <Argument>( context).  Does nothing if no such method is found. | 


## `ResourceAttribute`

Allows attaching custom resources to fields or to the model.  These resources become available to generated controls.
```csharp
public class MaterialForms.Wpf.Annotations.ResourceAttribute
    : Attribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Name | Resource name. Accepts a string. | 
| `Object` | Value | Resource value. Accepts an object or a dynamic expresion. | 


## `SelectFromAttribute`

Specifies that a field can have values from a collection.
```csharp
public class MaterialForms.Wpf.Annotations.SelectFromAttribute
    : Attribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | DisplayPath | Display member path. Accepts a string or a dynamic expression. | 
| `Object` | ItemsSource | Selection items source. Accepts an array, an enum type, or a dynamic resource. | 
| `String` | ItemStringFormat | Item string format. Accepts a string or a dynamic expression. | 
| `Object` | SelectionType | Field selection type. Accepts a  or a dynamic resource. | 
| `String` | ValuePath | Display value path. Accepts a string or a dynamic expression. | 


## `SelectionType`

Determines how a selection field should be displayed.
```csharp
public enum MaterialForms.Wpf.Annotations.SelectionType
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | ComboBox | A selectable ComboBox is displayed. | 
| `1` | ComboBoxEditable | An editable ComboBox is displayed.  Falls back to a non-editable ComboBox if editing is not applicable for a property type. | 
| `2` | RadioButtons | A list of radio buttons is displayed. | 


## `TextAttribute`

Draws regular text.
```csharp
public class MaterialForms.Wpf.Annotations.TextAttribute
    : TextElementAttribute, _Attribute

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FormElement` | CreateElement() |  | 


## `TextElementAttribute`

Represents textual content in a form.
```csharp
public abstract class MaterialForms.Wpf.Annotations.TextElementAttribute
    : FormContentAttribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | IconPadding | Push text to the right to align with icons. Accepts a boolean or a dynamic resource. | 
| `String` | Value | Element content. Accepts a string or a dynamic expression. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | InitializeElement(`FormElement` element) |  | 


## `TitleAttribute`

Draws title text.
```csharp
public class MaterialForms.Wpf.Annotations.TitleAttribute
    : TextElementAttribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Icon | Displayed icon. Accepts a PackIconKind or a dynamic resource. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `FormElement` | CreateElement() |  | 


## `ValidationAction`

Specifies field validation action when an event occurs.
```csharp
public enum MaterialForms.Wpf.Annotations.ValidationAction
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | DoNothing | Does nothing. | 
| `1` | ValidateField | Validates the field. | 
| `2` | ClearErrors | Clears the field of validation errors. | 


## `ValueAttribute`

Specifies a validation rule for a field.
```csharp
public class MaterialForms.Wpf.Annotations.ValueAttribute
    : Attribute, _Attribute

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Object` | Argument | Validator argument. Accepts an object or a dynamic expression.  May be ignored or throw errors if the supplied value is not suitable for the validator.  Accepts an object or a dynamic resource. | 
| `ValidationAction` | ArgumentUpdatedAction | Specifies what happens when argument values change. | 
| `Must` | Condition | Validator type. | 
| `String` | Converter | Value converter name. | 
| `Boolean` | HasValue |  | 
| `String` | Message | Error message if validation fails. Accepts a string or a dynamic expression. | 
| `Boolean` | StrictValidation | If set to true, values that don't pass validation  are prevented from being written to the property. | 
| `Boolean` | ValidatesOnTargetUpdated | Determines whether property changes cause validation. | 
| `Object` | When | Determines whether this validator is active. Accepts a boolean or a dynamic resource. | 


