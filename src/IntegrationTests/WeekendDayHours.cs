namespace IntegrationTests;

public class WeekendDayHours : TheoryData<DateOnly, TimeOnly, TimeOnly>
{
    public WeekendDayHours()
    {
        Add(new DateOnly(2025, 1, 4), new TimeOnly(11, 0), new TimeOnly(11, 59));
    }
}
