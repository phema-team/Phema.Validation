namespace Phema.Validation.Sandbox
{
	public class ModelValidation : Validation<Model>
	{
		protected override void Validate(IValidationContext validationContext, Model model)
		{
			validationContext.When(model, m => m.Name)
				.IsNullOrWhitespace()
				.AddError<ModelValidationComponent>(c => c.NameMustBeSet);
		}
	}
}