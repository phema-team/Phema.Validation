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
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.Key);
			Assert.Equal("Key is not valid", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationContext_BasicCondition_HasNoMessage()
		{
			validationContext.When("key", "value")
				.Is(value => false)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			Assert.Empty(validationContext.ValidationMessages);
		}

		[Fact]
		public void ValidationContext_NoCondition_HasMessage()
		{
			validationContext.When("key", "value")
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.Key);
			Assert.Equal("Key is not valid", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationContext_NoValidationKey_HasMessage()
		{
			validationContext.When("key")
				.Is(() => true)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.Key);
			Assert.Equal("Key is not valid", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationContext_HasMessage_IsNotValid()
		{
			validationContext.When("key")
				.Is(() => true)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			Assert.False(validationContext.IsValid());
		}

		[Fact]
		public void ValidationContext_HasMessage_IsNotValid_FilterByKey()
		{
			validationContext.When("key")
				.Is(() => true)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			Assert.False(validationContext.IsValid("key"));
		}

		[Fact]
		public void ValidationContext_HasNoMessage_IsValid()
		{
			validationContext.When("key")
				.Is(() => false)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void ValidationContext_HasMessages_IsNotValid_FilterByKey()
		{
			validationContext.When("key1", "value")
				.Is(value => value.Contains("value"))
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			validationContext.When("key2", "value")
				.Is(value => true)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			Assert.True(validationContext.IsValid("key3"));
		}

		[Fact]
		public void ValidationContext_HasMessages_EnsureIsNotValid()
		{
			validationContext.When("key", "value")
				.Is(value => true)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			var exception = Assert.Throws<ValidationContextException>(() => validationContext.EnsureIsValid());

			var validationMessage = Assert.Single(exception.ValidationMessages);

			Assert.Equal("key", validationMessage.Key);
			Assert.Equal("Key is not valid", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void ValidationContext_HasMessages_EnsureIsValid()
		{
			validationContext.When("key", "value")
				.Is(value => false)
				.AddMessage("Key is not valid", ValidationSeverity.Error);

			validationContext.EnsureIsValid();
		}

		[Fact]
		public void ValidationContext_ChangeSeverityToFatal_AddError_Valid()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Fatal;

			validationContext.When("key", "value")
				.Is(value => false)
				.AddError("Key is not valid");

			Assert.True(validationContext.IsValid());
		}
	}
}