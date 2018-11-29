using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class CommonAspNetCoreTests
	{
		private readonly IServiceProvider provider;
		
		public CommonAspNetCoreTests()
		{
			var services = new ServiceCollection();
			
			services.AddValidation(validation => {});
			
			provider = services.BuildServiceProvider();
		}
		
		[Fact]
		public void EmptyValidationResolves()
		{
			var validationContext = provider.GetRequiredService<IValidationContext>();
			
			Assert.IsType<ProviderValidationContext>(validationContext);
			Assert.Equal(ValidationSeverity.Error, validationContext.Severity);
			Assert.Equal(0, validationContext.Errors.Count);
		}
		
		[Fact]
		public void ConfigureValidationOptionsChangesSeverity()
		{
			var services = new ServiceCollection();
			
			services.AddValidation(validation => {});

			services.Configure<ValidationOptions>(options => options.Severity = ValidationSeverity.Debug);

			var provider = services.BuildServiceProvider();

			var validationContext = provider.GetRequiredService<IValidationContext>();
			
			Assert.Equal(ValidationSeverity.Debug, validationContext.Severity);
		}
	}
}