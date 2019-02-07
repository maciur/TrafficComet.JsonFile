using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TrafficComet.Abstracts.Consts;
using TrafficComet.Abstracts.Writers;
using TrafficComet.JsonFile.LogWriter.Abstracts.Configurations;
using TrafficComet.JsonFile.LogWriter.Configs;
using TrafficComet.JsonFile.LogWriter.Writers;

namespace TrafficComet.JsonFile.LogWriter.Installer
{
	public static class JsonFileLogWriterInstaller
	{
		public static IServiceCollection AddTrafficCometJsonFileLogWriter(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JsonFileLogWriterConfig>(configuration
				.GetSection($"{ConfigurationSelectors.ROOT}:{ConfigurationSelectors.WRITERS}:JsonFile"));

			services.AddScoped<ITrafficLogWriter, JsonFileLogWriter>();
			services.TryAddTransient<IJsonFileLogWriterConfiguration, JsonFileLogWriterConfiguration>();
			return services;
		}
	}
}