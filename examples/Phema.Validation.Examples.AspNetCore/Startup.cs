using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation.Examples.AspNetCore
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddValidation()
				.AddMvc(options => options.EnableEndpointRouting = false);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMvc();
		}
	}
}