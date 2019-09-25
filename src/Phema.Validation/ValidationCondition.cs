using System;

namespace Phema.Validation
{
	public interface IValidationCondition
	{
		IValidationContext ValidationContext { get; }
		string ValidationKey { get; }
		bool? IsValid { get; set; }
	}

	internal class ValidationCondition : IValidationCondition
	{
		public ValidationCondition(IValidationContext validationContext, string validationKey)
		{
			ValidationContext = validationContext ?? throw new ArgumentNullException(nameof(validationContext));
			ValidationKey = validationKey ?? throw new ArgumentNullException(nameof(validationKey));
		}

		public IValidationContext ValidationContext { get; }
		public string ValidationKey { get; }
		public bool? IsValid { get; set; }
	}

	public interface IValidationCondition<out TValue> : IValidationCondition
	{
		TValue Value { get; }
	}

	internal sealed class ValidationCondition<TValue> : ValidationCondition, IValidationCondition<TValue>
	{
		private readonly Lazy<TValue> value;

		public ValidationCondition(
			IValidationContext validationContext,
			string validationKey,
			Lazy<TValue> value)
			: base(validationContext, validationKey)
		{
			this.value = value;
		}

		public TValue Value => value.Value;
	}
}