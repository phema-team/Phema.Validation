using System;

namespace Phema.Validation
{
	public interface IValidationCondition
	{
		IValidationError Add(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity);
	}
	
	public interface IValidationCondition<out TValue> : IValidationCondition
	{
		IValidationCondition<TValue> Condition(Func<TValue, bool, bool> condition);
	}
}