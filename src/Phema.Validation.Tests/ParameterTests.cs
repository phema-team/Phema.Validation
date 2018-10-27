using Xunit;

namespace Phema.Validation.Tests
{
	public class ParameterTests
	{
		private readonly IValidationContext validationContext;

		public ParameterTests()
		{
			validationContext = new ValidationContext();
		}

		[Fact]
		public void AddWithOneParameter()
		{
			validationContext.When("test")
				.Is(() => true)
				.Add<int>(() => new ValidationMessage<int>("{0}"), 12);

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("12", error.Message);
		}

		[Fact]
		public void ThrowWithOneParameter()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("test")
					.Is(() => true)
					.Throw(() => new ValidationMessage<int>("{0}"), 12));

			Assert.Equal("test", exception.Error.Key);
			Assert.Equal("12", exception.Error.Message);
		}

		[Fact]
		public void AddWithTwoParameters()
		{
			validationContext.When("test")
				.Is(() => true)
				.Add<int, int>(() => new ValidationMessage<int, int>("{0}{1}"), 12, 12);

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("1212", error.Message);
		}

		[Fact]
		public void ThrowWithTwoParameters()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("test")
					.Is(() => true)
					.Throw(() => new ValidationMessage<int, int>("{0}{1}"), 12, 12));

			Assert.Equal("test", exception.Error.Key);
			Assert.Equal("1212", exception.Error.Message);
		}
	}
}