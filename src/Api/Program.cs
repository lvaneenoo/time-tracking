var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddSingleton<WriteStore>();
builder.Services.AddSingleton<ITimeSheets, TimeSheets>();

var app = builder.Build();

app.MapPost("/time-sheet-entries", async (
    CancellationToken cancellationToken,
    ITimeSheets timeSheets,
    PostTimeSheetEntryRequest request) =>
{
    var command = new PostTimeSheetEntry(timeSheets, request);

    return await command.ExecuteAsync();
});

app.MapDelete("/time-sheet-entries/{id}", async (
    CancellationToken cancellationToken,
    ITimeSheets timeSheets,
    WriteStore writeStore,
    TimeSheetEntryId id) =>
{
    var command = new DeleteTimeSheetEntry(timeSheets, writeStore, id);

    return await command.ExecuteAsync(cancellationToken);
});

app.MapGet("/time-sheets/{date}", async (
    CancellationToken cancellationToken,
    WriteStore writeStore,
    TrackedDate date) =>
{
    var query = new GetTimeSheet(writeStore, date);

    return await query.ExecuteAsync(cancellationToken);
});

app.Run();


public partial class Program { }
