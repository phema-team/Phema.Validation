using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Phema.Validation
{
	public class ValidationMiddleware : IMiddleware
	{
		public Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			throw new System.NotImplementedException();
		}
	}
}