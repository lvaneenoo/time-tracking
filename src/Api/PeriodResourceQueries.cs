internal static class PeriodResourceQueries
{
    public static Period? ToValue(this PeriodResource resource)
    {
        if (!TimeOnly.TryParse(resource.Start, out var start))
        {
            return null;
        }

        if (!TimeOnly.TryParse(resource.End, out var end))
        {
            return null;
        }

        return Period.TryCreate(start, end, out var result) ? result : null;
    }
}
