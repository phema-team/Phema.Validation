using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionIsBoolExtensions
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionIsBoolExtensions()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsTrue()
		{
			var error = validationContext.When("key", true)
				.IsTrue()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void IsTrue_Valid()
		{
			var error = validationContext.When("key", false)
				.IsTrue()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(error);
		}

		[Fact]
		public void IsFalse()
		{
			var error = validationContext.When("key", false)
				.IsFalse()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void IsFalse_Valid()
		{
			var error = validationContext.When("key", true)
				.IsFalse()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(error);
		}
	}
}