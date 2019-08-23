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
- Expression-based `When` extensions use expression compilation to get value (Invoke)
- Composite indexers `x => x.Collection[indexProvider.Parsed.Index]` use expression compilation (DynamicInvoke)

### Non-expression validation

|        Method |     Mean |     Error |    StdDev |      Max | Iterations |
|-------------- |---------:|----------:|----------:|---------:|-----------:|
|        Simple | 1.431 us | 0.0048 us | 0.0454 us | 1.547 us |      955.0 |
|   CreateScope | 1.316 us | 0.0043 us | 0.0412 us | 1.425 us |      977.0 |
|       IsValid | 1.627 us | 0.0155 us | 0.1441 us | 1.981 us |      942.0 |
| EnsureIsValid | 1.655 us | 0.0157 us | 0.1490 us | 2.006 us |      978.0 |

### Expression validation

|                                         Method |       Mean |     Error |    StdDev |        Max | Iterations |
|----------------------------------------------- |-----------:|----------:|----------:|-----------:|-----------:|
|                               SimpleExpression |   3.520 us | 0.0225 us | 0.2136 us |   4.237 us |      986.0 |
|                 SimpleExpression_CompiledValue |  49.909 us | 0.3173 us | 3.0343 us |  57.062 us |      996.0 |
|                              ChainedExpression |   4.047 us | 0.0334 us | 0.3183 us |   5.062 us |      990.0 |
|                ChainedExpression_CompiledValue |  55.812 us | 0.3958 us | 3.7844 us |  66.694 us |      996.0 |
|                          ArrayAccessExpression |   4.502 us | 0.0170 us | 0.1606 us |   4.981 us |      976.0 |
|            ArrayAccessExpression_CompiledValue |  70.870 us | 0.4830 us | 4.6186 us |  86.075 us |      996.0 |
|                   ChainedArrayAccessExpression |   4.823 us | 0.0439 us | 0.4174 us |   6.250 us |      983.0 |
|     ChainedArrayAccessExpression_CompiledValue |  75.644 us | 0.5110 us | 4.8820 us |  92.688 us |      994.0 |
|               ChainedArrayAccess_DynamicInvoke | 126.887 us | 0.6209 us | 5.9341 us | 147.000 us |      995.0 |
| ChainedArrayAccess_DynamicInvoke_CompiledValue | 280.266 us | 0.6133 us | 5.5828 us | 297.750 us |      903.0 |
|                   CreateScope_SimpleExpression |   3.465 us | 0.0185 us | 0.1744 us |   4.044 us |      972.0 |
|                  CreateScope_ChainedExpression |   4.031 us | 0.0380 us | 0.3573 us |   5.219 us |      964.0 |
|                                  IsValid_Empty |   4.125 us | 0.0228 us | 0.2142 us |   4.688 us |      960.0 |
|                             IsValid_Expression |   4.121 us | 0.0187 us | 0.1760 us |   4.641 us |      963.0 |
|                       EnsureIsValid_Expression |   4.113 us | 0.0196 us | 0.1829 us |   4.625 us |      949.0 |