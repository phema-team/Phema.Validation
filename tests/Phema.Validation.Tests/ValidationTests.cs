using System.Linq;

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
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
				.AddMvcCore()
				.AddPhemaValidation(configuration => configuration.AddValidation<TestModel, TestModelValidator>())
				.Services;
			
			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidator<TestModel>)));
		}
		
		[Fact]
		public void Validation_MultipleRegistrations()
		{
			var services = new ServiceCollection()
				.AddMvcCore()
				.AddPhemaValidation(configuration => configuration
					.AddValidation<TestModel, TestModelValidator>()
					.AddValidation<TestModel, TestModelValidator>())
				.Services;
			
			Assert.Equal(2, services.Count(s => s.ServiceType == typeof(IValidator<TestModel>)));
		}

		[Fact]
		public void ValidationComponent_SingleRegistration()
		{
			var services = new ServiceCollection()
				.AddMvcCore()
				.AddPhemaValidation(configuration => configuration
					.AddValidationComponent<TestModel, TestModelValidator, TestModelValidationComponent>())
				.Services;
			
			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidator<TestModel>)));
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}
		
		[Fact]
		public void ValidationComponent_MultipleRegistrations()
		{
			var services = new ServiceCollection()
				.AddMvcCore()
				.AddPhemaValidation(configuration => configuration
					.AddValidationComponent<TestModel, TestModelValidator, TestModelValidationComponent>()
					.AddValidationComponent<TestModel, TestModelValidator, TestModelValidationComponent>())
				.Services;
			
			Assert.Equal(2, services.Count(s => s.ServiceType == typeof(IValidator<TestModel>)));
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}

		[Fact]
		public void UsingMvcCoreBuilder()
		{
			var services = new MvcCoreBuilder(new ServiceCollection(), new ApplicationPartManager())
				.AddPhemaValidation(c => c.AddValidationComponent<TestModel, TestModelValidator, TestModelValidationComponent>())
				.Services;
			
			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidator<TestModel>)));
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}
		
		[Fact]
		public void UsingMvcBuilder()
		{
			var services = new MvcBuilder(new ServiceCollection(), new ApplicationPartManager())
				.AddPhemaValidation(c => c.AddValidationComponent<TestModel, TestModelValidator, TestModelValidationComponent>())
				.Services;
			
			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidator<TestModel>)));
			Assert.Single(services.Where(s => s.ImplementationType == typeof(TestModelValidationComponent)));
		}
	}
}