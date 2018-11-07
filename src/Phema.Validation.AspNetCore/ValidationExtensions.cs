﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Phema.Validation
{
	public static class ValidationExtensions
	{
		public static void AddValidation(this IServiceCollection services, Action<IValidationConfiguration> configuration)
		{
			services.AddTransient<IValidationContext, ValidationContext>();
			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new ValidationExceptionFilter());
				options.Filters.Add(new ValidationFilter());
			});

			configuration(new ValidationConfiguration(services));
		}
	}
}
