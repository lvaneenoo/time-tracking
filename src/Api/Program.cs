var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddSingleton<IDeleteCommandFactory, DeleteCommandFactory>();
builder.Services.AddSingleton<ITimeSheets, TimeSheets>();

var app = builder.Build();

app.MapPost("/time-sheet-entries/", TimeSheetEntriesEndpoints.PostAsync);
app.MapDelete("/time-sheet-entries/", TimeSheetEntriesEndpoints.DeleteAsync);

app.MapGet("/time-sheets/{date}", TimeSheetsEndpoints.GetAsync);

app.Run();


public partial class Program { }
