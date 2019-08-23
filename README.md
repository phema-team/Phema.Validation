# Phema.Validation

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Validation/status.svg)](https://cloud.drone.io/phema-team/Phema.Validation)
[![Nuget](https://img.shields.io/nuget/v/Phema.Validation.svg)](https://www.nuget.org/packages/Phema.Validation)
[![Nuget](https://img.shields.io/nuget/dt/Phema.Validation.svg)](https://nuget.org/packages/Phema.Validation)

A simple, lightweight and extensible validation library for .NET Core with fluent interfaces built on top of extension methods

## Installation

```bash
$> dotnet add package Phema.Validation
```

## Core concepts
- `IValidationContext` - Scoped service to store all validation details
- `IValidationCondition` - Contains a validation checks (e.g. `Is(() => ...)`)
- `IValidationDetail` - When `IValidationCondition` is not valid adds to `IValidationContext.ValidationDetails`
- `ValidationSeverity` - Validation error level, used in `IValidationContext.ValidationSeverity` and `IValidationDetail.ValidationSeverity`
- `IValidationScope` - Is a nested validation context with validation path override

## Usage ([ASP.NET Core](https://github.com/phema-team/Phema.Validation/tree/master/examples/Phema.Validation.Examples.AspNetCore), [HostedService](https://github.com/phema-team/Phema.Validation/tree/master/examples/Phema.Validation.Examples.WorkerService) examples)

```csharp
// Add `IValidationContext` as scoped service
services.AddValidation(options => ...);

// Get or inject
var validationContext = serviceProvider.GetRequiredService<IValidationContext>();

// Validation key will be `Name` using default validation part provider
validationContext.When(person, p => p.Name)
  .Is(name => name == null)
  .AddError("Name must be set");

// Validation key will be `Address.Locations[0].Latitude` using default validation part provider
validationContext.When(person, p => p.Address.Locations[0].Latitude)
  .Is(latitude => ...custom check...)
  .AddError("Some custom check failed");
```

## Validation conditions

- Monads are not composable, so `Is` and `IsNot`, `IsNull` and `IsNotNull`... duplication

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
  // Validation condition is valid
  .Is(() => false)
  .AddError("Age must be set");

// Use deconstruction
var (key, message) = validationContext.When(person, p => p.Age)
  .IsNull()
  .AddError("Age must be set");

// More deconstruction
var (key, message, severity) = validationContext.When(person, p => p.Age)
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

## Compose and reuse validation rules with extensions

- Call is allocation free
- Static checks

```csharp
// Extensions
public static void ValidateCustomer(this IValidationContext validationContext, Customer customer)
{
  // Some checks
}

validationContext.ValidateCustomer(customer);
```

- Write your own middleware or validation components/validators on top of `IValidationContext`

## Validation part resolvers

- `ValidationPartResolver` is a delegate, trying to get `string` valdiation part from `MemberInfo`
- Use built in resolvers with `ValidationPartResolvers` static class: `Default`, `DataMember`, `PascalCase`, `CamelCase`

```csharp
// Configure DataMember validation part resolver
services.AddValidation(options =>
  options.ValidationPartResolver = ValidationPartResolvers.DataMember);

// Override validation parts with `DataMemberAttribute`
[DataMember(Name = "name")]
public string Name { get; set; }
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

// Override local scope ValidationSeverity
using (var scope = validationContext.CreateScope(person, p => p.Address))
{
  scope.ValidationSeverity = ValidationSeverity.Warning;

  // Some scope validation checks syncing with validationContext
}
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
- Expression-based `When` with `Is(value => ...)` extensions use lazy expression compilation to get value (Invoke)

### ValidationPartResolvers

|     Method |     Mean |     Error |    StdDev |       Max | Iterations |
|----------- |---------:|----------:|----------:|----------:|-----------:|
|    Default | 2.002 us | 0.0584 us | 0.5578 us |  4.031 us |      995.0 |
| DataMember | 9.292 us | 0.1589 us | 1.5176 us | 13.200 us |      994.0 |
| PascalCase | 2.222 us | 0.0580 us | 0.5542 us |  4.062 us |      993.0 |
|  CamelCase | 2.341 us | 0.0527 us | 0.5020 us |  3.431 us |      988.0 |

### Non-expression validation

|        Method |     Mean |     Error |    StdDev |      Max | Iterations |
|-------------- |---------:|----------:|----------:|---------:|-----------:|
|        Simple | 1.431 us | 0.0048 us | 0.0454 us | 1.547 us |      955.0 |
|   CreateScope | 1.316 us | 0.0043 us | 0.0412 us | 1.425 us |      977.0 |
|       IsValid | 1.627 us | 0.0155 us | 0.1441 us | 1.981 us |      942.0 |
| EnsureIsValid | 1.655 us | 0.0157 us | 0.1490 us | 2.006 us |      978.0 |

### Expression validation

|                                     Method |       Mean |     Error |    StdDev |        Max | Iterations |
|------------------------------------------- |-----------:|----------:|----------:|-----------:|-----------:|
|                           SimpleExpression |   3.669 us | 0.0145 us | 0.1379 us |   4.025 us |      985.0 |
|             SimpleExpression_CompiledValue |  50.204 us | 0.2615 us | 2.4918 us |  56.900 us |      989.0 |
|                          ChainedExpression |   4.231 us | 0.0173 us | 0.1636 us |   4.694 us |      975.0 |
|            ChainedExpression_CompiledValue |  56.831 us | 0.3474 us | 3.3099 us |  67.562 us |      989.0 |
|                      ArrayAccessExpression |   4.404 us | 0.0291 us | 0.2747 us |   5.244 us |      968.0 |
|        ArrayAccessExpression_CompiledValue |  70.077 us | 0.4725 us | 4.5157 us |  82.300 us |      995.0 |
|               ChainedArrayAccessExpression |   4.958 us | 0.0431 us | 0.4031 us |   6.125 us |      951.0 |
| ChainedArrayAccessExpression_CompiledValue |  77.390 us | 0.4006 us | 3.8073 us |  88.612 us |      984.0 |
|                         ChainedArrayAccess |   7.129 us | 0.0797 us | 0.7534 us |   9.372 us |      974.0 |
|           ChainedArrayAccess_CompiledValue | 172.623 us | 0.6886 us | 6.5746 us | 194.213 us |      993.0 |
|               CreateScope_SimpleExpression |   3.577 us | 0.0169 us | 0.1591 us |   3.925 us |      968.0 |
|              CreateScope_ChainedExpression |   3.930 us | 0.0361 us | 0.3391 us |   5.069 us |      962.0 |
|                              IsValid_Empty |   4.086 us | 0.0164 us | 0.1553 us |   4.531 us |      974.0 |
|                         IsValid_Expression |   4.124 us | 0.0139 us | 0.1315 us |   4.463 us |      979.0 |
|                   EnsureIsValid_Expression |   3.978 us | 0.0251 us | 0.2298 us |   4.731 us |      912.0 |
