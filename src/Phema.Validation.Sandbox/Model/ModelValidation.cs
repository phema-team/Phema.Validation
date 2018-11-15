namespace Phema.Validation.Sandbox
{
	public class ModelValidation : Validation<Model>
	{
		protected override void Validate(IValidationContext validationContext, Model model)
		{
			validationContext.Validate(model, m => m.Name)
				.WhenNullOrWhitespace()
				.Add<ModelValidationComponent>(c => c.NameMustBeSet);
		}
	}
}