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
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void Is()
		{
			var error = validationContext.Validate("age", 10)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void When()
		{
			var error = validationContext.Validate("age", 10)
				.When(() => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void When_Empty()
		{
			var error = validationContext.Validate("age", 10)
				.When(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("age", 10)
				.When(() => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNot()
		{
			var error = validationContext.Validate("age", 10)
				.IsNot(value => value == 5)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNot_Valid()
		{
			validationContext.Validate("age", 10)
				.IsNot(value => value == 10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNot_Empty()
		{
			var error = validationContext.Validate("age", 10)
				.IsNot(() => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNot()
		{
			var error = validationContext.Validate("age", 10)
				.WhenNot(value => value == 12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNot_Empty()
		{
			var error = validationContext.Validate("age", 10)
				.WhenNot(() => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNot_Added()
		{
			var error = validationContext.Validate("age", 10)
				.IsNot(() => false)
				.AddError(() => new ValidationMessage(() => "template"));
			
			validationContext.Validate("age", 10)
				.WhenNot(() => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNot_Valid_Empty()
		{
			validationContext.Validate("age", 10)
				.IsNot(() => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNot_Valid_Empty()
		{
			validationContext.Validate("age", 10)
				.WhenNot(() => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNull()
		{
			var error = validationContext.Validate((ValidationKey)"name", (string)null)
				.IsNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNull_Empty()
		{
			var error = validationContext.Validate((ValidationKey)"name", (string)null)
				.WhenNull()
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNull_Added()
		{
			var error = validationContext.Validate((ValidationKey)"name", (string)null)
				.IsNull()
				.AddError(() => new ValidationMessage(() => "template"));

			
			validationContext.Validate((ValidationKey)"name", (string)null)
				.WhenNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Single(validationContext.Errors);
			
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNull_Valid()
		{
			validationContext.Validate((ValidationKey)"name", "")
				.IsNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void WhenNull_Valid()
		{
			validationContext.Validate((ValidationKey)"name", "")
				.WhenNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsNotNull()
		{
			var error = validationContext.Validate((ValidationKey)"name", "")
				.IsNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotNull_Empty()
		{
			var error = validationContext.Validate((ValidationKey)"name", "")
				.WhenNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotNull_Added()
		{
			var error = validationContext.Validate((ValidationKey)"name", "")
				.IsNotNull()
				.AddError(() => new ValidationMessage(() => "template"));
			
			validationContext.Validate((ValidationKey)"name", "")
				.WhenNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotNull_Valid()
		{
			validationContext.Validate((ValidationKey)"name", (string)null)
				.IsNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNotNull_Valid()
		{
			validationContext.Validate((ValidationKey)"name", (string)null)
				.WhenNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsEqual()
		{
			var error = validationContext.Validate((ValidationKey)"name", "john")
				.IsEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEqual_Empty()
		{
			var error = validationContext.Validate((ValidationKey)"name", "john")
				.WhenEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEqual_Added()
		{
			var error = validationContext.Validate((ValidationKey)"name", "john")
				.IsEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));
			
			validationContext.Validate((ValidationKey)"name", "john")
				.WhenEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsEqual_Valid()
		{
			validationContext.Validate((ValidationKey)"name", "john")
				.IsEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenEqual_Valid()
		{
			validationContext.Validate((ValidationKey)"name", "john")
				.WhenEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsEqualNull()
		{
			var error = validationContext.Validate((ValidationKey)"name", (string)null)
				.IsEqual(null)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEqualNull_Empty()
		{
			var error = validationContext.Validate((ValidationKey)"name", (string)null)
				.WhenEqual(null)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEqualNull_Added()
		{
			var error = validationContext.Validate((ValidationKey)"name", (string)null)
				.IsEqual(null)
				.AddError(() => new ValidationMessage(() => "template"));
			
			validationContext.Validate((ValidationKey)"name", (string)null)
				.WhenEqual(null)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEqual()
		{
			var error = validationContext.Validate((ValidationKey)"name", "john")
				.IsNotEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEqual_Empty()
		{
			var error = validationContext.Validate((ValidationKey)"name", "john")
				.WhenNotEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEqual_Added()
		{
			var error = validationContext.Validate((ValidationKey)"name", "john")
				.IsNotEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));
			
			validationContext.Validate((ValidationKey)"name", "john")
				.WhenNotEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Single(validationContext.Errors);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEqual_Valid()
		{
			validationContext.Validate((ValidationKey)"name", "john")
				.IsNotEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenNotEqual_Valid()
		{
			validationContext.Validate((ValidationKey)"name", "john")
				.WhenNotEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsNotEqualNull()
		{
			var error = validationContext.Validate((ValidationKey)"name", (string)null)
				.IsNotEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEqualNull()
		{
			var error = validationContext.Validate((ValidationKey)"name", (string)null)
				.WhenNotEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
	}
}