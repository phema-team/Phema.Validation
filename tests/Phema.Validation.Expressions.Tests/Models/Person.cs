using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Phema.Validation.Tests
{
	[DataContract]
	public class Person
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
			
		[DataMember(Name = "address")]
		public Address Address { get; set; }
			
		[DataMember(Name = "list")]
		public IList<Children> List { get; set; }
			
		[DataMember(Name = "array")]
		public Children[] Array { get; set; }
	}
}