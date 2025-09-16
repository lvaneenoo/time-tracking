internal class RetrieveTimeSheet(ITimeSheets timeSheets, string s)
{
    private readonly ITimeSheets _timeSheets = timeSheets;
    private readonly string _s = s;

    public async Task<IResult> ExecuteAsync()
    {
        if (!DateOnly.TryParseExact(_s, "yyyy-MM-dd", out DateOnly value))
        {
            return TypedResults.BadRequest();
        }

        if (!TrackedDate.TryCreate(value, out TrackedDate? date))
        {
            return TypedResults.BadRequest();
        }

        if (await _timeSheets.FindAsync(date) is not TimeSheet timeSheet)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(timeSheet.Date.ToString());
    }
}
