internal interface IStorageCommand
{
    Task<int> ExecuteAsync(CancellationToken cancellationToken);
}
