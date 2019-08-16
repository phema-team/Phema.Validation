using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation.Examples.AspNetCore
{
	[Route("orders")]
	public class ExampleOrdersController : Controller
	{
		private readonly IValidationContext validationContext;

		public ExampleOrdersController(IValidationContext validationContext)
		{
			this.validationContext = validationContext;
		}

		/*
			Try to remove or edit some fields and watch for validation messages
			POST
			{
				"name": "Cookies",
				"cost": 4,
				"address": {
					"city": "City",
					"street": "Street",
					"house": 12
				}
			}
		*/
		[HttpPost]
		public IActionResult CreateOrder([FromBody] ExampleOrderModel model)
		{
			model.Save( /*databaseContext, */validationContext);

			if (validationContext.IsValid())
				// databaseContext.SaveChanges();
				return Ok(new {Payload = model});

			return BadRequest(new {validationContext.ValidationDetails});
		}
	}
}