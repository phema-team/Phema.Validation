using System.Runtime.Serialization;

namespace Phema.Validation.Tests
{
	[DataContract]
	public class Room
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}