using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionTests
	{
		private IValidationContext CreateValidationContext(ValidationSeverity validationSeverity)
		{
			return new ServiceCollection()
				.AddValidation(o => o.ValidationSeverity = validationSeverity)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void AddDetail_ReturnsMessage()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);
			
			var (key, message, severity) =
				validationContext.When("key", "value").AddValidationDetail("Error", ValidationSeverity.Error);

			Assert.Equal("key", key);
			Assert.Equal("Error", message);
			Assert.Equal(ValidationSeverity.Error, severity);
		}

		[Fact]
		public void EmptyIs_HasMessage()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);
			
			var validationDetail = validationContext.When("key", "value").AddValidationDetail("Error", ValidationSeverity.Error);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("Error", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void AddWarning()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);
			
			var validationDetail = validationContext.When("key", "value").AddValidationWarning("Warning");

			Assert.Equal(ValidationSeverity.Warning, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void AddError()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);
			
			var validationDetail = validationContext.When("key", "value").AddValidationError("Error");

			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void Warning_ThrowErrorDetail_Greater()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Warning);
			
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddValidationDetail("Error", ValidationSeverity.Error));

			Assert.Equal("key", exception.ValidationDetail.ValidationKey);
			Assert.Equal("Error", exception.ValidationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Warning_ThrowError()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Warning);

			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddValidationError("Error"));

			Assert.Equal(ValidationSeverity.Error, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Error_ThrowFatal()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);

			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", "value").AddValidationFatal("Fatal"));

			Assert.Equal(ValidationSeverity.Fatal, exception.ValidationDetail.ValidationSeverity);
		}

		[Fact]
		public void Fatal_FatalNotThrows()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Fatal);

			Assert.NotNull(validationContext.When("key", "value").AddValidationFatal("Fatal"));
		}

		[Fact]
		public void MessageDeconstruction()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);
			
			var (key, message, severity) = validationContext.When("key", "value")
				.AddValidationDetail("Error", ValidationSeverity.Error);

			var validationDetail = Assert.Single(validationContext.ValidationDetails);
			Assert.Equal(validationDetail.ValidationKey, key);
			Assert.Equal(validationDetail.ValidationMessage, message);
			Assert.Equal(validationDetail.ValidationSeverity, severity);
		}

		[Fact]
		public void AndConditionJoin()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);
			
			var (key, message) = validationContext.When("key", "value")
				.IsNotNull()
				.IsEqual("value")
				.AddValidationError("template1");

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void AndConditionJoin_Valid()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);
			
			validationContext.When("key", "value")
				.IsNull()
				// No error, because value is not null
				.IsEqual("value")
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void AndConditionJoin_SecondCondition_Valid()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Error);
			
			validationContext.When("key", "value")
				.IsNotNull()
				.IsNotEqual("value")
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
	}
}