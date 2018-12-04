using System.Runtime.Serialization;

namespace Phema.Validation.Tests
{
	public class TestModel
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
		
		[DataMember(Name = "age")]
		public int Age { get; set; }
		
		[DataMember(Name = "phone")]
		public long Phone { get; set; }
	}
}