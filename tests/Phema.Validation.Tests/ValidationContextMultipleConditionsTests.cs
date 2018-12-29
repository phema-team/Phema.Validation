using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextMultipleConditionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextMultipleConditionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void AllConditionsIsTrue()
		{
			var error = validationContext.When("key", "value")
				.Is(value => value == "value")
				.Is(value => value == "value")
				.Is(value => value == "value")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void IsAnyConditionIsFalse_NoError()
		{
			var error = validationContext.When("key", "value")
				.Is(value => value == "value")
				.Is(value => value == "not_a_value")
				.Is(value => value == "value")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(error);
		}

		[Fact]
		public void IsAnyConditionIsFalse_NoConditionsCalled()
		{
			string input = null;

			var error1 = validationContext.When("key", input)
				.Is(value => value == null)
				.AddError(() => new ValidationMessage(() => "template"));

			var error2 = validationContext.When("key", input)
				.Is(value => value != null)
				.Is(value => throw new NotImplementedException())
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.NotNull(error1);

			Assert.Equal("key", error1.Key);
			Assert.Equal("template", error1.Message);
			Assert.Equal(ValidationSeverity.Error, error1.Severity);

			Assert.Null(error2);
		}
	}
}