using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateStringExtensions
	{
		public static IValidationCondition<string> IsEmpty(
			this IValidationCondition<string> condition)
		{
			return condition.Is(value => value == string.Empty);
		}

		public static IValidationCondition<string> IsNotEmpty(
			this IValidationCondition<string> condition)
		{
			return condition.Is(value => value != string.Empty);
		}

		public static IValidationCondition<string> IsNullOrWhitespace(
			this IValidationCondition<string> condition)
		{
			return condition.Is(string.IsNullOrWhiteSpace);
		}

		public static IValidationCondition<string> IsMatch(
			this IValidationCondition<string> condition,
			string regex,
			RegexOptions options = RegexOptions.None)
		{
			return condition.Is(value => Regex.IsMatch(value, regex, options));
		}

		public static IValidationCondition<string> IsNotMatch(
			this IValidationCondition<string> condition,
			string regex,
			RegexOptions options = RegexOptions.None)
		{
			return condition.Is(value => !Regex.IsMatch(value, regex, options));
		}

		public static IValidationCondition<string> IsNotEmail(
			this IValidationCondition<string> condition)
		{
			return condition.Is(value => !new EmailAddressAttribute().IsValid(value));
		}

		public static IValidationCondition<string> HasLength(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.Is(value => value != null && value.Length == length);
		}

		public static IValidationCondition<string> HasLengthLess(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.Is(value => value != null && value.Length < length);
		}

		public static IValidationCondition<string> HasLengthGreater(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.Is(value => value != null && value.Length > length);
		}
	}
}