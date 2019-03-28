using System;

namespace Phema.Validation.Tests
{
	public class TestModelValidator : IValidator<TestModel>
	{
		public void Validate(IValidationContext validationContext, TestModel model)
		{
			throw new NotImplementedException();
		}
	}
}