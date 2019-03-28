using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public interface IValidationBuilder
	{
		IServiceCollection Services { get; }
	}
}

namespace Phema.Validation.Internal
{
	internal sealed class ValidationBuilder : IValidationBuilder
	{
		public ValidationBuilder(IServiceCollection services)
		{
			Services = services;
		}

		public IServiceCollection Services { get; }
	}
}