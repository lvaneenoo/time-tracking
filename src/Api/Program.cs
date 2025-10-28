var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ITimeSheets, TimeSheets>();

var app = builder.Build();

app.MapGet("/time-sheets/{date}", TimeSheetsEndpoint.GetAsync);
app.MapPost("/time-sheets/{date}/entries", TimeSheetEntriesEndpoint.PostAsync);

app.Run();

public partial class Program { }
