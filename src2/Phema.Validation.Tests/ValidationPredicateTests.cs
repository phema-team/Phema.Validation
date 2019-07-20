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
		public void AddMessage_ReturnsMessage()
		{
			var (key, message, severity) = validationContext.When("key", "value")
				.Is(value => true)
				.AddMessage("Error", ValidationSeverity.Error);

			Assert.Equal("key", key);
			Assert.Equal("Error", message);
			Assert.Equal(ValidationSeverity.Error, severity);
		}

		[Fact]
		public void EmptyIs_HasMessage()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddMessage("Error", ValidationSeverity.Error);

			Assert.Equal("key", validationMessage.ValidationKey);
			Assert.Equal("Error", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void AddTrace()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddTrace("Trace");

			Assert.Equal(ValidationSeverity.Trace, validationMessage.Severity);
		}

		[Fact]
		public void AddDebug()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddDebug("Debug");

			Assert.Equal(ValidationSeverity.Debug, validationMessage.Severity);
		}

		[Fact]
		public void AddInformation()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddInformation("Information");

			Assert.Equal(ValidationSeverity.Information, validationMessage.Severity);
		}

		[Fact]
		public void AddWarning()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddWarning("Warning");

			Assert.Equal(ValidationSeverity.Warning, validationMessage.Severity);
		}

		[Fact]
		public void AddError()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddError("Error");

			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void AddFatal()
		{
			var validationMessage = validationContext.When("key", "value")
				.Is(() => true)
				.AddFatal("Fatal");

			Assert.Equal(ValidationSeverity.Fatal, validationMessage.Severity);
		}

		[Fact]
		public void ThrowMessage()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowMessage("Error", ValidationSeverity.Error));

			Assert.Equal("key", exception.ValidationMessage.ValidationKey);
			Assert.Equal("Error", exception.ValidationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ThrowTrace()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowTrace("Trace"));

			Assert.Equal(ValidationSeverity.Trace, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ThrowDebug()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowDebug("Debug"));

			Assert.Equal(ValidationSeverity.Debug, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ThrowInformation()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowInformation("Information"));

			Assert.Equal(ValidationSeverity.Information, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ThrowWarning()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowWarning("Warning"));

			Assert.Equal(ValidationSeverity.Warning, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ThrowError()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowError("Error"));

			Assert.Equal(ValidationSeverity.Error, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void ThrowFatal()
		{
			var exception = Assert.Throws<ValidationPredicateException>(() =>
				validationContext.When("key", "value")
					.Is(() => true)
					.ThrowFatal("Fatal"));

			Assert.Equal(ValidationSeverity.Fatal, exception.ValidationMessage.Severity);
		}

		[Fact]
		public void MessageDeconstruction()
		{
			var (key, message, severity) = validationContext.When("key", "value")
				.Is(value => true)
				.AddMessage("Error", ValidationSeverity.Error);

			var validationMessage = Assert.Single(validationContext.ValidationMessages);
			Assert.Equal(validationMessage.ValidationKey, key);
			Assert.Equal(validationMessage.Message, message);
			Assert.Equal(validationMessage.Severity, severity);
		}
	}
}