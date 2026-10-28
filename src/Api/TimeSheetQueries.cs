internal static class TimeSheetQueries
{
    public static string CreateResourceId(this TimeSheet sheet)
    {
        var snapshot = (TimeSheetSnapshot)sheet;

        return $"{Math.Abs(snapshot.ModifiedOn.GetHashCode())}";
    }

    public static TimeSheetResource ToResource(this TimeSheet sheet)
    {
        return new TimeSheetResource
        {
            Date = sheet.Date.ToCanonicalString(),
            Entries = [.. sheet.Entries.Select(entry => entry.ToResource())],
            Status = (int)sheet.Status
        };
    }
}
