internal class TimeSheetSnapshot(TimeSheet sheet) : TimeSheet(sheet.Date, [.. sheet.Entries], sheet.Status)
{
    public required DateTimeOffset ModifiedOn { get; init; }
}
