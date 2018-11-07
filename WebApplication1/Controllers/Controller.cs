using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation;

namespace WebApplication1
{
	[Route("test")]
	public class Controller : ControllerBase
	{
		[HttpPost("works")]
		public string Works([FromBody] Model model)
		{
			HttpContext.RequestServices.GetRequiredService<IValidationContext>()
					.When("", s => s.Length)
					.Is(() => true)
					.Throw(() => new ValidationMessage(() => "Works!"));

			return "ASDASD";
		}
	}
}
