using System.Text.RegularExpressions;

namespace Phema.Validation
{
	public static class ValueValidationConditionExtensions
	{
		public static IValueValidationCondition<TValue> IsNull<TValue>(
			this IValueValidationCondition<TValue> validationCondition)
		{
			return validationCondition.Is(value => value == null);
		}

		public static IValueValidationCondition<TValue> IsNotNull<TValue>(
			this IValueValidationCondition<TValue> validationCondition)
		{
			return validationCondition.Is(value => value != null);
		}

		public static IValueValidationCondition<string> IsEmpty(
			this IValueValidationCondition<string> validationCondition)
		{
			return validationCondition.Is(value => value == string.Empty);
		}

		public static IValueValidationCondition<string> IsNotEmpty(
			this IValueValidationCondition<string> validationCondition)
		{
			return validationCondition.Is(value => value != string.Empty);
		}

		public static IValueValidationCondition<string> IsNullOrWhitespace(
			this IValueValidationCondition<string> validationCondition)
		{
			return validationCondition.Is(value => string.IsNullOrWhiteSpace(value));
		}

		public static IValueValidationCondition<string> IsNotNullOrWhitespace(
			this IValueValidationCondition<string> validationCondition)
		{
			return validationCondition.Is(value => !string.IsNullOrWhiteSpace(value));
		}

		public static IValueValidationCondition<TValue> IsEqual<TValue>(
			this IValueValidationCondition<TValue> validationCondition,
			TValue expect)
		{
			return validationCondition.Is(value => value.Equals(expect));
		}

		public static IValueValidationCondition<TValue> IsNotEqual<TValue>(
			this IValueValidationCondition<TValue> validationCondition,
			TValue expect)
		{
			return validationCondition.Is(value => !value.Equals(expect));
		}

		public static IValueValidationCondition<string> IsMatch(
			this IValueValidationCondition<string> validationCondition,
			string regex, 
			RegexOptions options = RegexOptions.None)
		{
			return validationCondition.Is(value => Regex.IsMatch(value, regex, options));
		}

		public static IValueValidationCondition<string> IsNotMatch(
			this IValueValidationCondition<string> validationCondition,
			string regex, 
			RegexOptions options = RegexOptions.None)
		{
			return validationCondition.Is(value => !Regex.IsMatch(value, regex, options));
		}
		
		public static IValueValidationCondition<int> IsInRange(
			this IValueValidationCondition<int> validationCondition,
			int min, 
			int max)
		{
			return validationCondition.Is(value => value >= min && value <= max);
		}
	}
}