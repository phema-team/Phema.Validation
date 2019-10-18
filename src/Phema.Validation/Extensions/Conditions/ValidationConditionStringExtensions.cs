using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionStringExtensions
	{
		private static readonly EmailAddressAttribute EmailAddress = new EmailAddressAttribute();

		public static ValidationCondition<string> IsEmpty(
			this ValidationCondition<string> condition,
			StringComparison stringComparison = StringComparison.InvariantCulture)
		{
			return condition.Is(value => value.Equals(string.Empty, stringComparison));
		}

		public static ValidationCondition<string> IsNotEmpty(
			this ValidationCondition<string> condition,
			StringComparison stringComparison = StringComparison.InvariantCulture)
		{
			return condition.IsNot(value => value.Equals(string.Empty, stringComparison));
		}

		public static ValidationCondition<string> IsNullOrWhitespace(
			this ValidationCondition<string> condition)
		{
			return condition.Is(string.IsNullOrWhiteSpace);
		}

		public static ValidationCondition<string> IsNotNullOrWhitespace(
			this ValidationCondition<string> condition)
		{
			return condition.IsNot(string.IsNullOrWhiteSpace);
		}

		/// <summary>
		///   Checks if value is whitespace
		/// </summary>
		public static ValidationCondition<string> IsWhitespace(
			this ValidationCondition<string> condition)
		{
			return condition.Is(value => value?.All(x => char.IsWhiteSpace(x)) ?? false);
		}

		/// <summary>
		///   Checks if value is not whitespace
		/// </summary>
		public static ValidationCondition<string> IsNotWhitespace(
			this ValidationCondition<string> condition)
		{
			return condition.IsNot(value => value?.All(x => char.IsWhiteSpace(x)) ?? false);
		}

		/// <summary>
		///   Checks if value is match regex 
		/// </summary>
		public static ValidationCondition<string> IsMatch(
			this ValidationCondition<string> condition,
			string regex,
			RegexOptions options = RegexOptions.None)
		{
			return condition.Is(value => Regex.IsMatch(value, regex, options));
		}

		/// <summary>
		///   Checks if value is not match regex 
		/// </summary>
		public static ValidationCondition<string> IsNotMatch(
			this ValidationCondition<string> condition,
			string regex,
			RegexOptions options = RegexOptions.None)
		{
			return condition.IsNot(value => Regex.IsMatch(value, regex, options));
		}
		
		/// <summary>
		///   Checks if value is valid email string 
		/// </summary>
		public static ValidationCondition<string> IsEmail(
			this ValidationCondition<string> condition)
		{
			return condition.Is(value => EmailAddress.IsValid(value));
		}

		/// <summary>
		///   Checks if value is not valid email string 
		/// </summary>
		public static ValidationCondition<string> IsNotEmail(
			this ValidationCondition<string> condition)
		{
			return condition.IsNot(value => EmailAddress.IsValid(value));
		}

		/// <summary>
		///   Checks if value is valid url string 
		/// </summary>
		public static ValidationCondition<string> IsUrl(
			this ValidationCondition<string> condition,
			UriKind kind = UriKind.Absolute)
		{
			return condition.Is(value => Uri.TryCreate(condition.Value, kind, out _));
		}

		/// <summary>
		///   Checks if value is not valid url string 
		/// </summary>
		public static ValidationCondition<string> IsNotUrl(
			this ValidationCondition<string> condition,
			UriKind kind = UriKind.Absolute)
		{
			return condition.IsNot(value => Uri.TryCreate(condition.Value, kind, out _));
		}

		/// <summary>
		///   Checks if string has length 
		/// </summary>
		public static ValidationCondition<string> HasLength(
			this ValidationCondition<string> condition,
			Func<int, bool> predicate)
		{
			return condition.Is(value => value != null && predicate(value.Length));
		}

		/// <summary>
		///   Checks if string has length 
		/// </summary>
		public static ValidationCondition<string> HasLength(
			this ValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value == length);
		}

		/// <summary>
		///   Checks if string has length less
		/// </summary>
		public static ValidationCondition<string> HasLengthLess(
			this ValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value < length);
		}

		/// <summary>
		///   Checks if string has length greater 
		/// </summary>
		public static ValidationCondition<string> HasLengthGreater(
			this ValidationCondition<string> condition,
			int length)
		{
			return condition.HasLength(value => value > length);
		}
	}
}