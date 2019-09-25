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

			validationContext.When("key", "value").AddValidationError("Error");

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
				.AddValidationError("Error");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void NoCondition_HasMessage()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key", "value").AddValidationError("Error");

			var validationDetail = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("Error", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void NoValidationKey_HasMessage()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key").AddValidationError("Error");

			var validationDetail = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("Error", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void HasMessage_IsNotValid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key").AddValidationError("Error");

			Assert.False(validationContext.IsValid());
		}

		[Fact]
		public void HasMessage_IsNotValid_FilterByKey()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key").AddValidationError("Error");

			Assert.False(validationContext.IsValid("key"));
		}

		[Fact]
		public void HasNoMessage_IsValid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key")
				.Is(() => false)
				.AddValidationError("Error");

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void HasMessages_IsNotValid_FilterByKey()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key1", "value").AddValidationError("Error");
			validationContext.When("key2", "value").AddValidationError("Error");

			Assert.True(validationContext.IsValid("key3"));
		}

		[Fact]
		public void HasMessages_EnsureIsNotValid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key", "value").AddValidationError("Error");

			var exception = Assert.Throws<ValidationContextException>(() => validationContext.EnsureIsValid());

			var (key, message, severity) = Assert.Single(exception.ValidationDetails);

			Assert.Equal("key", key);
			Assert.Equal("Error", message);
			Assert.Equal(ValidationSeverity.Error, severity);
		}

		[Fact]
		public void HasMessages_EnsureIsValid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Error);

			validationContext.When("key", "value")
				.Is(value => false)
				.AddValidationError("Error");

			validationContext.EnsureIsValid();
		}

		[Fact]
		public void ChangeSeverityToFatal_AddError_Valid()
		{
			var validationContext = CreateValidationContext(x => x.ValidationSeverity = ValidationSeverity.Fatal);

			validationContext.When("key", "value")
				.Is(value => false)
				.AddValidationError("Error");

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void GlobalValidationKey()
		{
			var validationContext = CreateValidationContext(x => x.GlobalValidationKey = "global");

			var (key, _) = validationContext.When().AddValidationError("Error");

			Assert.Equal("global", key);
		}
	}
}