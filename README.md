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
- `IValidationScope` - Is a nested validation context with validation path and severity override

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
  // .IsNull()
  // .IsEqual()
  // .IsNotUrl()
  // .IsNotEmail()
  // .IsMatch(regex)
  .AddError("Name should be less than 20");

// Type checks
validationContext.When(person, p => p.Car)
  .IsType<Ferrari>()
  .Is(ferarriCar => ...Some Ferrari specific checks...)
  .AddError("You have Ferrari car, but ...");
```

## Validation details

```csharp
// Null if valid
var validationDetails = validationContext.When(person, p => p.Age)
  // Validation check is failed, validation condition is valid
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

|                      Method |     Mean |     Error |    StdDev |   Median | Allocated |
|---------------------------- |---------:|----------:|----------:|---------:|----------:|
|                     Default | 1.307 us | 0.0031 us | 0.0903 us | 1.309 us |     936 B |
|    DataMember_WithAttribute | 6.904 us | 0.0105 us | 0.2985 us | 6.909 us |    2398 B |
| DataMember_WithoutAttribute | 1.983 us | 0.0050 us | 0.1465 us | 1.991 us |    1430 B |
|            PascalCase_Lower | 1.604 us | 0.0046 us | 0.1333 us | 1.572 us |    1048 B |
|                  PascalCase | 1.365 us | 0.0038 us | 0.1132 us | 1.366 us |     936 B |
|             CamelCase_Upper | 1.549 us | 0.0035 us | 0.0986 us | 1.569 us |    1040 B |
|                   CamelCase | 1.432 us | 0.0037 us | 0.1092 us | 1.434 us |     936 B |

### Non-expression validation

|        Method |     Mean |     Error |   StdDev |   Median | Allocated |
|-------------- |---------:|----------:|---------:|---------:|----------:|
|        Simple | 215.7 ns | 0.9524 ns | 27.52 ns | 225.0 ns |     266 B |
|   CreateScope | 119.7 ns | 0.5997 ns | 17.87 ns | 128.1 ns |     112 B |
|       IsValid | 219.3 ns | 0.6558 ns | 19.38 ns | 228.1 ns |     424 B |
| EnsureIsValid | 218.2 ns | 0.6113 ns | 17.72 ns | 225.0 ns |     424 B |

### Expression validation

|                                     Method |       Mean |     Error |    StdDev |     Median | Allocated |
|------------------------------------------- |-----------:|----------:|----------:|-----------:|----------:|
|                           SimpleExpression | 1,216.5 ns | 2.0927 ns |  60.78 ns | 1,231.2 ns |     914 B |
|             SimpleExpression_CompiledValue | 1,437.8 ns | 2.2016 ns |  62.72 ns | 1,456.2 ns |    1010 B |
|                          ChainedExpression | 1,680.5 ns | 3.0921 ns |  90.31 ns | 1,712.5 ns |    1258 B |
|            ChainedExpression_CompiledValue | 1,908.5 ns | 3.4080 ns |  99.13 ns | 1,940.6 ns |    1354 B |
|                      ArrayAccessExpression | 1,766.2 ns | 3.5956 ns | 103.22 ns | 1,803.1 ns |    1410 B |
|        ArrayAccessExpression_CompiledValue | 2,002.5 ns | 4.0274 ns | 117.76 ns | 2,056.2 ns |    1506 B |
|               ChainedArrayAccessExpression | 2,312.1 ns | 4.5806 ns | 133.17 ns | 2,368.8 ns |    1770 B |
| ChainedArrayAccessExpression_CompiledValue | 2,542.6 ns | 5.6628 ns | 164.75 ns | 2,593.8 ns |    1866 B |
|                         ChainedArrayAccess | 3,998.3 ns | 9.4951 ns | 275.09 ns | 4,078.1 ns |    2715 B |
|           ChainedArrayAccess_CompiledValue | 4,669.4 ns | 9.7369 ns | 283.27 ns | 4,790.6 ns |    2802 B |
|               CreateScope_SimpleExpression | 1,050.5 ns | 1.2892 ns |  37.55 ns | 1,053.1 ns |     736 B |
|              CreateScope_ChainedExpression | 1,375.0 ns | 1.3573 ns |  39.32 ns | 1,375.0 ns |    1080 B |
|                              IsValid_Empty |   113.1 ns | 0.6117 ns |  17.99 ns |   121.9 ns |     184 B |
|                         IsValid_Expression | 1,155.1 ns | 1.0107 ns |  28.99 ns | 1,150.0 ns |    1048 B |
|                   EnsureIsValid_Expression | 1,098.0 ns | 1.1002 ns |  31.19 ns | 1,095.3 ns |    1048 B |
