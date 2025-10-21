var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ITimeSheets, TimeSheets>();

var app = builder.Build();

app.AddGetTimeSheetHandler();
app.AddPostTimeSheetEntryHandler();

app.Run();

public partial class Program { }
