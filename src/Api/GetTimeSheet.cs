using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Primitives;

internal class GetTimeSheet : IApplicationQuery
{
    private readonly HttpContext _httpContext;
    private readonly TrackedDate _date;

    private readonly string _connectionString;

    public GetTimeSheet(IConfiguration configuration, TrackedDate date, HttpContext httpContext)
    {
        var connectionString = configuration.GetConnectionString("WriteStore");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException();
        }

        _connectionString = connectionString;

        _date = date;
        _httpContext = httpContext;
    }

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = connection.CreateCommand();

        command.CommandText = RetrieveTimeSheets.ByDate;

        command.Parameters.AddRange(_date.ResolveParameters());

        await connection.OpenAsync(cancellationToken);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var materializer = new TimeSheetMaterializer(reader);

        if (await materializer.SingleOrDefaultAsync(cancellationToken) is not { } sheet)
        {
            return Results.NotFound();
        }

        var snapshot = (TimeSheetSnapshot)sheet;
        var hashCode = snapshot.ModifiedOn.GetHashCode();

        _httpContext.Response.Headers.ETag = new StringValues(hashCode.ToString());

        return Results.Ok(sheet.ToResource());
    }
}
