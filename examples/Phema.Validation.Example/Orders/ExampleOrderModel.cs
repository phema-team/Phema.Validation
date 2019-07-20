using System.Runtime.Serialization;

namespace Phema.Validation.Example
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
	}
}