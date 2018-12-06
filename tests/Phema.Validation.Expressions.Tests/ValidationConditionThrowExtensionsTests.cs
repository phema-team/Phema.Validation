using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionThrowExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionThrowExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(c => {})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void Throw()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("age", 10)
					.Is(value => value == 10)
					.Throw(() => new ValidationMessage(() => "template")));

			Assert.Equal("age", exception.Error.Key);
			Assert.Equal("template", exception.Error.Message);
			Assert.Equal(ValidationSeverity.Fatal, exception.Error.Severity);
		}
		
		[Fact]
		public void Throw_Valid()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.Throw(() => new ValidationMessage(() => "template"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void Throw_OneParameter()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("age", 10)
					.Is(value => value == 10)
					.Throw(() => new ValidationMessage<int>(one => $"template: {one}"), 11));

			Assert.Equal("age", exception.Error.Key);
			Assert.Equal("template: 11", exception.Error.Message);
			Assert.Equal(ValidationSeverity.Fatal, exception.Error.Severity);
		}
		
		[Fact]
		public void Throw_OneParameter_Valid()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.Throw(() => new ValidationMessage<int>(one => $"template: {one}"), 11);
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void Throw_TwoParameters()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("age", 10)
					.Is(value => value == 10)
					.Throw(() => new ValidationMessage<int, int>((one, two) => $"template: {one},{two}"), 11, 22));

			Assert.Equal("age", exception.Error.Key);
			Assert.Equal("template: 11,22", exception.Error.Message);
			Assert.Equal(ValidationSeverity.Fatal, exception.Error.Severity);
		}
		
		[Fact]
		public void Throw_TwoParameters_Valid()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.Throw(() => new ValidationMessage<int, int>((one, two) => $"template: {one},{two}"), 11, 22);
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void ThrowAddsSameErrorAsContext()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("age", 10)
					.Is(value => value == 10)
					.Throw(() => new ValidationMessage(() => "template")));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal(error, exception.Error);
			Assert.Same(error, exception.Error);
		}
	}
}