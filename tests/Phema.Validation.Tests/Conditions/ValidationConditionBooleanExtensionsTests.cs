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
			var detail = validationContext.When("key", true)
				.IsTrue()
				.AddError("template1");

			Assert.NotNull(detail);

			Assert.Equal("key", detail.ValidationKey);
			Assert.Equal("template1", detail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, detail.ValidationSeverity);
		}

		[Fact]
		public void IsTrue_Valid()
		{
			var detail = validationContext.When("key", false)
				.IsTrue()
				.AddError("template1");

			Assert.Null(detail);
		}

		[Fact]
		public void IsFalse()
		{
			var detail = validationContext.When("key", false)
				.IsFalse()
				.AddError("template1");

			Assert.NotNull(detail);

			Assert.Equal("key", detail.ValidationKey);
			Assert.Equal("template1", detail.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, detail.ValidationSeverity);
		}

		[Fact]
		public void IsFalse_Valid()
		{
			var detail = validationContext.When("key", true)
				.IsFalse()
				.AddError("template1");

			Assert.Null(detail);
		}
	}
}