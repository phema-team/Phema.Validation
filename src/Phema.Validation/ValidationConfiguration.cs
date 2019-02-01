using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationConfiguration"/>
	internal sealed class ValidationConfiguration : IValidationConfiguration
	{
		private readonly IServiceCollection services;

		public ValidationConfiguration(IServiceCollection services)
		{
			this.services = services;
		}
		
		/// <inheritdoc cref="IValidationConfiguration.AddComponent{TValidationComponent}"/>
		public IValidationConfiguration AddComponent<TValidationComponent>()
			where TValidationComponent : class, IValidationComponent
		{
			services.TryAddSingleton<TValidationComponent>();
			return this;
		}

		/// <inheritdoc cref="IValidationConfiguration.AddValidation{TModel,TValidation}"/>
		public IValidationConfiguration AddValidation<TModel, TValidation>()
			where TValidation : class, IValidation<TModel>
		{
			services.AddScoped<IValidation<TModel>, TValidation>();
			return this;
		}
	}
}