internal class TimeSheetResource
{
    public required string Date { get; init; }
    public required TimeSheetEntryResource[] Entries { get; init; }
}
