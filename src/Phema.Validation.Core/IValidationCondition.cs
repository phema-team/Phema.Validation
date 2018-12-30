using System;

namespace Phema.Validation
{
	public interface IValidationCondition
	{
		IValidationError Add(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity);
	}

	public interface IValidationCondition<out TValue> : IValidationCondition
	{
		IValidationCondition<TValue> Is(Func<TValue, bool> condition);
	}
}