using Xunit;

namespace Phema.Validation.Tests
{
	public class SeverityTests
	{
		[Fact]
		public void ErrorSeverity()
		{
			var validationContext = new ValidationContext();

			var error = validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddError(() => new ValidationMessage(() => "message"));
			
			Assert.NotNull(error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void ErrorSeverity_OneParameter()
		{
			var validationContext = new ValidationContext();

			var error = validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddError(() => new ValidationMessage<int>(() => "message: {0}"), 11);
			
			Assert.NotNull(error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void ErrorSeverity_TwoParameter()
		{
			var validationContext = new ValidationContext();

			var error = validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddError(() => new ValidationMessage<int, int>(() => "message: {0},{1}"), 11, 22);
			
			Assert.NotNull(error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void FatalSeverity()
		{
			var validationContext = new ValidationContext();

			Assert.Throws<ValidationConditionException>(() =>
				validationContext.Validate("key", 12)
					.WhenEqual(12)
					.Throw(() => new ValidationMessage(() => "message")));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}
		
		[Fact]
		public void FatalSeverity_OneParameter()
		{
			var validationContext = new ValidationContext();

			Assert.Throws<ValidationConditionException>(() =>
				validationContext.Validate("key", 12)
					.WhenEqual(12)
					.Throw(() => new ValidationMessage<int>(() => "message: {0}"), 11));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}
		
		[Fact]
		public void FatalSeverity_TwoParameters()
		{
			var validationContext = new ValidationContext();

			Assert.Throws<ValidationConditionException>(() =>
				validationContext.Validate("key", 12)
					.WhenEqual(12)
					.Throw(() => new ValidationMessage<int, int>(() => "message: {0},{1}"), 11, 22));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}
		
		[Fact]
		public void WarningSeverity()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Warning);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddWarning(() => new ValidationMessage(() => "message"));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void WarningSeverity_OneParameter()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Warning);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddWarning(() => new ValidationMessage<int>(() => "message: {0}"), 11);
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void WarningSeverity_TwoParameters()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Warning);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddWarning(() => new ValidationMessage<int, int>(() => "message: {0},{1}"), 11, 22);
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void InformationSeverity()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Information);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddInformation(() => new ValidationMessage(() => "message"));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void InformationSeverity_OneParameter()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Information);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddInformation(() => new ValidationMessage<int>(() => "message: {0}"), 11);
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void InformationSeverity_TwoParameters()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Information);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddInformation(() => new ValidationMessage<int, int>(() => "message: {0},{1}"), 11, 22);
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void DebugSeverity()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Debug);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddDebug(() => new ValidationMessage(() => "message"));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void DebugSeverity_OneParameter()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Debug);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddDebug(() => new ValidationMessage<int>(() => "message: {0}"), 11);
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void DebugSeverity_TwoParameters()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Debug);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddDebug(() => new ValidationMessage<int, int>(() => "message: {0},{1}"), 11, 22);
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void TraceSeverity()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Trace);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddTrace(() => new ValidationMessage(() => "message"));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void TraceSeverity_OneParameter()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Trace);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddTrace(() => new ValidationMessage<int>(() => "message: {0}"), 11);
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void TraceSeverity_TwoParameters()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Trace);

			validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddTrace(() => new ValidationMessage<int, int>(() => "message: {0},{1}"), 11, 22);
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void ValidContextWhenLowerErrorSeverity()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Debug);

			var error = validationContext.Validate("key", 12)
				.WhenEqual(12)
				.AddTrace(() => new ValidationMessage(() => "message"));
			
			Assert.NotNull(error);
			Assert.Single(validationContext.Errors);
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void ValidContextWhenLowerErrorSeverityByKey()
		{
			var validationContext = new ValidationContext(ValidationSeverity.Debug);

			validationContext.Validate("key1", 12)
				.WhenEqual(12)
				.AddTrace(() => new ValidationMessage(() => "message"));
			
			validationContext.Validate("key2", 12)
				.WhenEqual(12)
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