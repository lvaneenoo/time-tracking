namespace IntegrationTests;

public class WeekendDays : TheoryData<DateOnly>
{
    public WeekendDays()
    {
        Add(new DateOnly(2025, 1, 4));
    }
}
