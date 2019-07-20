using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	public sealed class ValidationContextException : Exception
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