using System.Runtime.Serialization;
using Phema.Validation.Conditions;

namespace Phema.Validation.Examples.AspNetCore
{
	[DataContract]
	public class ExampleOrderModel
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "cost")]
		public int Cost { get; set; }

		[DataMember(Name = "address")]
		public ExampleAddressModel Address { get; set; }

		public void Save(IValidationContext validationContext)
		{
			validationContext.When(this, m => m.Name)
				.IsNullOrWhitespace()
				.AddValidationError("Order name must be set");

			validationContext.When(this, m => m.Cost)
				.IsNotInRange(0, 10) // .IsLessOrEqual(0).IsGreaterOrEqual(10) 
				.AddValidationError("Cost must be in [1, 9] range");

			validationContext.When(this, m => m.Address)
				.IsNull()
				.AddValidationError("You should add your address");

			Address?.Save( /*databaseContext, */ validationContext.CreateScope(this, m => m.Address));
		}
	}
}