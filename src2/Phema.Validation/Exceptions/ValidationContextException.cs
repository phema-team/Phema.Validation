using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	public sealed class ValidationContextException : Exception
	{
		public ValidationContextException(IValidationContext validationContext)
		{
			ValidationMessages = validationContext.ValidationMessages
				.Where(m => m.Severity >= validationContext.ValidationSeverity)
				.ToList();
		}

		public IReadOnlyCollection<IValidationMessage> ValidationMessages { get; }
	}
}