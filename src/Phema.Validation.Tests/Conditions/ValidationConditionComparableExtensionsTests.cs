using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionComparableExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionComparableExtensionsTests()
		{
			validationContext = new ValidationContext();
		}
		
		[Fact]
		public void IsGreater()
		{
			var error = validationContext.When("age", 11)
				.IsGreater(10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsGreater_Valid()
		{
			validationContext.When("age", 9)
				.IsGreater(10)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsLess()
		{
			var error = validationContext.When("age", 11)
				.IsLess(12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsLess_Valid()
		{
			validationContext.When("age", 11)
				.IsLess(10)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsInRange()
		{
			var error = validationContext.When("age", 11)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsInRange_Less_Valid()
		{
			validationContext.When("age", 9)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void IsInRange_Greater_Valid()
		{
			validationContext.When("age", 13)
				.IsInRange(10, 12)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.True(validationContext.IsValid());
		}
	}
}