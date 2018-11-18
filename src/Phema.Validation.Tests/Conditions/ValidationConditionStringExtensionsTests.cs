using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionStringExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionStringExtensionsTests()
		{
			validationContext = new ValidationContext();
		}
		
		[Fact]
		public void IsEmpty()
		{
			var error = validationContext.When("name", string.Empty)
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsEmpty_Valid()
		{
			validationContext.When("name", "john")
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsNotEmpty()
		{
			var error = validationContext.When("name", "john")
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEmpty_Valid()
		{
			validationContext.When("name", string.Empty)
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsNullOrWhitespace()
		{
			var error = validationContext.When("name", " ")
				.IsNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNullOrWhitespace_Valid()
		{
			validationContext.When("name", "john")
				.IsNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsMatch()
		{
			var error = validationContext.When("name", "abc")
				.IsMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsMatch_Valid()
		{
			validationContext.When("name", "def")
				.IsMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsNotMatch()
		{
			var error = validationContext.When("name", "def")
				.IsNotMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotMatch_Valid()
		{
			validationContext.When("name", "abc")
				.IsNotMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsNotEmail()
		{
			var error = validationContext.When("email", "email")
				.IsNotEmail()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("email", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEmail_Valid()
		{
			validationContext.When("email", "email@email.com")
				.IsNotEmail()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void HasLength()
		{
			var error = validationContext.When("name", "john")
				.HasLength(4)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void HasLength_Valid()
		{
			validationContext.When("name", "john")
				.HasLength(5)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid());
		}
	}
}