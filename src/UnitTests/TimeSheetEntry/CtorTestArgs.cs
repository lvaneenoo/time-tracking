namespace TimeSheetEntryTests;

internal class CtorTestArgs : TheoryData<Period>
{
    public CtorTestArgs()
    {
        Add(new Period(TimeOnly.MinValue, TimeOnly.MinValue));
    }
}
