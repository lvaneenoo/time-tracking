internal class AddTimeSheetEntry(HttpContext httpContext, TrackedDate date, TimeSheetEntryPosted entryPosted)
{
    private readonly HttpContext _httpContext = httpContext;
    private readonly TimeSheetEntryPosted _entryPosted = entryPosted;
    private readonly TrackedDate _date = date;

    public async Task<IResult> ExecuteAsync()
    {
        var errors = new Dictionary<string, string[]>();

        if (!TimeOnly.TryParse(_entryPosted.Start, out var start))
        {
            errors.Add(nameof(_entryPosted.Start), []);
        }

        if (!TimeOnly.TryParse(_entryPosted.End, out var end))
        {
            errors.Add(nameof(_entryPosted.End), []);
        }

        if (!Period.TryCreate(start, end, out var period))
        {
            errors.Add(nameof(_entryPosted.End), []);
        }

        if (errors.Count > 0)
        {
            return Results.ValidationProblem(errors);
        }

        var timeSheets = _httpContext.RequestServices.GetRequiredService<ITimeSheets>();

        if (await timeSheets.FindAsync(_date) is not TimeSheet timeSheet)
        {
            return Results.NotFound();
        }

        timeSheet.AddEntry(period!);

        return Results.Created();
    }
}
