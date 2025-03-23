using System.Text;
using System.Text.Json;

namespace Patterns.Samples.SideCar;

public class SidecarLogger : ISidecarLogger
{
    private readonly HttpClient _httpClient;
    private readonly string _loggingServiceUrl;

    public SidecarLogger(string loggingServiceUrl)
    {
        _httpClient = new HttpClient();
        _loggingServiceUrl = loggingServiceUrl;
    }

    public void Log(string message)
    {
        // Simular el envío de logs a un servicio centralizado
        var logEntry = new { Timestamp = DateTime.UtcNow, Message = message };
        var content = new StringContent(JsonSerializer.Serialize(logEntry), Encoding.UTF8, "application/json");

        _httpClient.PostAsync(_loggingServiceUrl, content).Wait();
        Console.WriteLine($"Log sent to sidecar: {message}");
    }
}