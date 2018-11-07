using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation;

namespace WebApplication1
{
	public class VVVV : Validation<Test>
	{
		protected override void Validate(IValidationContext validationContext, Test model)
		{
			validationContext.When(model, m => m.MyProperty)
				.Add(() => new ValidationMessage(() => "Works 123123"));
		}
	}
	
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore()
				.AddJsonFormatters();

			services.AddValidation(
				config => config.AddValidation<Test, VVVV, ValidationComponent<Test, VVVV>>());
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseMvc();
		}
	}
}
