namespace IntegrationTests;

public class WeekdayHours : TheoryData<DateOnly, TimeOnly, TimeOnly>
{
    public WeekdayHours()
    {
        Add(new DateOnly(2025, 1, 1), new TimeOnly(11, 0), new TimeOnly(11, 59));
    }
}
