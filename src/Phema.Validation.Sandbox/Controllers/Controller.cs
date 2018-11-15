using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation.Sandbox
{
	[Route("test")]
	public class Controller : ControllerBase
	{
		private readonly IValidationContext validationContext;

		public Controller(IValidationContext validationContext)
		{
			this.validationContext = validationContext;
		}
		
		[HttpPost("works")]
		public Model Works([FromBody] Model model)
		{
			validationContext.Validate(model, s => s.Age)
				.WhenInRange(10, 12)
				.Add<ModelValidationComponent, int>(c => c.AgeInRange, model.Age);

			return model;
		}
	}
}
