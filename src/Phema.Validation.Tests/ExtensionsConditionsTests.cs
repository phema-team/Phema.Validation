using System;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ExtensionsConditionsTests
	{
		private readonly IValidationContext validationContext;

		public ExtensionsConditionsTests()
		{
			validationContext = new ValidationContext(ValidationSeverity.Trace);
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
		public void WhenNotEmail()
		{
			validationContext.Validate("test", "tutu")
				.WhenNotEmail()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}
		
		[Fact]
		public void WhenNotEmail_Valid()
		{
			validationContext.Validate("test", "tutu@tutu.ru")
				.WhenNotEmail()
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void WhenLength()
		{
			validationContext.Validate("test", "12345")
				.WhenLength(5)
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
		
		[Fact]
		public void ValueWhenLessRange()
		{
			validationContext.Validate("age", 11)
				.WhenLess(12)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueWhenGreaterRange()
		{
			validationContext.Validate("age", 11)
				.WhenGreater(10)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueWhenGreaterRange_Long()
		{
			validationContext.Validate("age", 11L)
				.WhenGreater(10)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueWhenLessRange_Long()
		{
			validationContext.Validate("age", 9L)
				.WhenLess(10)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueWhenIsRangeRange_Long()
		{
			validationContext.Validate("age", 11L)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void WhenEqualGuid()
		{
			validationContext.Validate("age", Guid.Empty)
				.WhenEqual(Guid.Empty)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
	}
}