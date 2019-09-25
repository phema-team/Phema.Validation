using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

		/// <summary>
		///   Checks if value is whitespace
		/// </summary>
		public static IValidationCondition<string> IsWhitespace(
			this IValidationCondition<string> condition)
		{
			return condition.Is(value => value?.All(x => char.IsWhiteSpace(x)) ?? false);
		}

		/// <summary>
		///   Checks if value is not whitespace
		/// </summary>
		public static IValidationCondition<string> IsNotWhitespace(
			this IValidationCondition<string> condition)
		{
			return condition.IsNot(value => value?.All(x => char.IsWhiteSpace(x)) ?? false);
		}

		/// <summary>
		///   Checks if value is match regex 
		/// </summary>
		public static IValidationCondition<string> IsMatch(
			this IValidationCondition<string> condition,
			string regex,
			RegexOptions options = RegexOptions.None)
		{
			return condition.Is(value => Regex.IsMatch(value, regex, options));
		}

		/// <summary>
		///   Checks if value is not match regex 
		/// </summary>
		public static IValidationCondition<string> IsNotMatch(
			this IValidationCondition<string> condition,
			string regex,
			RegexOptions options = RegexOptions.None)
		{
			return condition.IsNot(value => Regex.IsMatch(value, regex, options));
		}
		
		/// <summary>
		///   Checks if value is valid email string 
		/// </summary>
		public static IValidationCondition<string> IsEmail(
			this IValidationCondition<string> condition)
		{
			return condition.Is(value => EmailAddress.IsValid(value));
		}

		/// <summary>
		///   Checks if value is not valid email string 
		/// </summary>
		public static IValidationCondition<string> IsNotEmail(
			this IValidationCondition<string> condition)
		{
			return condition.IsNot(value => EmailAddress.IsValid(value));
		}

		/// <summary>
		///   Checks if value is valid url string 
		/// </summary>
		public static IValidationCondition<string> IsUrl(
			this IValidationCondition<string> condition,
			UriKind kind = UriKind.Absolute)
		{
			return condition.Is(value => Uri.TryCreate(condition.Value, kind, out _));
		}

		/// <summary>
		///   Checks if value is not valid url string 
		/// </summary>
		public static IValidationCondition<string> IsNotUrl(
			this IValidationCondition<string> condition,
			UriKind kind = UriKind.Absolute)
		{
			return condition.IsNot(value => Uri.TryCreate(condition.Value, kind, out _));
		}

		/// <summary>
		///   Checks if string has length 
		/// </summary>
		public static IValidationCondition<string> HasLength(
			this IValidationCondition<string> condition,
			Func<int, bool> predicate)
		{
			return condition.Is(value => value != null && predicate(value.Length));
		}

		/// <summary>
		///   Checks if string has length 
		/// </summary>
		public static IValidationCondition<string> HasLength(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value == length);
		}

		/// <summary>
		///   Checks if string has length less
		/// </summary>
		public static IValidationCondition<string> HasLengthLess(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value < length);
		}

		/// <summary>
		///   Checks if string has length greater 
		/// </summary>
		public static IValidationCondition<string> HasLengthGreater(
			this IValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value > length);
		}
	}
}