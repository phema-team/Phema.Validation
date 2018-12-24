using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionExtensions
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionExtensions()
		{
			validationContext = new ServiceCollection()
				.AddValidation(c => {})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void IsNot()
		{
			var error = validationContext.Validate("age", 10)
				.IsNot(value => value == 5)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNot_Valid()
		{
			validationContext.Validate("age", 10)
				.IsNot(value => value == 10)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNot_Empty()
		{
			var error = validationContext.Validate("age", 10)
				.IsNot(() => false)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNot_Empty()
		{
			var error = validationContext.Validate("age", 10)
				.WhenNot(() => false)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNot_Added()
		{
			var error = validationContext.Validate("age", 10)
				.IsNot(() => false)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			validationContext.Validate("age", 10)
				.WhenNot(() => false)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNot_Valid_Empty()
		{
			validationContext.Validate("age", 10)
				.IsNot(() => true)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNot_Valid_Empty()
		{
			validationContext.Validate("age", 10)
				.WhenNot(() => true)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNull()
		{
			var error = validationContext.Validate("name", (string)null)
				.IsNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNull_Empty()
		{
			var error = validationContext.Validate("name", (string)null)
				.WhenNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNull_Added()
		{
			var error = validationContext.Validate("name", (string)null)
				.IsNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			
			validationContext.Validate("name", (string)null)
				.WhenNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Single(validationContext.Errors);
			
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNull_Valid()
		{
			validationContext.Validate("name", "")
				.IsNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void WhenNull_Valid()
		{
			validationContext.Validate("name", "")
				.WhenNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotNull()
		{
			var error = validationContext.Validate("name", "")
				.IsNotNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotNull_Empty()
		{
			var error = validationContext.Validate("name", "")
				.WhenNotNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotNull_Added()
		{
			var error = validationContext.Validate("name", "")
				.IsNotNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			validationContext.Validate("name", "")
				.WhenNotNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotNull_Valid()
		{
			validationContext.Validate("name", (string)null)
				.IsNotNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNotNull_Valid()
		{
			validationContext.Validate("name", (string)null)
				.WhenNotNull()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsEqual()
		{
			var error = validationContext.Validate("name", "john")
				.IsEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEqual_Empty()
		{
			var error = validationContext.Validate("name", "john")
				.WhenEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEqual_Added()
		{
			var error = validationContext.Validate("name", "john")
				.IsEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			validationContext.Validate("name", "john")
				.WhenEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsEqual_Valid()
		{
			validationContext.Validate("name", "john")
				.IsEqual("notjohn")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenEqual_Valid()
		{
			validationContext.Validate("name", "john")
				.WhenEqual("notjohn")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsEqualNull()
		{
			var error = validationContext.Validate("name", (string)null)
				.IsEqual(null)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEqualNull_Empty()
		{
			var error = validationContext.Validate("name", (string)null)
				.WhenEqual(null)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEqualNull_Added()
		{
			var error = validationContext.Validate("name", (string)null)
				.IsEqual(null)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			validationContext.Validate("name", (string)null)
				.WhenEqual(null)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEqual()
		{
			var error = validationContext.Validate("name", "john")
				.IsNotEqual("notjohn")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEqual_Empty()
		{
			var error = validationContext.Validate("name", "john")
				.WhenNotEqual("notjohn")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEqual_Added()
		{
			var error = validationContext.Validate("name", "john")
				.IsNotEqual("notjohn")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			validationContext.Validate("name", "john")
				.WhenNotEqual("notjohn")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEqual_Valid()
		{
			validationContext.Validate("name", "john")
				.IsNotEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNotEqual_Valid()
		{
			validationContext.Validate("name", "john")
				.WhenNotEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsNotEqualNull()
		{
			var error = validationContext.Validate("name", (string)null)
				.IsNotEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEqualNull()
		{
			var error = validationContext.Validate("name", (string)null)
				.WhenNotEqual("john")
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
	}
}