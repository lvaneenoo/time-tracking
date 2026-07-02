internal class PostTimeSheetEntryRequest
{
    public required string Comment { get; init; }
    public required string Date { get; init; }
    public required PeriodResource Period { get; init; }
}
