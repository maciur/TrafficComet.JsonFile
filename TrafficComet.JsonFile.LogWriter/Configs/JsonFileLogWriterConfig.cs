namespace TrafficComet.JsonFile.LogWriter.Configs
{
	public class JsonFileLogWriterConfig
	{
		public string Folder { get; set; }
		public string DateTimeFormat { get; set; }
		public bool IgnoreClientAddressIp { get; set; }
	}
}