using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	public interface IValidationConfiguration
	{
		IValidationConfiguration AddComponent<TModel, TComponent>()
			where TComponent : class, IValidationComponent<TModel>;

		IValidationConfiguration AddValidation<TModel, TValidation>()
			where TValidation : class, IValidation<TModel>;
	}

	internal sealed class ValidationConfiguration : IValidationConfiguration
	{
		private readonly IServiceCollection services;

		public ValidationConfiguration(IServiceCollection services)
		{
			this.services = services;
		}

		public IValidationConfiguration AddValidation<TModel, TValidation>()
			where TValidation : class, IValidation<TModel>
		{
			services.TryAddScoped<TValidation>();

			services.Configure<ValidationComponentOptions>(options =>
			{
				if (!options.ValidationDispatchers.TryGetValue(typeof(TModel), out var dispatchers))
				{
					options.ValidationDispatchers.Add(typeof(TModel), dispatchers = new List<Action<IServiceProvider, object>>());
				}
				
				dispatchers.Add((provider, model) =>
				{
					var validation = provider.GetRequiredService<TValidation>();
					var validationContext = provider.GetRequiredService<IValidationContext>();
					validation.Validate(validationContext, (TModel)model);
				});
			});

			return this;
		}

		public IValidationConfiguration AddComponent<TModel, TComponent>()
			where TComponent : class, IValidationComponent<TModel>
		{
			services.TryAddSingleton<TComponent>();
			return this;
		}
	}
}