using Microsoft.AspNetCore.Mvc;

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

app.MapDelete("/time-sheet-entries", async (
    IConfiguration configuration,
    ITimeSheets timeSheets,
    TrackedDate date,
    [FromQuery(Name = "period-start")] TimeOnly periodStart,
    [FromQuery(Name = "period-end")] TimeOnly periodEnd,
    CancellationToken cancellationToken) =>
{
    var command = new DeleteTimeSheetEntry(configuration, timeSheets, date, periodStart, periodEnd);

    return await command.ExecuteAsync(cancellationToken);
});

app.MapGet("/time-sheets/{date}", async (
    IConfiguration configuration,
    TrackedDate date,
    HttpContext httpContext,
    CancellationToken cancellationToken) =>
{
    var query = new GetTimeSheet(configuration, date, httpContext);

    return await query.ExecuteAsync(cancellationToken);
});

app.Run();


public partial class Program { }
