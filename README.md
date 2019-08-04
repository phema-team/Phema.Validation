# Phema.Validation

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Validation/status.svg)](https://cloud.drone.io/phema-team/Phema.Validation)
[![Nuget](https://img.shields.io/nuget/v/Phema.Validation.svg)](https://www.nuget.org/packages/Phema.Validation)

C# strongly typed expression-based validation library for .NET built on extension methods

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

// Validation key will be `Name`
validationContext.When(person, p => p.Name)
  .Is(name => name == null)
  .AddError("Name must be set");

// Validation key will be `Address.Locations[0].Latitude`
validationContext.When(person, p => p.Address.Locations[0].Latitude)
  .Is(latitude => ...custom check...)
  .AddError("Some custom check failed");

// Override validation parts with `DataMemberAttribute`
[DataMember(Name = "name")]
public string Name { get; set; }
```

## Validation conditions

```csharp
// Check for Phema.Validation.Conditions namespace
validationContext.When(person, p => p.Name)
  .IsNullOrWhitespace()
  .AddError("Name must be set");

// Use multiple conditions (joined with OR)
validationContext.When(person, p => p.Name)
  .IsNull()
  .HasLengthGreater(20)
  // .IsNotNull()
  // .IsEqual()
  // .IsMatch(regex)
  .AddError("Name is invalid");
```

## Validation details

```csharp
// Null if valid
var validationDetails = validationContext.When(person, p => p.Age)
  .IsNull()
  .AddError("Age must be set");

// Use deconstruction
var (key, message) = validationContext.When(person, p => p.Age)
  .IsNull()
  .AddError("Age must be set");
```

## Check validation

```csharp
// Override default ValidationSeverity
validationContext.ValidationSeverity = ValidationSeverity.Warning;

// Throw exception when details severity greater than ValidationContext.ValidationSeverity
validationContext.When(person, p => p.Address)
  .IsNull()
  .AddFatal("Address is not presented!!!"); // If invalid throw ValidationConditionException

// Check if context is valid
validationContext.IsValid();
validationContext.EnsureIsValid(); // If invalid throw ValidationContextException

// Check concrete validation details
validationContext.IsValid(person, p => p.Age);
validationContext.EnsureIsValid(person, p => p.Age);
```

## Validation scopes

- Use scopes when you need to have:
  - Same nested validation path multiple times
  - Empty validation details collection (syncing with parent context/scope)
  - ValidationSeverity override

```csharp
// Validation key will be `Child.*ValidationPart*`
ValidateChild(validationContext.CreateScope(parent, p => p.Child))

// Validation key will be `Address.Locations[0].*ValidationPart*`
ValidateLocation(validationContext.CreateScope(person, p => p.Address.Locations[0]))
```

## High performance with non-expression constructions

```csharp
validationContext.When("key", value)
  .IsNull()
  .AddError("Value is null");

validationContext.CreateScope("key");

validationContext.IsValid("key");
validationContext.EnsureIsValid("key");
```

## Benchmarks (i7 9700k 3.60 GHz, 16Gb 3400 MHz)

- Simpler expression = less costs
- Try to use non-expression extensions in hot paths
- Use `CreateScope` to not to repeat chained member calls (`x => x.Property1.Property2[0].Property3`)
- Expression-based `When` extensions use expression compilation to get value (Invoke)
- Composite indexers `x => x.Collection[indexProvider.Parsed.Index]` use expression compilation (DynamicInvoke)

### Non-expression validation

|        Method |     Mean |     Error |    StdDev |      Max | Iterations |
|-------------- |---------:|----------:|----------:|---------:|-----------:|
|        Simple | 1.421 us | 0.0071 us | 0.0653 us | 1.581 us |      925.0 |
|   CreateScope | 1.287 us | 0.0046 us | 0.0431 us | 1.394 us |      971.0 |
|       IsValid | 1.350 us | 0.0042 us | 0.0401 us | 1.444 us |      986.0 |
| EnsureIsValid | 1.374 us | 0.0042 us | 0.0401 us | 1.475 us |      987.0 |

### Expression validation

|                                      Method |       Mean |     Error |    StdDev |        Max | Iterations |
|-------------------------------------------- |-----------:|----------:|----------:|-----------:|-----------:|
|                            SimpleExpression |  52.181 us | 0.2692 us | 2.5770 us |  60.106 us |      998.0 |
|                           ChainedExpression |  59.643 us | 0.3316 us | 3.1521 us |  68.800 us |      984.0 |
|                       ArrayAccessExpression |  73.636 us | 0.4902 us | 4.6804 us |  89.787 us |      993.0 |
|                ChainedArrayAccessExpression |  80.645 us | 0.5602 us | 5.3484 us |  98.931 us |      993.0 |
| ChainedArrayAccess_DynamicInvoke_Expression | 288.098 us | 0.9826 us | 9.3864 us | 317.175 us |      994.0 |
|                CreateScope_SimpleExpression |   4.443 us | 0.0156 us | 0.1469 us |   4.838 us |      965.0 |
|               CreateScope_ChainedExpression |   5.467 us | 0.0301 us | 0.2849 us |   6.237 us |      973.0 |
|                               IsValid_Empty |   4.642 us | 0.0241 us | 0.2275 us |   5.275 us |      970.0 |
|                          IsValid_Expression |   4.659 us | 0.0192 us | 0.1826 us |   5.138 us |      982.0 |
|                    EnsureIsValid_Expression |   4.664 us | 0.0262 us | 0.2496 us |   5.450 us |      991.0 |
