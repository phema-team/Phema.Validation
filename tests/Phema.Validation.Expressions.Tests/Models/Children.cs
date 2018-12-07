using System.Runtime.Serialization;

namespace Phema.Validation.Tests
{
	[DataContract]
	public class Children
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "address")]
		public Address Address { get; set; }
	}
}