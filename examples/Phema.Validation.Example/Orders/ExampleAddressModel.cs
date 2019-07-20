using System.Runtime.Serialization;

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
	}
}