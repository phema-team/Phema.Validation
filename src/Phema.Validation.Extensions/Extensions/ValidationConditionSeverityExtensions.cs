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
			return condition.Add(selector, null, severity);
		}
		
		public static IValidationError AddError(
			this IValidationCondition condition,
			Func<ValidationMessage> selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Error);
		}

		public static IValidationError AddWarning(
			this IValidationCondition condition,
			Func<ValidationMessage> selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Warning);
		}

		public static IValidationError AddInformation(
			this IValidationCondition condition,
			Func<ValidationMessage> selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Information);
		}

		public static IValidationError AddDebug(
			this IValidationCondition condition,
			Func<ValidationMessage> selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Debug);
		}

		public static IValidationError AddTrace(
			this IValidationCondition condition,
			Func<ValidationMessage> selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Trace);
		}
		
		public static IValidationError AddError<TArgument>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			return condition.Add(() => selector(), new object[] {argument}, ValidationSeverity.Error);
		}

		public static IValidationError AddWarning<TArgument>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			return condition.Add(() => selector(), new object[] {argument}, ValidationSeverity.Warning);
		}

		public static IValidationError AddInformation<TArgument>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			return condition.Add(() => selector(), new object[] {argument}, ValidationSeverity.Information);
		}

		public static IValidationError AddDebug<TArgument>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			return condition.Add(() => selector(), new object[] {argument}, ValidationSeverity.Debug);
		}

		public static IValidationError AddTrace<TArgument>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			return condition.Add(() => selector(), new object[] {argument}, ValidationSeverity.Trace);
		}

		public static IValidationError AddError<TArgument1, TArgument2>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2}, ValidationSeverity.Error);
		}

		public static IValidationError AddWarning<TArgument1, TArgument2>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2}, ValidationSeverity.Warning);
		}

		public static IValidationError AddInformation<TArgument1, TArgument2>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2}, ValidationSeverity.Information);
		}

		public static IValidationError AddDebug<TArgument1, TArgument2>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2}, ValidationSeverity.Debug);
		}

		public static IValidationError AddTrace<TArgument1, TArgument2>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2}, ValidationSeverity.Trace);
		}
		
		public static IValidationError AddError<TArgument1, TArgument2, TArgument3>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2, TArgument3>> selector,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2, argument3}, ValidationSeverity.Error);
		}

		public static IValidationError AddWarning<TArgument1, TArgument2, TArgument3>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2, TArgument3>> selector,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2, argument3}, ValidationSeverity.Warning);
		}

		public static IValidationError AddInformation<TArgument1, TArgument2, TArgument3>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2, TArgument3>> selector,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2, argument3}, ValidationSeverity.Information);
		}

		public static IValidationError AddDebug<TArgument1, TArgument2, TArgument3>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2, TArgument3>> selector,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2, argument3}, ValidationSeverity.Debug);
		}

		public static IValidationError AddTrace<TArgument1, TArgument2, TArgument3>(
			this IValidationCondition condition,
			Func<ValidationMessage<TArgument1, TArgument2, TArgument3>> selector,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3)
		{
			return condition.Add(() => selector(), new object[] {argument1, argument2, argument3}, ValidationSeverity.Trace);
		}
	}
}