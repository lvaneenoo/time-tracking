internal static class WriteStore
{
    private static readonly string[] Columns =
    [
        "time_sheet_date",
        "period_start",
        "period_end"
    ];

    private static readonly string Predicate = "time_sheet_date = @time_sheet_date";
    private static readonly string Table = "time_sheet_entries";

    public static string FetchTimeSheetsByDate = $"SELECT {string.Join(", ", Columns)} FROM {Table} WHERE {Predicate}";
}
