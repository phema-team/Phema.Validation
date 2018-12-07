using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextSeverityExtensionsTests
	{
		[Fact]
		public void WarningSeverity()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Warning);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddWarning(() => new ValidationMessage(() => "message"));
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void WarningSeverity_OneParameter()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Warning);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddWarning(() => new ValidationMessage<int>(one => $"message: {one}"), 11);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void WarningSeverity_TwoParameters()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Warning);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddWarning(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void WarningSeverity_ThreeParameters()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Warning);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddWarning(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), 11, 22, 33);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void InformationSeverity()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Information);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddInformation(() => new ValidationMessage(() => "message"));
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void InformationSeverity_OneParameter()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Information);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddInformation(() => new ValidationMessage<int>(one=> $"message: {one}"), 11);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void InformationSeverity_TwoParameters()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Information);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddInformation(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void InformationSeverity_ThreeParameters()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Information);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddInformation(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), 11, 22, 33);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void DebugSeverity()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Debug);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddDebug(() => new ValidationMessage(() => "message"));
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void DebugSeverity_OneParameter()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Debug);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddDebug(() => new ValidationMessage<int>(one => $"message: {one}"), 11);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void DebugSeverity_TwoParameters()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Debug);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddDebug(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void DebugSeverity_ThreeParameters()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Debug);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddDebug(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), 11, 22, 33);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void TraceSeverity()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Trace);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddTrace(() => new ValidationMessage(() => "message"));
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void TraceSeverity_OneParameter()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Trace);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddTrace(() => new ValidationMessage<int>(one=> $"message: {one}"), 11);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void TraceSeverity_TwoParameters()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Trace);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddTrace(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void TraceSeverity_ThreeParameters()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Trace);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddTrace(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), 11, 22, 33);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void ValidContextIsLowerErrorSeverity()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Debug);

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddTrace(() => new ValidationMessage(() => "message"));
			
			Assert.NotNull(error);
			Assert.Single(validationContext.Errors);
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void ValidContextIsLowerErrorSeverityByKey()
		{
			var validationContext = CreateValidationContext(ValidationSeverity.Debug);

			validationContext.When("key1", 12)
				.Is(value => value == 12)
				.AddTrace(() => new ValidationMessage(() => "message"));
			
			validationContext.When("key2", 12)
				.Is(value => value == 12)
				.AddInformation(() => new ValidationMessage(() => "message"));
			
			Assert.Collection(validationContext.Errors,
				e =>
				{
					Assert.Equal("key1", e.Key);
					Assert.Equal(ValidationSeverity.Trace, e.Severity);
				},
				e =>
				{
					Assert.Equal("key2", e.Key);
					Assert.Equal(ValidationSeverity.Information, e.Severity);
				});
			
			Assert.True(validationContext.IsValid("key1"));
			Assert.False(validationContext.IsValid("key2"));
		}

		private IValidationContext CreateValidationContext(ValidationSeverity severity)
		{
			return new ServiceCollection()
				.AddValidation(c => {})
				.Configure<ValidationOptions>(o => o.Severity = severity)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
	}
}