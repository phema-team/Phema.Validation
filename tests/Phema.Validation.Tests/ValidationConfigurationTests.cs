using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConfigurationTests
	{
		[Fact]
		public void NoConfiguration_NoServices()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation();
			
			Assert.Empty(services.Where(s => (s.ImplementationType ?? s.ServiceType).IsAssignableFrom(typeof(IValidationComponent))));
			Assert.Single(services.Where(s => (s.ImplementationType ?? s.ServiceType).IsAssignableFrom(typeof(IConfigureOptions<ValidationOptions>))));
		}

		[Fact]
		public void Component_SingleRegistration()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration.AddComponent<TestModelValidationComponent>());

			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}

		[Fact]
		public void Component_StronglyTyped_SingleRegistration()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration.AddComponent<TestModel, TestModelValidationComponent>());
			
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}
	}
}