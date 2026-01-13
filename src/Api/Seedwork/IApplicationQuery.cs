internal interface IApplicationQuery
{
    Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default);
}
