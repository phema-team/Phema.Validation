namespace Phema.Validation.Conditions
{
	public static class ValidationBooleanExtensions
	{
		/// <summary>
		///   Checks value is true
		/// </summary>
		public static ValidationCondition<bool> IsTrue(
			this ValidationCondition<bool> condition)
		{
			return condition.Is(value => value);
		}

		/// <summary>
		///   Checks value is false
		/// </summary>
		public static ValidationCondition<bool> IsFalse(
			this ValidationCondition<bool> condition)
		{
			return condition.IsNot(value => value);
		}
	}
}