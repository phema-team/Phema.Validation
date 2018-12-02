using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextExtensionsTests()
		{
			validationContext = new ValidationContext();
		}
		
		[Fact]
		public void EmptyWhen()
		{
			var error = validationContext.When()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsWorksAnsGenericVersion()
		{
			var stab = new TestModel();

			validationContext.When(stab, s => s.Name)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenWithFuncSelector()
		{
			var stab = new TestModel();

			validationContext.When(stab, s => s.Name, s => s.Name)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IfAnyErrorIsValidIsFalse()
		{
			validationContext.When("age", 10)
				.Is(value => value == 10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Single(validationContext.Errors);
			Assert.False(validationContext.IsValid());
		}
		
		[Fact]
		public void IfNoErrorIsValidIsTrue()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Empty(validationContext.Errors);
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsValidTrueIfNoKeyPresented()
		{
			Assert.True(validationContext.IsValid("key"));
			Assert.True(validationContext.IsValid("age"));
			Assert.True(validationContext.IsValid("name"));
		}
		
		[Fact]
		public void IsInvalidByKey()
		{
			validationContext.When("age", 10)
				.Is(value => value == 10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.False(validationContext.IsValid("age"));
		}
		
		[Fact]
		public void IsValidByKey()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid("key"));
		}
		
		[Fact]
		public void IsValidByKeyExpression()
		{
			var model = new TestModel();
			
			validationContext.When(model, s => s.Name)
				.Is(value => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid<TestModel>(s => s.Name));
		}
		
		[Fact]
		public void IsInvalidByKeyExpression()
		{
			var model = new TestModel();
			
			validationContext.When(model, s => s.Name)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.False(validationContext.IsValid<TestModel>(s => s.Name));
		}
		
		[Fact]
		public void IsValidByKeyModelExpression()
		{
			var model = new TestModel();
			
			validationContext.When(model, s => s.Name)
				.Is(value => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid(model, s => s.Name));
		}
		
		[Fact]
		public void IsInvalidByKeyModelExpression()
		{
			var model = new TestModel();
			
			validationContext.When(model, s => s.Name)
				.Is(() => false)
				.AddError(() => new ValidationMessage(() => "template1"));
			
			validationContext.When(model, s => s.Name)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template2"));

			Assert.False(validationContext.IsValid(model, s => s.Name));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("name", error.Key);
			Assert.Equal("template2", error.Message);
		}
		
		[Fact]
		public void EnsureThrowsIfAnyError()
		{
			validationContext.When("key", 10)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid());

			var error = Assert.Single(exception.Errors);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void EnsureThrowsMultipleError()
		{
			validationContext.When("age1", 10)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			validationContext.When("age2", 12)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid());

			Assert.Collection(exception.Errors,
				e =>
				{
					Assert.Equal("age1", e.Key);
					Assert.Equal("template", e.Message);
				},
				e =>
				{
					Assert.Equal("age2", e.Key);
					Assert.Equal("template", e.Message);
				});
		}

		[Fact]
		public void EnsureIsValidNotThrowsIfValid()
		{
			validationContext.When("key", 10)
				.Is(value => false)
				.AddError(() => new ValidationMessage(() => "template"));

			validationContext.EnsureIsValid();
		}
	}
}