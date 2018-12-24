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
				.AddValidation(c => {})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void IsEmpty()
		{
			var error = validationContext.Validate("name", string.Empty)
				.IsEmpty()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsEmpty_Valid()
		{
			validationContext.Validate("name", "john")
				.IsEmpty()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotEmpty()
		{
			var error = validationContext.Validate("name", "john")
				.IsNotEmpty()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEmpty_Valid()
		{
			validationContext.Validate("name", string.Empty)
				.IsNotEmpty()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNullOrWhitespace()
		{
			var error = validationContext.Validate("name", " ")
				.IsNullOrWhitespace()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNullOrWhitespace_Valid()
		{
			validationContext.Validate("name", "john")
				.IsNullOrWhitespace()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsMatch()
		{
			var error = validationContext.Validate("name", "abc")
				.IsMatch("[a-c]+")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsMatch_Valid()
		{
			validationContext.Validate("name", "def")
				.IsMatch("[a-c]+")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotMatch()
		{
			var error = validationContext.Validate("name", "def")
				.IsNotMatch("[a-c]+")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotMatch_Valid()
		{
			validationContext.Validate("name", "abc")
				.IsNotMatch("[a-c]+")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotEmail()
		{
			var error = validationContext.Validate("email", "email")
				.IsNotEmail()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("email", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEmail_Valid()
		{
			validationContext.Validate("email", "email@email.com")
				.IsNotEmail()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void HasLength()
		{
			var error = validationContext.Validate("name", "john")
				.HasLength(4)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void HasLength_Valid()
		{
			validationContext.Validate("name", "john")
				.HasLength(5)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
	}
}