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
			var validationDetail = validationContext.When("key", "value").AddDetail("Error", ValidationSeverity.Error);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("Error", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void AddTrace()
		{
			var validationDetail = validationContext.When("key", "value").AddTrace("Trace");

			Assert.Equal(ValidationSeverity.Trace, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void AddDebug()
		{
			var validationDetail = validationContext.When("key", "value").AddDebug("Debug");

			Assert.Equal(ValidationSeverity.Debug, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void AddInformation()
		{
			var validationDetail = validationContext.When("key", "value").AddInformation("Information");

			Assert.Equal(ValidationSeverity.Information, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void AddWarning()
		{
			var validationDetail = validationContext.When("key", "value").AddWarning("Warning");

			Assert.Equal(ValidationSeverity.Warning, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void AddError()
		{
			var validationDetail = validationContext.When("key", "value").AddError("Error");

			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
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

			var validationDetail = Assert.Single(validationContext.ValidationDetails);
			Assert.Equal(validationDetail.ValidationKey, key);
			Assert.Equal(validationDetail.ValidationMessage, message);
			Assert.Equal(validationDetail.ValidationSeverity, severity);
		}
	}
}