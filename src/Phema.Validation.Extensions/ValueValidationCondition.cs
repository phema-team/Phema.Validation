namespace Phema.Validation
{
	public interface IValueValidationCondition<TValue> : IValidationCondition
	{
		IValueValidationCondition<TValue> Is(Condition<TValue> condition);
	}
	
	internal class ValueValidationCondition<TValue> : IValueValidationCondition<TValue>
	{
		private readonly IValidationCondition validationCondition;
		private readonly TValue value;

		public ValueValidationCondition(IValidationCondition validationCondition, TValue value)
		{
			this.validationCondition = validationCondition;
			this.value = value;
		}
		
		public IValueValidationCondition<TValue> Is(Condition<TValue> condition)
		{
			validationCondition.Is(() => condition(value));

			return this;
		}

		public IValidationCondition Is(Condition condition)
		{
			return validationCondition.Is(condition);
		}

		public IValidationError Add(Selector selector, params object[] arguments)
		{
			return validationCondition.Add(selector, arguments);
		}
	}
}