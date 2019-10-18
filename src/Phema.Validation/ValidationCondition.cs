using System;

namespace Phema.Validation
{
	public interface IValidationCondition
	{
		IValidationContext ValidationContext { get; }
		string ValidationKey { get; }
		bool? IsValid { get; set; }
	}

	public struct ValidationCondition : IValidationCondition
	{
		public ValidationCondition(IValidationContext validationContext, string validationKey)
		{
			ValidationContext = validationContext ?? throw new ArgumentNullException(nameof(validationContext));
			ValidationKey = validationKey ?? throw new ArgumentNullException(nameof(validationKey));
			IsValid = null;
		}

		public IValidationContext ValidationContext { get; }
		public string ValidationKey { get; }
		public bool? IsValid { get; set; }
	}

	public interface IValidationCondition<out TValue> : IValidationCondition
	{
		TValue Value { get; }
	}

	public struct ValidationCondition<TValue> : IValidationCondition<TValue>
	{
		private readonly Lazy<TValue> value;

		public ValidationCondition(
			IValidationContext validationContext,
			string validationKey,
			Lazy<TValue> value)
		{
			ValidationContext = validationContext ?? throw new ArgumentNullException(nameof(validationContext));
			ValidationKey = validationKey ?? throw new ArgumentNullException(nameof(validationKey));
			this.value = value;
			IsValid = null;
		}

		public IValidationContext ValidationContext { get; }
		public string ValidationKey { get; }
		public bool? IsValid { get; set; }
		public TValue Value => value.Value;
	}
}