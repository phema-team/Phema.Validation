using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Phema.Validation
{
	public static class ValidationConditionWhenStringExtensions
	{
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
		
		public static IValidationCondition<string> WhenNotEmail(
			this IValidationCondition<string> builder)
		{
			return builder.When(value => !new EmailAddressAttribute().IsValid(value));
		}
		
		public static IValidationCondition<string> WhenHasLength(
			this IValidationCondition<string> builder,
			int length)
		{
			return builder.When(value => value != null && value.Length == length);
		}
	}
}