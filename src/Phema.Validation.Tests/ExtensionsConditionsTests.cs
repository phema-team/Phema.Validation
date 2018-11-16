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
			validationContext.When("test", (int?)null)
				.IsNull()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotNull()
		{
			validationContext.When("test", 12)
				.IsNotNull()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEmpty()
		{
			validationContext.When("test", "")
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEmpty()
		{
			validationContext.When("test", "done")
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNullOrWhitespace()
		{
			validationContext.When("test", " ")
				.IsNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotNullOrWhitespace()
		{
			validationContext.When("test", "done")
				.IsNotNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEqual()
		{
			validationContext.When("test", "done")
				.IsEqual("done")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEqualNull()
		{
			validationContext.When("test", (string)null)
				.IsEqual(null)
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEqual()
		{
			validationContext.When("test", "")
				.IsNotEqual("done")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEqualNull()
		{
			validationContext.When("test", (string)null)
				.IsNotEqual("done")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsMatch()
		{
			validationContext.When("test", "abc")
				.IsMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotMatch()
		{
			validationContext.When("test", "def")
				.IsNotMatch("[a-c]")
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}
		
		[Fact]
		public void IsNotEmail()
		{
			validationContext.When("test", "tutu")
				.IsNotEmail()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}
		
		[Fact]
		public void IsNotEmail_Valid()
		{
			validationContext.When("test", "tutu@tutu.ru")
				.IsNotEmail()
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsLength()
		{
			validationContext.When("test", "12345")
				.IsLength(5)
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}
		
		[Fact]
		public void ValueIsInRange()
		{
			validationContext.When("age", 11)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueLessThanRange()
		{
			validationContext.When("age", 9)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "Works"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void ValueGreaterThanRange()
		{
			validationContext.When("age", 13)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "Works"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void ValueIsLessRange()
		{
			validationContext.When("age", 11)
				.IsLess(12)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueIsGreaterRange()
		{
			validationContext.When("age", 11)
				.IsGreater(10)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueIsGreaterRange_Long()
		{
			validationContext.When("age", 11L)
				.IsGreater(10)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueIsLessRange_Long()
		{
			validationContext.When("age", 9L)
				.IsLess(10)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueIsIsRangeRange_Long()
		{
			validationContext.When("age", 11L)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void IsEqualGuid()
		{
			validationContext.When("age", Guid.Empty)
				.IsEqual(Guid.Empty)
				.AddError(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
	}
}