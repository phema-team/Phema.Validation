using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		ValidationSeverity Severity { get; }
		IReadOnlyCollection<IValidationError> Errors { get; }
		IValidationCondition<TValue> When<TValue>(IValidationKey key, TValue value);
	}
}