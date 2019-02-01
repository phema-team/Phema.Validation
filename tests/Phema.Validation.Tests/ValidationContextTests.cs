using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextTests
	{
		[Fact]
		public void EmptyValidationResolves()
		{
			var services = new ServiceCollection();

			services.AddPhemaValidation();

			var provider = services.BuildServiceProvider();

			var validationContext = provider.GetRequiredService<IValidationContext>();

			Assert.IsType<ValidationContext>(validationContext);
			Assert.Equal(ValidationSeverity.Error, validationContext.Severity);
			Assert.Equal(0, validationContext.Errors.Count);
		}
		
		[Fact]
		public void ConfigureValidationOptionsChangesSeverity()
		{
			var services = new ServiceCollection();

			services.AddPhemaValidation();

			services.Configure<ValidationOptions>(options => options.Severity = ValidationSeverity.Debug);

			var provider = services.BuildServiceProvider();

			var validationContext = provider.GetRequiredService<IValidationContext>();

			Assert.Equal(ValidationSeverity.Debug, validationContext.Severity);
		}
	}
}