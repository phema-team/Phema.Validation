﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Phema.Validation.Tests")]

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
	
	internal sealed class ValidationCondition<TValue> : IValidationCondition<TValue>
	{
		private readonly TValue value;
		private readonly ValidationKey key;
		private readonly ICollection<IValidationError> errors;
		private readonly ICollection<Func<TValue, bool>> conditions;
		private readonly CultureInfo cultureInfo;
		
		public ValidationCondition(TValue value, ValidationKey key, ICollection<IValidationError> errors, CultureInfo cultureInfo)
		{
			this.value = value;
			this.key = key;
			this.errors = errors;
			this.cultureInfo = cultureInfo;
			conditions = new List<Func<TValue, bool>>();
		}
		
		public IValidationCondition<TValue> Is(Func<TValue, bool> condition)
		{
			this.conditions.Add(condition);
			return this;
		}

		public IValidationError Add(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity)
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
		
		private ValidationError AddError(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity)
		{
			var validationMessage = selector();

			var message = validationMessage.GetMessage(arguments, cultureInfo);
			
			var error = new ValidationError(key.Key, message, severity);
			errors.Add(error);
			return error;
		}
	}
}