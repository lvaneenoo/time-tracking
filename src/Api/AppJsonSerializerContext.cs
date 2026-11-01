using System.Text.Json.Serialization;

[JsonSerializable(typeof(PostTimeSheetEntryRequest[]))]
[JsonSerializable(typeof(TimeSheetResource[]))]
[JsonSerializable(typeof(TimesheetEntryResource[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
