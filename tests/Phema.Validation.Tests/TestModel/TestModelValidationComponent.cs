namespace Phema.Validation.Tests
{
	public class TestModelValidationComponent : IValidationComponent<TestModel, TestModelValidation>
	{
		public TestModelValidationComponent()
		{
			TestModelTemplate1 = new ValidationTemplate(() => "template1");
			TestModelTemplate2 = new ValidationTemplate(() => "template2");
		}

		public ValidationTemplate TestModelTemplate1 { get; }
		public ValidationTemplate TestModelTemplate2 { get; }
	}
}