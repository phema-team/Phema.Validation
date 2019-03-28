using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Internal;

namespace Phema.Validation
{
	public static class ValidationConfigurationExtensions
	{
		public static IValidationBuilder AddValidation<TModel, TValidation>(
			this IValidationBuilder configuration)
				where TValidation : class, IValidator<TModel>
		{
			var services = configuration.Services;
			
			services.AddScoped<IValidator<TModel>, TValidation>()
				.Configure<ValidatorOptions>(options =>
					options.Dispatchers.Add(typeof(TModel), (provider, validationContext, model) =>
					{
						var validators = provider.GetServices<IValidator<TModel>>();

						var typedModel = (TModel) model;
						
						foreach (var validator in validators)
						{
							validator.Validate(validationContext, typedModel);
						}
					}));
			
			return configuration;
		}
		
		public static IValidationBuilder AddValidationComponent<TModel, TValidation, TComponent>(
			this IValidationBuilder configuration)
				where TValidation : class, IValidator<TModel>
				where TComponent : class, IValidationComponent<TModel, TValidation>
		{
			return configuration.AddComponent<TModel, TComponent>()
				.AddValidation<TModel, TValidation>();
		}
	}
}