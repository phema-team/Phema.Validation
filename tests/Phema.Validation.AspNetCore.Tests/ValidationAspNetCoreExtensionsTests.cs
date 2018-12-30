using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Validation.Tests
{
	public class TestValidation : IValidation<TestModel>
	{
		public void Validate(IValidationContext validationContext, TestModel model)
		{
			validationContext.When(nameof(model.Name), model.Name)
				.Is((value) => value == null)
				.AddError<TestValidationComponent>(c => c.NameIsNull);

			validationContext.When(nameof(model.Age), model.Age)
				.Is((value) => value > 0 && value < 17)
				.AddError<TestValidationComponent>(c => c.IsUnderage);
		}
	}

	public class TestValidationComponent : IValidationComponent<TestModel, TestValidation>
	{
		public TestValidationComponent()
		{
			NameIsNull = new ValidationMessage(() => "Name is null");
			IsUnderage = new ValidationMessage(() => "Is underage");
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
			services.AddPhemaValidation(validation =>
			{
			});

			Assert.Single(services.Where(s => s.ServiceType == typeof(IValidationContext)));
			Assert.Single(services.Where(s => s.ServiceType == typeof(IConfigureOptions<MvcOptions>)));
		}

		[Fact]
		public void AddsValidationModel()
		{
			services.AddPhemaValidation(
				validation =>
					validation.Add<TestModel, TestValidation, TestValidationComponent>());

			Assert.Single(services.Where(s => s.ServiceType == typeof(TestValidation)));
			Assert.Single(services.Where(s => s.ServiceType == typeof(TestValidationComponent)));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<ValidationComponentOptions>>().Value;

			var (type, _) = Assert.Single(options.Validations);

			Assert.Equal(typeof(TestModel), type);
		}

		[Fact]
		public void Validation()
		{
			services.AddPhemaValidation(
				v =>
					v.Add<TestModel, TestValidation, TestValidationComponent>());

			services.AddScoped<IHttpContextAccessor>(sp =>
				new HttpContextAccessor
				{
					HttpContext = new DefaultHttpContext()
				});

			var provider = services.BuildServiceProvider();

			var model = new TestModel
			{
				Name = null, Age = 10
			};

			var validation = provider.GetRequiredService<TestValidation>();
			var validationContext = provider.GetRequiredService<IValidationContext>();

			validation.Validate(validationContext, model);

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