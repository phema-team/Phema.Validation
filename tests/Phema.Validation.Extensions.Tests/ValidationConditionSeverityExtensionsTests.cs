using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionSeverityExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionSeverityExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(c => {})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void ErrorSeverity()
		{
			var error = validationContext.Validate("key", 12)
				.Is(value => value == 12)
				.AddError(() => new ValidationMessage(() => "message"));
			
			Assert.NotNull(error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void ErrorSeverity_OneParameter()
		{
			var error = validationContext.Validate("key", 12)
				.Is(value => value == 12)
				.AddError(() => new ValidationMessage<int>(one => $"message: {one}"), 11);
			
			Assert.NotNull(error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void ErrorSeverity_TwoParameter()
		{
			var error = validationContext.Validate("key", 12)
				.Is(value => value == 12)
				.AddError(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.NotNull(error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void ErrorSeverity_ThreeParameter()
		{
			var error = validationContext.Validate("key", 12)
				.Is(value => value == 12)
				.AddError(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), 11, 22, 33);
			
			Assert.NotNull(error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void FatalSeverity()
		{
			var (key, message, severity) = validationContext.Validate("key", 12)
				.Is(value => value == 12)
				.Add(() => new ValidationMessage(() => "message"), ValidationSeverity.Fatal);
			
			Assert.Equal("key", key);
			Assert.Equal("message", message);
			Assert.Equal(ValidationSeverity.Fatal, severity);
		}
		
		[Fact]
		public void FatalSeverity_OneParameter()
		{
			Assert.Throws<ValidationConditionException>(() =>
				validationContext.Validate("key", 12)
					.Is(value => value == 12)
					.Throw(() => new ValidationMessage<int>(one => $"message: {one}"), 11));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}
		
		[Fact]
		public void FatalSeverity_TwoParameters()
		{
			Assert.Throws<ValidationConditionException>(() =>
				validationContext.Validate("key", 12)
					.Is(value => value == 12)
					.Throw(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}
		
		[Fact]
		public void FatalSeverity_ThreeParameters()
		{
			Assert.Throws<ValidationConditionException>(() =>
				validationContext.Validate("key", 12)
					.Is(value => value == 12)
					.Throw(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), 11, 22, 33));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}
	}
}