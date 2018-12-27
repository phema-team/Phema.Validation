using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public class ExpressionValidationOutputFormatter : IValidationOutputFormatter
	{
		private readonly ValidationOptions options;

		public ExpressionValidationOutputFormatter(IOptions<ValidationOptions> options)
		{
			this.options = options.Value;
		}
		
		public IDictionary<string, object> FormatOutput(IEnumerable<IValidationError> errors)
		{
			var result = new Dictionary<string, object>();

			foreach (var error in errors)
			{
				Fill(result, error);
			}

			return result;
		}
		
		private void Fill(Dictionary<string, object> current, IValidationError error)
		{
			var parts = error.Key.Split(options.Separator);
			var message = error.Message;
			
			for (int i = 0; i < parts.Length; i++)
			{
				var part = parts[i];

				if (current.TryGetValue(part, out var value))
				{
					if (value is List<string> l)
					{
						l.Add(message);
						break;
					}
			
					if (value is Dictionary<string, object> nxt)
					{
						if (i + 1 == parts.Length)
						{
							if (nxt.TryGetValue(part, out var vv))
							{
								if (vv is string ss)
								{
									nxt[part] = new List<string>
									{
										ss,
										message
									};
								}
							}
							else
							{
								if (nxt.TryGetValue("", out var vvv))
								{
									if (vvv is List<string> lll)
									{
										lll.Add(message);
										break;
									}
							
									if(vvv is string sss)
									{
										nxt[""] = new List<string>
										{
											sss,
											message
										};
									}
								}
								else
								{
									nxt[""] = message;
								}
							}
					
							break;
						}

						current = nxt;
						continue;
					}
			
					if (value is string s)
					{
						current[part] = new List<string>
						{
							s,
							message
						};
				
						break;
					}
				}
				else
				{
					if (i + 1 == parts.Length)
					{
						current[part] = message;
						break;
					}
			
					var next = new Dictionary<string, object>();

					current.Add(part, next);

					current = next;
				}
			}
		}
	}
}