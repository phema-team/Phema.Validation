namespace Phema.Validation.Sandbox
{
	public class ModelValidation : Validation<Model>
	{
		protected override void When(IValidationContext validationContext, Model model)
		{
			validationContext.When(model, m => m.Name)
				.IsNullOrWhitespace()
				.AddError<ModelValidationComponent>(c => c.NameMustBeSet);
		}
	}
}