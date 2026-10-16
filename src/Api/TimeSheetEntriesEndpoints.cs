using Microsoft.AspNetCore.Mvc;

internal class TimeSheetEntriesEndpoints
{
    public static async Task<IResult> DeleteAsync(
        [FromQuery(Name = "date")] string date,
        [FromQuery(Name = "period-start")] string periodStart,
        [FromQuery(Name = "period-end")] string periodEnd,
        IDeleteCommandFactory factory,
        CancellationToken cancellationToken)
    {
        if (!TrackedDate.TryParse(date, null, out var d))
        {
            return Results.BadRequest();
        }

        if (!TimeOnly.TryParseExact(periodStart, "HH:mm", out var start))
        {
            return Results.BadRequest();
        }

        if (!TimeOnly.TryParseExact(periodEnd, "HH:mm", out var end))
        {
            return Results.BadRequest();
        }

        if (!Period.TryCreate(start, end, out var period))
        {
            return Results.BadRequest();
        }

        var command = factory.Create(d, period);

        return await command.ExecuteAsync(cancellationToken) switch
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

        if (entry is null)
        {
            return Results.Conflict();
        }

        var factory = context.RequestServices.GetRequiredService<IInsertCommandFactory>();
        var command = factory.Create(date!, period!, comment!);

        return await command.ExecuteAsync(cancellationToken) == 1 ? Results.Created() : Results.InternalServerError();
    }
}
