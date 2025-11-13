using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddSingleton<ITimeSheets, TimeSheets>();

var app = builder.Build();

app.MapPost("/time-sheet-entries", TimeSheetEntriesEndpoint.PostAsync);
app.MapGet("/time-sheets/{date}", TimeSheetsEndpoint.GetAsync);

app.Run();

public partial class Program { }

[JsonSerializable(typeof(TimeSheetEntryResource[]))]
[JsonSerializable(typeof(TimeSheetResource[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
