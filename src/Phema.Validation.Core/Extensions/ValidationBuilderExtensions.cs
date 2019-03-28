using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	public static class ValidationBuilderExtensions
	{
		public static IValidationBuilder AddComponent<TValidationComponent>(this IValidationBuilder builder)
			where TValidationComponent : class, IValidationComponent
		{
			builder.Services.TryAddSingleton<TValidationComponent>();
			return builder;
		}

		public static IValidationBuilder AddComponent<TModel, TValidationComponent>(this IValidationBuilder builder)
			where TValidationComponent : class, IValidationComponent<TModel>
		{
			return builder.AddComponent<TValidationComponent>();
		}
	}
}