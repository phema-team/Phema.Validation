using System.Linq;
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
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNot_Valid()
		{
			validationContext.When("age", 10)
				.IsNot(value => value == 10)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNull()
		{
			var error = validationContext.When("name", (string)null)
				.IsNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNull_Valid()
		{
			validationContext.When("name", "")
				.IsNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotNull()
		{
			var error = validationContext.When("name", "")
				.IsNotNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotNull_Valid()
		{
			validationContext.When("name", (string)null)
				.IsNotNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsEqual()
		{
			var error = validationContext.When("name", "john")
				.IsEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsEqual_Valid()
		{
			validationContext.When("name", "john")
				.IsEqual("notjohn")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsEqualNull()
		{
			var error = validationContext.When("name", (string)null)
				.IsEqual(null)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEqual()
		{
			var error = validationContext.When("name", "john")
				.IsNotEqual("notjohn")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEqual_Valid()
		{
			validationContext.When("name", "john")
				.IsNotEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsNotEqualNull()
		{
			var error = validationContext.When("name", (string)null)
				.IsNotEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
	}
}