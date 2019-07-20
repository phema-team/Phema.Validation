using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateStringExtensions
	{
		public static IValidationPredicate<string> IsEmpty(
			this IValidationPredicate<string> predicate)
		{
			return predicate.Is(value => value == string.Empty);
		}

		public static IValidationPredicate<string> IsNotEmpty(
			this IValidationPredicate<string> predicate)
		{
			return predicate.Is(value => value != string.Empty);
		}

		public static IValidationPredicate<string> IsNullOrWhitespace(
			this IValidationPredicate<string> predicate)
		{
			return predicate.Is(string.IsNullOrWhiteSpace);
		}

		public static IValidationPredicate<string> IsMatch(
			this IValidationPredicate<string> predicate,
			string regex,
			RegexOptions options = RegexOptions.None)
		{
			return predicate.Is(value => Regex.IsMatch(value, regex, options));
		}

		public static IValidationPredicate<string> IsNotMatch(
			this IValidationPredicate<string> predicate,
			string regex,
			RegexOptions options = RegexOptions.None)
		{
			return predicate.Is(value => !Regex.IsMatch(value, regex, options));
		}

		public static IValidationPredicate<string> IsNotEmail(
			this IValidationPredicate<string> predicate)
		{
			return predicate.Is(value => !new EmailAddressAttribute().IsValid(value));
		}

		public static IValidationPredicate<string> HasLength(
			this IValidationPredicate<string> predicate,
			int length)
		{
			return predicate.Is(value => value != null && value.Length == length);
		}

		public static IValidationPredicate<string> HasLengthLess(
			this IValidationPredicate<string> predicate,
			int length)
		{
			return predicate.Is(value => value != null && value.Length < length);
		}

		public static IValidationPredicate<string> HasLengthGreater(
			this IValidationPredicate<string> predicate,
			int length)
		{
			return predicate.Is(value => value != null && value.Length > length);
		}
	}
}