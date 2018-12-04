using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation
{
	internal sealed class ValidationResult : ObjectResult
	{
		public ValidationResult(IEnumerable<IValidationError> errors)
			: base(GetSummary(errors))
		{
			StatusCode = 400;
		}

		public ValidationResult(IValidationError error)
			: base(GetSummary(error))
		{
			StatusCode = 400;
		}

		private static IDictionary<string, string[]> GetSummary(IValidationError error)
		{
			return new Dictionary<string, string[]>
			{
				[error.Key] = new[]{ error.Message }
			};
		}

		private static IDictionary<string, string[]> GetSummary(IEnumerable<IValidationError> errors)
		{
			return errors
				.GroupBy(error => error.Key)
				.ToDictionary(
					grouping => grouping.Key,
					grouping => grouping.Select(error => error.Message).ToArray());
		}
	}
}