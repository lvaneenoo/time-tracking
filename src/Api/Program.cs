var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ITimeSheets, TimeSheetsStub>();

var app = builder.Build();

app.MapTimeSheetRetrieval();
app.AddPostTimeSheetEntryHandler();

app.Run();

public partial class Program { }
