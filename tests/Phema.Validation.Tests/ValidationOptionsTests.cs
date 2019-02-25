using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationOptionsTests
	{
		[Theory]
		[InlineData(ValidationSeverity.Fatal)]
		[InlineData(ValidationSeverity.Error)]
		[InlineData(ValidationSeverity.Warning)]
		[InlineData(ValidationSeverity.Information)]
		[InlineData(ValidationSeverity.Debug)]
		[InlineData(ValidationSeverity.Trace)]
		public void ConfigureValidationOptions(ValidationSeverity severity)
		{
			var provider = new ServiceCollection()
				.AddPhemaValidation(
					configuration: c => {},
					options: o => o.Severity = severity)
				.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<PhemaValidationOptions>>().Value;
			
			Assert.Equal(severity, options.Severity);
		}
	}
}