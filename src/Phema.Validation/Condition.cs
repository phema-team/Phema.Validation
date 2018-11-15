using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Phema.Validation.Tests")]

namespace Phema.Validation
{
	public delegate bool Condition<in TValue>(TValue value);
}