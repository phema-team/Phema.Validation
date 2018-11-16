using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Validation.Tests
{
	public class TestModel
	{
		public string Name { get; set; }
		public int Age { get; set; }
	}
	
	public class TestValidation : Validation<TestModel>
	{
		private readonly TestValidationComponent component;

		public TestValidation(TestValidationComponent component)
		{
			this.component = component;
		}
		
		protected override void Validate(IValidationContext validationContext, TestModel model)
		{
			validationContext.Validate(model, m => m.Name)
				.WhenNull()
				.AddError(() => component.NameIsNull);

			validationContext.Validate(model, m => m.Age)
				.WhenInRange(0, 17)
				.AddError(() => component.IsUnderage);
		}
	}
	
	public class TestValidationComponent : ValidationComponent<TestModel, TestValidation>
	{
		public TestValidationComponent()
		{
			NameIsNull = Register(() => "Name is null");
			IsUnderage = Register(() => "Is underage");
		}

		public ValidationMessage NameIsNull { get; }
		public ValidationMessage IsUnderage { get; }
	}
	
	public class ValidationAspNetCoreExtensionsTests
	{
		private readonly IServiceCollection services;

		public ValidationAspNetCoreExtensionsTests()
		{
			services = new ServiceCollection();
		}

		[Fact]
		public void AddsValidation()
		{
			services.AddValidation(validation => {});

			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidationContext)));
			Assert.Single(services.Where(s => s.ServiceType == typeof(IConfigureOptions<MvcOptions>)));
		}
		
		[Fact]
		public void AddsValidationOnce()
		{
			services.AddValidation(validation => {})
				.AddValidation(validation => {});

			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidationContext)));
			Assert.Single(services.Where(s => s.ServiceType == typeof(IConfigureOptions<MvcOptions>)));
		}
		
		[Fact]
		public void AddsValidationModel()
		{
			services.AddValidation(
				validation => 
					validation.AddValidation<TestModel, TestValidation, TestValidationComponent>());

			Assert.Single(services.Where(s => s.ServiceType == typeof(TestValidation)));
			Assert.Single(services.Where(s => s.ServiceType == typeof(TestValidationComponent)));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<ValidationOptions>>().Value;

			var (type, selector) = Assert.Single(options.Validations);

			Assert.Equal(typeof(TestModel), type);
			Assert.Equal(typeof(TestValidation), selector(provider).GetType());
		}

		[Fact]
		public void Validation()
		{
			services.AddValidation(
				v => 
					v.AddValidation<TestModel, TestValidation, TestValidationComponent>());
			
			var provider = services.BuildServiceProvider();


			var model = new TestModel
			{
				Name = null, Age = 10
			};
			var validation = provider.GetRequiredService<TestValidation>();
			var validationContext = provider.GetRequiredService<IValidationContext>();
			
			validation.ValidateCore(validationContext, model);
			
			Assert.Equal(2, validationContext.Errors.Count);
			
			Assert.Collection(validationContext.Errors,
				e =>
				{
					Assert.Equal("Name", e.Key);
					Assert.Equal("Name is null", e.Message);
				},
				e =>
				{
					Assert.Equal("Age", e.Key);
					Assert.Equal("Is underage", e.Message);
				});
		}
	}
}
