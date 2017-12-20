## `ProgressController`

```csharp
public class MaterialForms.Tasks.ProgressController

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `MaterialDialog` | Dialog |  | 
| `ProgressSchema` | Schema |  | 


Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | CancellationRequested |  | 
| `Int32` | Maximum |  | 
| `String` | Message |  | 
| `Double` | Progress |  | 


## `ProgressDialogOptions`

```csharp
public class MaterialForms.Tasks.ProgressDialogOptions

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Cancel |  | 
| `Boolean` | IsIndeterminate |  | 
| `Int32` | Maximum |  | 
| `String` | Message |  | 
| `Double` | Progress |  | 
| `String` | Title |  | 


## `TaskRunner`

Allows running background tasks while displaying a controllable progress dialog.
```csharp
public static class MaterialForms.Tasks.TaskRunner

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Task` | Run(`Func<ProgressController, Task>` callback) |  | 
| `Task` | Run(`Func<ProgressController, Task>` callback, `Session` session) |  | 
| `Task` | Run(`Func<ProgressController, Task>` callback, `String` dialogIdentifier) |  | 
| `Task` | Run(`Func<ProgressController, Task>` callback, `ProgressDialogOptions` options) |  | 
| `Task` | Run(`Func<ProgressController, Task>` callback, `ProgressDialogOptions` options, `Session` session) |  | 
| `Task` | Run(`Func<ProgressController, Task>` callback, `ProgressDialogOptions` options, `String` dialogIdentifier) |  | 
| `Task<T>` | Run(`Func<ProgressController, Task<T>>` callback) |  | 
| `Task<T>` | Run(`Func<ProgressController, Task<T>>` callback, `Session` session) |  | 
| `Task<T>` | Run(`Func<ProgressController, Task<T>>` callback, `String` dialogIdentifier) |  | 
| `Task<T>` | Run(`Func<ProgressController, Task<T>>` callback, `ProgressDialogOptions` options) |  | 
| `Task<T>` | Run(`Func<ProgressController, Task<T>>` callback, `ProgressDialogOptions` options, `Session` session) |  | 
| `Task<T>` | Run(`Func<ProgressController, Task<T>>` callback, `ProgressDialogOptions` options, `String` dialogIdentifier) |  | 


