using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionStringExtensions
	{
		private static readonly EmailAddressAttribute EmailAddress = new EmailAddressAttribute();

		public static IValidationCondition<string> IsEmpty(
			this IValidationCondition<string> condition,
			StringComparison stringComparison = StringComparison.InvariantCulture)
		{
			return condition.Is(value => value.Equals(string.Empty, stringComparison));
		}

		public static IValidationCondition<string> IsNotEmpty(
			this IValidationCondition<string> condition,
			StringComparison stringComparison = StringComparison.InvariantCulture)
		{
			return condition.IsNot(value => value.Equals(string.Empty, stringComparison));
		}

		public static IValidationCondition<string> IsNullOrWhitespace(
			this IValidationCondition<string> condition)
		{
			return condition.Is(string.IsNullOrWhiteSpace);
		}

		public static IValidationCondition<string> IsNotNullOrWhitespace(
			this IValidationCondition<string> condition)
		{
			return condition.IsNot(string.IsNullOrWhiteSpace);
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
			return condition.IsNot(value => Regex.IsMatch(value, regex, options));
		}

		public static IValidationCondition<string> IsNotEmail(
			this IValidationCondition<string> condition)
		{
			return condition.IsNot(value => EmailAddress.IsValid(value));
		}

		public static IValidationCondition<string> HasLength(
			this IValidationCondition<string> condition,
			Func<int, bool> predicate)
		{
			return condition.Is(value => value != null && predicate(value.Length));
		}

		public static IValidationCondition<string> HasLength(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value == length);
		}

		public static IValidationCondition<string> HasLengthLess(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value < length);
		}

		public static IValidationCondition<string> HasLengthGreater(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value > length);
		}
	}
}