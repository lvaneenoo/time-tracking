using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddSingleton<ITimeSheets, TimeSheets>();

var app = builder.Build();

app.MapGet("/time-sheets/{date}", TimeSheetsEndpoint.GetAsync);
app.MapPost("/time-sheets/{date}/entries", TimeSheetEntriesEndpoint.PostAsync);

app.Run();

public partial class Program { }

[JsonSerializable(typeof(TimeSheetEntryResource[]))]
[JsonSerializable(typeof(TimeSheetResource[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
