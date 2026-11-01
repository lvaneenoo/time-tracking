internal class DeleteCommandFactory : IDeleteCommandFactory
{
    public IStorageCommand Create(TrackedDate date, Period period)
    {
        return new DeleteTimesheetEntry(date, period);
    }
}
