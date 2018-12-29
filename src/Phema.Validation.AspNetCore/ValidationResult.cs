using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation
{
	internal sealed class ValidationResult : ObjectResult
	{
		public ValidationResult(IValidationOutputFormatter formatter, IEnumerable<IValidationError> errors) 
			: base(formatter.FormatOutput(errors))
		{
			StatusCode = StatusCodes.Status400BadRequest;
		}
	}
}