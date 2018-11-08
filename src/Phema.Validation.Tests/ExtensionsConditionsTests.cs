using System.Runtime.Serialization;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ExtensionsConditionsTests
	{
		private readonly IValidationContext validationContext;

		public ExtensionsConditionsTests()
		{
			validationContext = new ValidationContext();
		}

		[Fact]
		public void IsNull()
		{
			validationContext.When("test")
				.IsNull((int?)null)
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotNull()
		{
			validationContext.When("test")
				.IsNotNull(12)
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEmpty()
		{
			validationContext.When("test")
				.IsEmpty("")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEmpty()
		{
			validationContext.When("test")
				.IsNotEmpty("done")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNullOrWhitespace()
		{
			validationContext.When("test")
				.IsNullOrWhitespace("  ")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotNullOrWhitespace()
		{
			validationContext.When("test")
				.IsNotNullOrWhitespace("done")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEqual()
		{
			validationContext.When("test")
				.IsEqual("done", "done")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEqual()
		{
			validationContext.When("test")
				.IsNotEqual("", "done")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsMatch()
		{
			validationContext.When("test")
				.IsMatch("abc", "[a-c]+")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotMatch()
		{
			validationContext.When("test")
				.IsNotMatch("def", "[a-c]")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}
		
		[Fact]
		public void ValueIsInRange()
		{
			validationContext.When("age")
				.IsInRange(11, 10, 12)
				.Add(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueLessThanRange()
		{
			validationContext.When("age")
				.IsInRange(9, 10, 12)
				.Add(() => new ValidationMessage(() => "Works"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void ValueGreaterThanRange()
		{
			validationContext.When("age")
				.IsInRange(13, 10, 12)
				.Add(() => new ValidationMessage(() => "Works"));
			
			Assert.True(validationContext.IsValid());
		}
	}
}