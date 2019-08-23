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
|    Default | 2.050 us | 0.0579 us | 0.5535 us |  3.938 us |      997.0 |
| DataMember | 9.402 us | 0.1911 us | 1.8230 us | 14.894 us |      991.0 |
| PascalCase | 2.326 us | 0.0465 us | 0.4431 us |  3.612 us |      988.0 |
|  CamelCase | 2.430 us | 0.0498 us | 0.4721 us |  3.706 us |      981.0 |

### Non-expression validation

|        Method |     Mean |     Error |    StdDev |      Max | Iterations |
|-------------- |---------:|----------:|----------:|---------:|-----------:|
|        Simple | 1.425 us | 0.0053 us | 0.0500 us | 1.562 us |      971.0 |
|   CreateScope | 1.300 us | 0.0045 us | 0.0424 us | 1.413 us |      975.0 |
|       IsValid | 1.627 us | 0.0182 us | 0.1714 us | 2.034 us |      967.0 |
| EnsureIsValid | 1.646 us | 0.0156 us | 0.1477 us | 1.950 us |      977.0 |

### Expression validation

|                                     Method |      Mean |     Error |    StdDev |       Max | Iterations |
|------------------------------------------- |----------:|----------:|----------:|----------:|-----------:|
|                           SimpleExpression |  3.957 us | 0.0251 us | 0.2385 us |  4.625 us |      982.0 |
|             SimpleExpression_CompiledValue |  8.747 us | 0.0462 us | 0.4265 us |  9.800 us |      929.0 |
|                          ChainedExpression |  4.404 us | 0.0384 us | 0.3485 us |  5.544 us |      899.0 |
|            ChainedExpression_CompiledValue | 10.281 us | 0.0471 us | 0.4228 us | 11.188 us |      879.0 |
|                      ArrayAccessExpression |  5.068 us | 0.0385 us | 0.3670 us |  5.956 us |      988.0 |
|        ArrayAccessExpression_CompiledValue | 11.947 us | 0.0466 us | 0.4287 us | 13.025 us |      924.0 |
|               ChainedArrayAccessExpression |  5.475 us | 0.0291 us | 0.2772 us |  6.200 us |      986.0 |
| ChainedArrayAccessExpression_CompiledValue | 13.066 us | 0.0489 us | 0.4518 us | 14.041 us |      930.0 |
|                         ChainedArrayAccess |  8.384 us | 0.0547 us | 0.5218 us | 10.287 us |      993.0 |
|           ChainedArrayAccess_CompiledValue | 22.303 us | 0.0952 us | 0.8856 us | 25.038 us |      942.0 |
|               CreateScope_SimpleExpression |  3.389 us | 0.0132 us | 0.1247 us |  3.737 us |      973.0 |
|              CreateScope_ChainedExpression |  3.953 us | 0.0334 us | 0.3185 us |  4.831 us |      990.0 |
|                              IsValid_Empty |  4.071 us | 0.0192 us | 0.1828 us |  4.612 us |      987.0 |
|                         IsValid_Expression |  4.013 us | 0.0157 us | 0.1489 us |  4.431 us |      978.0 |
|                   EnsureIsValid_Expression |  3.971 us | 0.0178 us | 0.1625 us |  4.469 us |      909.0 |
