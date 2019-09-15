using System;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionDateTimeExtensions
	{
		public static IValidationCondition<DateTime> IsDateTimeKind(
			this IValidationCondition<DateTime> validationCondition,
			DateTimeKind dateTimeKind)
		{
			return validationCondition.Is(value => value.Kind == dateTimeKind);
		}

		public static IValidationCondition<DateTime> IsNotDateTimeKind(
			this IValidationCondition<DateTime> validationCondition,
			DateTimeKind dateTimeKind)
		{
			return validationCondition.IsNot(value => value.Kind == dateTimeKind);
		}

		public static IValidationCondition<DateTime> IsUtc(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Utc);
		}

		public static IValidationCondition<DateTime> IsNotUtc(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Utc);
		}

		public static IValidationCondition<DateTime> IsLocal(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Local);
		}

		public static IValidationCondition<DateTime> IsNotLocal(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Local);
		}

		public static IValidationCondition<DateTime> IsUnspecified(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Unspecified);
		}

		public static IValidationCondition<DateTime> IsNotUnspecified(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Unspecified);
		}
	}
}