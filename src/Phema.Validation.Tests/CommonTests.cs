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
			validationContext.Validate("test", 10)
				.When(value => true)
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IfNoIsConditionAddsError()
		{
			validationContext.Validate("test", 10)
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IfAnyIsConditionIsTrueAddsError()
		{
			validationContext.Validate("test", 10)
				.When(value => false)
				.When(value => true)
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsConditionIsTrueNotAddsError()
		{
			validationContext.Validate("test", 10)
				.When(value => false)
				.Add(() => new ValidationMessage(() => "works"));

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void ValidationMessageWithParameters()
		{
			validationContext.Validate("test", 10)
				.When(value => true)
				.Add(() => new ValidationMessage(() => "works {0}"), new object[] { 12 });

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works 12", error.Message);
		}

		[Fact]
		public void ThrowsSameExceptionInIsCondition()
		{
			Assert.Throws<Exception>(() =>
				validationContext.Validate("test", 10)
					.When(value => throw new Exception())
					.Add(() => new ValidationMessage(() => "works")));
		}

		[Fact]
		public void ThrowsSameExceptionInAdd()
		{
			Assert.Throws<Exception>(() =>
				validationContext.Validate("test", 10)
					.When(value => true)
					.Add(() => throw new Exception()));
		}
	}
}