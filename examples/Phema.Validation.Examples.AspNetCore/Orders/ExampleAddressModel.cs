using System.Runtime.Serialization;
using Phema.Validation.Conditions;

namespace Phema.Validation.Examples.AspNetCore
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
				.AddValidationDetail("City must be set");

			validationContext.When(this, a => a.Street)
				.IsNullOrWhitespace()
				.AddValidationDetail("Street must be set");

			validationContext.When(this, a => a.House)
				.IsNull()
				.AddValidationDetail("House must be set");
		}
	}
}