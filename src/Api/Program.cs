var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

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
    IConfiguration configuration,
    ITimeSheets timeSheets,
    TimeSheetEntryId id,
    CancellationToken cancellationToken) =>
{
    var command = new DeleteTimeSheetEntry(configuration, timeSheets, id);

    return await command.ExecuteAsync(cancellationToken);
});

app.MapGet("/time-sheets/{date}", async (
    IConfiguration configuration,
    TrackedDate date,
    CancellationToken cancellationToken) =>
{
    var query = new GetTimeSheet(configuration, date);

    return await query.ExecuteAsync(cancellationToken);
});

app.Run();


public partial class Program { }
