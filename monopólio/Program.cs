using Monopólio.App;

try
{
    var app = Bootstrap.CreateApplication();
    app.Run();
}
catch (Exception ex)
{
    System.Console.WriteLine($"Erro fatal: {ex.Message}");
}
