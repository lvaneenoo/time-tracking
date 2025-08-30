namespace IntegrationTests;

public class Weekdays : TheoryData<DateOnly>
{
    public Weekdays()
    {
        Add(new DateOnly(2025, 1, 1));
    }
}
