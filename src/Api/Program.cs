var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ITimeSheets, TimeSheetsStub>();

var app = builder.Build();

app.AddGetTimeSheetHandler();
app.AddPostTimeSheetEntryHandler();

app.Run();

public partial class Program { }
