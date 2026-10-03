internal interface IDeleteCommandFactory
{
    IStorageCommand Create(TrackedDate date, Period period);
}
