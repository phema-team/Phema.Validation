using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionComparableExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionComparableExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void IsGreater()
		{
			var error = validationContext.Validate("age", 11)
				.IsGreater(10)
				
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenGreater_Empty()
		{
			var error = validationContext.Validate("age", 11)
				.WhenGreater(10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenGreater_Added()
		{
			var error = validationContext.Validate("age", 11)
				.WhenGreater(10)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("age", 11)
				.WhenGreater(10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsGreater_Valid()
		{
			validationContext.Validate("age", 9)
				.IsGreater(10)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenGreater_Valid()
		{
			validationContext.Validate("age", 9)
				.WhenGreater(10)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsLess()
		{
			var error = validationContext.Validate("age", 11)
				.IsLess(12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenLess_Empty()
		{
			var error = validationContext.Validate("age", 11)
				.WhenLess(12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenLess_Added()
		{
			var error = validationContext.Validate("age", 11)
				.WhenLess(12)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("age", 11)
				.WhenLess(12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsLess_Valid()
		{
			validationContext.Validate("age", 11)
				.IsLess(10)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenLess_Valid()
		{
			validationContext.Validate("age", 11)
				.WhenLess(10)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsInRange()
		{
			var error = validationContext.Validate("age", 11)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenInRange_Empty()
		{
			var error = validationContext.Validate("age", 11)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenInRange_Added()
		{
			var error = validationContext.Validate("age", 11)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("age", 11)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsInRange_Less_Valid()
		{
			validationContext.Validate("age", 9)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenInRange_Less_Valid()
		{
			validationContext.Validate("age", 9)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsInRange_Greater_Valid()
		{
			validationContext.Validate("age", 13)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenInRange_Greater_Valid()
		{
			validationContext.Validate("age", 13)
				.WhenInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(!validationContext.Errors.Any());
		}
	}
}