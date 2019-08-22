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
				.Where(m => m.ValidationSeverity >= validationContext.ValidationSeverity)
				.ToList();
		}

		public IReadOnlyCollection<IValidationDetail> ValidationDetails { get; }
	}
}