using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationCondition
	{
		IValidationCondition Is(Condition condition);
		IValidationError Add(Selector selector, params object[] arguments);
	}

	internal class ValidationCondition : IValidationCondition
	{
		private readonly string key;
		private readonly List<ValidationError> errors;
		private readonly List<Condition> conditions;

		public ValidationCondition(string key, List<ValidationError> errors)
		{
			this.key = key;
			this.errors = errors;
			conditions = new List<Condition>();
		}

		public IValidationCondition Is(Condition condition)
		{
			conditions.Add(condition);
			return this;
		}

		public IValidationError Add(Selector selector, params object[] arguments)
		{
			if (conditions.Count == 0)
			{
				return AddError(selector, arguments);
			}

			foreach (var condition in conditions)
			{
				if (condition())
				{
					return AddError(selector, arguments);
				}
			}

			return null;
		}

		private ValidationError AddError(Selector selector, params object[] arguments)
		{
			var error = new ValidationError
			{
				Key = key, 
				Message = selector().GetMessage(arguments)
			};

			errors.Add(error);

			return error;
		}
	}
}