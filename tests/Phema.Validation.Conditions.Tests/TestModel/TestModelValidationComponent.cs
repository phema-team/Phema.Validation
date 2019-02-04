namespace Phema.Validation.Tests
{
	public class TestModelValidationComponent : IValidationComponent
	{
		public TestModelValidationComponent()
		{
			TestModelTemplate1 = new ValidationTemplate(() => "template1");
		}
		
		public ValidationTemplate TestModelTemplate1 { get; }
	}
}