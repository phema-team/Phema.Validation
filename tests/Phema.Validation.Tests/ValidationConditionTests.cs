using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void AddDetail_ReturnsMessage()
		{
			var (key, message, severity) =
				validationContext.When("key", "value").AddDetail("Error", ValidationSeverity.Error);

			Assert.Equal("key", key);
			Assert.Equal("Error", message);
			Assert.Equal(ValidationSeverity.Error, severity);
		}

		[Fact]
		public void EmptyIs_HasMessage()
		{
			var validationMessage = validationContext.When("key", "value").AddDetail("Error", ValidationSeverity.Error);

			Assert.Equal("key", validationMessage.ValidationKey);
			Assert.Equal("Error", validationMessage.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationMessage.ValidationSeverity);
		}

		[Fact]
		public void AddTrace()
		{
			var validationMessage = validationContext.When("key", "value").AddTrace("Trace");

			Assert.Equal(ValidationSeverity.Trace, validationMessage.ValidationSeverity);
		}

		[Fact]
		public void AddDebug()
		{
			var validationMessage = validationContext.When("key", "value").AddDebug("Debug");

			Assert.Equal(ValidationSeverity.Debug, validationMessage.ValidationSeverity);
		}

		[Fact]
		public void AddInformation()
		{
			var validationMessage = validationContext.When("key", "value").AddInformation("Information");

			Assert.Equal(ValidationSeverity.Information, validationMessage.ValidationSeverity);
		}

		[Fact]
		public void AddWarning()
		{
			var validationMessage = validationContext.When("key", "value").AddWarning("Warning");

			Assert.Equal(ValidationSeverity.Warning, validationMessage.ValidationSeverity);
		}

		[Fact]
		public void AddError()
		{
			var validationMessage = validationContext.When("key", "value").AddError("Error");

			Assert.Equal(ValidationSeverity.Error, validationMessage.ValidationSeverity);
		}

		[Fact]
		public void Warning_ThrowErrorDetail_Greater()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Warning;

			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddDetail("Error", ValidationSeverity.Error));

			Assert.Equal("key", exception.ValidationDetail.ValidationKey);
			Assert.Equal("Error", exception.ValidationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Trace_Never_Throw()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Trace;

			Assert.NotNull(validationContext.When("key", "value").AddTrace("Trace"));
		}

		[Fact]
		public void Trace_ThrowDebug()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Trace;

			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddDebug("Debug"));

			Assert.Equal(ValidationSeverity.Debug, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Debug_ThrowInformation()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Debug;

			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddInformation("Information"));

			Assert.Equal(ValidationSeverity.Information, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Information_ThrowWarning()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Information;

			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddWarning("Warning"));

			Assert.Equal(ValidationSeverity.Warning, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Warning_ThrowError()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Warning;

			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddError("Error"));

			Assert.Equal(ValidationSeverity.Error, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Error_ThrowFatal()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Error;

			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddFatal("Fatal"));

			Assert.Equal(ValidationSeverity.Fatal, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Fatal_FatalNotThrows()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Fatal;

			Assert.NotNull(validationContext.When("key", "value").AddFatal("Fatal"));
		}

		[Fact]
		public void MessageDeconstruction()
		{
			var (key, message, severity) = validationContext.When("key", "value")
				.AddDetail("Error", ValidationSeverity.Error);

			var validationMessage = Assert.Single(validationContext.ValidationDetails);
			Assert.Equal(validationMessage.ValidationKey, key);
			Assert.Equal(validationMessage.ValidationMessage, message);
			Assert.Equal(validationMessage.ValidationSeverity, severity);
		}
	}
}