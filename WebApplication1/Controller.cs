using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation;

namespace WebApplication1
{
	public class Test
	{
		public string MyProperty { get; set; }
		public int MyProperty2 { get; set; }
	}

	[Route("test")]
	public class Controller : ControllerBase
	{
		[HttpPost("works")]
		public string Works([FromBody] Test test)
		{
			HttpContext.RequestServices.GetRequiredService<IValidationContext>()
					.When("", s => s.Length)
					.Is(() => true)
					.Throw(() => new ValidationMessage(() => "Works!"));

			return "ASDASD";
		}
	}
}
