# Phema.Validation

C# tiny, fast and customizable validation library

- [x] Core library
- [x] Extensions
- [x] Conditions
- [x] Tests
- [ ] AspNetCore integration

```csharp
var validationContext = new ValidationContext();

validationContext.When("key")
  .Is(() => true)
  .Add(new ValidationMessage("template"));
  
var error = Assert.Single(validationContext.Errors);

Assert.Equal("key", error.Key);
Assert.Equal("template", error.Message);
```

```csharp
var person = new Person
{
  Name = null
};


var validationContext = new ValidationContext();

validationContext.When(person, p => p.Name)
  .IsNullOrWhitespace(person.Name)
  .Add(new ValidationMessage("Is null or whitespace"));
  
var error = Assert.Single(validationContext.Errors);

Assert.Equal("Name", error.Key);
Assert.Equal("Is null or whitespace", error.Message);
```
