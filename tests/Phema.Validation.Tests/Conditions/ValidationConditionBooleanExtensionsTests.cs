using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationPredicateBooleanExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationPredicateBooleanExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsTrue()
		{
			var validationDetail = validationContext.When("key", true)
				.IsTrue()
				.AddValidationDetail("template1");

			Assert.NotNull(validationDetail);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("template1", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void IsTrue_Valid()
		{
			var validationDetail = validationContext.When("key", false)
				.IsTrue()
				.AddValidationDetail("template1");

			Assert.True(validationDetail.IsValid);
		}

		[Fact]
		public void IsFalse()
		{
			var validationDetail = validationContext.When("key", false)
				.IsFalse()
				.AddValidationDetail("template1");

			Assert.NotNull(validationDetail);

			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("template1", validationDetail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, validationDetail.ValidationSeverity);
		}

		[Fact]
		public void IsFalse_Valid()
		{
			var validationDetail = validationContext.When("key", true)
				.IsFalse()
				.AddValidationDetail("template1");

			Assert.True(validationDetail.IsValid);
		}
	}
}