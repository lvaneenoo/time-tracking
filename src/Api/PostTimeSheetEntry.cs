internal class PostTimeSheetEntry(ITimeSheets timeSheets, string dateArg, TimeSheetEntryPosted body)
{
    private readonly TimeSheetEntryPosted _body = body;
    private readonly ITimeSheets _timeSheets = timeSheets;
    private readonly string _dateArg = dateArg;

    public async Task<IResult> ExecuteAsync()
    {
        if (!TimeOnly.TryParse(_body.Start, out var start))
        {
            return TypedResults.BadRequest();
        }

        if (!TimeOnly.TryParse(_body.End, out var end))
        {
            return TypedResults.BadRequest();
        }

        if (!Period.TryCreate(start, end, out var period))
        {
            return TypedResults.BadRequest();
        }

        if (!TrackedDate.TryParse(_dateArg, out var date))
        {
            return TypedResults.NotFound();
        }

        if (await _timeSheets.FindAsync(date) is not TimeSheet timeSheet)
        {
            return TypedResults.NotFound();
        }

        _ = timeSheet.AddEntry(period);

        return TypedResults.Created();
    }
}
