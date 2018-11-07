using Phema.Validation;

namespace WebApplication1
{
	public class ModelValidationComponent : ValidationComponent<Model, ModelValidation>
	{
		public ModelValidationComponent()
		{
			MyPropertyMustBeSet = Register(() => "My property must be set");
		}

		public ValidationMessage MyPropertyMustBeSet { get; }
	}
}