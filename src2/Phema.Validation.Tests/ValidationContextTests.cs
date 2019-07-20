using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void ValidationContext_BasicCondition_HasMessage()
		{
			validationContext.When("key", "value")
				.Is(value => true)
				.AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.ValidationKey);
			Assert.Equal("Error", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationContext_BasicCondition_HasNoMessage()
		{
			validationContext.When("key", "value")
				.Is(value => false)
				.AddError("Error");

			Assert.Empty(validationContext.ValidationMessages);
		}

		[Fact]
		public void ValidationContext_NoCondition_HasMessage()
		{
			validationContext.When("key", "value")
				.AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.ValidationKey);
			Assert.Equal("Error", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationContext_NoValidationKey_HasMessage()
		{
			validationContext.When("key")
				.Is(() => true)
				.AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.ValidationKey);
			Assert.Equal("Error", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationContext_HasMessage_IsNotValid()
		{
			validationContext.When("key")
				.Is(() => true)
				.AddError("Error");

			Assert.False(validationContext.IsValid());
		}

		[Fact]
		public void ValidationContext_HasMessage_IsNotValid_FilterByKey()
		{
			validationContext.When("key")
				.Is(() => true)
				.AddError("Error");

			Assert.False(validationContext.IsValid("key"));
		}

		[Fact]
		public void ValidationContext_HasNoMessage_IsValid()
		{
			validationContext.When("key")
				.Is(() => false)
				.AddError("Error");

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void ValidationContext_HasMessages_IsNotValid_FilterByKey()
		{
			validationContext.When("key1", "value")
				.Is(value => value.Contains("value"))
				.AddError("Error");

			validationContext.When("key2", "value")
				.Is(value => true)
				.AddError("Error");

			Assert.True(validationContext.IsValid("key3"));
		}

		[Fact]
		public void ValidationContext_HasMessages_EnsureIsNotValid()
		{
			validationContext.When("key", "value")
				.Is(value => true)
				.AddError("Error");

			var exception = Assert.Throws<ValidationContextException>(() => validationContext.EnsureIsValid());

			var (key, message, severity) = Assert.Single(exception.ValidationMessages);

			Assert.Equal("key", key);
			Assert.Equal("Error", message);
			Assert.Equal(ValidationSeverity.Error, severity);
		}

		[Fact]
		public void ValidationContext_HasMessages_EnsureIsValid()
		{
			validationContext.When("key", "value")
				.Is(value => false)
				.AddError("Error");

			validationContext.EnsureIsValid();
		}

		[Fact]
		public void ValidationContext_ChangeSeverityToFatal_AddError_Valid()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Fatal;

			validationContext.When("key", "value")
				.Is(value => false)
				.AddError("Error");

			Assert.True(validationContext.IsValid());
		}
	}
}