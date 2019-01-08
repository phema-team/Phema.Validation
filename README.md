# Phema.Validation

C# tiny, fast and customizable validation library

- [x] Core library
- [x] Extensions
- [x] Conditions
- [x] Tests
- [x] AspNetCore integration

# Validation
- Model
```csharp
public class Person
{
  public string Name { get; set; }
  public int Age { get; set; }
}
```

- Validation
```csharp
public class PersonValidation : IValidation<Person>
{
  public void Validate(IValidationContext validationContext, Person person)
  {
    validationContext.When(person, p => p.Name)
      .IsNullOrWhitespace()
      .AddError<PersonValidationComponent>(c => c.NameMustBeSet);
      
    validationContext.When(person, p => p.Name)
      .IsLess(2)
      .AddError<PersonValidationComponent>(c => c.NameIsShort);
      
    validationContext.When(person, p => p.Name)
      .IsGreater(20)
      .AddError<PersonValidationComponent>(c => c.NameIsLong);
      
    validationContext.When(person, p => p.Age)
      .IsNotGreater(18)
      .AddError<PersonValidationComponent>(c => c.Underage);
  }
}
```

- ValidationComponent
```csharp
public class PersonValidationComponent : IValidationComponent<Person, PersonValidation>
{
  public PersonValidationComponent()
  {
    NameMustBeSet = new ValidationMessage(() => "Name must be set");
    NameIsShort = new ValidationMessage(() => "Name must be at least two characters");
    NameIsLong = new ValidationMessage(() => "Name must be not longer twenty characters");
    
    Underage = new ValidationMessage(() => "Age must be greater 18");
  }

  public ValidationMessage NameMustBeSet { get; set; }
  public ValidationMessage NameIsShort { get; set; }
  public ValidationMessage NameIsLong { get; set; }
  
  public ValidationMessage Underage { get; set; }
}
```

- Glue all together
```csharp
services.AddPhemaValidation(validation => validation.Add<Person, PersonValidation, PersonValidationComponent>());
```

# Using core
```csharp
var validationContext = new ValidationContext();

// validationContext.When(nameof(model.Name), model.Name)
validationContext.When("key", "value")
  .Is(value => true)
  .Add(() => new ValidationMessage(() => "template"));
  
var error = Assert.Single(validationContext.Errors);

Assert.Equal("key", error.Key);
Assert.Equal("template", error.Message);
```
- This is a lightweight zero-dependent validation core
- You can add to `Is` callback any condition you want or even multiple of them (they will join using `OR`)
- You can store validation messages somewhere and pass them to `Add` callback or use extensions
- In `Errors` property you will find key (Property name or `DataMember` name override) and message (rendered after passing to `Add` callback)
- You can use parameterized validation messages adding arguments after in `Add` and placeholders ({0}, {1}) in template

# Using extensions
```csharp
var person = new Person
{
  Name = null
};

var validationContext = new ValidationContext();

validationContext.When(person, p => p.Name)
  .IsNullOrWhitespace()
  .Add(() => new ValidationMessage(() => "Is null or whitespace"));
  
var error = Assert.Single(validationContext.Errors);

Assert.Equal("Name", error.Key);
Assert.Equal("Is null or whitespace", error.Message);
```
- You can validate model value using expression like example above, or just passing `T`
- You can use predefined conditions like `IsNot`, `IsNull`, `IsEmpty`, etc.
- You can use `Throw` unstead or `Add` to stop execution flow. That will throw `ValidationConditionException` with `Error` property
- You can ensure that validation context is valid or throw `ValidationContextException` with `Errors` property by using `EnsureIsValid`
- You can validate that key in not presented in error using `IsValid<T>(t => t.Key)` or just passing string key
- You can use typed parameters (up to 2 for now) using `Add<TArgument>(...)` or `Throw<TArgument>(...)`

# Using aspnetcore
```csharp
public class TestModel
{
  public string Name { get; set; }
  public int Age { get; set; }
}

public class TestValidation : Validation<TestModel>
{
  private readonly TestValidationComponent component;
  
  public TestValidation(TestValidationComponent component)
  {
    this.component = component;
  }
		
  protected override void Validate(IValidationContext validationContext, TestModel model)
  {
    validationContext.When(model, m => m.Name)
      .IsNull()
      .Add(() => component.NameIsNull);

    validationContext.When(model, m => m.Age)
      .IsInRange(0, 17)
      .Add(() => component.IsUnderage);
  }
}
	
public class TestValidationComponent : ValidationComponent<TestModel, TestValidation>
{
  public TestValidationComponent()
  {
    NameIsNull = Register(() => "Name is null");
    IsUnderage = Register(() => "Is underage");
  }
  
  public ValidationMessage NameIsNull { get; }
  public ValidationMessage IsUnderage { get; }
}
  
// Startup
services.AddValidation(
  validation => 
    validation.Add<TestModel, TestValidation, TestValidationComponent>());
```
- You have to use `mvc` for validation, because validation uses filters for it
- You can inject `IValidationContext` to any part of your application
- You can override key by using `[DataMember(Name = "key")]` attribute
- You can add `Validation` without `ValidationComponent` or `IValidationComponent` without `Validation`, but i prefer not to do that because of responsibility 
- If your validation context will be invalid after controller action executed, result will be substituted by validation messages. Try to check validation messages added explicitly by injection of `IValidationContext` before submiting changes
