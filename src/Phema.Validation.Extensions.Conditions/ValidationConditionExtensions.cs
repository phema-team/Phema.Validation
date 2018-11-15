using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition<TValue> WhenNot<TValue>(
			this IValidationCondition<TValue> builder,
			Condition<TValue> condition)
		{
			return builder.When(value => !condition(value));
		}
		
		public static IValidationCondition<TValue> WhenNull<TValue>(
			this IValidationCondition<TValue> builder)
		{
			return builder.When(value => value == null);
		}

		public static IValidationCondition<TValue> WhenNotNull<TValue>(
			this IValidationCondition<TValue> builder)
		{
			return builder.When(value => value != null);
		}

		public static IValidationCondition<string> WhenEmpty(
			this IValidationCondition<string> builder)
		{
			return builder.When(value => value == string.Empty);
		}

		public static IValidationCondition<string> WhenNotEmpty(
			this IValidationCondition<string> builder)
		{
			return builder.When(value => value != string.Empty);
		}

		public static IValidationCondition<string> WhenNullOrWhitespace(
			this IValidationCondition<string> builder)
		{
			return builder.When(value => string.IsNullOrWhiteSpace(value));
		}

		public static IValidationCondition<string> WhenNotNullOrWhitespace(
			this IValidationCondition<string> builder)
		{
			return builder.When(value => !string.IsNullOrWhiteSpace(value));
		}

		public static IValidationCondition<TValue> WhenEqual<TValue>(
			this IValidationCondition<TValue> builder,
			TValue expect)
		{
			return builder.When(value => value?.Equals(expect) ?? expect?.Equals(value) ?? true);
		}

		public static IValidationCondition<TValue> WhenNotEqual<TValue>(
			this IValidationCondition<TValue> builder,
			TValue expect)
		{
			return builder.When(value => !(value?.Equals(expect) ?? expect?.Equals(value) ?? true));
		}

		public static IValidationCondition<string> WhenMatch(
			this IValidationCondition<string> builder,
			string regex, 
			RegexOptions options = RegexOptions.None)
		{
			return builder.When(value => Regex.IsMatch(value, regex, options));
		}

		public static IValidationCondition<string> WhenNotMatch(
			this IValidationCondition<string> builder,
			string regex, 
			RegexOptions options = RegexOptions.None)
		{
			return builder.When(value => !Regex.IsMatch(value, regex, options));
		}
		
		public static IValidationCondition<int> WhenInRange(
			this IValidationCondition<int> builder,
			int min, 
			int max)
		{
			return builder.When(value => value >= min && value <= max);
		}
		
		public static IValidationCondition<ICollection<TElement>> WhenEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> builder)
		{
			return builder.When(value => value.Count == 0);
		}
		
		public static IValidationCondition<ICollection<TElement>> WhenNotEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> builder)
		{
			return builder.When(value => value.Count != 0);
		}
	}
}