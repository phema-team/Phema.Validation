using System.Collections.Generic;

namespace Phema.Validation
{
	public class ExpressionValidationOutputFormatter : IValidationOutputFormatter
	{
		public IDictionary<string, object> FormatOutput(IEnumerable<IValidationError> errors)
		{
			var result = new Dictionary<string, object>();

			foreach (var error in errors)
			{
				FillError(result, error);
			}
		}

		public void FillError(Dictionary<string, object> result, IValidationError error)
		{
			// "addresses:1:street": "Street is invalid"
			
			/*
			 * {
			 *  	"addresses":
			 *  	{
			 *  		"1":
			 *  		{
			 *  			"street": "Street is invalid"
			 *  		}
			 *  	}
			 * }
			 */
		}
	}
}