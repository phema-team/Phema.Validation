# Phema.Validation

[![Nuget](https://img.shields.io/nuget/v/Phema.Validation.svg)](https://www.nuget.org/packages/Phema.Validation)
[![Nuget](https://img.shields.io/nuget/vpre/Phema.Validation.svg)](https://www.nuget.org/packages/Phema.Validation)

C# strongly typed validation library for `.NET Core` and `ASP.NET Core`

## Packages

- `Phema.Validation.Core` - Core library on top of `Microsoft.Extensions.DepencencyInjection`
- `Phema.Validation.Conditions` - Predefined useful conditions like `IsNot`, `IsNull`, `IsEmpty` etc.
- `Phema.Validation` - Mvc integration for `Phema.Validation.Core`

## Usage

### Phema.Validation.Core

```csharp
// Add
services.AddPhemaValidation(builder => builder.AddComponent<Model, ModelValidationComponent>());

// Get
var validationContext = serviceProvider.GetRequiredService<IValidationContext>();

// Use
validationContext.When("key", "invalid")
  .Is(value => value == "invalid")
  .AddError<ModelComponent>(component => component.ValueIsInvalid);

validationContext.When(model, m => m.Value)
  .IsEqual("invalid")
  .AddError<ModelComponent>(component => component.ValueIsInvalid);
```

- To check if value is valid use `Is` on `IValidationCondition`
- Store your `IValidationTemplate`'s in `IValidationComponent`'s
- To create validation messages use `ValidationTemplate`, `ValidationTemplate<TArg1>`, `ValidationTemplate<TArg1, TArg2>`, etc. or write your custom
- Validate model value using expression manner `model, m => m.Value`
- To override key use `[DataMember(Name = "key")]` attribute
- Check that context is valid by calling `IsValid(model, m => m.Value)`/`EnsureIsValid(...)` on `IValidationContext`

### Phema.Validation.Conditions

```csharp
validationContext.When("key", "invalid value")
  .IsEqual("invalid value")
  .AddError<ModelComponent>(component => component.ValueIsInvalid);
```

- Predefined conditions like `IsNot`, `IsNull`, `IsEmpty`, `IsEqual` etc.

### Phema.Validation

```csharp
services.AddMvcCore()
  .AddPhemaValidation(builder =>
    builder.AddValidationComponent<Model, ModelValidation, ModelValidationComponent>())
```

- To validate mvc input implement `IValidator<TModel>` interface