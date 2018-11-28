namespace Phema.Validation.Sandbox
{
	public class ModelValidation : IValidation<Model>
	{
		public void Validate(IValidationContext validationContext, Model model)
		{
			validationContext.When(model, m => m.Name)
				.IsNullOrWhitespace()
				.AddError<ModelValidationComponent>(c => c.NameMustBeSet);
		}
	}
}