namespace Phema.Validation.Sandbox
{
	public class ModelValidationComponent : ValidationComponent<Model, ModelValidation>
	{
		public ModelValidationComponent()
		{
			NameMustBeSet = Register(() => "Name must be set");
			AgeInRange = Register<int>(() => "Age {0} in range");
		}

		public ValidationMessage NameMustBeSet { get; }
		public ValidationMessage<int> AgeInRange { get; }
	}
}