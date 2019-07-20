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
		public void BasicCondition_HasMessage()
		{
			validationContext.When("key", "value").AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.ValidationKey);
			Assert.Equal("Error", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void BasicCondition_HasNoMessage()
		{
			validationContext.When("key", "value")
				.Is(value => false)
				.AddError("Error");

			Assert.Empty(validationContext.ValidationMessages);
		}

		[Fact]
		public void NoCondition_HasMessage()
		{
			validationContext.When("key", "value").AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.ValidationKey);
			Assert.Equal("Error", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void NoValidationKey_HasMessage()
		{
			validationContext.When("key").AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("key", validationMessage.ValidationKey);
			Assert.Equal("Error", validationMessage.Message);
			Assert.Equal(ValidationSeverity.Error, validationMessage.Severity);
		}

		[Fact]
		public void HasMessage_IsNotValid()
		{
			validationContext.When("key").AddError("Error");

			Assert.False(validationContext.IsValid());
		}

		[Fact]
		public void HasMessage_IsNotValid_FilterByKey()
		{
			validationContext.When("key").AddError("Error");

			Assert.False(validationContext.IsValid("key"));
		}

		[Fact]
		public void HasNoMessage_IsValid()
		{
			validationContext.When("key")
				.Is(() => false)
				.AddError("Error");

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void HasMessages_IsNotValid_FilterByKey()
		{
			validationContext.When("key1", "value").AddError("Error");
			validationContext.When("key2", "value").AddError("Error");

			Assert.True(validationContext.IsValid("key3"));
		}

		[Fact]
		public void HasMessages_EnsureIsNotValid()
		{
			validationContext.When("key", "value").AddError("Error");

			var exception = Assert.Throws<ValidationContextException>(() => validationContext.EnsureIsValid());

			var (key, message, severity) = Assert.Single(exception.ValidationMessages);

			Assert.Equal("key", key);
			Assert.Equal("Error", message);
			Assert.Equal(ValidationSeverity.Error, severity);
		}

		[Fact]
		public void HasMessages_EnsureIsValid()
		{
			validationContext.When("key", "value")
				.Is(value => false)
				.AddError("Error");

			validationContext.EnsureIsValid();
		}

		[Fact]
		public void ChangeSeverityToFatal_AddError_Valid()
		{
			validationContext.ValidationSeverity = ValidationSeverity.Fatal;

			validationContext.When("key", "value")
				.Is(value => false)
				.AddError("Error");

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void InnerPath()
		{
			var withPrefix = validationContext.CreateFor("path");

			var (key, _) = withPrefix.When("key", "value").AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationMessages);

			Assert.Equal("path.key", key);
			Assert.Equal("path.key", validationMessage.ValidationKey);
		}

		[Fact]
		public void DoubleInnerCreate()
		{
			var withPrefix = validationContext.CreateFor("path1");
			withPrefix = withPrefix.CreateFor("path2");

			var (key, _) = withPrefix.When("key", "value").AddError("Error");

			Assert.Equal("path1.path2.key", key);
		}
		
		[Fact]
		public void IsValid_InnerPath_InnerValidationContext()
		{
			var withPrefix = validationContext.CreateFor("path");

			withPrefix.When("key", "value").AddError("Error");

			Assert.False(withPrefix.IsValid("key"));
			Assert.False(validationContext.IsValid("path.key"));

			Assert.True(validationContext.IsValid("key"));
		}
	}
}