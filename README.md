# Phema.Validation

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Validation/status.svg)](https://cloud.drone.io/phema-team/Phema.Validation)
[![Nuget](https://img.shields.io/nuget/v/Phema.Validation.svg)](https://www.nuget.org/packages/Phema.Validation)
[![Nuget](https://img.shields.io/nuget/dt/Phema.Validation.svg)](https://nuget.org/packages/Phema.Validation)

A simple, lightweight and extensible validation library for .NET Core with fluent interfaces built using extension methods only

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

// Use multiple conditions (joined with AND)
validationContext.When(person, p => p.Name)
  .IsNotNull()
  // AND
  .HasLengthGreater(20)
  // .IsNotNull()
  // .IsEqual()
  // .IsMatch(regex)
  .AddError("Name should be less than 20");
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

|     Method |       Mean |     Error |    StdDev |        Max |
|----------- |-----------:|----------:|----------:|-----------:|
|    Default |   937.1 ns |  13.13 ns |  19.65 ns |   985.4 ns |
| DataMember | 5,178.0 ns |  61.97 ns |  92.75 ns | 5,414.8 ns |
| PascalCase | 1,451.4 ns | 211.22 ns | 328.85 ns | 1,875.8 ns |
|  CamelCase | 1,647.5 ns |  51.41 ns |  78.52 ns | 1,844.8 ns |

### Non-expression validation

|        Method |     Mean |     Error |    StdDev |      Max |
|-------------- |---------:|----------:|----------:|---------:|
|        Simple | 183.1 ns | 0.2391 ns | 0.3429 ns | 184.0 ns |
|   CreateScope | 134.2 ns | 0.7517 ns | 1.1479 ns | 136.9 ns |
|       IsValid | 375.5 ns | 0.7937 ns | 1.1384 ns | 379.1 ns |
| EnsureIsValid | 361.0 ns | 2.3126 ns | 3.2419 ns | 369.8 ns |

### Expression validation

|                                     Method |     Mean |     Error |    StdDev |      Max |
|------------------------------------------- |---------:|----------:|----------:|---------:|
|                           SimpleExpression | 1.622 us | 0.0220 us | 0.0343 us | 1.712 us |
|             SimpleExpression_CompiledValue | 2.347 us | 0.0302 us | 0.0453 us | 2.453 us |
|                          ChainedExpression | 2.305 us | 0.0214 us | 0.0320 us | 2.385 us |
|            ChainedExpression_CompiledValue | 2.037 us | 0.0158 us | 0.0222 us | 2.103 us |
|                      ArrayAccessExpression | 2.492 us | 0.0279 us | 0.0425 us | 2.595 us |
|        ArrayAccessExpression_CompiledValue | 2.468 us | 0.0209 us | 0.0313 us | 2.542 us |
|               ChainedArrayAccessExpression | 3.028 us | 0.1101 us | 0.1614 us | 3.147 us |
| ChainedArrayAccessExpression_CompiledValue | 2.427 us | 0.0364 us | 0.0567 us | 2.558 us |
|                         ChainedArrayAccess | 3.521 us | 0.0235 us | 0.0352 us | 3.596 us |
|           ChainedArrayAccess_CompiledValue | 4.337 us | 0.0536 us | 0.0803 us | 4.525 us |
|               CreateScope_SimpleExpression | 1.619 us | 0.0088 us | 0.0137 us | 1.645 us |
|              CreateScope_ChainedExpression | 2.060 us | 0.0131 us | 0.0200 us | 2.115 us |
|                              IsValid_Empty | 2.044 us | 0.0184 us | 0.0276 us | 2.099 us |
|                         IsValid_Expression | 2.110 us | 0.0096 us | 0.0135 us | 2.152 us |
|                   EnsureIsValid_Expression | 2.027 us | 0.0209 us | 0.0312 us | 2.116 us |
