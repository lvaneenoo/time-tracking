var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ITimeSheets, TimeSheetsStub>();

var app = builder.Build();

app.MapGet("/time-sheets/{s}", async (ITimeSheets timeSheets, string s) =>
{
    if (!DateOnly.TryParseExact(s, "yyyy-MM-dd", out DateOnly value))
    {
        return Results.BadRequest();
    }

    if (!TrackedDate.TryCreate(value, out TrackedDate? date))
    {
        return Results.BadRequest();
    }

    if (await timeSheets.FindAsync(date) is not TimeSheet timeSheet)
    {
        return Results.NotFound();
    }

    return Results.Ok(timeSheet.Date.ToString());
});

app.Run();

public partial class Program { }
