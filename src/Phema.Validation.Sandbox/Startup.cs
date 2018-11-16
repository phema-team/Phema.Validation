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
					.AddValidation<Model, ModelValidation, ModelValidationComponent>());

			services.Configure<ValidationOptions>(
				options => options.Severity = ValidationSeverity.Error);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseMvc();
		}
	}
}
