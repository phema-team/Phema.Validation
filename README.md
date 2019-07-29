# Phema.Validation

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Validation/status.svg)](https://cloud.drone.io/phema-team/Phema.Validation)
[![Nuget](https://img.shields.io/nuget/v/Phema.Validation.svg)](https://www.nuget.org/packages/Phema.Validation)

C# strongly typed expression-based validation library for .NET

## Installation

```bash
$> dotnet add package Phema.Validation
```

## Usage ([example](https://github.com/phema-team/Phema.Validation/blob/master/examples/Phema.Validation.Example/Orders/ExampleOrdersController.cs))

```csharp
// Add `IValidationContext` as scoped service
services.AddValidation(options => ...);

// Get or inject
var validationContext = serviceProvider.GetRequiredService<IValidationContext>();

// Use with a lot of validation rules (Check for Phema.Validation.Conditions namespace)
validationContext.When(person, p => p.Name)
  .IsNullOrWhitespace()
  .Is(name => ...Custom checks...)
  .AddError("Name must be set");

// Get validation details. Null if valid
var details = validationContext.When(person, p => p.Age)
  .IsNull()
  .AddError("Age must be set");

// Throw exception when details severity greater than ValidationContext.ValidationSeverity
validationContext.When(person, p => p.Address)
  .IsNull()
  .AddFatal("Address is not presented!!!"); // If invalid throw ValidationConditionException

// Validate collections
validationContext.When(person, p => p.Children)
  .IsEmpty()
  .AddError("You have no children");

// Check if context is valid
validationContext.IsValid();
validationContext.EnsureIsValid(); // If invalid throw ValidationContextException

// Check concrete validation details
validationContext.IsValid(person, p => p.Age);

// Create nested validationContext
// It will be `Child.*ValidationPart*` validation key
ValidateChild(validationContext.CreateScope(parent, p => p.Child))

// Combine paths
// It will be `Address.Locations[0].*ValidationPart*` validation key
ValidateLocation(validationContext.CreateScope(person, p => p.Address.Locations[0]))

// It will be `Address.Locations[0].Latitude`
validationContext.When(person, p => p.Address.Locations[0].Latitude)
  .Is(latitude => ...)
  .AddError("Some custom check failed");
  
// Override validation parts with `DataMemberAttribute`
[DataMember(Name = "age")]
public int Age { get; set; }
```

## Performance

- Simpler expression = less costs
- Try to use non-expression extensions in hot paths
- Use CreateScope to not to repeat chained member calls (`x => x.Property1.Property2[0].Property3`)
- Expression-based `When` extensions use expression compilation to get value (Invoke)
- Composite indexers `x => x.Collection[indexProvider.Parsed.Index]` use expression compilation (DynamicInvoke)

```csharp
validationContext.When("key", value)
  .IsNull()
  .AddError("Value is null");

validationContext.CreateScope("key");

validationContext.IsValid("key");
validationContext.EnsureIsValid("key");
```

## Benchmarks (i7 9700k 3.60 GHz, 16Gb 3400 MHz)

### Non-expression validation

|        Method |     Mean |     Error |    StdDev |      Max | Iterations |
|-------------- |---------:|----------:|----------:|---------:|-----------:|
|        Simple | 1.414 us | 0.0043 us | 0.0405 us | 1.519 us |      970.0 |
|     CreateScope | 1.315 us | 0.0100 us | 0.0937 us | 1.475 us |      964.0 |
|       IsValid | 1.353 us | 0.0045 us | 0.0430 us | 1.469 us |      985.0 |
| EnsureIsValid | 1.389 us | 0.0042 us | 0.0394 us | 1.494 us |      979.0 |

### Expression validation

|                                      Method |       Mean |     Error |    StdDev |        Max | Iterations |
|-------------------------------------------- |-----------:|----------:|----------:|-----------:|-----------:|
|                            SimpleExpression |  52.033 us | 0.3253 us | 3.1138 us |  60.147 us |      998.0 |
|                           ChainedExpression |  59.606 us | 0.3156 us | 3.0182 us |  66.756 us |      996.0 |
|                       ArrayAccessExpression |  73.399 us | 0.4523 us | 4.3227 us |  87.112 us |      995.0 |
|                ChainedArrayAccessExpression |  80.073 us | 0.4517 us | 4.3173 us |  92.631 us |      995.0 |
| ChainedArrayAccess_DynamicInvoke_Expression | 287.499 us | 0.7942 us | 7.4353 us | 307.544 us |      955.0 |
|                  CreateScope_SimpleExpression |   4.764 us | 0.0276 us | 0.2634 us |   5.484 us |      991.0 |
|                 CreateScope_ChainedExpression |   5.840 us | 0.0239 us | 0.2261 us |   6.375 us |      978.0 |
|                               IsValid_Empty |   4.669 us | 0.0316 us | 0.3008 us |   5.513 us |      990.0 |
|                          IsValid_Expression |   4.653 us | 0.0193 us | 0.1834 us |   5.169 us |      985.0 |
|                    EnsureIsValid_Expression |   4.689 us | 0.0307 us | 0.2890 us |   5.537 us |      966.0 |
