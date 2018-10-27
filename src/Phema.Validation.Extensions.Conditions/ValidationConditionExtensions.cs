using System.Text.RegularExpressions;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition IsNull<TValue>(this IValidationCondition validationCondition, TValue value)
		{
			return validationCondition.Is(() => value == null);
		}

		public static IValidationCondition IsNotNull<TValue>(this IValidationCondition validationCondition, TValue value)
		{
			return validationCondition.Is(() => value != null);
		}

		public static IValidationCondition IsEmpty(this IValidationCondition validationCondition, string value)
		{
			return validationCondition.Is(() => value == string.Empty);
		}

		public static IValidationCondition IsNotEmpty(this IValidationCondition validationCondition, string value)
		{
			return validationCondition.Is(() => value != string.Empty);
		}

		public static IValidationCondition IsNullOrWhitespace(
			this IValidationCondition validationCondition, 
			string value)
		{
			return validationCondition.Is(() => string.IsNullOrWhiteSpace(value));
		}

		public static IValidationCondition IsNotNullOrWhitespace(
			this IValidationCondition validationCondition,
			string value)
		{
			return validationCondition.Is(() => !string.IsNullOrWhiteSpace(value));
		}

		public static IValidationCondition IsEqual<TValue>(
			this IValidationCondition validationCondition, 
			TValue value,
			TValue expect)
		{
			return validationCondition.Is(() => value.Equals(expect));
		}

		public static IValidationCondition IsNotEqual<TValue>(
			this IValidationCondition validationCondition, 
			TValue value,
			TValue expect)
		{
			return validationCondition.Is(() => !value.Equals(expect));
		}

		public static IValidationCondition IsMatch(
			this IValidationCondition validationCondition, string value,
			string regex, 
			RegexOptions options = RegexOptions.None)
		{
			return validationCondition.Is(() => Regex.IsMatch(value, regex, options));
		}

		public static IValidationCondition IsNotMatch(
			this IValidationCondition validationCondition, string value,
			string regex, 
			RegexOptions options = RegexOptions.None)
		{
			return validationCondition.Is(() => !Regex.IsMatch(value, regex, options));
		}
	}
}