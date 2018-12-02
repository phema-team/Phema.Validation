using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationConditionSeverityExtensions
	{
		public static IValidationError AddError<TValidationComponent>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage> selector)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.Add(() => message, null, ValidationSeverity.Error);
		}
		
		public static IValidationError AddWarning<TValidationComponent>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage> selector)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddWarning(() => message);
		}
		
		public static IValidationError AddInformation<TValidationComponent>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage> selector)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddInformation(() => message);
		}
		
		public static IValidationError AddDebug<TValidationComponent>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage> selector)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddDebug(() => message);
		}
		
		public static IValidationError AddTrace<TValidationComponent>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage> selector)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddTrace(() => message);
		}
		
		public static IValidationError AddError<TValidationComponent, TArgument>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddError(() => message, argument);
		}
		
		public static IValidationError AddWarning<TValidationComponent, TArgument>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddWarning(() => message, argument);
		}
		
		public static IValidationError AddInformation<TValidationComponent, TArgument>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddInformation(() => message, argument);
		}
		
		public static IValidationError AddDebug<TValidationComponent, TArgument>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddDebug(() => message, argument);
		}
		
		public static IValidationError AddTrace<TValidationComponent, TArgument>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument>> selector, TArgument argument)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddTrace(() => message, argument);
		}
		
		public static IValidationError AddError<TValidationComponent, TArgument1, TArgument2>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2>> selector, TArgument1 argument1, TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddError(() => message, argument1, argument2);
		}
		
		public static IValidationError AddWarning<TValidationComponent, TArgument1, TArgument2>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2>> selector, TArgument1 argument1, TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddWarning(() => message, argument1, argument2);
		}
		
		public static IValidationError AddInformation<TValidationComponent, TArgument1, TArgument2>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2>> selector, TArgument1 argument1, TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddInformation(() => message, argument1, argument2);
		}
		
		public static IValidationError AddDebug<TValidationComponent, TArgument1, TArgument2>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2>> selector, TArgument1 argument1, TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddDebug(() => message, argument1, argument2);
		}
		
		public static IValidationError AddTrace<TValidationComponent, TArgument1, TArgument2>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2>> selector, TArgument1 argument1, TArgument2 argument2)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddTrace(() => message, argument1, argument2);
		}
		
		public static IValidationError AddError<TValidationComponent, TArgument1, TArgument2, TArgument3>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddError(() => message, argument1, argument2, argument3);
		}
		
		public static IValidationError AddWarning<TValidationComponent, TArgument1, TArgument2, TArgument3>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddWarning(() => message, argument1, argument2, argument3);
		}
		
		public static IValidationError AddInformation<TValidationComponent, TArgument1, TArgument2, TArgument3>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddInformation(() => message, argument1, argument2, argument3);
		}
		
		public static IValidationError AddDebug<TValidationComponent, TArgument1, TArgument2, TArgument3>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddDebug(() => message, argument1, argument2, argument3);
		}
		
		public static IValidationError AddTrace<TValidationComponent, TArgument1, TArgument2, TArgument3>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2, TArgument3>> selector, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
			where TValidationComponent : IValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.AddTrace(() => message, argument1, argument2, argument3);
		}
	}
}