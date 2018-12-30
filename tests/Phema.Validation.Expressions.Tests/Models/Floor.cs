using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Phema.Validation.Tests
{
	[DataContract]
	public class Floor
	{
		[DataMember(Name = "list")]
		public IList<Room> List { get; set; }

		[DataMember(Name = "array")]
		public Room[] Array { get; set; }
	}
}