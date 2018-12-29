using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionStringExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionStringExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void IsEmpty()
		{
			var error = validationContext.Validate("name", string.Empty)
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEmpty_Empty()
		{
			var error = validationContext.Validate("name", string.Empty)
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEmpty_Added()
		{
			var error = validationContext.Validate("name", string.Empty)
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			var @null = validationContext.Validate("name", string.Empty)
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsEmpty_Valid()
		{
			validationContext.Validate("name", "john")
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenEmpty_Valid()
		{
			validationContext.Validate("name", "john")
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotEmpty()
		{
			var error = validationContext.Validate("name", "john")
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEmpty_Empty()
		{
			var error = validationContext.Validate("name", "john")
				.WhenNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEmpty_Added()
		{
			var error = validationContext.Validate("name", "john")
				.WhenNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("name", "john")
				.WhenNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEmpty_Valid()
		{
			validationContext.Validate("name", string.Empty)
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNotEmpty_Valid()
		{
			validationContext.Validate("name", string.Empty)
				.WhenNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNullOrWhitespace()
		{
			var error = validationContext.Validate("name", " ")
				.IsNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNullOrWhitespace_Empty()
		{
			var error = validationContext.Validate("name", " ")
				.WhenNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNullOrWhitespace_Added()
		{
			var error = validationContext.Validate("name", " ")
				.WhenNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "template"));

			var @null = validationContext.Validate("name", " ")
				.WhenNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNullOrWhitespace_Valid()
		{
			validationContext.Validate("name", "john")
				.IsNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNullOrWhitespace_Valid()
		{
			validationContext.Validate("name", "john")
				.WhenNullOrWhitespace()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsMatch()
		{
			var error = validationContext.Validate("name", "abc")
				.IsMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenMatch()
		{
			var error = validationContext.Validate("name", "abc")
				.WhenMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsMatch_Valid()
		{
			validationContext.Validate("name", "def")
				.IsMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenMatch_Valid()
		{
			validationContext.Validate("name", "def")
				.WhenMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotMatch()
		{
			var error = validationContext.Validate("name", "def")
				.IsNotMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotMatch()
		{
			var error = validationContext.Validate("name", "def")
				.WhenNotMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotMatch_Valid()
		{
			validationContext.Validate("name", "abc")
				.IsNotMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNotMatch_Valid()
		{
			validationContext.Validate("name", "abc")
				.WhenNotMatch("[a-c]+")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotEmail()
		{
			var error = validationContext.Validate((ValidationKey)"email", "email")
				.IsNotEmail()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("email", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEmail()
		{
			var error = validationContext.Validate((ValidationKey)"email", "email")
				.WhenNotEmail()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("email", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEmail_Valid()
		{
			validationContext.Validate((ValidationKey)"email", "email@email.com")
				.IsNotEmail()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNotEmail_Valid()
		{
			validationContext.Validate((ValidationKey)"email", "email@email.com")
				.WhenNotEmail()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void HasLength()
		{
			var error = validationContext.Validate("name", "john")
				.HasLength(4)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenHasLength()
		{
			var error = validationContext.Validate("name", "john")
				.WhenHasLength(4)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void HasLength_Valid()
		{
			validationContext.Validate("name", "john")
				.HasLength(5)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenHasLength_Valid()
		{
			validationContext.Validate("name", "john")
				.WhenHasLength(5)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
	}
}