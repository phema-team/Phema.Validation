using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation.Sandbox
{
	[Route("key")]
	public class Controller : ControllerBase
	{
		private readonly IValidationContext validationContext;

		public Controller(IValidationContext validationContext)
		{
			this.validationContext = validationContext;
		}
		
		[HttpPost("template")]
		public Model Works([FromBody] Model model)
		{
			validationContext.When(model, s => s.Age)
				.IsInRange(10, 12)
				.AddError<ModelValidationComponent, int>(c => c.AgeInRange, model.Age);

			return model;
		}
	}
}
