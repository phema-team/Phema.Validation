using System.Runtime.Serialization;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ExtensionsTests()
		{
			validationContext = new ValidationContext();
		}

		[Fact]
		public void IfAnyErrorIsValidIsFalse()
		{
			validationContext.When("test")
				.Is(() => true)
				.Add(() => new ValidationMessage("works"));

			Assert.Single(validationContext.Errors);
			Assert.False(validationContext.IsValid());
		}

		[Fact]
		public void IsNotWorksAsNotIs()
		{
			validationContext.When("test")
				.IsNot(() => true)
				.Add(() => new ValidationMessage("works"));

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void ThrowsIfConditionIsTrue()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("test")
					.Is(() => true)
					.Throw(() => new ValidationMessage("works")));

			Assert.Equal("test", exception.Error.Key);
			Assert.Equal("works", exception.Error.Message);
		}

		[Fact]
		public void EnsureThrowsIfAnyError()
		{
			validationContext.When("test")
				.Is(() => true)
				.Add(() => new ValidationMessage("works"));

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid());

			var error = Assert.Single(exception.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		public class Stab
		{
			[DataMember(Name = "message")]
			public string Message { get; set; }
		}

		[Fact]
		public void WhenGetsKeyFromDataMember()
		{
			validationContext.When<Stab>(s => s.Message)
				.Is(() => true)
				.Add(() => new ValidationMessage("works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("message", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void WhenWorksAnsGenericVersion()
		{
			var stab = new Stab();

			validationContext.When(stab, s => s.Message)
				.Is(() => true)
				.Add(() => new ValidationMessage("works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("message", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void EmptyWhenExtension()
		{
			validationContext.When()
				.Is(() => true)
				.Add(() => new ValidationMessage("works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void EnsureIsValidNotThrowsIfValid()
		{
			validationContext.When("test")
				.Is(() => false)
				.Add(() => new ValidationMessage("works"));

			validationContext.EnsureIsValid();
		}
	}
}