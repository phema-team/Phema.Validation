namespace Phema.Validation.Sandbox
{
	public class ModelValidation : Validation<Model>
	{
		protected override void Validate(IValidationContext validationContext, Model model)
		{
			validationContext.Validate(model, m => m.Name)
				.WhenNullOrWhitespace()
				.AddError<ModelValidationComponent>(c => c.NameMustBeSet);
		}
	}
}