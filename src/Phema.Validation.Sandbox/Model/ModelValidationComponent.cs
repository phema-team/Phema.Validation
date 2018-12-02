﻿namespace Phema.Validation.Sandbox
{
	public class ModelValidationComponent : IValidationComponent<Model, ModelValidation>
	{
		public ModelValidationComponent()
		{
			NameMustBeSet = new ValidationMessage(() => "Name must be set");
			AgeInRange = new ValidationMessage<int>(age => $"Age {age} in range");
		}

		public ValidationMessage NameMustBeSet { get; }
		public ValidationMessage<int> AgeInRange { get; }
	}
}