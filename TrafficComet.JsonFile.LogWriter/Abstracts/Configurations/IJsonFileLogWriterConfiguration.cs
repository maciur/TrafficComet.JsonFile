namespace TrafficComet.JsonFile.LogWriter.Abstracts.Configurations
{
    public interface IJsonFileLogWriterConfiguration
    {
        string DateTimeFormat { get; }
        string Folder { get; }
        bool IgnoreClientAddressIp { get; }
    }
}