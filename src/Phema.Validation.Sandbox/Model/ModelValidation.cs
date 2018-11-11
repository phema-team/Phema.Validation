using Phema.Validation;

namespace WebApplication1
{
	public class ModelValidation : Validation<Model>
	{
		private readonly ModelValidationComponent component;

		public ModelValidation(ModelValidationComponent component)
		{
			this.component = component;
		}
		
		protected override void Validate(IValidationContext validationContext, Model model)
		{
			validationContext.When(model, m => m.Name)
				.IsNullOrWhitespace()
				.Add(() => component.NameMustBeSet);
		}
	}
}