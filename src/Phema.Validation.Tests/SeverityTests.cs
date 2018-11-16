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
	}
}