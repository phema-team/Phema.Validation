using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	internal class ValidationComponentOptions
	{
		public ValidationComponentOptions()
		{
			ValidationDispatchers = new Dictionary<Type, IList<Action<IServiceProvider, object>>>();
		}

		public IDictionary<Type, IList<Action<IServiceProvider, object>>> ValidationDispatchers { get; }
	}
}