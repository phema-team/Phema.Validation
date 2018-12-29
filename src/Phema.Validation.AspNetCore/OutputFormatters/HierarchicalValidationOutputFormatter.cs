using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public class HierarchicalValidationOutputFormatter : IValidationOutputFormatter
	{
		private readonly ValidationOptions options;

		public HierarchicalValidationOutputFormatter(IOptions<ValidationOptions> options)
		{
			this.options = options.Value;
		}
		
		public IDictionary<string, object> FormatOutput(IEnumerable<IValidationError> errors)
		{
			var result = new Dictionary<string, object>();

			foreach (var (key, message) in errors)
			{
				var parts = key.Split(options.Separator);
				
				Fill(null, result, parts, 0, message);
			}

			return result;
		}

		private void Fill(Dictionary<string, object> root, object current, string[] parts, int index, string message)
		{
			if (index == parts.Length)
				return;

			switch (current)
			{
				case Dictionary<string, object> node:
					FillNode(node, parts, index, message);
					break;

				case object @object:
					FillObject(@object, root, parts, index, message);
					break;
			}
		}

		private void FillNode(Dictionary<string, object> node, string[] parts, int index, string message)
		{
			var key = parts[index];
			var isLast = index + 1 == parts.Length;
			
			if (node.TryGetValue(key, out var next))
			{
				if (isLast)
				{
					switch (next)
					{
						case string @string:
							node[key] = new List<string> { @string, message };
							break;
								
						case List<string> messages:
							messages.Add(message);
							break;
								
						case Dictionary<string, object> dictionary:
							FillGlobal(dictionary, message);
							break;
					}
				}
				else
				{
					Fill(node, next, parts, index + 1, message);
				}
			}
			else
			{
				if (isLast)
				{
					node[key] = message;
				}
				else
				{
					Fill(node, node[key] = new Dictionary<string, object>(), parts, index + 1, message);
				}
			}
		}

		private void FillGlobal(Dictionary<string, object> node, string message)
		{
			if (node.TryGetValue(options.Global, out var value))
			{
				switch (value)
				{
					case string @string:
						node[options.Global] = new List<string> { @string, message };
						break;

					case List<string> messages:
						messages.Add(message);
						break;
				}
			}
			else
			{
				node[options.Global] = message;
			}
		}

		private void FillObject(object @object, Dictionary<string, object> root, string[] parts, int index, string message)
		{
			var prevKey = parts[index - 1];
					
			root[prevKey] = new Dictionary<string, object>
			{
				[options.Global] = @object
			};
					
			Fill(root, root[prevKey], parts, index, message);
		}
	}
}