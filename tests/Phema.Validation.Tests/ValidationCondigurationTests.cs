using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Validation.Tests
{
	public class TestModel
	{
	}
		
	public class TestModelValidation : IValidation<TestModel>
	{
		public void Validate(IValidationContext validationContext, TestModel model)
		{
			throw new System.NotImplementedException();
		}
	}

	public class TestModelValidationComponent : IValidationComponent<TestModel, TestModelValidation>
	{
		public TestModelValidationComponent()
		{
			TestModelTemplate = new ValidationTemplate(() => "template");
		}

		public ValidationTemplate TestModelTemplate { get; }
	}
	
	public class ValidationCondigurationTests
	{
		

		[Fact]
		public void NoConfiguration_NoServices()
		{
			var services = new ServiceCollection()
				.AddPhemaValidation();
			
			Assert.False(services.Any(s => (s.ImplementationType ?? s.ServiceType).IsAssignableFrom(typeof(IValidation<>))));
			Assert.False(services.Any(s => (s.ImplementationType ?? s.ServiceType).IsAssignableFrom(typeof(IValidationComponent))));
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

		[Fact]
		public void ConfigureValidationOptions()
		{
			var provider = new ServiceCollection()
				.AddPhemaValidation(options: o => o.Severity = ValidationSeverity.Trace)
				.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<ValidationOptions>>().Value;
			
			Assert.Equal(ValidationSeverity.Trace, options.Severity);
		}
	}
}