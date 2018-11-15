using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationCondition
	{
		IValidationError Add(Selector selector, object[] arguments = null);
	}
	
	public interface IValidationCondition<out TValue> : IValidationCondition
	{
		IValidationCondition<TValue> When(Condition<TValue> condition);
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
		
		public IValidationCondition<TValue> When(Condition<TValue> condition)
		{
			conditions.Add(condition);
			return this;
		}

		public IValidationError Add(Selector selector, object[] arguments = null)
		{
			if (conditions.Count == 0)
			{
				return AddError(selector, arguments);
			}

			foreach (var condition in conditions)
			{
				if (condition(value))
				{
					return AddError(selector, arguments);
				}
			}

			return null;
		}
		
		private ValidationError AddError(Selector selector, object[] arguments)
		{
			var error = new ValidationError(key.Key, selector().GetMessage(arguments));
			errors.Add(error);
			return error;
		}
	}
}