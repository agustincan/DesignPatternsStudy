namespace Patterns.Samples.SideCar;

// El patrón de diseño Sidecar es un patrón arquitectónico utilizado en sistemas distribuidos y aplicaciones
// en la nube. Su objetivo es desacoplar funcionalidades auxiliares o secundarias de la aplicación principal,
// encapsulándolas en un componente separado que se ejecuta junto con la aplicación principal.
// Este componente "sidecar" (en inglés, "sidecar" significa "sidecar" o "acoplado lateralmente")
// se encarga de tareas como monitoreo, logging, gestión de configuración, seguridad, etc.,
// sin afectar la lógica principal de la aplicación.

class Program
{
    static void Main(string[] args)
    {
        // Configurar el sidecar
        var loggingServiceUrl = "http://localhost:5000/logs";
        var logger = new SidecarLogger(loggingServiceUrl);

        // Configurar la aplicación principal
        var app = new MainApplication(logger);

        // Simular una solicitud
        app.ProcessRequest("Sample Request");
    }
}