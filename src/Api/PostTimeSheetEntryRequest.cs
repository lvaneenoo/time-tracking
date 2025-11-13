internal class PostTimeSheetEntryRequest
{
    public required string Date { get; init; }
    public required PeriodResource Period { get; init; }
}
