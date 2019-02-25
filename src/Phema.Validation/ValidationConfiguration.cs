using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationConfiguration"/>
	internal sealed class ValidationConfiguration : IValidationConfiguration
	{
		public ValidationConfiguration(IServiceCollection services)
		{
			Services = services;
		}

		public IServiceCollection Services { get; }
	}
}