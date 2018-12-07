using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(c => {})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
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
				.AddError(() => new ValidationMessage(() => "template1"));
			
			validationContext.When("age2", 12)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template2"));

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid());

			Assert.Collection(exception.Errors,
				e =>
				{
					Assert.Equal("age1", e.Key);
					Assert.Equal("template1", e.Message);
				},
				e =>
				{
					Assert.Equal("age2", e.Key);
					Assert.Equal("template2", e.Message);
				});
		}
		
		[Fact]
		public void EnsureThrowsOnlySevereErrors()
		{
			validationContext.When("age1", 10)
				.Is(value => true)
				.AddWarning(() => new ValidationMessage(() => "template1"));
			
			validationContext.When("age2", 12)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template2"));

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid());
			
			var error = Assert.Single(exception.Errors.Where(err => err.Severity >= exception.Severity));

			Assert.Equal("age2", error.Key);
			Assert.Equal("template2", error.Message);
		}

		[Fact]
		public void EnsureIsValidNotThrowsIfValid()
		{
			validationContext.When("key", 10)
				.Is(value => false)
				.AddError(() => new ValidationMessage(() => "template"));

			validationContext.EnsureIsValid();
		}
		
		[Fact]
		public void EnsureIsValidNotThrowsIfValidByKey()
		{
			validationContext.When("key1", 10)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			validationContext.EnsureIsValid("key2");
		}
		
		[Fact]
		public void EnsureIsValidThrowsIfValidByKey()
		{
			validationContext.When("key1", 10)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			validationContext.EnsureIsValid("key2");
		}
	}
}