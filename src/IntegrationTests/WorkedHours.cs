namespace IntegrationTests;

public class WorkedHours : TheoryData<DateOnly, TimeOnly, TimeOnly>
{
    public WorkedHours()
    {
        Add(new DateOnly(2025, 1, 1), new TimeOnly(9, 0), new TimeOnly(9, 59));
    }
}
