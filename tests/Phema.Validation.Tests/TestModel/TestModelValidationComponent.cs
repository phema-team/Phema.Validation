namespace Phema.Validation.Tests
{
	public class TestModelValidationComponent : IValidationComponent<TestModel, TestModelValidation>
	{
		public TestModelValidationComponent()
		{
			TestModelTemplate1 = new ValidationTemplate(() => "template1");
			TestModelTemplate2 = new ValidationTemplate(() => "template2");
			TestModelTemplate3 = new ValidationTemplate<int>(a => $"template: {a}");
			TestModelTemplate4 = new ValidationTemplate<int, int>((a, b) => $"template: {a}, {b}");
			TestModelTemplate5 = new ValidationTemplate<int, int, int>((a, b, c) => $"template: {a}, {b}, {c}");
		}

		public ValidationTemplate TestModelTemplate1 { get; }
		public ValidationTemplate TestModelTemplate2 { get; }
		public ValidationTemplate<int> TestModelTemplate3 { get; }
		public ValidationTemplate<int, int> TestModelTemplate4 { get; }
		public ValidationTemplate<int, int, int> TestModelTemplate5 { get; }
	}
}