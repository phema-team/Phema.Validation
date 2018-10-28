using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public class ValidationComponent
	{
		private readonly IList<Action<IServiceCollection>> actions;

		protected ValidationComponent()
		{
			actions = new List<Action<IServiceCollection>>();
		}
		
		protected void Validation<TModel, TValidation, TSection>()
			where TModel : class
			where TValidation : Validation<TModel>
			where TSection : ValidationSection
		{
			actions.Add(services => 
				services
					.AddScoped<Validation<TModel>, TValidation>()
					.AddSingleton<TSection>());
		}

		internal void Configure(IServiceCollection services)
		{
			foreach (var action in actions)
			{
				action(services);
			}
		}
	}
}