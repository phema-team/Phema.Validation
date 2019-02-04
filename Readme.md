## Phema.Validation
```csharp
// Add
services.AddPhemaValidation(configuration => configuration.AddComponent<Model, ModelValidationComponent>());

// Get
var validationContext = serviceProvider.GetRequiredService<IValidationContext>();

// Use
validationContext.When("key", "invalid value")
	.Is(value => value == "invalid value")
	.AddError<ModelComponent>(component => component.ValueIsInvalid);
```
- You can add to `Is` callback any condition you want or even multiple of them (they will join using AND)
- You should store validation templates in your custom `IValidationComponent`'s
- You should use `ValidationTemplate`, `ValidationTemplate<TArg1>`, `ValidationTemplate<TArg2>`etc. or write your custom with `IValidationTemplate`
- You can check if context is valid by calling `IsValid`/`EnsureIsValid` on `IValidationContext`

## Phema.Validation.Conditions
```csharp
validationContext.When("key", "invalid value")
	.IsEqual("invalid value")
	.AddError<ModelComponent>(component => component.ValueIsInvalid);
```
- You can use predefined conditions like IsNot, IsNull, IsEmpty, IsEqual etc.

## Phema.Validation.Expressions
```csharp
validationContext.When(model, m => m.Value)
	.IsEqual("invalid value")
	.AddError<ModelComponent>(component => component.ValueIsInvalid);
```

- You can validate model value using expression manner
- You can override key by using `[DataMember(Name = "key")]` attribute
- You can check if context is valid by calling `IsValid(model, m => m.Value)`/`EnsureIsValid(...)` on `IValidationContext`

## Phema.Validation.Mvc
```csharp
services.AddPhemaValidation(c => c.AddValidationComponent<Model, ModelValidation, ModelValidationComponent>())
	.AddMvcCore()
		.AddPhemaValidationIntegration();
```

- You can use `IValidator<TModel>` to validate mvc input
- You should use `AddPhemaValidationIntegration` to setup mvc