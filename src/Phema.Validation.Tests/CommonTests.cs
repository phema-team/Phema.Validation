using System;
using System.Runtime.Serialization;
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
			validationContext.When("test")
				.Is(() => true)
				.Add(() => new ValidationMessage("works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IfNoIsConditionAddsError()
		{
			validationContext.When("test")
				.Add(() => new ValidationMessage("works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IfAnyIsConditionIsTrueAddsError()
		{
			validationContext.When("test")
				.Is(() => false)
				.Is(() => true)
				.Add(() => new ValidationMessage("works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsConditionIsTrueNotAddsError()
		{
			validationContext.When("test")
				.Is(() => false)
				.Add(() => new ValidationMessage("works"));

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void ValidationMessageWithParameters()
		{
			validationContext.When("test")
				.Is(() => true)
				.Add(() => new ValidationMessage("works {0}"), 12);

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works 12", error.Message);
		}

		[Fact]
		public void ThrowsSameExceptionInIsCondition()
		{
			Assert.Throws<Exception>(() =>
				validationContext.When("test")
					.Is(() => throw new Exception())
					.Add(() => new ValidationMessage("works")));
		}

		[Fact]
		public void ThrowsSameExceptionInAdd()
		{
			Assert.Throws<Exception>(() =>
				validationContext.When("test")
					.Is(() => true)
					.Add(() => throw new Exception()));
		}
	}
}