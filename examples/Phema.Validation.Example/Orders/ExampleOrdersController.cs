using Microsoft.AspNetCore.Mvc;
using Phema.Validation.Conditions;

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
			validationContext.When(model, m => m.Name)
				.IsNullOrWhitespace()
				.AddError("Order name must be set");

			validationContext.When(model, m => m.Cost)
				.IsLessOrEqual(0)
				.IsGreaterOrEqual(10)
				.AddError("Cost must be in [1, 9] range");

			validationContext.When(model, m => m.Address)
				.IsNull()
				.AddError("You should add your address");

			if (validationContext.IsValid(model, m => m.Address))
			{
				var addressValidationContext = validationContext.CreateFor(model, m => m.Address);

				addressValidationContext.When(model.Address, a => a.City)
					.IsNullOrWhitespace()
					.AddError("City must be set");

				addressValidationContext.When(model.Address, a => a.Street)
					.IsNullOrWhitespace()
					.AddError("Street must be set");

				addressValidationContext.When(model.Address, a => a.House)
					.IsNull()
					.AddError("House must be set");
			}

			if (validationContext.IsValid())
			{
				return Ok(new { Payload = model });
			}

			return BadRequest(new { validationContext.ValidationDetails });
		}
	}
}