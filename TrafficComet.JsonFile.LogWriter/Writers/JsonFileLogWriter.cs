using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstracts.Writers;
using TrafficComet.JsonFile.LogWriter.Abstracts.Configurations;

namespace TrafficComet.JsonFile.LogWriter.Writers
{
    public class JsonFileLogWriter : ITrafficLogWriter
    {
        protected JsonSerializerSettings JsonSerializerSettings { get; }

        protected IJsonFileLogWriterConfiguration Configuration { get; }

        public JsonFileLogWriter(IJsonFileLogWriterConfiguration jsonFileLogWriterConfiguration)
        {
            Configuration = jsonFileLogWriterConfiguration
                ?? throw new ArgumentNullException(nameof(jsonFileLogWriterConfiguration));

            JsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public bool SaveLog(ITrafficLog trafficLog)
        {
            if (trafficLog == null)
                throw new ArgumentNullException(nameof(trafficLog));

            string trafficLogAsString = PrepareLog(trafficLog);

            if (string.IsNullOrEmpty(trafficLogAsString))
                throw new NullReferenceException(nameof(trafficLogAsString));

            var pathToUserLogFolder = Path.Combine(Configuration.Folder,
                DateTime.UtcNow.ToString("yyyy-MM-dd"), trafficLog.Client.Id);

            HandleLogFolder(pathToUserLogFolder);

            var trafficLogFileName = GetFileName(trafficLog);

            if (string.IsNullOrEmpty(trafficLogFileName))
                throw new NullReferenceException(nameof(trafficLogFileName));

            var pathToTrafficLogFile = Path.Combine(pathToUserLogFolder, trafficLogFileName);

            WriteLogToFile(pathToTrafficLogFile, PrepareLog(trafficLog), Encoding.UTF8);

            return true;
        }

        protected internal virtual string GetFileName(ITrafficLog trafficLog)
        {
            if (trafficLog == null)
                throw new ArgumentNullException(nameof(trafficLog));

            var formatedDateTime = trafficLog.Dates.StartUtc.ToString(Configuration.DateTimeFormat);

            return $"{formatedDateTime}{trafficLog.Request.Path.Replace("/", "-")}.json";
        }

        protected internal virtual void HandleLogFolder(string pathToFolder)
        {
            if (string.IsNullOrEmpty(pathToFolder))
                throw new ArgumentNullException(pathToFolder);

            if (!Directory.Exists(pathToFolder))
                Directory.CreateDirectory(pathToFolder);
        }

        protected internal virtual string PrepareLog(ITrafficLog trafficLog)
        {
            if (trafficLog == null)
                throw new ArgumentNullException(nameof(trafficLog));

            if (Configuration.IgnoreClientAddressIp)
            {
                trafficLog.Client.IpAddress = null;
            }

            return JsonConvert.SerializeObject(trafficLog, JsonSerializerSettings);
        }

        protected internal virtual void WriteLogToFile(string pathToLogFile, string logMessage, Encoding encoding)
        {
            if (string.IsNullOrEmpty(pathToLogFile))
                throw new ArgumentNullException(pathToLogFile);

            if (string.IsNullOrEmpty(logMessage))
                throw new ArgumentNullException(logMessage);

            File.WriteAllText(pathToLogFile, logMessage, encoding);
        }
    }
}