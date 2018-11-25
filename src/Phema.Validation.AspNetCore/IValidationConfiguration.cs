using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: InternalsVisibleTo("Phema.Validation.Tests")]

namespace Phema.Validation
{
	public interface IValidationConfiguration
	{
		IValidationConfiguration Add<TModel, TValidation, TComponent>()
			where TValidation : Validation<TModel>
			where TComponent : class, IValidationComponent<TModel, TValidation>;

		IValidationConfiguration AddComponent<TModel, TComponent>()
			where TComponent : class, IValidationComponent<TModel>;

		IValidationConfiguration AddValidation<TModel, TValidation>()
			where TValidation : Validation<TModel>;
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
		
		public IValidationConfiguration AddComponent<TModel, TComponent>()
			where TComponent : class, IValidationComponent<TModel>
		{
			services.TryAddSingleton<TComponent>();
			return this;
		}

		public IValidationConfiguration Add<TModel, TValidation, TComponent>()
			where TValidation : Validation<TModel>
			where TComponent : class, IValidationComponent<TModel, TValidation>
		{
			return AddComponent<TModel, TComponent>()
				.AddValidation<TModel, TValidation>();
		}
	}
}