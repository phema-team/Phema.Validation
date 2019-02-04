using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationTests
	{
		[Fact]
		public void Validation_SingleRegistration()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration.AddValidation<TestModel, TestModelValidation>());
			
			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidation<TestModel>)));
		}
		
		[Fact]
		public void Validation_MultipleRegistrations()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration
					.AddValidation<TestModel, TestModelValidation>()
					.AddValidation<TestModel, TestModelValidation>());
			
			Assert.Equal(2, services.Count(s => s.ServiceType == typeof(IValidation<TestModel>)));
		}

		[Fact]
		public void ValidationComponent_SingleRegistration()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration
					.AddValidationComponent<TestModel, TestModelValidation, TestModelValidationComponent>());
			
			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidation<TestModel>)));
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}
		
		[Fact]
		public void ValidationComponent_MultipleRegistrations()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration
					.AddValidationComponent<TestModel, TestModelValidation, TestModelValidationComponent>()
					.AddValidationComponent<TestModel, TestModelValidation, TestModelValidationComponent>());
			
			Assert.Equal(2, services.Count(s => s.ServiceType == typeof(IValidation<TestModel>)));
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}
	}
}