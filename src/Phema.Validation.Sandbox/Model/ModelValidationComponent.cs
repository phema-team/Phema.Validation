using Phema.Validation;

namespace WebApplication1
{
	public class ModelValidationComponent : ValidationComponent<Model, ModelValidation>
	{
		public ModelValidationComponent()
		{
			NameMustBeSet = Register(() => "Name must be set");
		}

		public ValidationMessage NameMustBeSet { get; }
	}
}