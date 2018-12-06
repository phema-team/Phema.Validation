using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation
{
	internal sealed class ValidationResult : ObjectResult
	{
		public ValidationResult(IValidationError error)
			: this(new[] { error })
		{
		}
		
		public ValidationResult(IEnumerable<IValidationError> errors)
			: base(GetSummary(errors))
		{
			StatusCode = StatusCodes.Status400BadRequest;
		}

		private static IDictionary<string, object> GetSummary(IEnumerable<IValidationError> errors)
		{
			return errors
				.GroupBy(error => error.Key)
				.ToDictionary(
					grouping => grouping.Key,
					grouping =>
					{
						var messages = grouping.Select(error => error.Message).ToArray();
						
						return messages.Length == 1 
							? (object) messages[0] 
							: (object) messages;
					});
		}
	}
}