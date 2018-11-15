using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: InternalsVisibleTo("Phema.Validation.Tests")]

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
		
		public IValidationConfiguration AddValidation<TModel, TValidation>()
			where TValidation : Validation<TModel>
		{
			services.TryAddScoped<TValidation>();

			services.Configure<ValidationOptions>(options =>
			{
				options.Validations.TryAdd(
					typeof(TModel),
					provider => provider.GetRequiredService<TValidation>());
			});

			return this;
		}

		public IValidationConfiguration AddValidation<TModel, TValidation, TComponent>()
			where TValidation : Validation<TModel>
			where TComponent : ValidationComponent<TModel, TValidation>
		{
			services.TryAddSingleton<TComponent>();
			return AddValidation<TModel, TValidation>();
		}
	}
}