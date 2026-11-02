internal class TimesheetResource
{
    public required string Date { get; init; }
    public required TimesheetEntryResource[] Entries { get; init; }
    public required int Status { get; init; }
}
