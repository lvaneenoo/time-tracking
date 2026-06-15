internal static class TimeSheetQueries
{
    public static Uri Resolve(this TimeSheet sheet) => new($"/time-sheets/{sheet.Date:yyyy-MM-dd}");
}
