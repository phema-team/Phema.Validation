using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextTests
	{
		private IValidationContext CreateValidationContext(Action<ValidationOptions> options = null)
		{
			return new ServiceCollection()
				.AddValidation(options)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void BasicCondition_HasMessage()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key", "value").AddValidationDetail("Error");

			var validationDetail = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("Error", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void BasicCondition_HasNoMessage()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key", "value")
				.Is(value => false)
				.AddValidationDetail("Error");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void NoCondition_HasMessage()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key", "value").AddValidationDetail("Error");

			var validationDetail = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("Error", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void NoValidationKey_HasMessage()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key").AddValidationDetail("Error");

			var validationDetail = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("Error", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void HasMessage_IsNotValid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key").AddValidationDetail("Error");

			Assert.False(validationContext.IsValid());
		}

		[Fact]
		public void HasMessage_IsNotValid_FilterByKey()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key").AddValidationDetail("Error");

			Assert.False(validationContext.IsValid("key"));
		}

		[Fact]
		public void HasNoMessage_IsValid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key")
				.Is(() => false)
				.AddValidationDetail("Error");

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void HasMessages_IsNotValid_FilterByKey()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key1", "value").AddValidationDetail("Error");
			validationContext.When("key2", "value").AddValidationDetail("Error");

			Assert.True(validationContext.IsValid("key3"));
		}

		[Fact]
		public void HasMessages_EnsureIsNotValid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key", "value").AddValidationDetail("Error");

			var exception = Assert.Throws<ValidationContextException>(() => validationContext.EnsureIsValid());

			var (key, message, isValid, severity) = Assert.Single(exception.ValidationDetails);

			Assert.Equal("key", key);
			Assert.Equal("Error", message);
			Assert.False(isValid);
			Assert.Equal(ValidationSeverity.Error, severity);
		}

		[Fact]
		public void HasMessages_EnsureIsValid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key", "value")
				.Is(value => false)
				.AddValidationDetail("Error");

			validationContext.EnsureIsValid();
		}

		[Fact]
		public void ChangeSeverityToFatal_AddError_Valid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Fatal);

			validationContext.When("key", "value")
				.Is(value => false)
				.AddValidationDetail("Error");

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void GlobalValidationKey()
		{
			var validationContext = CreateValidationContext(x => x.GlobalValidationKey = "global");

			var (key, _) = validationContext.When().AddValidationDetail("Error");

			Assert.Equal("global", key);
		}
	}
}