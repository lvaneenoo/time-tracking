internal interface IInsertCommandFactory
{
    IStorageCommand Create(TrackedDate date, Period period, Comment comment);
}
