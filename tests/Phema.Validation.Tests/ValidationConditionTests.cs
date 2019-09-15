using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
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

		[Fact]
		public void AndConditionJoin()
		{
			var (key, message) = validationContext.When("key", "value")
				.IsNotNull()
				.IsEqual("value")
				.AddError("template1");

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void AndConditionJoin_Valid()
		{
			validationContext.When("key", "value")
				.IsNull()
				// No error, because value is not null
				.IsEqual("value")
				.AddError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void AndConditionJoin_SecondCondition_Valid()
		{
			validationContext.When("key", "value")
				.IsNotNull()
				.IsNotEqual("value")
				.AddError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
	}
}