var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ITimeSheets, TimeSheetsStub>();

var app = builder.Build();

app.MapTimeSheetRetrieval();
app.MapTimeSheetEntryCreation();

app.Run();

public partial class Program { }
