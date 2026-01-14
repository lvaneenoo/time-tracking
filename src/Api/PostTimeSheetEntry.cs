internal class PostTimeSheetEntry(ITimeSheets timeSheets, PostTimeSheetEntryRequest request) : IApplicationCommand
{
    private readonly ITimeSheets _timeSheets = timeSheets;
    private readonly PostTimeSheetEntryRequest _request = request;

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (!TrackedDate.TryParse(_request.Date, null, out var date))
        {
            return Results.BadRequest();
        }

        if (_request.Period.ToValue() is not { } period)
        {
            return Results.BadRequest();
        }

        if (await _timeSheets.FindAsync(date) is not { } timeSheet)
        {
            return Results.NotFound();
        }

        var (_, entry) = timeSheet.AddEntry(period);

        return entry is null ? Results.Conflict() : Results.Created();
    }
}
