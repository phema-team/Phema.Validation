using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationOutputFormatter
	{
		IDictionary<string, object> FormatOutput(IEnumerable<IValidationError> errors);
	}
}