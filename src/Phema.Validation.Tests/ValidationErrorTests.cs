using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationErrorTests
	{
		[Fact]
		public void Deconstruct()
		{
			var error = new ValidationError("key", "template", ValidationSeverity.Error);

			var (key, message) = error;
			
			Assert.Equal("key", key);
			Assert.Equal("template", message);
		}
		
		[Fact]
		public void DeconstructSeverity()
		{
			var error = new ValidationError("key", "template", ValidationSeverity.Error);

			var (key, message, severity) = error;
			
			Assert.Equal("key", key);
			Assert.Equal("template", message);
			Assert.Equal(ValidationSeverity.Error, severity);
		}
	}
}