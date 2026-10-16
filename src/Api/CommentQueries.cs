using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal static class CommentQueries
{
    public static ReadOnlyCollection<SqliteParameter> Resolve(this Comment comment)
    {
        return
        [
            new SqliteParameter($"@{TimeSheetEntryTable.Comment}", SqliteType.Text)
            {
                Value = comment.ToString()
            }
        ];
    }
}
