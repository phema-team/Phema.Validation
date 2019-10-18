using System;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionDateTimeExtensions
	{
		/// <summary>
		///   Checks DateTime kind
		/// </summary>
		public static ValidationCondition<DateTime> IsDateTimeKind(
			this ValidationCondition<DateTime> validationCondition,
			DateTimeKind dateTimeKind)
		{
			return validationCondition.Is(value => value.Kind == dateTimeKind);
		}

		/// <summary>
		///   Checks DateTime kind not
		/// </summary>
		public static ValidationCondition<DateTime> IsNotDateTimeKind(
			this ValidationCondition<DateTime> validationCondition,
			DateTimeKind dateTimeKind)
		{
			return validationCondition.IsNot(value => value.Kind == dateTimeKind);
		}

		/// <summary>
		///   Checks DateTime kind is utc
		/// </summary>
		public static ValidationCondition<DateTime> IsUtc(this ValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Utc);
		}

		/// <summary>
		///   Checks DateTime kind not utc
		/// </summary>
		public static ValidationCondition<DateTime> IsNotUtc(this ValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Utc);
		}

		/// <summary>
		///   Checks DateTime kind local
		/// </summary>
		public static ValidationCondition<DateTime> IsLocal(this ValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Local);
		}

		/// <summary>
		///   Checks DateTime kind not local
		/// </summary>
		public static ValidationCondition<DateTime> IsNotLocal(this ValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Local);
		}

		/// <summary>
		///   Checks DateTime kind unspecified
		/// </summary>
		public static ValidationCondition<DateTime> IsUnspecified(this ValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsDateTimeKind(DateTimeKind.Unspecified);
		}

		/// <summary>
		///   Checks DateTime kind not unspecified
		/// </summary>
		public static ValidationCondition<DateTime> IsNotUnspecified(this ValidationCondition<DateTime> validationCondition)
		{
			return validationCondition.IsNotDateTimeKind(DateTimeKind.Unspecified);
		}
	}
}