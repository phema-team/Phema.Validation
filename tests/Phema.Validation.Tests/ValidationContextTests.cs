using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddComponent<TestModel, TestModelValidationComponent>())
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void EmptyWhen()
		{
			var (key, message) = validationContext.When()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IfAnyErrorIsValidIsFalse()
		{
			validationContext.When("age", 10)
				.Is(value => value == 10)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Single(validationContext.Errors);
			Assert.False(validationContext.IsValid());
		}

		[Fact]
		public void IfNoErrorIsValidIsTrue()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

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
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.False(validationContext.IsValid("age"));
		}

		[Fact]
		public void IsValidByKey()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(validationContext.IsValid("key"));
		}

		[Fact]
		public void EnsureThrowsIfAnyError()
		{
			validationContext.When("key", 10)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid());

			var (key, message) = Assert.Single(exception.Errors);

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void EnsureThrowsMultipleError()
		{
			validationContext.When("age1", 10)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			validationContext.When("age2", 12)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate2);

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
				.AddWarning<TestModelValidationComponent>(c => c.TestModelTemplate1);

			validationContext.When("age2", 12)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate2);

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid());

			var (key, message) = Assert.Single(exception.Errors.Where(err => err.Severity >= exception.Severity));

			Assert.Equal("age2", key);
			Assert.Equal("template2", message);
		}

		[Fact]
		public void EnsureIsValidNotThrowsIfValid()
		{
			validationContext.When("key", 10)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			validationContext.EnsureIsValid();
		}

		[Fact]
		public void EnsureIsValidNotThrowsIfValidByKey()
		{
			validationContext.When("key1", 10)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			validationContext.EnsureIsValid("key2");
		}

		[Fact]
		public void EnsureIsValidThrowsIfValidByKey()
		{
			validationContext.When("key1", 10)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			validationContext.EnsureIsValid("key2");
		}
		
		[Fact]
		public void EmptyValidationResolves()
		{
			var services = new ServiceCollection();

			services.AddPhemaValidation();

			var provider = services.BuildServiceProvider();

			var validationContext = provider.GetRequiredService<IValidationContext>();

			Assert.IsType<ValidationContext>(validationContext);
			Assert.Equal(ValidationSeverity.Error, validationContext.Severity);
			Assert.Equal(0, validationContext.Errors.Count);
		}
		
		[Fact]
		public void ConfigureValidationOptionsChangesSeverity()
		{
			var services = new ServiceCollection();

			services.AddPhemaValidation();

			services.Configure<PhemaValidationOptions>(options => options.Severity = ValidationSeverity.Debug);

			var provider = services.BuildServiceProvider();

			var validationContext = provider.GetRequiredService<IValidationContext>();

			Assert.Equal(ValidationSeverity.Debug, validationContext.Severity);
		}
	}
}