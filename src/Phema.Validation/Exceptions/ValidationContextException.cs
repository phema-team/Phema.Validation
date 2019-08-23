using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Phema.Validation
{
	public sealed class ValidationContextException : ValidationException
	{
		public ValidationContextException(IValidationContext validationContext)
		{
			ValidationDetails = validationContext.ValidationDetails
				.Where(detail => detail.ValidationSeverity >= validationContext.ValidationSeverity)
				.ToList();
		}

		public IReadOnlyCollection<ValidationDetail> ValidationDetails { get; }
	}
}