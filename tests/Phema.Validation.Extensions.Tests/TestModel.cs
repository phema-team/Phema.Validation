using System.Runtime.Serialization;

namespace Phema.Validation.Tests
{
	public class TestModel
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}