var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.MapGet("/hello-world", () => "hello, world");

app.Run();

public partial class Program { }
