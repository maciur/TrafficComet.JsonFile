using Microsoft.Extensions.Options;
using System;
using TrafficComet.JsonFile.LogWriter.Abstracts.Configurations;

namespace TrafficComet.JsonFile.LogWriter.Configs
{
    public class JsonFileLogWriterConfiguration : IJsonFileLogWriterConfiguration
    {
        protected IOptions<JsonFileLogWriterConfig> Config { get; }
        protected const string DefaultDateTimeFormat = "yyyy-MM-dd_HH-mm-ss-fffffff";

        public string DateTimeFormat => !string.IsNullOrEmpty(Config.Value.DateTimeFormat)
            ? Config.Value.DateTimeFormat : DefaultDateTimeFormat;

        public string Folder => Config.Value.Folder;
        public bool IgnoreClientAddressIp => Config.Value.IgnoreClientAddressIp;

        public JsonFileLogWriterConfiguration(IOptions<JsonFileLogWriterConfig> jsonFileLogWriterConfig)
        {
            if (jsonFileLogWriterConfig == null || jsonFileLogWriterConfig.Value == null)
                throw new ArgumentNullException(nameof(jsonFileLogWriterConfig));

            Config = jsonFileLogWriterConfig;
        }
    }
}