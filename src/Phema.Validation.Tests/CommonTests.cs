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
		public void IfIsConditionIsTrueAddsError()
		{
			validationContext.When("test", 10)
				.Is(value => true)
				.Add(() => new ValidationMessage(() => "works"), Array.Empty<object>(), ValidationSeverity.Error);

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IfNoIsConditionAddsError()
		{
			validationContext.When("test", 10)
				.Add(() => new ValidationMessage(() => "works"), Array.Empty<object>(), ValidationSeverity.Error);

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IfAnyIsConditionIsTrueAddsError()
		{
			validationContext.When("test", 10)
				.Is(value => false)
				.Is(value => true)
				.Add(() => new ValidationMessage(() => "works"), Array.Empty<object>(), ValidationSeverity.Error);

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsConditionIsTrueNotAddsError()
		{
			validationContext.When("test", 10)
				.Is(value => false)
				.Add(() => new ValidationMessage(() => "works"), Array.Empty<object>(), ValidationSeverity.Error);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void ValidationMessageWithParameters()
		{
			validationContext.When("test", 10)
				.Is(value => true)
				.Add(() => new ValidationMessage(() => "works {0}"), new object[] { 12 }, ValidationSeverity.Error);

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works 12", error.Message);
		}

		[Fact]
		public void ThrowsSameExceptionInIsCondition()
		{
			Assert.Throws<Exception>(() =>
				validationContext.When("test", 10)
					.Is(value => throw new Exception())
					.Add(() => new ValidationMessage(() => "works"), Array.Empty<object>(), ValidationSeverity.Error));
		}

		[Fact]
		public void ThrowsSameExceptionInAdd()
		{
			Assert.Throws<Exception>(() =>
				validationContext.When("test", 10)
					.Is(value => true)
					.Add(() => throw new Exception(), Array.Empty<object>(), ValidationSeverity.Error));
		}
	}
}