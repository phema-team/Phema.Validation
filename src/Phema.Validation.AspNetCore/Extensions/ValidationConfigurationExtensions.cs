namespace Phema.Validation
{
	public static class ValidationConfigurationExtensions
	{
		public static IValidationConfiguration Add<TModel, TValidation, TComponent>(this IValidationConfiguration configuration)
			where TValidation : class, IValidation<TModel>
			where TComponent : class, IValidationComponent<TModel, TValidation>
		{
			return configuration.AddComponent<TModel, TComponent>()
				.AddValidation<TModel, TValidation>();
		}
	}
}