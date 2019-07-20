using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationPredicateTests
	{
		private readonly IValidationContext validationContext;

		public ValidationPredicateTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void ValidationPredicate_AddMessage_ReturnsMessage()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(value => true)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			Assert.Equal("key", validationMessage.Key);
			Assert.Equal("Key is not valid", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_EmptyIs_HasMessage()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			Assert.Equal("key", validationMessage.Key);
			Assert.Equal("Key is not valid", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}


		[Fact]
		public void ValidationPredicate_AddTrace()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddTrace("Key is not valid");

			Assert.Equal(ValidationSeverity.Trace, validationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_AddDebug()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddDebug("Key is not valid");

			Assert.Equal(ValidationSeverity.Debug, validationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_AddInformation()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddInformation("Key is not valid");

			Assert.Equal(ValidationSeverity.Information, validationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_AddWarning()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddWarning("Key is not valid");

			Assert.Equal(ValidationSeverity.Warning, validationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_AddError()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddError("Key is not valid");

			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_AddFatal()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddFatal("Key is not valid");

			Assert.Equal(ValidationSeverity.Fatal, validationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_ThrowMessage()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowMessage("Key is not valid", ValidationSeverity.Error));

			Assert.Equal("key", exception.ValidationMessage.Key);
			Assert.Equal("Key is not valid", exception.ValidationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_ThrowTrace()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowTrace("Key is not valid"));

			Assert.Equal(ValidationSeverity.Trace, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_ThrowDebug()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowDebug("Key is not valid"));

			Assert.Equal(ValidationSeverity.Debug, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_ThrowInformation()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowInformation("Key is not valid"));

			Assert.Equal(ValidationSeverity.Information, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_ThrowWarning()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowWarning("Key is not valid"));

			Assert.Equal(ValidationSeverity.Warning, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_ThrowError()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowError("Key is not valid"));

			Assert.Equal(ValidationSeverity.Error, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ValidationPredicate_ThrowFatal()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowFatal("Key is not valid"));

			Assert.Equal(ValidationSeverity.Fatal, exception.ValidationMessage.Severity);
		}
	}
}