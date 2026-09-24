using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

internal class TimeSheetEntriesEndpoints
{
    public static async Task<IResult> DeleteAsync(
        [FromQuery(Name = "date")] TrackedDate date,
        [FromQuery(Name = "period-start")] TimeOnly periodStart,
        [FromQuery(Name = "period-end")] TimeOnly periodEnd,
        CancellationToken cancellationToken)
    {
        if (!Period.TryCreate(periodStart, periodEnd, out var period))
        {
            return Results.BadRequest();
        }

        using var connection = new SqliteConnection(ConnectionStrings.WriteStore);
        using var command = connection.CreateCommand();

        command.CommandText = DeleteTimeSheetEntries.ByDateAndPeriod;

        command.Parameters.AddRange(date.Resolve());
        command.Parameters.AddRange(period.Resolve());

        await connection.OpenAsync(cancellationToken);

        return await command.ExecuteNonQueryAsync(cancellationToken) switch
        {
            0 => Results.NotFound(),
            1 => Results.NoContent(),
            _ => Results.InternalServerError()
        };
    }

    public static async Task<IResult> PostAsync(
        PostTimeSheetEntryRequest request,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        var errors = new Dictionary<string, string[]>();

        if (!TrackedDate.TryParse(request.Date, null, out var date))
        {
            errors.Add(nameof(request.Date), []);
        }

        if (!TimeOnly.TryParse(request.Period.Start, out var start))
        {
            errors.Add(nameof(request.Period.Start), []);
        }

        if (!TimeOnly.TryParse(request.Period.End, out var end))
        {
            errors.Add(nameof(request.Period.End), []);
        }

        if (!Period.TryCreate(start, end, out var period))
        {
            errors.Add(nameof(request.Period), []);
        }

        if (!Comment.TryParse(request.Comment, null, out var comment))
        {
            errors.Add(nameof(request.Comment), []);
        }

        if (errors.Count > 0)
        {
            return Results.ValidationProblem(errors);
        }

        var sheets = context.RequestServices.GetRequiredService<ITimeSheets>();

        if (await sheets.FindAsync(date!, cancellationToken) is not { } sheet)
        {
            return Results.NotFound();
        }

        var (_, entry) = sheet.AddEntry(period!, comment!);

        return entry is null ? Results.Conflict() : Results.Created();
    }
}
