using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Phema.Validation
{
	public static class ValidationConditionIsStringExtensions
	{
		public static IValidationCondition<string> IsEmpty(
			this IValidationCondition<string> builder)
		{
			return builder.Is(value => value == string.Empty);
		}

		public static IValidationCondition<string> IsNotEmpty(
			this IValidationCondition<string> builder)
		{
			return builder.Is(value => value != string.Empty);
		}

		public static IValidationCondition<string> IsNullOrWhitespace(
			this IValidationCondition<string> builder)
		{
			return builder.Is(value => string.IsNullOrWhiteSpace(value));
		}

		public static IValidationCondition<string> IsMatch(
			this IValidationCondition<string> builder,
			string regex, 
			RegexOptions options = RegexOptions.None)
		{
			return builder.Is(value => Regex.IsMatch(value, regex, options));
		}

		public static IValidationCondition<string> IsNotMatch(
			this IValidationCondition<string> builder,
			string regex, 
			RegexOptions options = RegexOptions.None)
		{
			return builder.Is(value => !Regex.IsMatch(value, regex, options));
		}
		
		public static IValidationCondition<string> IsNotEmail(
			this IValidationCondition<string> builder)
		{
			return builder.Is(value => !new EmailAddressAttribute().IsValid(value));
		}
		
		public static IValidationCondition<string> HasLength(
			this IValidationCondition<string> builder,
			int length)
		{
			return builder.Is(value => value != null && value.Length == length);
		}
	}
}