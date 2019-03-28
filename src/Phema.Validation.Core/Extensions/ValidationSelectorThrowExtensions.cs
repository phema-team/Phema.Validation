using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationSelectorThrowExtensions
	{
		private static void Throw<TValidationComponent, TValidationTemplate>(
			this IValidationSelector condition,
			Func<TValidationComponent, TValidationTemplate> selector,
			object[] arguments)
			where TValidationComponent : IValidationComponent
			where TValidationTemplate : IValidationTemplate
		{
			var error = condition.Add(sp => 
				selector(sp.GetRequiredService<TValidationComponent>()), arguments, ValidationSeverity.Fatal);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}

		public static void Throw<TValidationComponent>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate> selector)
			where TValidationComponent : IValidationComponent
		{
			condition.Throw(selector, Array.Empty<object>());
		}

		public static void Throw<TValidationComponent, TArgument>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument>> selector,
			TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			condition.Throw(selector, new object[] {argument});
		}

		public static void Throw<TValidationComponent, TArgument1, TArgument2>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			condition.Throw(selector, new object[] {argument1, argument2});
		}

		public static void Throw<TValidationComponent, TArgument1, TArgument2, TArgument3>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			condition.Throw(selector, new object[] {argument1, argument2, argument3});
		}
	}
}