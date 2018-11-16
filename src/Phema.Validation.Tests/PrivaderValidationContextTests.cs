using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xunit;

namespace Phema.Validation.Tests
{
	public class PrivaderValidationContextTests
	{
		public class Model
		{
			[DataMember(Name = "name")]
			public string Name { get; set; }

			[DataMember(Name = "age")]
			public int Age { get; set; }

			[DataMember(Name = "phone")]
			public long Phone { get; set; }
		}

		public class Validation : Validation<Model>
		{
			protected override void Validate(IValidationContext validationContext, Model model)
			{
				validationContext.Validate(model, m => m.Name)
					.WhenEqual("Invalid")
					.AddError<ValidationComponent>(c => c.NameIsInvalid);

				validationContext.Validate(model, m => m.Age)
					.WhenEqual(12)
					.AddError<ValidationComponent, int>(c => c.AgeIsInvalid, model.Age);

				validationContext.Validate(model, m => m.Phone)
					.WhenEqual(8_800_555_35_35)
					.AddError<ValidationComponent, long, int>(c => c.PhoneIsInvalid, model.Phone, model.Age);
			}
		}

		public class ValidationComponent : ValidationComponent<Model, Validation>
		{
			public ValidationComponent()
			{
				NameIsInvalid = Register(() => "message");
				AgeIsInvalid = Register<int>(() => "age: {0}");
				PhoneIsInvalid = Register<long, int>(() => "phone: {0} age: {1}");
			}

			public ValidationMessage NameIsInvalid { get; }
			public ValidationMessage<int> AgeIsInvalid { get; }
			public ValidationMessage<long, int> PhoneIsInvalid { get; }
		}

		[Fact]
		public void ProviderValidationContext()
		{
			var services = new ServiceCollection();

			services.AddValidation(v => v.AddValidation<Model, Validation, ValidationComponent>());

			var provider = services.BuildServiceProvider();

			var validationContext = provider.GetRequiredService<IValidationContext>();

			var validation = provider.GetRequiredService<Validation>();

			validation.ValidateCore(validationContext, new Model { Name = "Invalid" });

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("name", error.Key);
			Assert.Equal("message", error.Message);
		}

		[Fact]
		public void ProviderValidationContext_ValidationFilter()
		{
			var services = new ServiceCollection();

			services.AddValidation(v => v.AddValidation<Model, Validation, ValidationComponent>());

			var provider = services.BuildServiceProvider();

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
					["test"] = new Model
					{
						Age = 12
					}
				},
				new object());

			filter.OnActionExecuting(context);

			var result = Assert.IsType<ValidationResult>(context.Result);

			var value = Assert.IsType<Dictionary<string, string[]>>(result.Value);

			var element = Assert.Single(value);

			Assert.Equal("age", element.Key);
			Assert.Equal("age: 12", Assert.Single(element.Value));
		}

		[Fact]
		public void ProviderValidationContext_ValidationFilter_Executed()
		{
			var services = new ServiceCollection();

			services.AddValidation(v => v.AddValidation<Model, Validation, ValidationComponent>());

			var provider = services.BuildServiceProvider();

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

			validation.ValidateCore(validationContext, new Model
			{
				Age = 322,
				Phone = 8_800_555_35_35
			});

			filter.OnActionExecuted(context);

			var result = Assert.IsType<ValidationResult>(context.Result);

			var value = Assert.IsType<Dictionary<string, string[]>>(result.Value);

			var element = Assert.Single(value);

			Assert.Equal("phone", element.Key);
			Assert.Equal("phone: 88005553535 age: 322", Assert.Single(element.Value));
		}
	}
}
