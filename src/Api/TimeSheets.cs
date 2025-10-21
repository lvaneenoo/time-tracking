using Microsoft.Data.Sqlite;

internal class TimeSheets : ITimeSheets
{
    private readonly string _connectionString;

    public TimeSheets(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WriteStore");

        if (connectionString is null || connectionString.Trim() == "")
        {
            throw new InvalidOperationException();
        }

        _connectionString = connectionString;
    }

    public async Task<TimeSheet?> FindAsync(TrackedDate date)
    {
        using var connection = new SqliteConnection(_connectionString);
        var retrievedTimeSheets = connection.RetrieveTimeSheets(date);

        return await retrievedTimeSheets.SingleOrDefaultAsync();
    }
}
