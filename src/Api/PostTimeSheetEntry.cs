internal class PostTimeSheetEntry(ITimeSheets timeSheets, PostTimeSheetEntryRequest request) : IApplicationCommand
{
    private readonly ITimeSheets _timeSheets = timeSheets;
    private readonly PostTimeSheetEntryRequest _request = request;

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var errors = new Dictionary<string, string[]>();

        if (!TrackedDate.TryParse(_request.Date, null, out var date))
        {
            errors.Add(nameof(_request.Date), []);
        }

        if (!TimeOnly.TryParse(_request.Period.Start, out var start))
        {
            errors.Add(nameof(_request.Period.Start), []);
        }

        if (!TimeOnly.TryParse(_request.Period.End, out var end))
        {
            errors.Add(nameof(_request.Period.End), []);
        }

        if (!Period.TryCreate(start, end, out var period))
        {
            errors.Add(nameof(_request.Period), []);
        }

        if (!Comment.TryParse(_request.Comment, null, out var comment))
        {
            errors.Add(nameof(_request.Comment), []);
        }

        if (errors.Count > 0)
        {
            return Results.ValidationProblem(errors);
        }

        if (await _timeSheets.FindAsync(date!, cancellationToken) is not { } sheet)
        {
            return Results.NotFound();
        }

        var (_, entry) = sheet.AddEntry(period!, comment!);

        return entry is null ? Results.Conflict() : Results.Created();
    }
}
