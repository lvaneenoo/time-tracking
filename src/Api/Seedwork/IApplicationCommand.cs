internal interface IApplicationCommand
{
    Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default);
}
