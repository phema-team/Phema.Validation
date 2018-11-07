using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public interface IValidationConfiguration
	{
		IValidationConfiguration AddValidation<TModel, TValidation, TComponent>()
			where TValidation : Validation<TModel>
			where TComponent : ValidationComponent<TModel, TValidation>;
	}
	
	internal class ValidationConfiguration : IValidationConfiguration
	{
		private readonly IServiceCollection services;

		public ValidationConfiguration(IServiceCollection services)
		{
			this.services = services;
		}

		public IValidationConfiguration AddValidation<TModel, TValidation, TComponent>()
			where TValidation : Validation<TModel>
			where TComponent : ValidationComponent<TModel, TValidation>
		{
			services.AddSingleton<TValidation>()
				.AddSingleton<TComponent>();

			services.Configure<ValidationOptions>(o =>
				o.Validations.Add(
					typeof(TModel), 
					provider => provider.GetRequiredService<TValidation>()));

			return this;
		}
	}
}