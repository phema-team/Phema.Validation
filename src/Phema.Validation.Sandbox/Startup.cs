using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation.Sandbox
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore()
				.AddJsonFormatters();

			services.AddValidation(
				validation => validation
					.Add<Model, ModelValidation, ModelValidationComponent>()
					.AddComponent<Model, ModelValidationComponent>()
					.AddValidation<Model, ModelValidation>());

			services.Configure<ValidationOptions>(
				options => options.Severity = ValidationSeverity.Error);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseMvc();
		}
	}
}
