using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionExtensions
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionExtensions()
		{
			validationContext = new ValidationContext();
		}
		
		[Fact]
		public void IsNot()
		{
			var error = validationContext.When("age", 10)
				.IsNot(value => value == 5)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNot_Valid()
		{
			validationContext.When("age", 10)
				.IsNot(value => value == 10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsNull()
		{
			var error = validationContext.When("name", (string)null)
				.IsNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNull_Valid()
		{
			validationContext.When("name", "")
				.IsNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsNotNull()
		{
			var error = validationContext.When("name", "")
				.IsNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotNull_Valid()
		{
			validationContext.When("name", (string)null)
				.IsNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsEqual()
		{
			var error = validationContext.When("name", "john")
				.IsEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsEqual_Valid()
		{
			validationContext.When("name", "john")
				.IsEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void IsEqualNull()
		{
			var error = validationContext.When("name", (string)null)
				.IsEqual(null)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEqual()
		{
			var error = validationContext.When("name", "john")
				.IsNotEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEqual_Valid()
		{
			validationContext.When("name", "john")
				.IsNotEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void IsNotEqualNull()
		{
			var error = validationContext.When("name", (string)null)
				.IsNotEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
	}
}