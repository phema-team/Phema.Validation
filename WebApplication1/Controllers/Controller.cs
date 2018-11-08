using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phema.Validation;

namespace WebApplication1
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
			validationContext.When(model, s => s.Age)
				.IsInRange(10, 12)
				.Add<int>(() => new ValidationMessage<int>(() => "{0} in [10, 12] range"), model.Age);

			return model;
		}
	}
}
