using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation
{
	internal sealed class ValidationResult : ObjectResult
	{
		public ValidationResult(IDictionary<string, object> result) : base(result)
		{
			StatusCode = StatusCodes.Status400BadRequest;
		}
	}
}