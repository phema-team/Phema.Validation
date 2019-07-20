using System;

namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateExtensions
	{
		public static IValidationPredicate<TValue> IsNot<TValue>(
			this IValidationPredicate<TValue> predicate,
			Func<TValue, bool> condition)
		{
			return predicate.Is(value => !condition(value));
		}

		public static IValidationPredicate<TValue> IsNot<TValue>(
			this IValidationPredicate<TValue> predicate,
			Func<bool> condition)
		{
			return predicate.Is(value => !condition());
		}

		public static IValidationPredicate<TValue> IsNull<TValue>(
			this IValidationPredicate<TValue> predicate)
		{
			return predicate.Is(value => value == null);
		}

		public static IValidationPredicate<TValue> IsNotNull<TValue>(
			this IValidationPredicate<TValue> predicate)
		{
			return predicate.Is(value => value != null);
		}

		public static IValidationPredicate<TValue> IsEqual<TValue>(
			this IValidationPredicate<TValue> predicate,
			TValue expect)
		{
			return predicate.Is(value => value?.Equals(expect) ?? expect?.Equals(null) ?? true);
		}

		public static IValidationPredicate<TValue> IsNotEqual<TValue>(
			this IValidationPredicate<TValue> predicate,
			TValue expect)
		{
			return predicate.Is(value => !(value?.Equals(expect) ?? expect?.Equals(null) ?? true));
		}
	}
}