using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		/// <summary>
		///   Specifies validation part with global validation key
		/// </summary>
		public static ValidationCondition When(this IValidationContext validationContext)
		{
			var serviceProvider = (IServiceProvider)validationContext;

			var options = serviceProvider.GetRequiredService<IOptions<ValidationOptions>>().Value;

			return new ValidationCondition(
				validationContext,
				options.GlobalValidationKey);
		}

		/// <summary>
		///   Specifies validation part with object predicate without value. Use with closures in conditions
		/// </summary>
		public static ValidationCondition When(
			this IValidationContext validationContext,
			string validationPart)
		{
			var validationKey = validationContext.CombineValidationPath(validationPart);

			return new ValidationCondition(
				validationContext,
				validationKey);
		}

		/// <summary>
		///   Specifies validation part with provider of <see cref="TValue" /> value
		/// </summary>
		public static ValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationPart,
			Lazy<TValue> value)
		{
			var validationKey = validationContext.CombineValidationPath(validationPart);

			return new ValidationCondition<TValue>(
				validationContext,
				validationKey,
				value);
		}

		/// <summary>
		///   Specifies validation part with <see cref="TValue" /> value
		/// </summary>
		public static ValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationPart,
			TValue value)
		{
			return validationContext.When(validationPart, new Lazy<TValue>(() => value));
		}

		/// <summary>
		///   Checks validation context for any detail with greater or equal severity. Additional filtering by validation key
		/// </summary>
		public static bool IsValid(this IValidationContext validationContext, params string[] validationParts)
		{
			if (validationParts.Length > 0)
			{
				if (validationParts.Length == 1)
				{
					var validationKey = validationContext.CombineValidationPath(validationParts[0]);

					foreach (var validationDetail in validationContext.ValidationDetails)
					{
						if (validationDetail.ValidationSeverity >= validationDetail.ValidationContext.ValidationSeverity)
						{
							if (validationKey == validationDetail.ValidationKey)
							{
								return false;
							}
						}
					}

					return true;
				}

				var validationKeys = validationParts.Select(validationContext.CombineValidationPath).ToList();

				foreach (var validationDetail in validationContext.ValidationDetails)
				{
					if (validationDetail.ValidationSeverity >= validationDetail.ValidationContext.ValidationSeverity)
					{
						if (validationKeys.Contains(validationDetail.ValidationKey))
						{
							return false;
						}
					}
				}

				return true;
			}

			foreach (var validationDetail in validationContext.ValidationDetails)
			{
				if (validationDetail.ValidationSeverity >= validationDetail.ValidationContext.ValidationSeverity)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		///   Checks validation context is not valid
		/// </summary>
		public static bool IsNotValid(this IValidationContext validationContext, params string[] validationParts)
		{
			return !validationContext.IsValid(validationParts);
		}

		/// <summary>
		///   If validation context is not valid, throws <see cref="ValidationContextException" />
		/// </summary>
		public static void EnsureIsValid(this IValidationContext validationContext, params string[] validationParts)
		{
			if (!validationContext.IsValid(validationParts))
			{
				throw new ValidationContextException(validationContext);
			}
		}

		/// <summary>
		///   Creates new validation scope with specified validation path
		/// </summary>
		public static IValidationScope CreateScope(
			this IValidationContext validationContext,
			string validationPart,
			ValidationSeverity? validationSeverity = null)
		{
			var validationPath = validationContext.CombineValidationPath(validationPart);

			return new ValidationScope(
				validationContext,
				validationPath,
				validationSeverity);
		}

		/// <summary>
		///   Combines validation contexts path with specified validation part
		/// </summary>
		public static string CombineValidationPath(this IValidationContext validationContext, string validationPart)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationPathResolver = serviceProvider.GetRequiredService<IValidationPathResolver>();

			return validationPathResolver.CombineValidationPath(validationContext.ValidationPath, validationPart);
		}
	}
}