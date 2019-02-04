using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigurePhemaValidationExpressions(
			this IServiceCollection services,
			Action<ExpressionValidationOptions> options = null)
		{
			return services.Configure(options ?? (o => {}));
		}
	}
}