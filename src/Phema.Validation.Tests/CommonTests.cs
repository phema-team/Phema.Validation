using System;
using Xunit;

namespace Phema.Validation.Tests
{
	public class CommonTests
	{
		private readonly IValidationContext validationContext;

		public CommonTests()
		{
			validationContext = new ValidationContext();
		}

		[Fact]
		public void AddReturnsSameErrorAsContext()
		{
			var error1 = validationContext.When("key", 10)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			var error2 = Assert.Single(validationContext.Errors);

			Assert.Same(error1, error2);
			Assert.Equal(error1, error2);
		}
		
		[Fact]
		public void IfIsConditionIsTrueAddsError()
		{
			var error = validationContext.When("key", 10)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IfNoIsConditionAddsError()
		{
			var error =validationContext.When("key", 10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IfAnyIsConditionIsTrueAddsError()
		{
			var error =validationContext.When("key", 10)
				.Is(value => false)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsConditionIsTrueNotAddsError()
		{
			validationContext.When("key", 10)
				.Is(value => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void ValidationMessageWithParameters()
		{
			var error = validationContext.When("key", 10)
				.Is(value => true)
				.AddError(() => new ValidationMessage<int, int>(() => "template {0}, {1}"), 12, 13);

			Assert.Equal("key", error.Key);
			Assert.Equal("template 12, 13", error.Message);
		}

		[Fact]
		public void ThrowsSameExceptionInIsCondition()
		{
			Assert.Throws<Exception>(() =>
				validationContext.When("key", 10)
					.Is(value => throw new Exception())
					.AddError(() => new ValidationMessage(() => "template")));
		}

		[Fact]
		public void ThrowsSameExceptionInAdd()
		{
			Assert.Throws<Exception>(() =>
				validationContext.When("key", 10)
					.Is(value => true)
					.AddError(() => throw new Exception()));
		}
	}
}