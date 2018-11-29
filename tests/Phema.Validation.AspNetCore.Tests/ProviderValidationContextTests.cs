using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ProviderValidationContextTests
	{
		public class Validation : IValidation<TestModel>
		{
			public void Validate(IValidationContext validationContext, TestModel model)
			{
				validationContext.When(model, m => m.Name)
					.Is(value => value == "Invalid")
					.AddError<ValidationComponent>(c => c.NameIsInvalid);

				validationContext.When(model, m => m.Age)
					.Is(value => value == 12)
					.AddError<ValidationComponent, int>(c => c.AgeIsInvalid, model.Age);

				validationContext.When(model, m => m.Phone)
					.Is(value => value == 8_800_555_35_35)
					.AddError<ValidationComponent, long, int>(c => c.PhoneIsInvalid, model.Phone, model.Age);
			}
		}

		public class ValidationComponent : IValidationComponent<TestModel, Validation>
		{
			public ValidationComponent()
			{
				NameIsInvalid = new ValidationMessage(() => "message");
				AgeIsInvalid =  new ValidationMessage<int>(() => "age: {0}");
				PhoneIsInvalid = new ValidationMessage<long, int>(() => "phone: {0} age: {1}");
			}

			public ValidationMessage NameIsInvalid { get; }
			public ValidationMessage<int> AgeIsInvalid { get; }
			public ValidationMessage<long, int> PhoneIsInvalid { get; }
		}

		private readonly IServiceProvider provider;

		public ProviderValidationContextTests()
		{
			var services = new ServiceCollection();

			services.AddValidation(v => v.Add<TestModel, Validation, ValidationComponent>());

			services.AddScoped<IHttpContextAccessor>(sp =>
				new HttpContextAccessor
				{
					HttpContext = new DefaultHttpContext()
				});
			
			provider = services.BuildServiceProvider();
		}


		[Fact]
		public void ProviderValidationContext()
		{
			var validationContext = provider.GetRequiredService<IValidationContext>();

			var validation = provider.GetRequiredService<Validation>();

			validation.Validate(validationContext, new TestModel { Name = "Invalid" });

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("name", error.Key);
			Assert.Equal("message", error.Message);
		}

		[Fact]
		public void ProviderValidationContext_ValidationFilter()
		{
			var filter = new ValidationFilter();

			var context = new ActionExecutingContext(
				new ActionContext
				{
					HttpContext = new DefaultHttpContext
					{
						RequestServices = provider
					},
					RouteData = new RouteData(),
					ActionDescriptor = new ActionDescriptor()
				},
				new List<IFilterMetadata>(),
				new Dictionary<string, object>
				{
					["key"] = new TestModel
					{
						Age = 12
					}
				},
				new object());

			filter.OnActionExecuting(context);

			var result = Assert.IsType<ValidationResult>(context.Result);

			var value = Assert.IsType<Dictionary<string, string[]>>(result.Value);

			var (key, messages) = Assert.Single(value);

			Assert.Equal("age", key);
			Assert.Equal("age: 12", Assert.Single(messages));
		}

		[Fact]
		public void ProviderValidationContext_ValidationFilter_Executed()
		{
			var filter = new ValidationFilter();

			var context = new ActionExecutedContext(
				new ActionContext
				{
					HttpContext = new DefaultHttpContext
					{
						RequestServices = provider
					},
					RouteData = new RouteData(),
					ActionDescriptor = new ActionDescriptor()
				},
				new List<IFilterMetadata>(),
				new object());

			var validationContext = provider.GetRequiredService<IValidationContext>();

			var validation = provider.GetRequiredService<Validation>();

			validation.Validate(validationContext, new TestModel
			{
				Age = 322,
				Phone = 8_800_555_35_35
			});

			filter.OnActionExecuted(context);

			var result = Assert.IsType<ValidationResult>(context.Result);

			var value = Assert.IsType<Dictionary<string, string[]>>(result.Value);

			var (key, messages) = Assert.Single(value);

			Assert.Equal("phone", key);
			Assert.Equal("phone: 88005553535 age: 322", Assert.Single(messages));
		}
	}
}
