using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	public class KeyValidationOutputFormatter : IValidationOutputFormatter
	{
		public IDictionary<string, object> FormatOutput(IEnumerable<IValidationError> errors)
		{
			return errors
				.GroupBy(error => error.Key)
				.ToDictionary(
					grouping => grouping.Key,
					grouping =>
						grouping.Select(error => error.Message).ToArray() is var messages
							? messages.Length == 1
								? (object)messages[0]
								: messages
							: throw new InvalidOperationException());
		}
	}
}