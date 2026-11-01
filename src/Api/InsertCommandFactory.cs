internal class InsertCommandFactory : IInsertCommandFactory
{
    public IStorageCommand Create(TrackedDate date, Period period, Comment comment)
    {
        return new InsertTimesheetEntry(date, period, comment);
    }
}
