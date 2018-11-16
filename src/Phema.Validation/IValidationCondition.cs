using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationCondition
	{
		IValidationError Add(Selector selector, object[] arguments, ValidationSeverity severity);
	}
	
	public interface IValidationCondition<out TValue> : IValidationCondition
	{
		IValidationCondition<TValue> Is(Condition<TValue> condition);
	}
	
	internal class ValidationCondition<TValue> : IValidationCondition<TValue>
	{
		private readonly TValue value;
		private readonly ValidationKey key;
		private readonly ICollection<IValidationError> errors;
		private readonly ICollection<Condition<TValue>> conditions;

		public ValidationCondition(TValue value, ValidationKey key, ICollection<IValidationError> errors)
		{
			this.value = value;
			this.key = key;
			this.errors = errors;
			conditions = new List<Condition<TValue>>();
		}
		
		public IValidationCondition<TValue> Is(Condition<TValue> condition)
		{
			conditions.Add(condition);
			return this;
		}

		public IValidationError Add(Selector selector, object[] arguments, ValidationSeverity severity)
		{
			if (conditions.Count == 0)
			{
				return AddError(selector, arguments, severity);
			}

			foreach (var condition in conditions)
			{
				if (condition(value))
				{
					return AddError(selector, arguments, severity);
				}
			}

			return null;
		}
		
		private ValidationError AddError(Selector selector, object[] arguments, ValidationSeverity severity)
		{
			var error = new ValidationError(key.Key, selector().GetMessage(arguments), severity);
			errors.Add(error);
			return error;
		}
	}
}