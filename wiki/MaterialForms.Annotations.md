## `AssertionConditionType`

Specifies assertion type. If the assertion method argument satisfies the condition,  then the execution continues. Otherwise, execution is assumed to be halted.
```csharp
public enum MaterialForms.Annotations.AssertionConditionType
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | IS_TRUE | Marked parameter should be evaluated to true. | 
| `1` | IS_FALSE | Marked parameter should be evaluated to false. | 
| `2` | IS_NULL | Marked parameter should be evaluated to null value. | 
| `3` | IS_NOT_NULL | Marked parameter should be evaluated to not null value. | 


## `CollectionAccessType`

```csharp
public enum MaterialForms.Annotations.CollectionAccessType
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | None | Method does not use or modify content of the collection. | 
| `1` | Read | Method only reads content of the collection but does not modify it. | 
| `2` | ModifyExistingContent | Method can change content of the collection but does not add new elements. | 
| `6` | UpdatedContent | Method can add new elements to the collection. | 


## `ImplicitUseKindFlags`

```csharp
public enum MaterialForms.Annotations.ImplicitUseKindFlags
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `1` | Access | Only entity marked with attribute considered used. | 
| `2` | Assign | Indicates implicit assignment to a member. | 
| `4` | InstantiatedWithFixedConstructorSignature | Indicates implicit instantiation of a type with fixed constructor signature.  That means any unused constructor parameters won't be reported as such. | 
| `7` | Default |  | 
| `8` | InstantiatedNoFixedConstructorSignature | Indicates implicit instantiation of a type. | 


## `ImplicitUseTargetFlags`

Specify what is considered used implicitly when marked  with  or .
```csharp
public enum MaterialForms.Annotations.ImplicitUseTargetFlags
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `1` | Default |  | 
| `1` | Itself |  | 
| `2` | Members | Members of entity marked with attribute are considered used. | 
| `3` | WithMembers | Entity marked with attribute and all its members considered used. | 


