using System.Runtime.Serialization;

namespace Phema.Validation.Tests
{
	[DataContract]
	public class Address
	{
		[DataMember(Name = "street")]
		public string Street { get; set; }

		[DataMember(Name = "floor")]
		public Floor Floor { get; set; }
	}
}