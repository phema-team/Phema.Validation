# Phema.Validation

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Validation/status.svg)](https://cloud.drone.io/phema-team/Phema.Validation)
[![Nuget](https://img.shields.io/nuget/v/Phema.Validation.svg)](https://www.nuget.org/packages/Phema.Validation)

C# strongly typed validation library for .NET

## Installation

```bash
$> dotnet add package Phema.Validation
```

## Usage

```csharp
// Add
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

// Throw details when completely invalid state
validationContext.When(model, m => m.Address)
  .IsNull()
  .ThrowFatal("Address is not presented!!!"); // If invalid throw ValidationConditionException

// Validate collections
for(var index = 0; index < 10; index++)
{
  var forCollection = validationContext.CreateFor(model, m => m.Collection[index]);

  // It will produce `Collection[0].Property` key. 0 - concrete index
  forCollection.When(model.Collection[index], m => m.Property)
    .IsNull()
    .AddError($"Property is null. Index: {index}");

  // It will produce `Collection[0].Field` key. 0 - concrete index
  forCollection.When(model.Collection[index], m => m.Field)
    .IsNull()
    .AddError($"Field is null. Index: {index}");
}

// Check if context is valid
validationContext.IsValid();
validationContext.EnsureIsValid(); // If invalid throw ValidationContextException

// Check concrete validation details
validationContext.IsValid(person, p => p.Age);

// Create nested validationContext
ValidateChildren(validationContext.CreateFor(person, p => p.Children)) // It will be `Children.*ValidationKey*` path

// Combine paths
ValidateLocation(validationContext
  .CreateFor(person, p => p.Address)
  .CreateFor(person.Address, a => a.Location)) // It will be `Address.Location.*ValidationKey*` path

```
