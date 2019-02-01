using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationSelectorAddExtensions
	{
		private static IValidationError Add<TValidationComponent, TValidationTemplate>(
			this IValidationSelector condition,
			Func<TValidationComponent, TValidationTemplate> selector,
			object[] arguments,
			ValidationSeverity severity)
				where TValidationComponent : IValidationComponent
				where TValidationTemplate : IValidationTemplate
		{
			var component = condition.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.Add(() => message, arguments, severity);
		}
		
		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddError<TValidationComponent>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate> selector)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, Array.Empty<object>(), ValidationSeverity.Error);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddWarning<TValidationComponent>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate> selector)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, Array.Empty<object>(), ValidationSeverity.Warning);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddInformation<TValidationComponent>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate> selector)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, Array.Empty<object>(), ValidationSeverity.Information);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddDebug<TValidationComponent>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate> selector)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, Array.Empty<object>(), ValidationSeverity.Debug);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddTrace<TValidationComponent>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate> selector)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, Array.Empty<object>(), ValidationSeverity.Trace);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddError<TValidationComponent, TArgument>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument }, ValidationSeverity.Error);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddWarning<TValidationComponent, TArgument>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument }, ValidationSeverity.Warning);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddInformation<TValidationComponent, TArgument>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument }, ValidationSeverity.Information);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddDebug<TValidationComponent, TArgument>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument }, ValidationSeverity.Debug);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddTrace<TValidationComponent, TArgument>(this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument }, ValidationSeverity.Trace);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddError<TValidationComponent, TArgument1, TArgument2>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2>> selector, TArgument1 argument1,
			TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2 }, ValidationSeverity.Error);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddWarning<TValidationComponent, TArgument1, TArgument2>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2>> selector, TArgument1 argument1,
			TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2 }, ValidationSeverity.Warning);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddInformation<TValidationComponent, TArgument1, TArgument2>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2>> selector, TArgument1 argument1,
			TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2 }, ValidationSeverity.Information);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddDebug<TValidationComponent, TArgument1, TArgument2>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2>> selector, TArgument1 argument1,
			TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2 }, ValidationSeverity.Debug);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddTrace<TValidationComponent, TArgument1, TArgument2>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2>> selector, TArgument1 argument1,
			TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2 }, ValidationSeverity.Trace);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddError<TValidationComponent, TArgument1, TArgument2, TArgument3>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1,
			TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2, argument3 }, ValidationSeverity.Error);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddWarning<TValidationComponent, TArgument1, TArgument2, TArgument3>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1,
			TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2, argument3 }, ValidationSeverity.Warning);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddInformation<TValidationComponent, TArgument1, TArgument2, TArgument3>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1,
			TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2, argument3 }, ValidationSeverity.Information);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddDebug<TValidationComponent, TArgument1, TArgument2, TArgument3>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1,
			TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2, argument3 }, ValidationSeverity.Debug);
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public static IValidationError AddTrace<TValidationComponent, TArgument1, TArgument2, TArgument3>(
			this IValidationSelector condition,
			Func<TValidationComponent, ValidationTemplate<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1,
			TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			return condition.Add(selector, new object[] { argument1, argument2, argument3 }, ValidationSeverity.Trace);
		}
	}
}