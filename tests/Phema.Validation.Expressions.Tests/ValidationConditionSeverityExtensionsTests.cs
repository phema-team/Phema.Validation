using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
			var error = validationContext.When("key", 12)
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
			var error = validationContext.When("key", 12)
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
			var error = validationContext.When("key", 12)
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
			var error = validationContext.When("key", 12)
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
			var (key, message, severity) = validationContext.When("key", 12)
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
				validationContext.When("key", 12)
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
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Throw(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}
		
		[Fact]
		public void WarningSeverity()
		{
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Warning
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Warning
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Warning
			}));

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddWarning(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void InformationSeverity()
		{
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Information
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Information
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Information
			}));

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddInformation(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void DebugSeverity()
		{
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Debug
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Debug
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Debug
			}));

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddDebug(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void TraceSeverity()
		{
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Trace
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Trace
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Trace
			}));

			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddTrace(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void ValidContextIsLowerErrorSeverity()
		{
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Debug
			}));

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
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions
			{
				Severity = ValidationSeverity.Debug
			}));

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
	}
}