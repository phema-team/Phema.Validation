using System;

namespace Phema.Validation
{
	public static class ValidationConditionSeverityExtensions
	{
		public static IValidationError Add(
			this IValidationCondition condition,
			Func<ValidationMessage> selector,
			ValidationSeverity severity)
		{
			return condition.Add(selector, Array.Empty<object>(), severity);
		}
		
		public static IValidationError AddError(
			this IValidationCondition condition,
			Func<ValidationMessage> selector,
			object[] arguments = null)
		{
			arguments = arguments ?? Array.Empty<object>();
			return condition.Add(selector, arguments, ValidationSeverity.Error);
		}

		public static IValidationError AddWarning(
			this IValidationCondition condition,
			Func<ValidationMessage> selector,
			object[] arguments = null)
		{
			arguments = arguments ?? Array.Empty<object>();
			return condition.Add(selector, arguments, ValidationSeverity.Warning);
		}

		public static IValidationError AddInformation(
			this IValidationCondition condition,
			Func<ValidationMessage> selector,
			object[] arguments = null)
		{
			arguments = arguments ?? Array.Empty<object>();
			return condition.Add(selector, arguments, ValidationSeverity.Information);
		}

		public static IValidationError AddDebug(
			this IValidationCondition condition,
			Func<ValidationMessage> selector,
			object[] arguments = null)
		{
			arguments = arguments ?? Array.Empty<object>();
			return condition.Add(selector, arguments, ValidationSeverity.Debug);
		}

		public static IValidationError AddTrace(
			this IValidationCondition condition,
			Func<ValidationMessage> selector,
			object[] arguments = null)
		{
			arguments = arguments ?? Array.Empty<object>();
			return condition.Add(selector, arguments, ValidationSeverity.Trace);
		}

	}
}