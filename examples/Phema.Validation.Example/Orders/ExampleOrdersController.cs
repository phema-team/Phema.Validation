using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation.Example
{
	[Route("orders")]
	public class ExampleOrdersController : Controller
	{
		private readonly IValidationContext validationContext;

		public ExampleOrdersController(IValidationContext validationContext)
		{
			this.validationContext = validationContext;
		}

		[HttpPost]
		public IActionResult CreateOrder([FromBody] ExampleOrderModel model)
		{
			model.Save(/*databaseContext, */validationContext);

			if (validationContext.IsValid())
			{
				// databaseContext.SaveChanges();
				return Ok(new { Payload = model });
			}

			return BadRequest(new { validationContext.ValidationDetails });
		}
	}
}