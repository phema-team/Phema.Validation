using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Phema.Validation.Conditions;

namespace Phema.Validation.Examples.WorkerService
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> logger;
		private readonly IValidationContext validationContext;

		public Worker(ILogger<Worker> logger, IValidationContext validationContext)
		{
			this.logger = logger;
			this.validationContext = validationContext;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var index = 0;
			var random = new Random();

			while (!stoppingToken.IsCancellationRequested)
			{
				using (var validationScope = validationContext.CreateScope("SomeScope"))
				{
					var value = random.Next(0, 10);

					var (validationKey, validationMessage) = validationScope.When($"Key{index}", value)
						.IsGreater(4)
						.AddError($"Value '{value}' is greater 4!");

					if (validationScope.IsValid($"Key{index}"))
					{
						logger.LogInformation($"Running {index} iteration. Value: '{value}'");
					}
					else
					{
						logger.LogError($"{validationKey}: {validationMessage}");
					}

					await Task.Delay(1000, stoppingToken);
				}

				index++;
			}
		}
	}
}