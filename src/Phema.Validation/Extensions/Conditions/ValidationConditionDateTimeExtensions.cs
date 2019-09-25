using System;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionDateTimeExtensions
	{
		/// <summary>
		///   Checks DateTime kind
		/// </summary>
		public static IValidationCondition<DateTime> IsDateTimeKind(
			this IValidationCondition<DateTime> validationCondition,
			DateTimeKind dateTimeKind)
		{
			return validationCondition.Is(value => value.Kind == dateTimeKind);
		}

		/// <summary>
		///   Checks DateTime kind not
		/// </summary>
		public static IValidationCondition<DateTime> IsNotDateTimeKind(
			this IValidationCondition<DateTime> validationCondition,
			DateTimeKind dateTimeKind)
		{
			return validationCondition.IsNot(value => value.Kind == dateTimeKind);
		}

		/// <summary>
		///   Checks DateTime kind is utc
		/// </summary>
		public static IValidationCondition<DateTime> IsUtc(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Utc);
		}

		/// <summary>
		///   Checks DateTime kind not utc
		/// </summary>
		public static IValidationCondition<DateTime> IsNotUtc(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Utc);
		}

		/// <summary>
		///   Checks DateTime kind local
		/// </summary>
		public static IValidationCondition<DateTime> IsLocal(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Local);
		}

		/// <summary>
		///   Checks DateTime kind not local
		/// </summary>
		public static IValidationCondition<DateTime> IsNotLocal(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Local);
		}

		/// <summary>
		///   Checks DateTime kind unspecified
		/// </summary>
		public static IValidationCondition<DateTime> IsUnspecified(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Unspecified);
		}

		/// <summary>
		///   Checks DateTime kind not unspecified
		/// </summary>
		public static IValidationCondition<DateTime> IsNotUnspecified(this IValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Unspecified);
		}
	}
}