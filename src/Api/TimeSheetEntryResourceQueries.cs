using System.Diagnostics.CodeAnalysis;

internal static class TimeSheetEntryResourceQueries
{
    public static bool TryGetPeriod(this TimeSheetEntryResource resource, [NotNullWhen(true)] out Period? result)
    {
        if (!TimeOnly.TryParse(resource.Start, out var start))
        {
            result = null;
            return false;
        }

        if (!TimeOnly.TryParse(resource.End, out var end))
        {
            result = null;
            return false;
        }

        return Period.TryCreate(start, end, out result);
    }
}
