using System.Runtime.Serialization;
using Phema.Validation.Conditions;

namespace Phema.Validation.Example
{
	[DataContract]
	public class ExampleAddressModel
	{
		[DataMember(Name = "city")]
		public string City { get; set; }

		[DataMember(Name = "street")]
		public string Street { get; set; }

		[DataMember(Name = "house")]
		public int? House { get; set; }

		public void Save(IValidationContext validationContext)
		{
			validationContext.When(this, a => a.City)
				.IsNullOrWhitespace()
				.AddError("City must be set");

			validationContext.When(this, a => a.Street)
				.IsNullOrWhitespace()
				.AddError("Street must be set");

			validationContext.When(this, a => a.House)
				.IsNull()
				.AddError("House must be set");
		}
	}
}