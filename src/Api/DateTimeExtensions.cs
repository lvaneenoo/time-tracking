internal static class DateTimeExtensions
{
    public static TrackedDate ToTrackedDate(this DateTime dt) => new(DateOnly.FromDateTime(dt));
}
