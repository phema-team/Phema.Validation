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
// It will be `Child.*ValidationKey*` path
ValidateChild(validationContext.CreateFor(parent, p => p.Child))

// Combine paths
// It will be `Address.Locations[0].*ValidationKey*` path
ValidateLocation(validationContext.CreateFor(person, p => p.Address.Locations[0]))

// It will be `Address.Locations[0].Latitude`
validationContext.When(person, p => p.Address.Locations[0].Latitude)
  .Is(latitude => ...)
  .AddError("Some custom check failed");
  
// Override validation key parts with `DataMemberAttribute`
[DataMember(Name = "age")]
public int Age { get; set; }
```
