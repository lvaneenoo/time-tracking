using System.Text.Json.Serialization;

[JsonSerializable(typeof(PostTimeSheetEntryRequest[]))]
[JsonSerializable(typeof(TimesheetEntryResource[]))]
[JsonSerializable(typeof(TimesheetResource[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
