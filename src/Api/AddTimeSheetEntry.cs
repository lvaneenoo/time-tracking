internal class AddTimeSheetEntry(ITimeSheets timeSheets, TrackedDate date, TimeSheetEntryPosted entryPosted)
{
    private readonly TimeSheetEntryPosted _entryPosted = entryPosted;
    private readonly ITimeSheets _timeSheets = timeSheets;
    private readonly TrackedDate _date = date;

    public async Task<IResult> ExecuteAsync()
    {
        if (!TimeOnly.TryParse(_entryPosted.Start, out var start))
        {
            return TypedResults.BadRequest();
        }

        if (!TimeOnly.TryParse(_entryPosted.End, out var end))
        {
            return TypedResults.BadRequest();
        }

        if (!Period.TryCreate(start, end, out var period))
        {
            return TypedResults.BadRequest();
        }

        if (await _timeSheets.FindAsync(_date) is not TimeSheet timeSheet)
        {
            return TypedResults.NotFound();
        }

        _ = timeSheet.AddEntry(period);

        return TypedResults.Created();
    }
}
