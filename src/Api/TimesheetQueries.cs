internal static class TimesheetQueries
{
    public static string CreateResourceId(this Timesheet timesheet)
    {
        var snapshot = (TimesheetSnapshot)timesheet;

        return $"{Math.Abs(snapshot.ModifiedOn.GetHashCode())}";
    }

    public static TimesheetResource ToResource(this Timesheet timesheet)
    {
        return new TimesheetResource
        {
            Date = timesheet.Date.ToCanonicalString(),
            Entries = [.. timesheet.Entries.Select(entry => entry.ToResource())],
            Status = (int)timesheet.Status
        };
    }
}
