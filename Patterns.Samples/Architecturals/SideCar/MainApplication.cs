namespace Patterns.Samples.SideCar;

public class MainApplication
{
    private readonly ISidecarLogger _logger;

    public MainApplication(ISidecarLogger logger)
    {
        _logger = logger;
    }

    public void ProcessRequest(string request)
    {
        // Lógica de negocio
        Console.WriteLine($"Processing request: {request}");

        // Registrar el evento en el sidecar
        _logger.Log($"Request processed: {request}");
    }
}