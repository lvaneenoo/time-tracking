using System.Text;

internal class WriteStore
{
    private static readonly Lazy<WriteStore> LazyInstance = new(CreateInstance);

    private WriteStore(string fetchTimeSheetsByDate)
    {
        FetchTimeSheetsByDate = fetchTimeSheetsByDate;
    }

    public static WriteStore Instance => LazyInstance.Value;

    public string FetchTimeSheetsByDate { get; }

    private static WriteStore CreateInstance()
    {
        string[][] columns =
        [
            ["time_sheets", "time_sheet_date"],
            ["time_sheets", "time_sheet_status"],
            ["time_sheet_entries", "rowid"],
            ["time_sheet_entries", "period_start"],
            ["time_sheet_entries", "period_end"]
        ];

        string predicate = "time_sheets.time_sheet_date = @time_sheet_date";

        string[][] tables =
        [
            ["time_sheets", "time_sheet_date"],
            ["time_sheet_entries", "time_sheet_date"]
        ];

        return new WriteStore($"SELECT {ExpandColumns(columns)} FROM {ExpandTables(tables)} WHERE {predicate}");
    }

    private static string ExpandColumns(string[][] columns) =>
        string.Join(", ", columns.Select(column => $"{column[0]}.{column[1]}"));

    private static string ExpandTables(string[][] tables)
    {
        var builder = new StringBuilder(tables[0][0]);

        for (int index = 1; index < tables.Length; index++)
        {
            builder.Append($" LEFT JOIN {tables[index][0]} ON {JoinCondition(tables[index - 1], tables[index])}");
        }

        return builder.ToString();
    }

    private static string JoinCondition(string[] leftTable, string[] rightTable) =>
        $"{leftTable[0]}.{leftTable[1]} = {rightTable[0]}.{rightTable[1]}";
}
