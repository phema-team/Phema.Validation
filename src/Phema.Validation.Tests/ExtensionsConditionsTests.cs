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
			validationContext.Validate("test", (int?)null)
				.WhenNull()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotNull()
		{
			validationContext.Validate("test", 12)
				.WhenNotNull()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEmpty()
		{
			validationContext.Validate("test", "")
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEmpty()
		{
			validationContext.Validate("test", "done")
				.WhenNotEmpty()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNullOrWhitespace()
		{
			validationContext.Validate("test", " ")
				.WhenNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotNullOrWhitespace()
		{
			validationContext.Validate("test", "done")
				.WhenNotNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEqual()
		{
			validationContext.Validate("test", "done")
				.WhenEqual("done")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEqualNull()
		{
			validationContext.Validate("test", (string)null)
				.WhenEqual(null)
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEqual()
		{
			validationContext.Validate("test", "")
				.WhenNotEqual("done")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEqualNull()
		{
			validationContext.Validate("test", (string)null)
				.WhenNotEqual("done")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsMatch()
		{
			validationContext.Validate("test", "abc")
				.WhenMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotMatch()
		{
			validationContext.Validate("test", "def")
				.WhenNotMatch("[a-c]")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}
		
		[Fact]
		public void ValueIsInRange()
		{
			validationContext.Validate("age", 11)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueLessThanRange()
		{
			validationContext.Validate("age", 9)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "Works"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void ValueGreaterThanRange()
		{
			validationContext.Validate("age", 13)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "Works"));
			
			Assert.True(validationContext.IsValid());
		}
	}
}