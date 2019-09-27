using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Phema.Validation.Examples.WorkerService
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> logger;
		private readonly IServiceProvider serviceProvider;

		public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
		{
			this.logger = logger;
			this.serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var stopwatch = Stopwatch.StartNew();

			while (!stoppingToken.IsCancellationRequested)
			{
				using (var serviceScope = serviceProvider.CreateScope())
				{
					var message = new ReceivedMessage
					{
						Elapsed = stopwatch.Elapsed
					};

					ProcessInSeparateScope(serviceScope, message);
				}

				await Task.Delay(1000, stoppingToken);
			}
		}

		private void ProcessInSeparateScope(IServiceScope serviceScope, ReceivedMessage message)
		{
			var validationContext = serviceScope.ServiceProvider.GetRequiredService<IValidationContext>();

			validationContext.When(message, m => m.Elapsed)
				.Is(timeSpan => timeSpan.Seconds % 2 == 0)
				.AddValidationDetail($"Elapsed: {message.Elapsed.Seconds} seconds");

			if (validationContext.IsValid())
			{
				logger.LogInformation("Everything is ok");
			}
			else
			{
				var (validationKey, validationMessage) = validationContext.ValidationDetails.Single();

				logger.LogError($"Key: '{validationKey}', message: '{validationMessage}'");
			}
		}
	}
}