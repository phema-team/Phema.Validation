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
				.AddPhemaValidation(configuration => configuration.AddValidation<TestModel, TestModelValidator>());
			
			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidator<TestModel>)));
		}
		
		[Fact]
		public void Validation_MultipleRegistrations()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration
					.AddValidation<TestModel, TestModelValidator>()
					.AddValidation<TestModel, TestModelValidator>());
			
			Assert.Equal(2, services.Count(s => s.ServiceType == typeof(IValidator<TestModel>)));
		}

		[Fact]
		public void ValidationComponent_SingleRegistration()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration
					.AddValidationComponent<TestModel, TestModelValidator, TestModelValidationComponent>());
			
			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidator<TestModel>)));
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}
		
		[Fact]
		public void ValidationComponent_MultipleRegistrations()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation(configuration => configuration
					.AddValidationComponent<TestModel, TestModelValidator, TestModelValidationComponent>()
					.AddValidationComponent<TestModel, TestModelValidator, TestModelValidationComponent>());
			
			Assert.Equal(2, services.Count(s => s.ServiceType == typeof(IValidator<TestModel>)));
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}
	}
}